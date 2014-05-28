﻿using System;
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
    // 大招时的位置偏移，需要往镜头靠近
    public const float SCALE = 1.3F;
    public static readonly Color COLOR_MASH = new Color (0.5F, 0.5F, 0.5F, 1F);
    private HeroStatusBhvr statusBhvr;
    private TDB.DragonBonesBhvr dbBhvr;
    private HashSet<BigMoveBhvr> bigMoveSenders = new HashSet<BigMoveBhvr> ();
    // 接收到的所有大招发送者
    private bool inited = false;
    // 该组件是否已初始化
    private Transform myTransform;
    // Transform
    private Vector3 backupPos = Vector3.zero;
    // 备份的位置
    private DirectedNavAgent agent;
    // 导航代理
    private HeroBhvr heroBhvr;
    // heroBhvr
    private Vector3 backupScale = Vector3.zero;
    // 大招前的人物比例
    private Armature armature;
    // Dragonbones armature
    private Skill skill;
    // 大招对应的技能
    private bool ready = false;
    // 大招是否准备好
    private MaterialBhvr matBhvr;

    #region MonoMethods

    void Start ()
    {
      myTransform = transform;
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
      }
      agent = GetComponent<DirectedNavAgent> ();
      heroBhvr = GetComponent<HeroBhvr> ();

      // 原始人物比例
      backupScale = myTransform.localScale;

      // 获取大招技能
      skill = heroBhvr.BigMoveSkill ();

      matBhvr = GetComponent<MaterialBhvr> ();

    }

    void Update ()
    {
      if (IsBigMoveReady ()) {
        // 可以施放大招，点亮大招按钮
        if (!ready) {
          ready = true;
          if (RaiseEvent != null) {
            RaiseEvent (ready);
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
      if (!inited) {
        if (statusBhvr == null) {
          statusBhvr = GetComponent<HeroStatusBhvr> ();
        }

        // DragonBonesBhvr
        if (dbBhvr == null) {
          // dragonbones behaviour
          dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
          dbBhvr.GotoAndPlay (statusBhvr.Status.ToString ());
          armature = dbBhvr.armature;
        }

        inited = true;
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

      backupPos = myTransform.localPosition;
      myTransform.localPosition += OFFSET;

      backupScale = myTransform.localScale;
      myTransform.localScale = new Vector3 (backupScale.x * SCALE, backupScale.y * SCALE, 1);

      statusBhvr.IsBigMove = true;
      LevelController.BigMoveCounter++;

      armature.Animation.TimeScale = 0.5F;

    }

    /// <summary>
    /// 大招施放完毕
    /// </summary>
    public void StopBigMove ()
    {
      myTransform.localPosition = backupPos;

      statusBhvr.IsBigMove = false;

      LevelController.BigMoveCounter--;

      heroBhvr.hero.mp = 0;
      agent.enabled = true;

      // 人物比例还原
      myTransform.localScale = backupScale;

      armature.Animation.TimeScale = 1F;

    }

    /// <summary>
    /// 大招锁定
    /// </summary>
    public void Lock(){

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
      if (statusBhvr.IsPause) {
        statusBhvr.IsPause = false;
        matBhvr.RestoreColor ();
      }
    }

    #endregion

    #region PrivateMethods

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
              target.GetComponent<HeroBhvr> ().hero.hp > 0 &&
              Mathf.Abs (myTransform.localPosition.x - target.transform.localPosition.x)
              <= skill.distance) {
            return true;
          }
          break;
        case Skill.TARGET_SELF_WEAKEST:
          // 己方最虚弱者
          List<GameObject> targetGroup = heroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
            LevelContext.AliveSelfGobjs : LevelContext.AliveEnemyGobjs;
          target = HeroSelector.FindWeakest (targetGroup);
          if (target != null &&
              Mathf.Abs (myTransform.localPosition.x - target.transform.localPosition.x)
              <= skill.distance) {
            return true;
          }
          break;
        case Skill.TARGET_ENEMY_WEAKEST:
          // 敌方最虚弱者
          targetGroup = heroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
            LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
          target = HeroSelector.FindWeakest (targetGroup);
          if (target != null &&
              Mathf.Abs (myTransform.localPosition.x - target.transform.localPosition.x)
              <= skill.distance) {
            return true;
          }
          break;
        case Skill.TARGET_REGION:
          // 指定区域
          Rect region = skill.region;
          Vector2 center = new Vector2 (region.x - region.width / 2, region.y - region.height / 2);
          if (Mathf.Abs (myTransform.localPosition.x - center.x)
             <= skill.distance) {
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

