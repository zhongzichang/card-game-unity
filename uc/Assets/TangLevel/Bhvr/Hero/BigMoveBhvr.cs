using System;
using UnityEngine;
using System.Collections.Generic;
using DBE = DragonBones.Events;
using TDB = TangDragonBones;
using DragonBones;

namespace TangLevel
{
  [RequireComponent (typeof(MaterialBhvr))]
  public class BigMoveBhvr : MonoBehaviour
  {
    // 大招准备
    public delegate void BigMoveHandler (bool ready);
    // 大招不可行
    public BigMoveHandler RaiseEvent;
    public static readonly Vector3 OFFSET = new Vector3 (0F, 0F, 0F);
    // 大招时的大小比例
    public const float SCALE = 1.3F;
    // 放大招时的时间比例
    public const float TIME_SCALE = 1F;
    // 其他人放大招，自己被定住时的材质颜色
    public static readonly Color COLOR_MASH = new Color (0.5F, 0.5F, 0.5F, 1F);
    // 自动施放大招
    public bool auto = false;
    // 接收到的所有大招发送者
    private HashSet<BigMoveBhvr> bigMoveSenders = new HashSet<BigMoveBhvr> ();
    // 该组件是否已初始化
    private bool inited = false;
    // Transform
    private Transform myTransform;
    // 备份的位置
    private Vector3 backupPos = Vector3.zero;
    // 导航代理
    private DirectedNavAgent agent;
    // heroBhvr
    private HeroBhvr heroBhvr;
    // 大招前的人物比例
    private Vector3 backupScale = Vector3.zero;
    // Dragonbones armature
    private Armature armature;
    // 大招对应的技能
    private Skill skill;
    // 大招是否准备好
    private bool ready = false;
    // 其他组件
    private HeroStatusBhvr statusBhvr;
    private TDB.DragonBonesBhvr dbBhvr;
    private MaterialBhvr matBhvr;

    #region MonoMethods

    void Update ()
    {
      if (IsBigMoveReady ()) {
        if (auto) {
          // 自动，能量满了自动施放大招
          heroBhvr.BigMove ();
        } else {
          // 手动，点击英雄头像施放大招
          // 可以施放大招，点亮大招按钮
          if (!ready) {
            ready = true;
            if (RaiseEvent != null) {
              RaiseEvent (ready);
            }
          }
        }
      } else {
        // 熄灭大招按钮
        if (ready) {
          ready = false;
          if (RaiseEvent != null) {
            RaiseEvent (ready);
          }
        }
      }
    }

    void OnEnable ()
    {
      // init ---
      if (myTransform == null) {
        myTransform = transform;
      }
      statusBhvr = GetComponent<HeroStatusBhvr> ();
      if (statusBhvr == null) {
        statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
      }
      agent = GetComponent<DirectedNavAgent> ();
      if (agent == null) {
        agent = gameObject.AddComponent<DirectedNavAgent> ();
      }

      heroBhvr = GetComponent<HeroBhvr> ();
      if (heroBhvr == null) {
        heroBhvr = gameObject.AddComponent<HeroBhvr> ();
      }
      matBhvr = GetComponent<MaterialBhvr> ();
      if (matBhvr == null) {
        matBhvr = gameObject.AddComponent<MaterialBhvr> ();
      }
      dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
      if (dbBhvr == null) {
        dbBhvr = gameObject.AddComponent<TDB.DragonBonesBhvr> ();
      }
      armature = dbBhvr.armature;
      // 原始人物比例
      if (backupScale == Vector3.zero) {
        backupScale = myTransform.localScale;
      }
      // 获取大招技能
      if (skill == null) {
        skill = heroBhvr.BigMoveSkill ();
      }

      LevelController.BigMoveStart += OnBigMoveStart;
      LevelController.BigMoveEnd += OnBigMoveEnd;

      // armature
      if (armature != null) {
        armature.AddEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
      }
    }

    void OnDisable ()
    {

      LevelController.BigMoveStart -= OnBigMoveStart;
      LevelController.BigMoveEnd -= OnBigMoveEnd;

      // armature
      if (armature != null) {
        armature.RemoveEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
      }
    }

    #endregion

    #region SceneEvents

    /// <summary>
    /// 大招开始
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBigMoveStart (object sender, EventArgs args)
    {
      Lock ();
    }

    /// <summary>
    /// 大招开始
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBigMoveEnd (object sender, EventArgs args)
    {
      BreakLock ();
    }

    #endregion

    #region DragonBonesEvents

    private void OnAnimationFrameEvent (Com.Viperstudio.Events.Event e)
    {

      DBE.FrameEvent evt = e as DBE.FrameEvent;
      if (evt != null) {

        // 如果是人物比例还原
        if (Config.SCALE_RESUME_LABEL.Equals (evt.FrameLabel)) {
          // 人物比例还原
          myTransform.localScale = backupScale;
        }
      }
    }

    #endregion

    #region PublicMethods

    /// <summary>
    /// 开始施放大招
    /// </summary>
    public void StartBigMove ()
    {

      BreakLock ();


      statusBhvr.IsBigMove = true;
      agent.enabled = true;

      if (skill.bigMoveCloseUp) {

        // 位移
        backupPos = myTransform.localPosition;
        myTransform.localPosition += OFFSET;
        // 放大
        backupScale = myTransform.localScale;
        myTransform.localScale = new Vector3 (backupScale.x * SCALE, backupScale.y * SCALE, 1);
        // 动画速度变慢
        armature.Animation.TimeScale = TIME_SCALE;
        LevelController.BigMoveCounter++;
      }

    }

    /// <summary>
    /// 大招施放完毕
    /// </summary>
    public void StopBigMove ()
    {
      statusBhvr.IsBigMove = false;
      heroBhvr.hero.mp = 0;
      agent.enabled = true;

      if (skill.bigMoveCloseUp) {
        // 位移还原
        myTransform.localPosition = backupPos;
        // 人物比例还原
        myTransform.localScale = backupScale;
        // 动画速度还原
        armature.Animation.TimeScale = 1F;
        LevelController.BigMoveCounter--;
      }
    }

    /// <summary>
    /// 大招锁定
    /// </summary>
    public void Lock ()
    {

      if (!statusBhvr.IsBigMove) { // 本人没有放大招
        statusBhvr.IsPause = true;
        matBhvr.color = matBhvr.color * COLOR_MASH;
      }
    }

    /// <summary>
    /// 解除大招锁定
    /// </summary>
    public void BreakLock ()
    {
      statusBhvr.IsPause = false;
      matBhvr.RestoreColor ();
    }

    #endregion

    #region PrivateMethods

    /// <summary>
    /// 判断是否能放大招
    /// </summary>
    /// <returns><c>true</c> if this instance is big move ready; otherwise, <c>false</c>.</returns>
    private bool IsBigMoveReady ()
    {

      // 如果能量足够，能不能施放大招
      if (skill != null && heroBhvr.hero.maxMp == heroBhvr.hero.mp) {

        switch (skill.targetType) {

        case Skill.TARGET_SELF:
          // 自己
          return true;
          break;
        case Skill.TARGET_LOCKED:
          // 已锁定的目标
          GameObject target = heroBhvr.target;
          // 目标存在，目标活着，与目标的距离小于技能攻击的距离
          if (target != null &&
              target.GetComponent<HeroBhvr> ().hero.hp > 0
              && Mathf.Abs (myTransform.localPosition.x - target.transform.localPosition.x) <= skill.distance) {
            return true;
          }
          break;
        case Skill.TARGET_SELF_WEAKEST:
          // 己方最虚弱者
          List<GameObject> targetGroup = heroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
            LevelContext.AliveSelfGobjs : LevelContext.AliveEnemyGobjs;
          target = HeroSelector.FindWeakest (targetGroup);
          if (target != null
              && Mathf.Abs (myTransform.localPosition.x - target.transform.localPosition.x) <= skill.distance) {
            return true;
          }
          break;
        case Skill.TARGET_ENEMY_WEAKEST:
          // 敌方最虚弱者
          targetGroup = heroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
            LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
          target = HeroSelector.FindWeakest (targetGroup);
          if (target != null
              && Mathf.Abs (myTransform.localPosition.x - target.transform.localPosition.x) <= skill.distance) {
            return true;
          }
          break;
        case Skill.TARGET_REGION:
          // 指定区域
          Rect region = skill.region;
          Vector2 center = new Vector2 (region.x - region.width / 2, region.y - region.height / 2);
          if (Mathf.Abs (myTransform.localPosition.x - center.x) <= skill.distance) {
            return true;
          }
          break;
        }
      }
      return false;
    }

    #endregion
  }
}

