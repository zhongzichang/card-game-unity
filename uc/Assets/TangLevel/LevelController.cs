﻿using System;
using UnityEngine;
using System.Collections.Generic;
using TDB = TangDragonBones;

namespace TangLevel
{
  /// <summary>
  /// Level controller.
  /// </summary>
  public class LevelController : MonoBehaviour
  {
    /// <summary>
    /// Occurs when raise sub level loaded event.
    /// 关卡加载完毕
    /// </summary>
    public static event EventHandler RaiseLevelLoadedEvent;
    /// <summary>
    /// 挑战成功
    /// </summary>
    public static event EventHandler RaiseChallengeSuccessEvent;
    /// <summary>
    /// 挑战失败
    /// </summary>
    public static event EventHandler RaiseChallengeFailEvent;

    /// <summary>
    /// 子关卡需要的英雄游戏对象以及数量
    /// </summary>
    public static Dictionary<string, int> requiredHeroGobjTable = new Dictionary<string, int> ();
    public static UIManager uiMgr = null;

    #region MonoMethods

    void Start ()
    {

      GameObject gobj = GameObject.Find ("BattleUIRoot");
      if (gobj != null) {
        uiMgr = gobj.GetComponent<UIManager> ();
      }

      LevelContext.InLevel = false;

      Mocker.Configure ();

    }
    // test ............
    void OnGUI ()
    {
      if (GUI.Button (new Rect (10, 10, 150, 100), "Load Level")) {

        ChallengeLevel (1, Mocker.MockGroup ());

      }


      if (GUI.Button (new Rect (200, 10, 150, 100), " Next Sub Level")) {

        ChallengeNextSubLevel ();

      }

      if (GUI.Button (new Rect (10, 210, 150, 100), " Left Level")) {

        LeftLevel ();

      }
    }
    // .................
    void OnEnable ()
    {
      // 监听Dragonbone资源装载完毕事件
      TDB.DbgoManager.RaiseLoadedEvent += OnDragonBonesResLoaded;
    }

    void OnDisable ()
    {
      TDB.DbgoManager.RaiseLoadedEvent -= OnDragonBonesResLoaded;
    }

    #endregion

    #region PublicStaticMethods

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

        LoadTargetSubLevelRes ();

      }
    }

    /// <summary>
    /// 挑战下一个子关卡
    /// </summary>
    public static void ChallengeNextSubLevel ()
    {
      // 如果子关卡还没有加载，加载子关卡资源
      // 否则进入子关卡

      if (LevelContext.TargetSubLevel != null) {

        // 离开子关卡
        LeftSubLevel ();

        // 记载目标子关卡资源
        LoadTargetSubLevelRes ();

      }

    }

    /// <summary>
    /// Lefts the current level.
    /// </summary>
    public static void LeftLevel ()
    {

      // 先退出子关卡
      LeftSubLevel ();

      LevelContext.InLevel = false;
      // 发出离开关卡通知
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

    /// <summary>
    /// 获取距离最近的可攻击目标
    /// </summary>
    /// <returns>The closest target.</returns>
    /// <param name="sgobj">Sgobj.</param>
    /// <param name="ol">Ol.</param>
    public static GameObject FindClosestTarget (GameObject sgobj, List<GameObject>  ol)
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

    #endregion

    #region PrivateStaticMethods

    /// <summary>
    /// 加载目标子关卡的资源
    /// </summary>
    /// <param name="levelId">Level identifier.</param>
    private static void LoadTargetSubLevelRes ()
    {

      Debug.Log ("LoadTargetSubLevelRes");

      SubLevel subLevel = LevelContext.TargetSubLevel;
      Debug.Log ("subLevel.resName " + subLevel.resName);
      GameObject bgGobj = GobjManager.FetchUnused (subLevel.resName);
      if (bgGobj == null) {

        // 需要加载地图资源
        if (!GobjManager.HasHandler (OnSubLevelMapLoaded)) {
          GobjManager.RaiseLoadEvent += OnSubLevelMapLoaded;
        }
        GobjManager.LazyLoad (subLevel.resName);

      } else {

        // 加载子关卡所需英雄资源
        LoadSubLevelHeroResources ();
      }

    }

    /// <summary>
    /// 地图加载完毕的回调
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private static void OnSubLevelMapLoaded (object sender, LoadResultEventArgs args)
    {

      Debug.Log ("OnSubLevelMapLoaded");

      if (args.Name == LevelContext.TargetSubLevel.resName) {

        if (GobjManager.HasHandler (OnSubLevelMapLoaded)) {
          GobjManager.RaiseLoadEvent -= OnSubLevelMapLoaded;
        }

        // 加载子关卡所需英雄资源
        LoadSubLevelHeroResources ();

      }
    }

    /// <summary>
    /// 计算加载子关卡所需要的英雄
    /// </summary>
    private static void LoadSubLevelHeroResources ()
    {
      Debug.Log ("LoadSubLevelHeroResources");

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

      // 加载所需要的英雄资源
      foreach (KeyValuePair<string, int> kvp in tmpHeroTable) {
        // 已有的英雄数量
        int has = HeroGobjManager.Size (kvp.Key);
        int need = 0;
        if (has < kvp.Value) {
          need = kvp.Value - has;
          requiredHeroGobjTable [kvp.Key] = need;
          HeroGobjManager.LazyLoad (kvp.Key, need);
        }
        Debug.Log ("Has " + has + " " + kvp.Key + " , Need " + need + " " + kvp.Key);
      }

      // 所需英雄资源已经存在
      if (requiredHeroGobjTable.Count == 0) {

        // 通知所有的资源已经准备完毕
        AllSubLevelResourceReady ();

      }
    }

    /// <summary>
    /// DragonBones 资源记载后的回调，每加载一个调用一次
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private static void OnDragonBonesResLoaded (object sender, TDB.ResEventArgs args)
    {
      string name = args.Name;

      // 修正英雄资源需求表
      if (requiredHeroGobjTable.ContainsKey (name)) {
        requiredHeroGobjTable [name] = requiredHeroGobjTable [name] - 1;
      }

      // 检查所有需求是否完成
      bool loadedCompleted = true;
      foreach (KeyValuePair<string, int> kvp in requiredHeroGobjTable) {
        if (kvp.Value > 0) {
          loadedCompleted = false;
          break;
        }
      }

      // 如果需求已经完成，则进入下一个子关卡
      if (loadedCompleted) {

        // 所有的资源都已准备完毕
        AllSubLevelResourceReady ();

      }
    }

    /// <summary>
    /// 子关卡需要的资源已准备好
    /// </summary>
    private static void AllSubLevelResourceReady ()
    {

      Debug.Log ("AllSubLevelResourceReady");

      // 如果还在关卡外面
      if (!LevelContext.InLevel) {

        // 发出关卡加载完成通知
        if (RaiseLevelLoadedEvent != null)
          RaiseLevelLoadedEvent (null, EventArgs.Empty);

        // 进入子关卡
        EnterNextSubLevel ();

        // 设置关卡状态 InLevel
        LevelContext.InLevel = true;

      } else {

        // 记录
        // TODO 用于优化子关卡加载

        // 进入子关卡
        EnterNextSubLevel ();
      }
    }

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    private static void EnterNextSubLevel ()
    {
      LevelContext.CurrentSubLevel = LevelContext.TargetSubLevel;

      // 确保清场
      LevelContext.enemyGobjs.Clear ();
      LevelContext.selfGobjs.Clear ();

      // 创建背景
      GameObject bgGobj = GobjManager.FetchUnused (LevelContext.CurrentSubLevel.resName);
      if (bgGobj != null) {
        bgGobj.SetActive (true);
        Debug.Log ("Background Created.");
        LevelContext.background = bgGobj;
      }

      // 敌方小组列阵
      Group enemyGroup = LevelContext.CurrentSubLevel.enemyGroup;
      Embattle (enemyGroup, BattleDirection.LEFT);
      // 我方小组列阵
      Embattle (LevelContext.selfGroup, BattleDirection.RIGHT);

      // 我方进场
      foreach (Hero hero in LevelContext.selfGroup.heros) {
        if (hero.hp > 0) {
          GameObject g = AddHeroToScene (hero);
          LevelContext.selfGobjs.Add (g);
        }
      }

      // 敌方进场
      foreach (Hero hero in enemyGroup.heros) {
        GameObject g = AddHeroToScene (hero);
        LevelContext.enemyGobjs.Add (g);
      }

      // 监听场景中的英雄
      // 己方英雄
      ListenSelftHeros ();
      // 敌方英雄
      ListenEnemyHeros ();

      //LevelContext.CurrentSubLevelIndex++;

    }

    /// <summary>
    /// Lefts the sub level. 
    /// </summary>
    private static void LeftSubLevel ()
    {
      // 离开子关卡需要做一下清理工作 ----

      // 取消对我方英雄的监听
      UnlistenSelfHeros ();
      // 取消对敌方英雄的监听
      UnlistenEnemyHeros ();

      // 释放己方英雄
      foreach (GameObject gobj in LevelContext.selfGobjs) {
        HeroGobjManager.Release (gobj);
      }

      // 释放敌方英雄
      foreach (GameObject gobj in LevelContext.enemyGobjs) {
        HeroGobjManager.Release (gobj);
      }

      // 确保清场
      LevelContext.enemyGobjs.Clear ();
      LevelContext.selfGobjs.Clear ();


      // 释放背景
      if (LevelContext.background != null) {
        GobjManager.Release (LevelContext.background, true);
      }


    }

    /// <summary>
    /// Listens the selft heros.
    /// </summary>
    private static void ListenSelftHeros ()
    {

      GameObject[] gobjs = LevelContext.selfGobjs.ToArray ();
      Hero[] heros = LevelContext.selfGroup.heros;

      for (int i = 0; i < gobjs.Length && i < heros.Length; i++) {

        GameObject g = gobjs [i];
        Hero h = heros [i];

        if (g != null) {
          DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
          if (uiMgr != null) {
            // 监听己方英雄HP变化 ----
            // 英雄身上的血条填充
            h.raiseHpChange += uiMgr.greenHpMonitors [i].OnChange;
            // 血条显示与隐藏
            h.raiseHpChange += uiMgr.greenDisplayByHurts [i].OnHpChange;
            // 英雄头像 ----
            // 血条
            h.raiseHpChange += uiMgr.selfHpMonitors [i].OnChange;
            // 监听己方英雄的位置变化 -----
            if (agent != null) {
              agent.raisePositionChange += uiMgr.greenHpPosMonitors [i].OnChange;
            }
          }
        }
      }
    }

    /// <summary>
    /// 取消对己方英雄的监控
    /// </summary>
    private static void UnlistenSelfHeros ()
    {

      GameObject[] gobjs = LevelContext.selfGobjs.ToArray ();
      Hero[] heros = LevelContext.selfGroup.heros;
      for (int i = 0; i < gobjs.Length && i < heros.Length; i++) {

        GameObject g = gobjs [i];
        Hero h = heros [i];

        if (g != null) {
          DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
          if (uiMgr != null) {
            // 监听己方英雄HP变化 ----
            // 英雄身上的血条填充
            h.raiseHpChange -= uiMgr.greenHpMonitors [i].OnChange;
            // 血条显示与隐藏
            h.raiseHpChange -= uiMgr.greenDisplayByHurts [i].OnHpChange;
            // 英雄头像 ----
            // 血条
            h.raiseHpChange -= uiMgr.selfHpMonitors [i].OnChange;
            // 监听己方英雄的位置变化 -----
            if (agent != null) {
              agent.raisePositionChange -= uiMgr.greenHpPosMonitors [i].OnChange;
            }
          }
        }
      }
    }

    /// <summary>
    /// Listens the enemy heros.
    /// </summary>
    private static void ListenEnemyHeros ()
    {

      GameObject[] gobjs = LevelContext.enemyGobjs.ToArray ();
      Hero[] heros = LevelContext.CurrentSubLevel.enemyGroup.heros;

      for (int i = 0; i < gobjs.Length; i++) {

        GameObject g = gobjs [i];
        Hero h = heros [i];

        if (g != null) {
          DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
          if (uiMgr != null && uiMgr.redHpMonitors.Length > i) {
            // 监听敌方英雄HP变化 ----
            // 英雄身上的血条填充
            h.raiseHpChange += uiMgr.redHpMonitors [i].OnChange;
            // 血条的显示与隐藏
            h.raiseHpChange += uiMgr.redDisplayByHurts [i].OnHpChange;
            // 敌方英雄的位置变化 ----
            if (agent != null) {
              agent.raisePositionChange += uiMgr.redHpPosMonitors [i].OnChange;
            }
          }
        }
      }
    }

    private static void UnlistenEnemyHeros ()
    {

      GameObject[] gobjs = LevelContext.enemyGobjs.ToArray ();
      Hero[] heros = LevelContext.CurrentSubLevel.enemyGroup.heros;

      for (int i = 0; i < gobjs.Length; i++) {

        GameObject g = gobjs [i];
        Hero h = heros [i];

        if (g != null) {
          DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
          if (uiMgr != null) {
            // 监听敌方英雄HP变化 ----
            // 英雄身上的血条填充
            h.raiseHpChange -= uiMgr.redHpMonitors [i].OnChange;
            // 血条的显示与隐藏
            h.raiseHpChange -= uiMgr.redDisplayByHurts [i].OnHpChange;
            // 敌方英雄的位置变化 ----
            if (agent != null) {
              agent.raisePositionChange -= uiMgr.redHpPosMonitors [i].OnChange;
            }
          }
        }
      }
    }

    /// <summary>
    /// 增加一个英雄到场景中
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="side">Side.</param>
    private static GameObject AddHeroToScene (Hero hero)
    {

      GameObject gobj = HeroGobjManager.FetchUnused (hero);
      if (gobj != null) {
        gobj.transform.localPosition = new Vector3 (hero.birthPoint.x, hero.birthPoint.y, 0);
        gobj.SetActive (true);
      }
      return gobj;
    }

    /// <summary>
    /// 列阵
    /// </summary>
    /// <param name="group">Group.</param>
    /// <param name="side">Side.</param>
    private static void Embattle (Group group, BattleDirection direction)
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

    #endregion
  }
}

