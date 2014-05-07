using System;
using UnityEngine;
using TDB = TangDragonBones;
using DragonBones;
using DBE = DragonBones.Events;

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
      }

    }

    void OnDisable ()
    {

      if (armature != null) {
        armature.RemoveEventListener (DBE.AnimationEvent.MOVEMENT_CHANGE, OnMovementChange);
        armature.RemoveEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
      }

    }

    #endregion

    #region DragonBones Events

    private void OnMovementChange (Com.Viperstudio.Events.Event e)
    {

      string movementId = armature.Animation.MovementID;

      // 前摇动作
      if (movementId.Equals (HeroStatus.attack.ToString ())) {
        // 如果有前摇特效，播放前摇特效

      }

    }
    //private void
    private void OnAnimationLoopComplete (Com.Viperstudio.Events.Event e)
    {

      string movementId = armature.Animation.MovementID;

      if (movementId.Equals (HeroStatus.attack.ToString ())) {
      
        // 播放完前摇动作后，播放后摇。如果有后摇特效，放出后摇特效
        if (skillBhvr != null && skill != null && target != null) {
          skillBhvr.Cast (skill.effector, skill, gameObject, target);

        }

        // 播放完后摇动作后，转成英雄状态 idle
        statusBhvr.Status = HeroStatus.idle;

      } else if (movementId.Equals (HeroStatus.beat.ToString ())) {

        statusBhvr.Status = HeroStatus.idle;
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
      case HeroStatus.attack:
        // TODO 技能需要特殊处理，不同的技能使用不同的动作
        dbBhvr.GotoAndPlay (status.ToString ());
        //armature.Animation.GotoAndPlay (status.ToString (), -1, -1, 1);
        break;
      case HeroStatus.dead:
        armature.Animation.GotoAndPlay (status.ToString (), -1, -1, 1);
        FadeOut ();
        break;
      default:
        dbBhvr.GotoAndPlay (status.ToString ());
        break;
      }
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

      statusBhvr.Status = HeroStatus.attack;

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
      if (statusBhvr.Status == HeroStatus.idle ||
          statusBhvr.Status == HeroStatus.attack) {
        statusBhvr.Status = HeroStatus.beat;

      } else if (statusBhvr.Status == HeroStatus.running) {
        statusBhvr.Status = HeroStatus.beat;
        agent.ResetPath ();
      }
    }

    public void CelebrateVictory ()
    {

      statusBhvr.Status = HeroStatus.victory;

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

