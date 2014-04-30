using System;
using UnityEngine;
using System.Collections.Generic;
using TDB = TangDragonBones;

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
    public static UIManager uiMgr = null;

    public static void Init ()
    {

      // 监听Dragonbone资源装载完毕事件
      TDB.DbgoManager.RaiseLoadedEvent += OnDbResLoaded;
      //TDB.DbgoManager.RaiseLoadedEvent.

      GameObject gobj = GameObject.Find ("UI Root");
      if (gobj != null) {
        Debug.Log ("xxxx");
        uiMgr = gobj.GetComponent<UIManager> ();
      }
    }

    public static void OnDestory ()
    {
      TDB.DbgoManager.RaiseLoadedEvent -= OnDbResLoaded;
    }

    /// <summary>
    /// 挑战这个关卡
    /// </summary>
    /// <param name="levelId">关卡ID</param>
    /// <param name="group">我方小组</param>
    public static void ChallengeLevel (int levelId, Group group)
    {

      // 确保不在关卡里面
      if (!LevelContext.InLevel) {
        // 设置当前关卡
        if (Config.levelTable.ContainsKey (levelId)) {
          LevelContext.CurrentLevel = Config.levelTable [levelId];
        }
        LevelContext.selfGroup = group;

        // 判断目标子关卡的资源是否已经加载
        if (LevelContext.subLevelStatus != LevelStatus.INTENT) {
          // 试图加载
          LevelContext.subLevelStatus = LevelStatus.INTENT;
          LoadTargetSubLevelRes ();
        }
      }
    }

    /// <summary>
    /// 预加载目标子关卡的资源
    /// </summary>
    /// <param name="levelId">Level identifier.</param>
    private static void LoadTargetSubLevelRes ()
    {
      Debug.Log ("LoadTargetSubLevelRes");
      SubLevel subLevel = LevelContext.TargetSubLevel;

      if (Cache.gobjTable.ContainsKey (subLevel.resName)) {
        // 游戏背景资源已准备完毕
        OnSubLevelResReady ();
      } else if (Config.use_packed_res) {
        // 使用 Assetbundle
        string name = LevelContext.CurrentSubLevel.resName;
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

      Debug.Log ("OnSubLevelLoaded");
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
        GobjManager.Add (gobj);
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

      // 临时英雄表
      Dictionary<string, int> tmpHeroTable = new Dictionary<string, int> ();
      // -- 统计敌方英雄资源 --
      if (LevelContext.TargetSubLevel.enemyGroup != null) {
        foreach (Hero hero in LevelContext.TargetSubLevel.enemyGroup.heros) {
          if (tmpHeroTable.ContainsKey (hero.resName)) {
            int count = tmpHeroTable [hero.resName] + 1;
            tmpHeroTable [hero.resName] = count;
          } else
            tmpHeroTable.Add (hero.resName, 1);
        }
      }

      // -- 统计我方英雄资源 --
      if (LevelContext.selfGroup != null) {
        foreach (Hero hero in LevelContext.selfGroup.heros) {
          if (tmpHeroTable.ContainsKey (hero.resName)) {
            int count = tmpHeroTable [hero.resName] + 1;
            tmpHeroTable [hero.resName] = count;
          } else
            tmpHeroTable.Add (hero.resName, 1);
        }
      }

      foreach (KeyValuePair<string, int> kvp in tmpHeroTable) {
        // 已有的英雄数量
        int has = HeroGobjManager.Size (kvp.Key);
        Debug.Log ("Has " + has + " " + kvp.Key);
        if (has < kvp.Value) {
          int need = kvp.Value - has;
          requiredHeroGobjTable [kvp.Key] = need;
          HeroGobjManager.LazyLoad (kvp.Key, need);
          Debug.Log ("Need " + need + " " + kvp.Key);
        }
      }
    }

    /// <summary>
    /// DragonBones 资源记载后的回调，每加载一个调用一次
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private static void OnDbResLoaded (object sender, TDB.ResEventArgs args)
    {
      Debug.Log ("OnDbResLoaded ====");
      string name = args.Name;
      if (requiredHeroGobjTable.ContainsKey (name)) {
        requiredHeroGobjTable [name] = requiredHeroGobjTable [name] - 1;
      }
      bool loadedCompleted = true;
      foreach (KeyValuePair<string, int> kvp in requiredHeroGobjTable) {
        if (kvp.Value > 0) {
          loadedCompleted = false;
          break;
        }
      }
      if (loadedCompleted) {
        //Debug.Log ("DragonBones Resource Loaded Completed.");
        if (RaiseSubLevelLoadedEvent != null)
          RaiseSubLevelLoadedEvent (null, EventArgs.Empty);
      }
    }

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    public static void EnterNextSubLevel ()
    {
      // 创建背景
      GameObject bgGobj = GobjManager.FetchUnused (LevelContext.TargetSubLevel.resName);
      if (bgGobj != null) {
        bgGobj.SetActive (true);
        Debug.Log ("Background Created.");
        LevelContext.background = bgGobj;
      }

      // 敌方小组列阵
      Group enemyGroup = LevelContext.TargetSubLevel.enemyGroup;
      Embattle (enemyGroup, BattleDirection.LEFT);
      Debug.Log ("EnemyGroup Embattle.");

      // 我方小组列阵
      Embattle (LevelContext.selfGroup, BattleDirection.RIGHT);
      Debug.Log ("SelfGroup Embattle.");


      // 确保清场
      LevelContext.enemyGobjs.Clear ();
      LevelContext.selfGobjs.Clear ();


      // 我方进场
      int i = 0;
      foreach (Hero hero in LevelContext.selfGroup.heros) {
        AddHeroToScene (hero);
        if (uiMgr != null) {
          Debug.Log ("moniter hero --");
          hero.raiseHpChange += uiMgr.greenHpMonitors [i].OnChange;
        }
        i++;
      }
      i = 0;
      // 敌方进场
      foreach (Hero hero in enemyGroup.heros) {
        AddHeroToScene (hero);
        if (uiMgr != null) {
          hero.raiseHpChange += uiMgr.redHpMonitors [i].OnChange;
        }
        i++;
      }

      // 设置关卡状态 InLevel
      if (!LevelContext.InLevel)
        LevelContext.InLevel = true;

      LevelContext.subLevelStatus = LevelStatus.IN;
    }

    /// <summary>
    /// Lefts the sub level. 
    /// </summary>
    public static void LeftSubLevel(){

      // TODO 离开子关卡需要做一下清理工作
    }

    /// <summary>
    /// Lefts the current level.
    /// </summary>
    public static void LeftLevel ()
    {

      // 卸载场景资源
      if (LevelContext.background != null)
        GobjManager.Release (LevelContext.background, true);
      // 卸载场景人物
      foreach (GameObject gobj in LevelContext.enemyGobjs) {
        HeroGobjManager.Release (gobj, true);
      }
      foreach (GameObject gobj in LevelContext.selfGobjs) {
        HeroGobjManager.Release (gobj, true);
      }

      HeroGobjManager.Clear ();

      // 卸载背景
      LevelContext.InLevel = false;
      LevelContext.subLevelStatus = LevelStatus.OUT;
      // 发出离开关卡通知
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

      GameObject gobj = HeroGobjManager.FetchUnused (hero);
      if (gobj != null) {
        gobj.SetActive (true);
        gobj.transform.localPosition = new Vector3 (hero.birthPoint.x, hero.birthPoint.y, 0);
        if (hero.battleDirection == BattleDirection.RIGHT)
          LevelContext.selfGobjs.Add (gobj);
        else
          LevelContext.enemyGobjs.Add (gobj);
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
      List<GameObject> ol = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT 
        ? LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;

      //Debug.Log (" ------- sourceHeroBhvr.hero.battleDirection = " + sourceHeroBhvr.hero.battleDirection);

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
          if (distance < closestDistance) {
            closestDistance = distance;
            closestGobj = gobj;
          }
        }
      }

      return closestGobj;
    }
  }
}

