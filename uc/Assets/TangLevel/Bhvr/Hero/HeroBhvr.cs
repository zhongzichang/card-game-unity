﻿using System;
using UnityEngine;
using TDB = TangDragonBones;
using DragonBones;
using DBE = DragonBones.Events;
using System.Collections.Generic;

namespace TangLevel
{
  /// <summary>
  /// 英雄的基本行为
  /// </summary>
  [RequireComponent (typeof(DirectedNavigable), typeof(HeroStatusBhvr))]
  public class HeroBhvr : MonoBehaviour
  {
    public event EventHandler RaiseDead;

    public Hero hero;
    private DirectedNavigable navigable;
    private DirectedNavAgent agent;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private TDB.DragonBonesBhvr dbBhvr;
    private Armature armature;
    private SkillBhvr skillBhvr;
    private Skill skill;
    private GameObject target;
    private bool isPlay = true;
    private List<string> animationList;

    #region MonoBehaviours

    void Start ()
    {
      // nav agent
      agent = GetComponent<DirectedNavAgent> ();
      if (agent == null) {
        agent = gameObject.AddComponent<DirectedNavAgent> ();
      }
      // navigable
      navigable = GetComponent<DirectedNavigable> ();
      if (navigable == null) {
        navigable = gameObject.AddComponent<DirectedNavigable> ();
      }
      // transform
      myTransform = transform;
      // skill behaviour
      skillBhvr = GetComponent<SkillBhvr> ();

    }

    void OnEnable ()
    { 
      // status behaviour
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
        if (statusBhvr == null) {
          statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
        }
        statusBhvr.statusChangedHandler += OnStatusChanged;
      }
      // 重新打开，状态设置为空闲
      statusBhvr.Status = HeroStatus.idle;

      // DragonBonesBhvr
      if (dbBhvr == null) {
        // dragonbones behaviour
        dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
        dbBhvr.GotoAndPlay (statusBhvr.Status.ToString ());
        armature = dbBhvr.armature;
      }

      // armature
      if (armature != null) {
        armature.AddEventListener (DBE.AnimationEvent.MOVEMENT_CHANGE, OnMovementChange);
        armature.AddEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
        armature.AddEventListener (DBE.AnimationEvent.COMPLETE, OnAnimationComplete);
        animationList = armature.Animation.AnimationList;
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;
      if (!isPlay) {
        Resume ();
      }

    }

    void OnDisable ()
    {

      if (armature != null) {
        armature.RemoveEventListener (DBE.AnimationEvent.MOVEMENT_CHANGE, OnMovementChange);
        armature.RemoveEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
        armature.RemoveEventListener (DBE.AnimationEvent.COMPLETE, OnAnimationComplete);
      }

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;
      if (isPlay) {
        Pause ();
      }
    }

    #endregion

    #region DragonBones Events

    private void OnMovementChange (Com.Viperstudio.Events.Event e)
    {

      string movementId = armature.Animation.MovementID;


    }
    //private void
    private void OnAnimationLoopComplete (Com.Viperstudio.Events.Event e)
    {
      string movementId = armature.Animation.MovementID;

      //
      switch (statusBhvr.Status) {

      case HeroStatus.beat:
        statusBhvr.Status = HeroStatus.idle;
        break;
      case HeroStatus.charge:
        statusBhvr.Status = HeroStatus.release;
        break;
      }

    }

    private void OnAnimationComplete (Com.Viperstudio.Events.Event e)
    {

      switch (statusBhvr.Status) {
      case HeroStatus.release:
        statusBhvr.Status = HeroStatus.idle;
        break;
      }
    }

    #endregion

    #region Tang Callback

    /// <summary>
    /// 状态回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusChanged (HeroStatus status)
    {

      switch (status) {


      case HeroStatus.charge: // 起手 ----

        // 有则播放，无则转到释放状态
        if (skill.chargeClip != null) {
          if (animationList.Contains (skill.chargeClip)) {
            dbBhvr.GotoAndPlay (skill.chargeClip);
          } else {
            statusBhvr.Status = HeroStatus.release;
          }
        }
        break;
      
      case HeroStatus.release: // 释放 ----

        string clip = null;
        if (skill.releaseClip != null && animationList.Contains (skill.releaseClip)) {
          clip = skill.releaseClip;
        } else {
          clip = Config.DEFAULT_ATTACK_CLIP;
        }
        //dbBhvr.GotoAndPlay (clip);
        armature.Animation.GotoAndPlay (clip, -1, -1, 1);

        // 抛出作用器
        if (skill != null) {
          skillBhvr.Cast (skill.effector, skill, gameObject, target);
        }
        break;

      case HeroStatus.dead: // 死亡 ----

        armature.Animation.GotoAndPlay (status.ToString (), -1, -1, 1);
        FadeOut ();
        break;

      default: // 其他 ----

        dbBhvr.GotoAndPlay (status.ToString ());
        break;
      }
    }

    #endregion

    #region LevelController Events

    /// <summary>
    /// 战斗暂停
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnPause (object sender, EventArgs args)
    {
      Pause ();
    }

    /// <summary>
    /// 战斗恢复
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnResume (object sender, EventArgs args)
    {
      Resume ();
    }

    #endregion

    #region Private Methods

    private void FadeOut ()
    {

      // 死亡动画播放完毕
      // 淡出
      FadeOut fadeout = GetComponent<FadeOut> ();
      if (fadeout == null) {
        fadeout = gameObject.AddComponent<FadeOut> ();
      }
      fadeout.Play ();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 攻击指定目标
    /// </summary>
    /// <param name="target">Target.</param>
    public void Attack (GameObject target, Skill skill)
    {

      this.skill = skill;
      this.target = target;

      statusBhvr.Status = HeroStatus.charge;

    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Die ()
    {
      if (statusBhvr.Status == HeroStatus.running) {
        agent.ResetPath ();
      }
      statusBhvr.Status = HeroStatus.dead;
      if (RaiseDead != null) {
        RaiseDead (this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// 被击打
    /// </summary>
    public void Beat ()
    {
      // 下面的行为会被打断
      switch(statusBhvr.Status){

      case HeroStatus.idle:
      case HeroStatus.charge:
      case HeroStatus.release:
        statusBhvr.Status = HeroStatus.beat;
        break;

      case HeroStatus.running:
        agent.ResetPath ();
        statusBhvr.Status = HeroStatus.beat;
        break;
      }
    }

    public void CelebrateVictory ()
    {

      statusBhvr.Status = HeroStatus.victory;

    }

    public void Pause ()
    {

      isPlay = false;

      // 暂停动画
      if (dbBhvr != null) {
        dbBhvr.Pause ();
      }
      // 暂停行走
      if (agent.enabled)
        agent.enabled = false;
    }

    public void Resume ()
    {

      isPlay = true;

      // 恢复动画
      if (dbBhvr != null) {
        dbBhvr.Resume ();
      }
      // 恢复行走
      if (!agent.enabled)
        agent.enabled = true;
    }

    /// <summary>
    /// 找距离最近的目标
    /// </summary>
    /// <returns>The closest target.</returns>
    public GameObject FindClosestTarget ()
    {
      return LevelController.FindClosestTarget (this);
    }

    #endregion
  }
}

