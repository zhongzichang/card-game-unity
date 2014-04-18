using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  /// <summary>
  /// Level controller.
  /// </summary>
  public class LevelController
  {
    /// <summary>
    /// Occurs when raise sub level loaded event.
    /// 子关卡加载完毕事件处理
    /// </summary>
    public static event EventHandler RaiseSubLevelLoadedEvent;

    /// <summary>
    /// 子关卡需要的英雄游戏对象以及数量
    /// </summary>
    public static Dictionary<string, int> requiredHeroGobjTable = new Dictionary<string, int> ();
    // 当前关卡ID
    private static int m_currentLevelId = 0;

    public static int CurrentLevelId {
      get {
        // 获取当前关卡ID
        return m_currentLevelId;
      }
      private set {
        // 设置当前关卡
        if (Config.levelTable.ContainsKey (value)) {
          m_currentLevelId = value;
          LevelContext.CurrentLevel = Config.levelTable [m_currentLevelId];
        }
      }
    }

    /// <summary>
    /// 挑战这个关卡
    /// </summary>
    /// <param name="levelId">关卡ID</param>
    /// <param name="group">我方小组</param>
    public static void ChallengeLevel (int levelId, Group group)
    {
      CurrentLevelId = levelId;
      LevelContext.selfGroup = group;
      LoadTargetSubLevelRes ();
    }

    /// <summary>
    /// 预加载指定关卡的资源
    /// </summary>
    /// <param name="levelId">Level identifier.</param>
    private static void LoadTargetSubLevelRes ()
    {

      SubLevel subLevel = LevelContext.TargetSubLevel;

      if (Cache.gobjTable.ContainsKey (subLevel.resName)) {
        // 资源已准备完毕
        OnSubLevelResReady ();
      } else if (Config.use_packed_res) {
        string name = LevelContext.CurrentSubLevel.resName;
        string path = Tang.ResourceUtils.GetAbFilePath (name);
        Tang.AssetBundleLoader.LoadAsync (name, OnSubLevelLoaded);
      } else {
        OnSubLevelLoaded (null);
      }

    }

    /// <summary>
    /// 资源加载后
    /// </summary>
    /// <param name="ab">Ab.</param>
    private static void OnSubLevelLoaded (AssetBundle ab)
    {

      //Debug.Log ("OnSubLevelLoaded");

      UnityEngine.Object assets = null;
      if (ab == null) {
        string filepath = Config.BATTLE_BG_PATH + Tang.Config.DIR_SEP + LevelContext.TargetSubLevel.resName;
        Debug.Log (filepath);
        assets = Resources.Load (filepath);
      } else
        assets = ab.Load (ab.name);

      if (assets != null) {
        GameObject gobj = GameObject.Instantiate (assets) as GameObject;
        gobj.SetActive (false);
        gobj.name = assets.name;
        GameObjectManager.Add (gobj);
        // 资源已准备完毕
        OnSubLevelResReady ();
      }

    }

    /// <summary>
    /// 发出子关卡资源已准备好的事件
    /// </summary>
    private static void OnSubLevelResReady ()
    {
      //Debug.Log ("OnSubLevelResReady");
      // -- 加载场景中的其他资源 --

      // -- 加载英雄 --
      // 统计需要加载的英雄对象数量
      requiredHeroGobjTable.Clear ();


      // -- 统计敌方英雄资源 --
      if (LevelContext.TargetSubLevel.enemyGroup != null) {
        foreach (Hero hero in LevelContext.TargetSubLevel.enemyGroup.heros) {
          if (requiredHeroGobjTable.ContainsKey (hero.resName)) {
            int count = requiredHeroGobjTable [hero.resName] + 1;
            requiredHeroGobjTable [hero.resName] = count;
          } else
            requiredHeroGobjTable.Add (hero.resName, 1);
        }
      }

      // -- 统计我方英雄资源 --
      if (LevelContext.selfGroup != null) {
        foreach (Hero hero in LevelContext.selfGroup.heros) {
          if (requiredHeroGobjTable.ContainsKey (hero.resName)) {
            int count = requiredHeroGobjTable [hero.resName] + 1;
            requiredHeroGobjTable [hero.resName] = count;
          } else
            requiredHeroGobjTable.Add (hero.resName, 1);
        }
      }

      foreach (KeyValuePair<string, int> kvp in requiredHeroGobjTable) {
        int has = HeroGobjManager.Size (kvp.Key);
        if (has < kvp.Value) {
          int need = kvp.Value - has;
          Debug.Log ("need :" + need);
          HeroGobjManager.LazyLoad (kvp.Key, need);
        }
      }

      if (RaiseSubLevelLoadedEvent != null)
        RaiseSubLevelLoadedEvent (null, EventArgs.Empty);
    }

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    public static void EnterNextSubLevel ()
    {
      // 创建背景
      GameObject bgGobj = GameObjectManager.FetchUnused (LevelContext.TargetSubLevel.resName);
      if (bgGobj != null) {
        bgGobj.SetActive (true);
        //Debug.Log ("Background Created.");
      }

      // 敌方小组列阵
      Group enemyGroup = LevelContext.TargetSubLevel.enemyGroup;
      Embattle (enemyGroup, BattleDirection.LEFT);
      //Debug.Log ("EnemyGroup Embattle.");

      // 我方小组列阵
      Embattle (LevelContext.selfGroup, BattleDirection.RIGHT);
      //Debug.Log ("SelfGroup Embattle.");

      // 我方进场
      foreach (Hero hero in LevelContext.selfGroup.heros) {
        AddHeroToScene (hero);
      }

      // 敌方进场
      foreach (Hero hero in enemyGroup.heros) {
        AddHeroToScene (hero);
      }

      // 设置关卡状态 InLevel
      if (!LevelContext.InLevel)
        LevelContext.InLevel = true;
    }

    /// <summary>
    /// Lefts the current level.
    /// </summary>
    public static void LeftLevel ()
    {

      // 发出离开关卡通知

      // 卸载场景资源

      // 卸载背景

      // 卸载场景人物

      LevelContext.InLevel = false;
    }

    /// <summary>
    /// 列阵
    /// </summary>
    /// <param name="group">Group.</param>
    /// <param name="side">Side.</param>
    public static void Embattle (Group group, BattleDirection direction)
    {

      Hero[] heros = group.heros;

      // 调整英雄面对的方向
      for (int i = 0; i < heros.Length; i++) {
        heros [i].battleDirection = direction;
      }

      // 根据攻击距离进行排序
      Array.Sort (heros, delegate(Hero hero1, Hero hero2) {
        return hero1.attackDistance.CompareTo (hero2.attackDistance);
      });

      // 分成两列
      List<Hero> column1 = new List<Hero> ();
      List<Hero> column2 = new List<Hero> ();
      Vector2 origin = Vector2.zero;
      int stepx = 6; // 排与排之间的距离

      if (BattleDirection.RIGHT == direction) {

        // 分成两列
        for (int i = 0; i < heros.Length; i++) {
          if (i % 2 == 0) {
            column1.Add (heros [i]);
          } else {
            column2.Add (heros [i]);
          }
        }

        int offsety = 0;
        origin = new Vector2 (0, 12);
        // 对于第一列
        for (int i = 0; i < column1.Count; i++) {
          // 如果当前英雄与前面英雄的攻击距离相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column1 [i].attackDistance == column1 [j].attackDistance) {
              useOffset = true;
              offsety++;
              break;
            }
          }
          if (useOffset)
            column1 [i].birthPoint = origin - new Vector2 (stepx * i, offsety);
          else
            column1 [i].birthPoint = origin - new Vector2 (stepx * i, 0);
        }

        // 对于第二列
        origin = new Vector2 (-3, 8);
        offsety = 0;
        for (int i = 0; i < column2.Count; i++) {
          // 如果当前英雄与前面英雄的攻击距离相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column2 [i].attackDistance == column2 [j].attackDistance) {
              useOffset = true;
              offsety++;
              break;
            }
          }
          if (useOffset)
            column2 [i].birthPoint = origin - new Vector2 (stepx * i, -offsety);
          else
            column2 [i].birthPoint = origin - new Vector2 (stepx * i, 0);
        }

      } else {

        // 清空两列内容
        column1.Clear ();
        column2.Clear ();

        // 分成两列
        for (int i = 0; i < heros.Length; i++) {
          if (i % 2 == 0) {
            column1.Add (heros [i]);
          } else {
            column2.Add (heros [i]);
          }
        }

        // 对于第一列
        int offsety = 0;
        origin = new Vector2 (32, 12);
        // 对于第一列
        for (int i = 0; i < column1.Count; i++) {
          // 如果当前英雄与前面英雄的攻击距离相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column1 [i].attackDistance == column1 [j].attackDistance) {
              useOffset = true;
              offsety++;
              break;
            }
          }
          if (useOffset)
            column1 [i].birthPoint = origin + new Vector2 (stepx * i, -offsety);
          else
            column1 [i].birthPoint = origin + new Vector2 (stepx * i, 0);
        }

        // 对于第二列
        offsety = 0;
        origin = new Vector2 (35, 8);
        for (int i = 0; i < column2.Count; i++) {
          // 如果当前英雄与前面英雄的攻击距离相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column2 [i].attackDistance == column2 [j].attackDistance) {
              useOffset = true;
              offsety++;
              break;
            }
          }
          if (useOffset)
            column2 [i].birthPoint = origin + new Vector2 (stepx * i, offsety);
          else
            column2 [i].birthPoint = origin + new Vector2 (stepx * i, 0);
        }

      }

    }

    /// <summary>
    /// 增加一个英雄到场景中
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="side">Side.</param>
    public static void AddHeroToScene (Hero hero)
    {

      GameObject enemyGobj = HeroGobjManager.FetchUnused (hero);
      if (enemyGobj != null) {
        enemyGobj.SetActive (true);
        enemyGobj.transform.localPosition = new Vector3 (hero.birthPoint.x, hero.birthPoint.y, 0);

      }
    }

    /// <summary>
    /// 暂停当前关卡
    /// </summary>
    public static void Pause ()
    {

    }

    /// <summary>
    /// 恢复当前关卡
    /// </summary>
    public static void Resume ()
    {

    }

    /// <summary>
    /// Determines if is last sub level.
    /// </summary>
    /// <returns><c>true</c> if is last sub level; otherwise, <c>false</c>.</returns>
    public static bool IsLastSubLevel ()
    {
      return false;
    }

    /// <summary>
    /// 找距离最近的目标
    /// </summary>
    /// <returns>The target.</returns>
    /// <param name="sourceGobj">Source gobj.</param>
    public static GameObject FindClosestTarget (HeroBhvr sourceHeroBhvr)
    {


      GameObject sgobj = sourceHeroBhvr.gameObject;
      List<GameObject> ol = null;
      ol = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT 
          ? LevelContext.aliveEnemyGobjs : LevelContext.aliveSelfGobjs;

      return FindClosestTarget (sgobj, ol);
    }

    private static GameObject FindClosestTarget (GameObject sgobj, List<GameObject>  ol)
    {
      // 源对象的 x 坐标
      float posx = sgobj.transform.localPosition.x;
      // 最接近的对象
      GameObject closestGobj = null;
      // 最短的距离
      float closestDistance = 0;
      foreach (GameObject gobj in ol) {

        if (closestGobj == null)
          closestGobj = gobj;
        else {
          float distance = Mathf.Abs (posx - gobj.transform.localPosition.x);
          if (distance > closestDistance) {
            closestDistance = distance;
            closestGobj = gobj;
          }
        }
      }

      return closestGobj;
    }
  }
}

