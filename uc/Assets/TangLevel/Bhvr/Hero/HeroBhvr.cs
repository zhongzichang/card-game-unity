using System;
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
    #region Events

    /// <summary>
    /// 死亡通知
    /// </summary>
    public event EventHandler RaiseDead;

    #endregion

    #region Attributes

    public Hero hero;
    private DirectedNavigable navigable;
    private DirectedNavAgent agent;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private TDB.DragonBonesBhvr dbBhvr;
    private Armature armature;
    private SkillBhvr skillBhvr;
    private BigMoveBhvr bmBhvr;
    private Skill skill;
    private GameObject target;
    private List<string> animationList;

    #endregion

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

    void Update ()
    {
      if (hero.hp == 0 && statusBhvr.Status != HeroStatus.dead) {
        statusBhvr.Status = HeroStatus.dead;
      }
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
        statusBhvr.pauseChangedHandler += OnPauseChanged;
      }

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

      // 大招行为
      if (bmBhvr == null) {
        bmBhvr = GetComponent<BigMoveBhvr> ();
        if (bmBhvr == null) {
          bmBhvr = gameObject.AddComponent<BigMoveBhvr> ();
        }
      }

      if (statusBhvr.IsPause) {
        statusBhvr.IsPause = false;
      }

      statusBhvr.Status = HeroStatus.idle;
      if (dbBhvr != null) {
        dbBhvr.Resume ();
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;

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

    
    }

    #endregion

    #region DragonBones Events

    private void OnMovementChange (Com.Viperstudio.Events.Event e)
    {

      switch (statusBhvr.Status) {
      case HeroStatus.charge:
        // 发大招通知
        if (skill.bigMove) {
          bmBhvr.StartBigMove (skill.chargeTime);
        }
        break;
      }

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
        if (statusBhvr.IsBigMove) {
          // 大招结束
          bmBhvr.StopBigMove ();
        }
        statusBhvr.Status = HeroStatus.release;
        if (statusBhvr.IsBigMove) {
          // 大招结束
          bmBhvr.StopBigMove ();
        }
        break;
      case HeroStatus.release:
        statusBhvr.Status = HeroStatus.idle;
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
    /// 暂停回调
    /// </summary>
    /// <param name="pause">If set to <c>true</c> pause.</param>
    private void OnPauseChanged (bool pause)
    {
      if (pause) {
        PauseSelf ();
      } else {
        if (hero.hp == 0) {
          Die ();
        } else {
          ResumeSelf ();
        }
      }
    }

    /// <summary>
    /// 状态回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusChanged (HeroStatus status)
    {

      string clip = null;
      switch (status) {

      case HeroStatus.charge: // 起手 ----

        // 有则播放，无则转到释放状态

        if (skill.chargeClip != null) {
          if (animationList.Contains (skill.chargeClip)) {
            clip = skill.chargeClip;
          }
        }
        if (clip == null) {
          statusBhvr.Status = HeroStatus.release;
        } else {
          // 播放起手动作
          //dbBhvr.Stop ();
          dbBhvr.GotoAndPlay (clip);
        }

          // 播放起手特效
        if (skill.chargeSpecials != null) {
          skillBhvr.CastChargeSpecial (skill, gameObject, target);
        }
        break;
      
      case HeroStatus.release: // 释放 ----

        // 大招结束
        /*
        if (skill.bigMove) {
          Debug.Log ("bigmoveend hero "+hero.id);
          bmBhvr.StopBigMove ();
        }*/

        if (skill.releaseClip != null && animationList.Contains (skill.releaseClip)) {
          clip = skill.releaseClip;
        } else {
          clip = Config.DEFAULT_ATTACK_CLIP;
        }
        // 播放施放动作
        //dbBhvr.Stop ();
        armature.Animation.GotoAndPlay (clip, -1, -1, 1);

        // 播放施放特效
        if (skill.releaseSpecials != null) {
          skillBhvr.CastReleaseSpecial (skill, gameObject, target);
        }
        // 抛出作用器s
        if (skill != null && skill.effectors != null) {
          foreach (Effector e in skill.effectors) {
            skillBhvr.Cast (e, skill, gameObject, target);
          }
        }
        break;

      case HeroStatus.dead: // 死亡 ----

        //dbBhvr.Stop ();
        armature.Animation.GotoAndPlay (status.ToString (), -1, -1, 1);
        FadeOut ();
        break;

      default: // 其他 ----

        //dbBhvr.Stop ();
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

    /// <summary>
    /// 暂停自己
    /// </summary>
    private void PauseSelf ()
    {

      // 暂停动画
      if (dbBhvr != null) {
        dbBhvr.Pause ();
      }

      // 暂停行走

      if (agent.enabled) {
        agent.enabled = false;
      }
    }

    /// <summary>
    /// 恢复自己
    /// </summary>
    private void ResumeSelf ()
    {

      // 恢复动画
      if (dbBhvr != null) {
        dbBhvr.Resume ();
      }
      // 恢复行走

      if (!agent.enabled) {
        agent.enabled = true;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 攻击指定目标
    /// </summary>
    /// <param name="target">Target.</param>
    public void Attack (GameObject target, Skill skill)
    {

      if (statusBhvr.Status != HeroStatus.running) {

        this.target = target;
        this.skill = skill;

        statusBhvr.Status = HeroStatus.charge;
      }

    }

    /// <summary>
    /// 使用技能攻击，攻击目标由技能决定
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void Attack (Skill skill)
    {
      Attack (null, skill);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Die ()
    {

      // 正在走路，停止走路
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
      switch (statusBhvr.Status) {

      case HeroStatus.idle:
        statusBhvr.Status = HeroStatus.beat;
        break;
      case HeroStatus.charge:
      case HeroStatus.release:
        if (!statusBhvr.IsBigMove) {
          statusBhvr.Status = HeroStatus.beat;
        }
        break;

      case HeroStatus.running:
        agent.ResetPath ();
        statusBhvr.Status = HeroStatus.beat;
        break;
      }
    }

    /// <summary>
    /// 庆祝胜利
    /// </summary>
    public void CelebrateVictory ()
    {

      statusBhvr.Status = HeroStatus.victory;

    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause ()
    {
      if (!statusBhvr.IsPause)
        statusBhvr.IsPause = true;
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume ()
    {

      if (statusBhvr.IsPause) {
        statusBhvr.IsPause = false;
      }

    }

    /// <summary>
    /// 找距离最近的目标
    /// </summary>
    /// <returns>The closest target.</returns>
    public GameObject FindClosestTarget ()
    {
      return LevelController.FindClosestTarget (this);
    }

    /// <summary>
    /// 使用大招攻击
    /// </summary>
    public void BigMove ()
    {
      // 获取大招
      Skill bs = null;
      foreach (Skill s in hero.skills) {
        if (s.bigMove) {
          bs = s;
          break;
        }
      }

      if (bs != null) {
        Attack (bs);
      }
    }

    #endregion
  }
}

