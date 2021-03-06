﻿using System;
using UnityEngine;
using System.Collections.Generic;
using TDB = TangDragonBones;
using TG = TangGame;
using TGU = TangGame.UI;
using TUI = TangUI;
using TangPlace;
using System.Collections;
using TU = TangUtils;

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
    /// 离开关卡
    /// </summary>
    public static event EventHandler RaiseLeftLevel;
    /// <summary>
    /// 子关卡的怪物已被清除完毕
    /// </summary>
    public static event EventHandler RaiseSubLevelCleaned;
    /// <summary>
    /// 进入子关卡
    /// </summary>
    public static event EventHandler RaiseEnterSubLevel;
    /// <summary>
    /// 离开子关卡
    /// </summary>
    public static event EventHandler RaiseLeftSubLevel;
    /// <summary>
    /// 战斗开始
    /// </summary>
    public static event EventHandler RaiseBattleStart;
    /// <summary>
    /// 战斗结束
    /// </summary>
    public static event EventHandler RaiseBattleOver;
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
    public static event EventHandler BigMoveStart;
    public static event EventHandler BigMoveEnd;

    #endregion

    #region Attributes

    /// <summary>
    /// 子关卡需要的英雄游戏对象以及数量
    /// </summary>
    public static Dictionary<string, int> requiredHeroGobjTable = new Dictionary<string, int> ();
    public static Dictionary<string, int> requiredEffectorGobjTable = new Dictionary<string, int> ();
    private static int m_bigMoveCounter = 0;
    private static bool lazyShowSuccess = false;
    // 显示挑战成功信息
    private static bool lazyShowFailure = false;
    // 显示挑战失败信息

    #endregion

    #region UIAttribures

    public static GameObject levelUIRoot;
    public static UIManager uiMgr = null;
    private static TG.LevelHeroPanel levelHeroPanel;
    private static TG.LevelPausePanel levelPausePanel;
    private static TGU.BattleResultPanel battleResultPanel;
    private static TG.LevelControllPanel levelControllPanel;
    private static TGU.LevelResourcePanel levelResourcePanel;
    private static TGU.LevelNextPanel levelNextPanel;
    private static TUI.UIPanelNodeManager centerPanelMgr;
    private static TUI.UIPanelNodeManager ltPanelMgr;
    private static TUI.UIPanelNodeManager rtPanelMgr;
    private static TUI.UIPanelNodeManager rightPanelMgr;
    public GameObject uiRoot;
    public UIAnchor centerAnchor;
    public UIAnchor ltAnchor;
    public UIAnchor rtAnchor;
    public UIAnchor rightAnchor;

    #endregion

    #region Properties

    public static int BigMoveCounter {
      get {
        return m_bigMoveCounter;
      }
      set {
        if (m_bigMoveCounter != value) {
          if (m_bigMoveCounter == 0) {
            BigMoveStart (null, EventArgs.Empty);
          }
          m_bigMoveCounter = value;
          if (m_bigMoveCounter == 0) {
            BigMoveEnd (null, EventArgs.Empty);
          }
        }
      }
    }

    #endregion

    #region MonoMethods

    void Awake ()
    {
      // UI ----
      if (uiRoot != null) {

        // 确保 LevelRootUI 的存在
        levelUIRoot = uiRoot;
        if (levelUIRoot == null) {
          levelUIRoot = NewUIRoot ();
        }

        if (levelUIRoot != null) {

          // 隐藏整个UI
          if (levelUIRoot.activeSelf) {
            levelUIRoot.SetActive (false);
          }

          // 血条
          uiMgr = levelUIRoot.GetComponent<UIManager> ();

        }

        // 中心锚点
        if (centerAnchor != null) {

          // 下面的英雄操作面板
          centerPanelMgr = new TUI.UIPanelNodeManager (centerAnchor, OnPanelEvent);
          centerPanelMgr.LazyOpen (UIContext.HERO_OP_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, 
            TUI.UIPanelNode.BlockMode.NONE);

          // 中间的关卡暂停面板
          centerPanelMgr = new TUI.UIPanelNodeManager (centerAnchor, OnPanelEvent);
          centerPanelMgr.LazyOpen (UIContext.LEVEL_PAUSE_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, 
            TUI.UIPanelNode.BlockMode.SPRITE);

          // 战斗结果面板
          centerPanelMgr = new TUI.UIPanelNodeManager (centerAnchor, OnPanelEvent);
          centerPanelMgr.LazyOpen (UIContext.BATTLE_RESULT_PANEL, TUI.UIPanelNode.OpenMode.OVERRIDE,
            TUI.UIPanelNode.BlockMode.NONE);
        }

        // 左上锚点
        if (ltAnchor != null) {
          // 控制面板
          ltPanelMgr = new TUI.UIPanelNodeManager (ltAnchor, OnPanelEvent);
          ltPanelMgr.LazyOpen (UIContext.LEVEL_CONTROLL_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, 
            TUI.UIPanelNode.BlockMode.NONE);
        }

        // 右上锚点
        if (rtAnchor != null) {
          rtPanelMgr = new TUI.UIPanelNodeManager (rtAnchor, OnPanelEvent);
          rtPanelMgr.LazyOpen (UIContext.LEVEL_RESOURCE_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, 
            TUI.UIPanelNode.BlockMode.NONE);
        }

        // 右锚点
        if (rightAnchor != null) {
          rightPanelMgr = new TUI.UIPanelNodeManager (rightAnchor, OnPanelEvent);
          rightPanelMgr.LazyOpen (UIContext.LEVEL_NEXT_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, 
            TUI.UIPanelNode.BlockMode.NONE);
        }

      } else {
        Debug.LogError ("Can not found Level UI Root");
      }

      // Scene ----

      LevelContext.InLevel = false;
      LevelContext.Challenging = false;

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

    void Update ()
    {

      if (lazyShowFailure) {
        lazyShowFailure = false;
        StartCoroutine (ShowFailure ());
      } else if (lazyShowSuccess) {
        lazyShowSuccess = false;
        StartCoroutine (ShowSuccess ());
      }

    }


    #endregion

    #region UIEventHandlers

    /// <summary>
    /// 中央锚点
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnPanelEvent (object sender, TUI.PanelEventArgs args)
    {
      TUI.UIPanelNode node = sender as TUI.UIPanelNode;
      if (node != null) {
        switch (args.EventType) {
        case TUI.EventType.OnLoad:
          // 关卡暂停面板
          if (UIContext.LEVEL_PAUSE_PANEL.Equals (node.name)) {
            node.gameObject.SetActive (false);
            levelPausePanel = node.gameObject.GetComponent<TG.LevelPausePanel> ();
            levelPausePanel.continueBtn.onClick += OnContinueBtnClick;
            levelPausePanel.quitBtn.onClick += OnQuitBtnClick;
          }
          // 战斗结果面板
          if (UIContext.BATTLE_RESULT_PANEL.Equals (node.name)) {
            node.gameObject.SetActive (false);
            battleResultPanel = node.gameObject.GetComponent<TGU.BattleResultPanel> ();
            battleResultPanel.winNextBtn.onClick += OnQuitBtnClick; 
            battleResultPanel.loseNextBtn.onClick += OnQuitBtnClick;
            battleResultPanel.winReplayBtn.onClick += OnReplayBtnClick;
          }
          // 英雄操作面板
          else if (UIContext.HERO_OP_PANEL.Equals (node.name)) {
            //node.gameObject.SetActive (false);
            levelHeroPanel = node.gameObject.GetComponent<TG.LevelHeroPanel> ();
          }
          // 关卡控制面板
          else if (UIContext.LEVEL_CONTROLL_PANEL.Equals (node.name)) {
            levelControllPanel = node.gameObject.GetComponent<TG.LevelControllPanel> ();
            levelControllPanel.pauseBtn.onClick += OnPauseBtnClick;
          }
          // 资源面板
          else if (UIContext.LEVEL_RESOURCE_PANEL.Equals (node.name)) {
            levelResourcePanel = node.gameObject.GetComponent<TGU.LevelResourcePanel> ();
          }
          // 下一个子关卡的按钮面板
          else if (UIContext.LEVEL_NEXT_PANEL.Equals (node.name)) {
            levelNextPanel = node.gameObject.GetComponent<TGU.LevelNextPanel> ();
            levelNextPanel.nextBtn.onClick = OnLevelNextBtnClick;
          }
          break;
        }
      }
    }

    /// <summary>
    /// 当点击暂停面板的继续按钮
    /// </summary>
    /// <param name="g">The green component.</param>
    private void OnContinueBtnClick (GameObject g)
    {
      Resume ();
      levelPausePanel.gameObject.SetActive (false);
    }

    /// <summary>
    /// 退出按钮被点击
    /// </summary>
    /// <param name="g">The green component.</param>
    private void OnQuitBtnClick (GameObject g)
    {

      LeftLevel ();

      // 暂停强设置为 home ，等加上 History 再做处理
      PlaceController.Back ();

    }

    /// <summary>
    /// 面板上的英雄图标被点击
    /// </summary>
    /// <param name="item">Item.</param>
    private static void OnHeroIconClick (ViewItem viewItem)
    {
      if (LevelContext.AliveDefenseGobjs.Count > 0) {
        TG.LevelHeroItem item = viewItem as TG.LevelHeroItem;
        if (item != null) {
          // 获取相应的英雄对象
          HeroBhvr h = LevelContext.GetHeroComponent<HeroBhvr> (item.heroId);
          if (h != null && h.hero != null && h.hero.hp > 0 && h.hero.mp == h.hero.maxMp) {
            h.BigMove ();
          }
        }
      }
    }

    /// <summary>
    /// 暂停按钮被点击
    /// </summary>
    private static void OnPauseBtnClick (GameObject g)
    {
      Pause ();
    }

    /// <summary>
    /// 下一个子关卡按钮被点击
    /// </summary>
    /// <param name="g">The green component.</param>
    private static void OnLevelNextBtnClick (GameObject g)
    {
      levelNextPanel.gameObject.SetActive (false);
      ChallengeNextSubLevel ();
    }

    /// <summary>
    /// 重新挑战按钮被点击
    /// </summary>
    /// <param name="g">The green component.</param>
    private static void OnReplayBtnClick (GameObject g)
    {
      ReChallengeLevel ();
    }

    #endregion

    #region PublicStaticMethods

    public static GameObject EnsureGObj ()
    {
      GameObject gobj = GameObject.Find (GOBJ_NAME);
      if (gobj == null) {
        gobj = NewGobj ();
      }
      return gobj;
    }

    /// <summary>
    /// 挑战这个关卡
    /// </summary>
    /// <param name="levelId">关卡ID</param>
    /// <param name="heroIds">选中的多个英雄ID</param>
    public static void ChallengeLevel (int levelId, int[] heroIds)
    {

      if (heroIds.Length > 0) {

        Debug.Log ("TangLevel: Set levelId=1001 for debuging ");
        Debug.Log ("TangLevel: HeroIds = " + TU.TextUtil.Join (heroIds));
        LevelController.ChallengeLevel (1001, DomainHelper.GetInitGroup (heroIds));
      }

    }

    /// <summary>
    /// 挑战这个关卡
    /// </summary>
    /// <param name="levelId">关卡ID</param>
    /// <param name="group">我方小组</param>
    public static void ChallengeLevel (int levelId, Group group)
    {
      PlaceController.Place = Place.level;
      LevelContext.isPlayback = false;

      // 确保没有在挑战
      if (!LevelContext.InLevel) {

        // 挑战中
        LevelContext.InLevel = true;

        // 设置当前关卡
        if (Config.levelTable.ContainsKey (levelId)) {
          // 显示UI
          if (!levelUIRoot.activeSelf) {
            levelUIRoot.SetActive (true);
          }
          // 设置各个面板的显示和隐藏
          if (!levelControllPanel.gameObject.activeSelf) {
            levelControllPanel.gameObject.SetActive (true);
          }
          if (!levelResourcePanel.gameObject.activeSelf) {
            levelResourcePanel.gameObject.SetActive (true);
          }
          if (battleResultPanel.gameObject.activeSelf) {
            battleResultPanel.gameObject.SetActive (false);
          }
          // 克隆一份场景数据
          Level lvl = Config.levelTable [levelId].DeepCopy ();
          // 是否打开敌人的大招特写
          if (lvl.defenseBigMoveCloseUp) {
            lvl.EnableDefenseBigMoveCloseUp ();
          } else {
            lvl.DisableDefenseBigMoveCloseUp ();
          }
          // 设置为当前关卡
          LevelContext.CurrentLevel = lvl;
          LevelContext.CurrentSubLevel = lvl.subLevels [0];

          // 确保玩家队伍的大招特写都打开
          group.EnableBigMoveCloseUp ();
          // 设置为当前战斗队伍
          LevelContext.attackGroup = group;
          // 备份团队数据，方便重新挑战关卡使用
          LevelContext.attackGroupBackup = group.DeepCopy ();

          // 加载目标子关卡资源
          LoadCurrentSubLevelRes ();


        } else {
          Debug.Log ("Level not found by id " + levelId);
        }

      }
    }

    /// <summary>
    /// 重新挑战这个关卡
    /// </summary>
    private static void ReChallengeLevel ()
    {

      // 确保在关卡里面
      if (LevelContext.InLevel) {

        // 离开关卡
        LeftLevel ();

        int levelId = LevelContext.CurrentLevel.id;

        // 设置当前关卡
        if (Config.levelTable.ContainsKey (levelId)) {

          // 显示UI
          if (!levelUIRoot.activeSelf) {
            levelUIRoot.SetActive (true);
          }
          // 设置各个面板的显示和隐藏
          if (!levelControllPanel.gameObject.activeSelf) {
            levelControllPanel.gameObject.SetActive (true);
          }
          if (!levelResourcePanel.gameObject.activeSelf) {
            levelResourcePanel.gameObject.SetActive (true);
          }
          if (battleResultPanel.gameObject.activeSelf) {
            battleResultPanel.gameObject.SetActive (false);
          }

          // 克隆一份场景数据
          LevelContext.CurrentLevel = Config.levelTable [levelId].DeepCopy ();
          // 克隆一份团队数据
          LevelContext.attackGroup = LevelContext.attackGroupBackup.DeepCopy ();

          // 加载目标子关卡资源
          LoadCurrentSubLevelRes ();

        }
      }
    }

    /// <summary>
    /// 挑战下一个子关卡
    /// </summary>
    public static void ChallengeNextSubLevel ()
    {
      if (LevelContext.AliveDefenseGobjs.Count == 0) {
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

      // 发出离开关卡通知
      LevelContext.InLevel = false;
      LevelContext.Challenging = false;

      // 取消 HeroOpPanel 对 英雄数据变化的监听
      UnsetHeroOpPanel ();

      // 是否暂停状态
      if (LevelContext.isPause) {
        LevelContext.isPause = false;
        // 暂停面板
        levelPausePanel.gameObject.SetActive (false);
      }
      // 隐藏UI
      if (levelUIRoot.activeSelf) {
        levelUIRoot.SetActive (false);
      }

      if (RaiseLeftLevel != null) {
        RaiseLeftLevel (null, EventArgs.Empty);
      }

    }

    /// <summary>
    /// 暂停当前关卡
    /// </summary>
    public static void Pause ()
    {
      //Debug.Log ("Pause");
      if (!LevelContext.isPause) {
        LevelContext.isPause = true;

        if (RaisePause != null) {
          RaisePause (null, EventArgs.Empty);
        }
        // 显示暂停面板
        levelPausePanel.gameObject.SetActive (true);
      }
    }

    /// <summary>
    /// 恢复当前关卡
    /// </summary>
    public static void Resume ()
    {
      if (LevelContext.isPause) {
        LevelContext.isPause = false;
        if (RaiseResume != null) {
          RaiseResume (null, EventArgs.Empty);
        }
      }
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

      //Debug.Log ("OnSubLevelMapLoaded");

      if (args.Name == LevelContext.CurrentSubLevel.resName) {

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
      // 修正作用器资源需求表
      else if (requiredEffectorGobjTable.ContainsKey (name)) {
        requiredEffectorGobjTable [name] = requiredEffectorGobjTable [name] - 1;
      }


      // 检查所有需求是否完成
      bool loadedCompleted = true;
      foreach (KeyValuePair<string, int> kvp in requiredHeroGobjTable) {
        if (kvp.Value > 0) {
          loadedCompleted = false;
          break;
        }
      }

      foreach (KeyValuePair<string, int> kvp in requiredEffectorGobjTable) {
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

            // 战斗结束
            if (RaiseBattleOver != null) {
              RaiseBattleOver (null, EventArgs.Empty);
            }

            // 显示挑战失败面板
            lazyShowFailure = true;
          }

        } else {

          // 是敌方英雄
          // 如果敌方英雄全部死亡，
          //   如果最后的子关卡，则发出关卡已被清除通知 ，否则发出子关卡已被清除通知
          //   
          if (LevelContext.AliveDefenseGobjs.Count == 0) {


            if (LevelContext.CurrentSubLevel.index == LevelContext.CurrentLevel.subLevels.Count - 1) {

              // 如果是最后一关 ---

              // 发出关卡挑战成功
              Debug.Log ("challenge success");

              // 英雄们庆祝胜利
              CelebrateVictory ();

              // 显示挑战成功面板
              lazyShowSuccess = true;

            } else {

              // 子关卡完成
              if (RaiseSubLevelCleaned != null) {
                RaiseSubLevelCleaned (null, EventArgs.Empty);
              }

              levelHeroPanel.SwitchMpEffect (false);

              // 显示下一子关卡按钮
              if (!levelNextPanel.gameObject.activeSelf)
                levelNextPanel.gameObject.SetActive (true);
            }

            // 战斗结束
            if (RaiseBattleOver != null) {
              RaiseBattleOver (null, EventArgs.Empty);
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
    private static void LoadCurrentSubLevelRes ()
    {

      //Debug.Log ("LoadTargetSubLevelRes");

      SubLevel subLevel = LevelContext.CurrentSubLevel;
      //Debug.Log ("subLevel.resName " + subLevel.resName);
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
      //Debug.Log ("LoadSubLevelHeroResources");

      // -- 加载场景中的其他资源 --

      // -- 加载英雄 --
      // 统计需要加载的英雄对象数量
      requiredHeroGobjTable.Clear ();

      // 临时英雄表
      Dictionary<string, int> tmpHeroTable = new Dictionary<string, int> ();
      // -- 统计敌方英雄资源 --
      if (LevelContext.CurrentSubLevel.defenseGroup != null) {
        foreach (Hero hero in LevelContext.CurrentSubLevel.defenseGroup.heros) {
          if (tmpHeroTable.ContainsKey (hero.resName)) {
            int count = tmpHeroTable [hero.resName] + 1;
            tmpHeroTable [hero.resName] = count;
          } else
            tmpHeroTable.Add (hero.resName, 1);
        }
      }

      // -- 统计我方英雄资源 --
      if (LevelContext.attackGroup != null) {
        foreach (Hero hero in LevelContext.attackGroup.heros) {
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
        //Debug.Log ("Has " + has + " " + kvp.Key + " , Need " + need + " " + kvp.Key);
      }

      // 加载作用器
      LoadSubLevelEffectorResources ();

    }

    /// <summary>
    /// 计算加载子关卡所需要的作用器
    /// </summary>
    private static void LoadSubLevelEffectorResources ()
    {
      //Debug.Log ("LoadSubLevelEffectorResources");

      // -- 加载场景中的其他资源 --

      // -- 加载作用器 --
      // 统计需要加载的作用器对象数量
      requiredEffectorGobjTable.Clear ();

      // 临时作用器表
      Dictionary<string, int> tmpEffectorTable = new Dictionary<string, int> ();
      // -- 统计敌方作用器资源 --
      if (LevelContext.CurrentSubLevel.defenseGroup != null) {
        foreach (Hero hero in LevelContext.CurrentSubLevel.defenseGroup.heros) {
          foreach (Skill skill in hero.skills.Values) {
            if (skill.enable && skill.effectors != null) {
              foreach (Effector effector in skill.effectors) {
                AddEffector (effector, tmpEffectorTable);
              }
            }
          }
        }
      }

      // -- 统计我方作用器资源 --
      if (LevelContext.attackGroup != null) {
        foreach (Hero hero in LevelContext.attackGroup.heros) {
          foreach (Skill skill in hero.skills.Values) {
            if (skill.enable && skill.effectors != null) {
              foreach (Effector effector in skill.effectors) {
                AddEffector (effector, tmpEffectorTable);
              }
            }
          }
        }
      }

      // 加载所需要的作用器资源
      foreach (KeyValuePair<string, int> kvp in tmpEffectorTable) {
        // 已有的作用器数量
        int has = EffectorGobjManager.Size (kvp.Key);
        int need = 0;
        if (has < kvp.Value) {
          need = kvp.Value - has;
          requiredEffectorGobjTable [kvp.Key] = need;
          EffectorGobjManager.LazyLoad (kvp.Key, need);
        }
        Debug.Log ("Has " + has + " " + kvp.Key + " , Need " + need + " " + kvp.Key);
      }

      // 所需作用器资源已经存在
      if (requiredHeroGobjTable.Count == 0 && requiredEffectorGobjTable.Count == 0) {
        // 通知所有的资源已经准备完毕
        AllSubLevelResourceReady ();
      }
    }

    /// <summary>
    /// 子关卡需要的资源已准备好
    /// </summary>
    private static void AllSubLevelResourceReady ()
    {

      //Debug.Log ("AllSubLevelResourceReady");


      if (LevelContext.CurrentSubLevel.index == 0) {

        // 首次进入子关卡 ----

        // 设置英雄操作面板
        SetupHeroOpPanel ();

        // 发出关卡进入成功通知
        if (RaiseEnterLevelSuccess != null)
          RaiseEnterLevelSuccess (null, EventArgs.Empty);

        // 进入子关卡
        EnterSubLevel ();

      } else { // 已经在关卡里面

        // 记录
        // TODO 用于优化子关卡加载

        // 进入子关卡
        EnterSubLevel ();
      }
    }

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    private static void EnterSubLevel ()
    {

      // 确保清场
      LevelContext.heroGobjs.Clear ();
      LevelContext.defenseGobjs.Clear ();
      LevelContext.attackGobjs.Clear ();

      // 创建背景
      GameObject bgGobj = GobjManager.FetchUnused (LevelContext.CurrentSubLevel.resName);
      if (bgGobj != null) {
        bgGobj.SetActive (true);
        //Debug.Log ("Background Created.");
        LevelContext.background = bgGobj;
      }

      // 整理双方战队 - 去掉死亡的队员
      LevelContext.attackGroup.Arrange ();
      LevelContext.CurrentSubLevel.defenseGroup.Arrange ();

      // 敌方小组列阵
      Group defenseGroup = LevelContext.CurrentSubLevel.defenseGroup;
      Embattle (defenseGroup, BattleDirection.LEFT);
      // 我方小组列阵
      Embattle (LevelContext.attackGroup, BattleDirection.RIGHT);

      // 我方进场
      foreach (Hero hero in LevelContext.attackGroup.aliveHeros) {
        if (hero.hp > 0) {
          GameObject g = AddHeroToScene (hero);
          BigMoveBhvr bmBhvr = g.GetComponent<BigMoveBhvr> ();
          // 自动施放大招
          bmBhvr.auto = LevelContext.CurrentLevel.autoFight;
          // 加入我方队伍中
          LevelContext.attackGobjs.Add (g);
          // 保存到当前子关卡开场时的英雄列表中
          LevelContext.SubLevelBeganGobjs.Add (g);
          // 保存到场景所有英雄的表中
          LevelContext.heroGobjs.Add (hero.id, g);
        }
      }

      // 敌方进场
      foreach (Hero hero in defenseGroup.aliveHeros) {
        GameObject g = AddHeroToScene (hero);
        BigMoveBhvr bmBhvr = g.GetComponent<BigMoveBhvr> ();
        // 自动施放大招
        bmBhvr.auto = true;
        LevelContext.defenseGobjs.Add (g);
        // 保存到场景所有英雄的表中
        LevelContext.heroGobjs.Add (hero.id, g);
      }

      // 监听场景中的英雄
      // 己方英雄
      ListenSelftHeros ();
      // 敌方英雄
      ListendefenseHeros ();

      // UI 控制 --

      // 英雄头像MP效果打开
      // levelHeroPanel.SwitchMpEffect (true);

      // 隐藏下一个小关的按钮
      if (levelNextPanel.gameObject.activeSelf) {
        levelNextPanel.gameObject.SetActive (false);
      }

      // 发出进入子关卡通知
      if (RaiseEnterSubLevel != null) {
        RaiseEnterSubLevel (null, EventArgs.Empty);
      }

      // 战斗开始
      if (RaiseBattleStart != null) {
        RaiseBattleStart (null, EventArgs.Empty);
      }

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
      UnlistendefenseHeros ();

      // 释放己方英雄
      foreach (GameObject gobj in LevelContext.attackGobjs) {
        HeroGobjManager.Release (gobj);
      }

      // 释放敌方英雄
      foreach (GameObject gobj in LevelContext.defenseGobjs) {
        HeroGobjManager.Release (gobj, false);
      }

      // 确保清场
      LevelContext.defenseGobjs.Clear ();
      LevelContext.attackGobjs.Clear ();
      LevelContext.SubLevelBeganGobjs.Clear ();

      // 释放其他(背景，特效)资源
      GobjManager.ReleaseAll (false);

      // 发出离开子关卡通知
      if (RaiseLeftSubLevel != null) {
        RaiseLeftSubLevel (null, EventArgs.Empty);
      }

    }

    /// <summary>
    /// Listens the attackt heros.
    /// </summary>
    private static void ListenSelftHeros ()
    {
      // 得到进子关卡时的英雄数据
      List<Hero>.Enumerator heroEnum = LevelContext.attackGroup.aliveHeros.GetEnumerator ();
      // 得到进子关卡时的英雄游戏对象
      List<GameObject>.Enumerator gobjEnum = LevelContext.SubLevelBeganGobjs.GetEnumerator ();

      int i = 0;
      while (heroEnum.MoveNext () && gobjEnum.MoveNext ()) {

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

      // 监听玩家英雄的大招是否能施放
      foreach (TG.LevelHeroItem item in levelHeroPanel.itemList) {
        BigMoveBhvr bmBhvr = LevelContext.GetHeroComponent<BigMoveBhvr> (item.heroId);
        if (bmBhvr != null) {
          // MP 效果开关
          bmBhvr.RaiseEvent += item.SwitchMpEffect;
        }
      }
    }

    /// <summary>
    /// 取消对己方英雄的监控
    /// </summary>
    private static void UnlistenSelfHeros ()
    {
      // 得到进子关卡时的英雄数据
      List<Hero>.Enumerator heroEnum = LevelContext.attackGroup.aliveHeros.GetEnumerator ();
      // 得到进子关卡时的英雄游戏对象
      List<GameObject>.Enumerator gobjEnum = LevelContext.SubLevelBeganGobjs.GetEnumerator ();

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

      // 取消监听玩家英雄的大招是否能施放
      foreach (TG.LevelHeroItem item in levelHeroPanel.itemList) {
        BigMoveBhvr bmBhvr = LevelContext.GetHeroComponent<BigMoveBhvr> (item.heroId);
        if (bmBhvr != null) {
          bmBhvr.RaiseEvent -= item.SwitchMpEffect;
        }
      }
 
    }

    /// <summary>
    /// Listens the defense heros.
    /// </summary>
    private static void ListendefenseHeros ()
    {

      List<Hero>.Enumerator heroEnum = LevelContext.CurrentSubLevel.defenseGroup.aliveHeros.GetEnumerator ();
      List<GameObject>.Enumerator gobjEnum = LevelContext.defenseGobjs.GetEnumerator ();

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

    private static void UnlistendefenseHeros ()
    {

      List<Hero>.Enumerator heroEnum = LevelContext.CurrentSubLevel.defenseGroup.aliveHeros.GetEnumerator ();
      List<GameObject>.Enumerator gobjEnum = LevelContext.defenseGobjs.GetEnumerator ();

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
        GroupBhvr gbhvr = gobj.GetComponent<GroupBhvr> ();
        if (gbhvr != null) {
          gbhvr.Status = GroupStatus.battle;
        }
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

      // 排序
      heros.Sort (delegate(Hero hero1, Hero hero2) {
        return hero1.sort.CompareTo (hero2.sort);
      });

      // 分成两列
      List<Hero> column1 = new List<Hero> ();
      List<Hero> column2 = new List<Hero> ();
      Vector2 origin = Vector2.zero;
      int stepx = 16; // 排与排之间的距离

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
        origin = new Vector2 (0, 20);
        // 对于第一列
        for (int i = 0; i < column1.Count; i++) {
          // 如果当前英雄与前面英雄的sort相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column1 [i].sort == column1 [j].sort) {
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
        origin = new Vector2 (-8, 16);
        offsety = 0;
        for (int i = 0; i < column2.Count; i++) {
          // 如果当前英雄与前面英雄的sort相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column2 [i].sort == column2 [j].sort) {
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
        origin = new Vector2 (64, 20);
        // 对于第一列
        for (int i = 0; i < column1.Count; i++) {
          // 如果当前英雄与前面英雄的sort相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column1 [i].sort == column1 [j].sort) {
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
        origin = new Vector2 (72, 16);
        for (int i = 0; i < column2.Count; i++) {
          // 如果当前英雄与前面英雄的sort相等，则 offsety++;
          bool useOffset = false;
          for (int j = i - 1; j >= 0; j--) {
            if (column2 [i].sort == column2 [j].sort) {
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

      if (LevelContext.NextSubLevel != null) {

        // 离开子关卡
        LeftSubLevel ();

        LevelContext.CurrentSubLevel = LevelContext.NextSubLevel;

        // 加载目标子关卡资源
        LoadCurrentSubLevelRes ();
      }
    }

    /// <summary>
    /// 英雄们庆祝胜利
    /// </summary>
    private static void CelebrateVictory ()
    {

      foreach (GameObject g in LevelContext.attackGobjs) {

        HeroBhvr heroBhvr = g.GetComponent<HeroBhvr> ();
        if (heroBhvr == null) {
          continue;
        }

        heroBhvr.CelebrateVictory ();
      }
    }

    /// <summary>
    /// 设置英雄操作面板
    /// </summary>
    private static void SetupHeroOpPanel ()
    {
      if (levelHeroPanel != null) {

        // 创建英雄UI操作面板
        Hero[] heros = LevelContext.attackGroup.heros;
        TG.LevelHeroPanelData data = new TG.LevelHeroPanelData ();
        data.heroCount = LevelContext.attackGroup.heros.Length;
        levelHeroPanel.param = data;
        List<TG.LevelHeroItem>.Enumerator wgtEnum = levelHeroPanel.itemList.GetEnumerator ();
        int i = 0;
        while (wgtEnum.MoveNext ()) {
          Hero h = heros [i];
          TG.LevelHeroItem w = wgtEnum.Current;
          // 停止 MP 特效, 改为由 BigMoveBhvr 来控制
          w.SwitchMpEffect (false);
          // 英雄头像 ----
          w.SetHeroId (h.id); // 英雄ID
          h.raiseHpChange += w.SetHp; // 英雄HP变化
          h.raiseMpChange += w.SetMp; // 英雄MP变化
          w.onClick += OnHeroIconClick; // 英雄头像点击
          i++;

        }

        levelHeroPanel.gameObject.SetActive (true);
      }
    }

    /// <summary>
    /// 取消英雄配置面板
    /// </summary>
    private static void UnsetHeroOpPanel ()
    {

      if (levelHeroPanel != null) {

        // 创建英雄UI操作面板
        Hero[] heros = LevelContext.attackGroup.heros;
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

        levelHeroPanel.gameObject.SetActive (false);
      }
    }

    /// <summary>
    /// 创建一个 LevelController 游戏对象
    /// </summary>
    /// <returns>The gobj.</returns>
    private static GameObject NewGobj ()
    {

      GameObject gobj = null;
      UnityEngine.Object asset = Resources.Load ("Prefabs/" + GOBJ_NAME);
      if (asset != null) {
        gobj = Instantiate (asset) as GameObject;
      }
      return gobj;
    }

    /// <summary>
    /// 创建一个 LevelUIRoot 游戏对象
    /// </summary>
    /// <returns>The user interface root.</returns>
    private static GameObject NewUIRoot ()
    {
      GameObject gobj = null;
      UnityEngine.Object asset = Resources.Load ("Prefabs/" + UI_ROOT_NAME);
      if (asset != null) {
        gobj = Instantiate (asset) as GameObject;
      }
      return gobj;
      
    }

    private static IEnumerator ShowSuccess ()
    {

      yield return new WaitForSeconds (3);

      if (RaiseChallengeSuccess != null) {
        RaiseChallengeSuccess (null, EventArgs.Empty);
      }

      // 释放己方英雄
      foreach (GameObject gobj in LevelContext.attackGobjs) {
        HeroGobjManager.Release (gobj);
      }

      // 显示战斗结果面板，隐藏其他面板
      levelControllPanel.gameObject.SetActive (false);
      levelResourcePanel.gameObject.SetActive (false);
      levelHeroPanel.gameObject.SetActive (false);
      battleResultPanel.gameObject.SetActive (true);

      ArrayList heroIds = new ArrayList ();
      foreach (Hero hero in LevelContext.attackGroup.heros) {
        heroIds.Add (hero.id);
      }
      int resultType = UnityEngine.Random.Range (2, 6); // 超时战斗结果也会返回失败
      battleResultPanel.param = TangGame.UI.TestDataStore.RandomBattleResult (heroIds, resultType);

    }

    private static IEnumerator ShowFailure ()
    {
      yield return new WaitForSeconds (3);

      // 释放敌方英雄
      foreach (GameObject gobj in LevelContext.defenseGobjs) {
        HeroGobjManager.Release (gobj, false);
      }

      if (RaiseChangengeFailure != null) {
        RaiseChangengeFailure (null, EventArgs.Empty);
      }

      // 显示战斗结果面板，隐藏其他面板
      levelControllPanel.gameObject.SetActive (false);
      levelResourcePanel.gameObject.SetActive (false);
      levelHeroPanel.gameObject.SetActive (false);
      battleResultPanel.gameObject.SetActive (true);
      ArrayList heroIds = new ArrayList ();
      foreach (Hero hero in LevelContext.attackGroup.heros) {
        heroIds.Add (hero.id);
      }
      battleResultPanel.param = TangGame.UI.TestDataStore.RandomBattleResult (heroIds, 0);
    }

    private static void AddEffector (Effector effector, Dictionary<string, int> counterTable)
    {

      if (effector.specialName != null && effector.specialName.StartsWith (Config.DBFX_PREFIX)) {

        if (counterTable.ContainsKey (effector.specialName)) {
          int count = counterTable [effector.specialName] + 1;
          counterTable [effector.specialName] = count;
        } else {
          counterTable.Add (effector.specialName, 1);
        }

        if (effector.subEffectors != null && effector.subEffectors.Length > 0) {
          foreach (Effector sub in effector.subEffectors) {
            AddEffector (sub, counterTable);
          }
        }
      }
    }

    #endregion
  }
}