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
    public Hero hero;
    private DirectedNavigable navigable;
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
      // navigable
      navigable = GetComponent<DirectedNavigable> ();
      if (navigable == null) {
        navigable = gameObject.AddComponent<DirectedNavigable> ();
      }
      // status behaviour
      statusBhvr = GetComponent<HeroStatusBhvr> ();
      if (statusBhvr == null) {
        statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
      }
      statusBhvr.statusEndHandler += OnStatusEnd;
      // transform
      myTransform = transform;
      // dragonbones behaviour
      dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
      dbBhvr.GotoAndPlay (statusBhvr.Status.ToString ());
      armature = dbBhvr.armature;
      // skill behaviour
      skillBhvr = GetComponent<SkillBhvr> ();
      /*
      if (armature != null) {
        armature.AddEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
      }*/

    }

    void OnEnable ()
    {
      // 重新打开，状态设置为空闲
      if (statusBhvr != null)
        statusBhvr.Status = HeroStatus.idle;

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

          skillBhvr.Cast (skill.effector, skill, target);

        }

        // 播放完后摇动作后，转成英雄状态 idle
        statusBhvr.Status = HeroStatus.idle;

      } else if (movementId.Equals (HeroStatus.dead.ToString ())) {


      }
    }

    #endregion

    #region Tang Callback

    /// <summary>
    /// 状态开始回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusEnd (HeroStatus status)
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
    private void FadeOut(){

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

    public void Die ()
    {
      statusBhvr.Status = HeroStatus.dead;
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

