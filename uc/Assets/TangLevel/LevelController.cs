using System;
using UnityEngine;
using System.Collections.Generic;
using TDB = TangDragonBones;
using TG = TangGame;
using TUI = TangUI;

namespace TangLevel
{
  /// <summary>
  /// Level controller.
  /// </summary>
  public class LevelController : MonoBehaviour
  {

    public static readonly string GOBJ_NAME = "LevelController";
    public static readonly string UI_ROOT_NAME = "LevelUIRoot";

#region Events

    /// <summary>
    /// 成功进入关卡
    /// </summary>
    public static event EventHandler RaiseEnterLevelSuccess;
    /// <summary>
    /// 子关卡的怪物已被清除完毕
    /// </summary>
    public static event EventHandler RaiseSubLevelCleaned;
    /// <summary>
    /// 挑战成功
    /// </summary>
    public static event EventHandler RaiseChallengeSuccess;
    /// <summary>
    /// 挑战失败
    /// </summary>
    public static event EventHandler RaiseChangengeFailure;
    /// <summary>
    /// 进入子关卡成功
    /// </summary>
    public static event EventHandler RaiseEnterSubLevelSuccess;
    /// <summary>
    /// 战斗暂停
    /// </summary>
    public static event EventHandler RaisePause;
    /// <summary>
    /// 战斗恢复
    /// </summary>
    public static event EventHandler RaiseResume;
    /// <summary>
    /// 大招开始
    /// </summary>
    public static event EventHandler UniqueSkillStart;
    /// <summary>
    /// 大招结束
    /// </summary>
    public static event EventHandler UniqueSkillFinish;

#endregion

    /// <summary>
    /// 子关卡需要的英雄游戏对象以及数量
    /// </summary>
    public static Dictionary<string, int> requiredHeroGobjTable = new Dictionary<string, int> ();

#region UIAttribures

    public static GameObject levelUIRoot;
    public static UIManager uiMgr = null;
    public UIAnchor bottomAnchor;
    private static TUI.UIPanelNodeManager bottomPanelMgr;
    private static TG.LevelHeroPanel levelHeroPanel;

#endregion

#region MonoMethods

    void Awake ()
    {
      // UI ----

      levelUIRoot = GameObject.Find ("LevelUIRoot");
      if( levelUIRoot == null )
	{
	  levelUIRoot = NewUIRoot();
	}
      if (levelUIRoot != null) {
        uiMgr = levelUIRoot.GetComponent<UIManager> ();
      }
      if (bottomAnchor != null) {
        bottomPanelMgr = new TUI.UIPanelNodeManager (bottomAnchor, OnBottomPanelEvent);
        bottomPanelMgr.LazyOpen (UIContext.HERO_OP_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, 
				 TUI.UIPanelNode.BlockMode.NONE);
      }

      // Scene ----

      LevelContext.InLevel = false;


    }

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

#region UIEvents

#endregion

    void OnBottomPanelEvent (object sender, TUI.PanelEventArgs args)
    {
      TUI.UIPanelNode node = sender as TUI.UIPanelNode;
      if (node != null) {
        switch (args.EventType) {
        case TUI.EventType.OnLoad:
          // 面板加载成功
          if (UIContext.HERO_OP_PANEL.Equals (node.name)) {
            levelHeroPanel = node.gameObject.GetComponent<TG.LevelHeroPanel> ();
          }
          break;
        }
      }
    }

#region PublicStaticMethods

    public static GameObject EnsureGObj()
    {
      GameObject gobj = GameObject.Find(GOBJ_NAME);
      if(gobj == null)
	{
	  gobj = NewGobj();
	}
      return gobj;
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

        // 显示UI
        if (!levelUIRoot.activeSelf) {
          levelUIRoot.SetActive (true);
        }

        // 设置当前关卡
        if (Config.levelTable.ContainsKey (levelId)) {

          // 克隆一份场景数据
          LevelContext.CurrentLevel = Config.levelTable [levelId].DeepCopy ();
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
      if (LevelContext.AliveEnemyGobjs.Count == 0) {
        ContinueAhead ();
      }
    }

    /// <summary>
    /// Lefts the current level.
    /// </summary>
    public static void LeftLevel ()
    {

      // 先退出子关卡
      LeftSubLevel ();

      // TODO 发出离开关卡通知
      LevelContext.InLevel = false;

      // 取消 HeroOpPanel 对 英雄数据变化的监听
      UnsetHeroOpPanel ();

      // 隐藏UI
      if (levelUIRoot.activeSelf) {
        levelUIRoot.SetActive (false);
      }

    }

    /// <summary>
    /// 暂停当前关卡
    /// </summary>
    public static void Pause ()
    {
      if (RaisePause != null) {
        RaisePause (null, EventArgs.Empty);
      }
    }

    /// <summary>
    /// 恢复当前关卡
    /// </summary>
    public static void Resume ()
    {
      if (RaiseResume != null) {
        RaiseResume (null, EventArgs.Empty);
      }
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

#region Callback

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
    /// 处理英雄死亡事件
    /// </summary>
    private static void OnHeroDead (object sender, EventArgs args)
    {

      HeroBhvr bhvr = sender as HeroBhvr;
      if (bhvr != null) {

        if (bhvr.hero.battleDirection == BattleDirection.RIGHT) {
          // 是己方英雄
          // 如果己方英雄全部死亡，则发出挑战失败通知
          if (LevelContext.AliveSelfGobjs.Count == 0) {
            Debug.Log ("challenge failure");
            if (RaiseChangengeFailure != null) {
              RaiseChangengeFailure (null, EventArgs.Empty);
            }
          }
        } else {
          // 是敌方英雄
          // 如果敌方英雄全部死亡，
          //   如果最后的子关卡，则发出关卡已被清除通知 ，否则发出子关卡已被清除通知
          //   
          if (LevelContext.AliveEnemyGobjs.Count == 0) {
            if (LevelContext.CurrentSubLevel.index == LevelContext.CurrentLevel.subLeves.Length - 1) {
              // 发出关卡挑战成功
              Debug.Log ("challenge success");
              CelebrateVictory ();
              if (RaiseChallengeSuccess != null) {
                RaiseChallengeSuccess (null, EventArgs.Empty);
              }
            } else {
              if (RaiseSubLevelCleaned != null) {
                RaiseSubLevelCleaned (null, EventArgs.Empty);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// 处理英雄位置变化事件
    /// </summary>
    private static void OnHeroPosChanged (object sender, EventArgs args)
    {

      DirectedNavAgent agent = sender as DirectedNavAgent;
      if (agent != null) {

        // 英雄的位置超过设定的边际
        if (agent.myTransform.localPosition.x > Config.RIGHT_BOUND) {

          // 离开当前子关卡
          LeftSubLevel ();

          // 进入下一个子关卡
          ContinueNextSubLevel ();

        }

      }

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
    /// 子关卡需要的资源已准备好
    /// </summary>
    private static void AllSubLevelResourceReady ()
    {

      Debug.Log ("AllSubLevelResourceReady");


      if (!LevelContext.InLevel) { // 如果还在关卡外面

        // 首次进入子关卡 ----
        SetupHeroOpPanel ();

        // 进入子关卡
        EnterNextSubLevel ();

        // 设置关卡状态 InLevel
        LevelContext.InLevel = true;

        // 发出关卡进入成功通知
        if (RaiseEnterLevelSuccess != null)
          RaiseEnterLevelSuccess (null, EventArgs.Empty);

      } else { // 已经在关卡里面

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

      // 整理双方战队 - 去掉死亡的队员
      LevelContext.selfGroup.Arrange ();
      LevelContext.CurrentSubLevel.enemyGroup.Arrange ();

      // 敌方小组列阵
      Group enemyGroup = LevelContext.CurrentSubLevel.enemyGroup;
      Embattle (enemyGroup, BattleDirection.LEFT);
      // 我方小组列阵
      Embattle (LevelContext.selfGroup, BattleDirection.RIGHT);

      // 我方进场
      foreach (Hero hero in LevelContext.selfGroup.aliveHeros) {
        if (hero.hp > 0) {
          GameObject g = AddHeroToScene (hero);
          LevelContext.selfGobjs.Add (g);
        }
      }

      // 敌方进场
      foreach (Hero hero in enemyGroup.aliveHeros) {
        GameObject g = AddHeroToScene (hero);
        LevelContext.enemyGobjs.Add (g);
      }

      // 监听场景中的英雄
      // 己方英雄
      ListenSelftHeros ();
      // 敌方英雄
      ListenEnemyHeros ();

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

      // 释放其他(背景，特效)资源
      GobjManager.ReleaseAll (false);

    }

    /// <summary>
    /// Listens the selft heros.
    /// </summary>
    private static void ListenSelftHeros ()
    {

      List<Hero>.Enumerator heroEnum = LevelContext.selfGroup.aliveHeros.GetEnumerator ();
      List<GameObject>.Enumerator gobjEnum = LevelContext.selfGobjs.GetEnumerator ();

      int i = 0;
      while (heroEnum.MoveNext () && gobjEnum.MoveNext () ) {

        Hero h = heroEnum.Current;
        GameObject g = gobjEnum.Current;

        // 监听己方英雄HP变化 ----
        // 英雄身上的血条填充
        h.raiseHpChange += uiMgr.greenHpMonitors [i].OnChange;
        // 血条显示与隐藏
        h.raiseHpChange += uiMgr.greenDisplayByHurts [i].OnHpChange;
        // 监听己方英雄的位置变化 -----
        DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
        if (agent != null) {
          agent.raiseScrPosChanged += uiMgr.greenHpPosMonitors [i].OnChange;
          agent.RaisePosChanged += OnHeroPosChanged;
        }
        // 监听英雄的死亡 ----
        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr != null) {
          heroBhvr.RaiseDead += OnHeroDead;
        }
        i++;
      }
    }

    /// <summary>
    /// 取消对己方英雄的监控
    /// </summary>
    private static void UnlistenSelfHeros ()
    {

      List<Hero>.Enumerator heroEnum = LevelContext.selfGroup.aliveHeros.GetEnumerator ();
      List<GameObject>.Enumerator gobjEnum = LevelContext.AliveSelfGobjs.GetEnumerator ();

      int i = 0;
      while (heroEnum.MoveNext () && gobjEnum.MoveNext ()) {

        Hero h = heroEnum.Current;
        GameObject g = gobjEnum.Current;

        // 监听己方英雄HP变化 ----
        // 英雄身上的血条填充
        h.raiseHpChange -= uiMgr.greenHpMonitors [i].OnChange;
        // 血条显示与隐藏
        h.raiseHpChange -= uiMgr.greenDisplayByHurts [i].OnHpChange;
        uiMgr.greenDisplayByHurts [i].gameObject.SetActive (false);
        // 监听己方英雄的位置变化 -----
        DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
        if (agent != null) {
          agent.raiseScrPosChanged -= uiMgr.greenHpPosMonitors [i].OnChange;
          agent.RaisePosChanged -= OnHeroPosChanged;
        }
        // 监听英雄的死亡
        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr != null) {
          heroBhvr.RaiseDead -= OnHeroDead;
        }
        i++;
      }
    }

    /// <summary>
    /// Listens the enemy heros.
    /// </summary>
    private static void ListenEnemyHeros ()
    {

      List<Hero>.Enumerator heroEnum = LevelContext.CurrentSubLevel.enemyGroup.aliveHeros.GetEnumerator ();
      List<GameObject>.Enumerator gobjEnum = LevelContext.enemyGobjs.GetEnumerator ();

      int i = 0;
      while (heroEnum.MoveNext () && gobjEnum.MoveNext ()) {

        Hero h = heroEnum.Current;
        GameObject g = gobjEnum.Current;

        // 监听敌方英雄HP变化 ----
        // 英雄身上的血条填充
        h.raiseHpChange += uiMgr.redHpMonitors [i].OnChange;
        // 血条的显示与隐藏
        h.raiseHpChange += uiMgr.redDisplayByHurts [i].OnHpChange;
        // 敌方英雄的位置变化 ----
        DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
        if (agent != null) {
          agent.raiseScrPosChanged += uiMgr.redHpPosMonitors [i].OnChange;
        }
          

        // 监听英雄的死亡
        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr != null) {
          heroBhvr.RaiseDead += OnHeroDead;
        }
        i++;
      }
    }

    private static void UnlistenEnemyHeros ()
    {

      List<Hero>.Enumerator heroEnum = LevelContext.CurrentSubLevel.enemyGroup.aliveHeros.GetEnumerator ();
      List<GameObject>.Enumerator gobjEnum = LevelContext.enemyGobjs.GetEnumerator ();

      int i = 0;
      while (heroEnum.MoveNext () && gobjEnum.MoveNext ()) {

        Hero h = heroEnum.Current;
        GameObject g = gobjEnum.Current;

        // 监听敌方英雄HP变化 ----
        // 英雄身上的血条填充
        h.raiseHpChange -= uiMgr.redHpMonitors [i].OnChange;
        // 血条的显示与隐藏
        h.raiseHpChange -= uiMgr.redDisplayByHurts [i].OnHpChange;
        uiMgr.redDisplayByHurts [i].gameObject.SetActive (false);
        // 敌方英雄的位置变化 ----
        DirectedNavAgent agent = g.GetComponent<DirectedNavAgent> ();
        if (agent != null) {
          agent.raiseScrPosChanged -= uiMgr.redHpPosMonitors [i].OnChange;
        }

        // 监听英雄的死亡
        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr != null) {
          heroBhvr.RaiseDead -= OnHeroDead;
        }
        
        i++;
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

      List<Hero> heros = group.aliveHeros;

      // 调整英雄面对的方向
      foreach (Hero hero in heros) {
        hero.battleDirection = direction;
      }

      // 根据攻击距离进行排序
      heros.Sort (delegate(Hero hero1, Hero hero2) {
	  return hero1.attackDistance.CompareTo (hero2.attackDistance);
	});

      // 分成两列
      List<Hero> column1 = new List<Hero> ();
      List<Hero> column2 = new List<Hero> ();
      Vector2 origin = Vector2.zero;
      int stepx = 6; // 排与排之间的距离

      if (BattleDirection.RIGHT == direction) { // 我方战队

        // 分成两列
        for (int i = 0; i < heros.Count; i++) {
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

      } else {  // 敌方战队

        // 清空两列内容
        column1.Clear ();
        column2.Clear ();

        // 分成两列
        for (int i = 0; i < heros.Count; i++) {
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
    /// 英雄们继续前进
    /// </summary>
    private static void ContinueAhead ()
    {

      foreach (GameObject g in LevelContext.AliveSelfGobjs) {

        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr == null) {
          continue;
        }

        DirectedNavigable nav = g.GetComponent<DirectedNavigable> ();
        if (nav != null) {
          nav.NavTo (Config.RIGHT_BOUND + 10);
        }
      }

    }

    /// <summary>
    /// 继续攻打下一个子关卡
    /// </summary>
    private static void ContinueNextSubLevel ()
    {

      // 如果子关卡还没有加载，加载子关卡资源
      // 否则进入子关卡

      if (LevelContext.TargetSubLevel != null) {

        // 离开子关卡
        LeftSubLevel ();

        // 加载目标子关卡资源
        LoadTargetSubLevelRes ();
      }
    }

    /// <summary>
    /// 英雄们庆祝胜利
    /// </summary>
    private static void CelebrateVictory ()
    {

      foreach (GameObject g in LevelContext.selfGobjs) {

        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr == null) {
          continue;
        }

        heroBhvr.CelebrateVictory ();
      }
    }

    private static void SetupHeroOpPanel(){

      // 创建英雄UI操作面板
      Hero[] heros = LevelContext.selfGroup.heros;
      TG.LevelHeroPanelData data = new TG.LevelHeroPanelData ();
      data.heroCount = LevelContext.selfGroup.heros.Length;
      levelHeroPanel.param = data;
      List<TG.LevelHeroItem>.Enumerator wgtEnum = levelHeroPanel.itemList.GetEnumerator ();
      int i = 0;
      while (wgtEnum.MoveNext ()) {
        Hero h = heros [i];
        TG.LevelHeroItem w = wgtEnum.Current;

        // 英雄头像 ----
        w.SetHeroId (h.id);
        h.raiseHpChange += w.SetHp;
        h.raiseMpChange += w.SetMp;
        i++;

      }
    }

    private static void UnsetHeroOpPanel(){

      // 创建英雄UI操作面板
      Hero[] heros = LevelContext.selfGroup.heros;
      List<TG.LevelHeroItem>.Enumerator wgtEnum = levelHeroPanel.itemList.GetEnumerator ();
      int i = 0;
      while (wgtEnum.MoveNext ()) {
        Hero h = heros [i];
        TG.LevelHeroItem w = wgtEnum.Current;
        // 英雄头像 ----
        h.raiseHpChange -= w.SetHp;
        h.raiseMpChange -= w.SetMp;
        i++;

      }
    }

    private static GameObject NewGobj ()
    {

      GameObject gobj = null;
      UnityEngine.Object asset = Resources.Load("Prefabs/"+GOBJ_NAME);
      if( asset != null)
	{
	  gobj = Instantiate(asset) as GameObject;
	}
      return gobj;
    }

    private static GameObject NewUIRoot()
    {
      GameObject gobj = null;
      UnityEngine.Object asset = Resources.Load("Prefabs/"+UI_ROOT_NAME);
      if( asset != null)
	{
	  gobj = Instantiate(asset) as GameObject;
	}
      return gobj;
      
    }


#endregion
  }
}

