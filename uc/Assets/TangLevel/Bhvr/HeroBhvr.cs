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
    private Skill skill;

    #region MonoBehaviours

    void Awake ()
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
      statusBhvr.statusStartHandler += OnStatusStart;
      // transform
      myTransform = transform;
      // dragonbones behaviour
      dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
      dbBhvr.GotoAndPlay (statusBhvr.Status.ToString ());
      armature = dbBhvr.armature;
    }

    void OnEnable ()
    {
      // 重新打开，状态设置为空闲
      if (statusBhvr != null)
        statusBhvr.Status = HeroStatus.idle;

      if (armature != null) {
        //armature.AddEventListener (DBE.AnimationEvent.FADE_OUT, OnAnimationFadeOut);
        //armature.AddEventListener (DBE.AnimationEvent.FADE_OUT_COMPLETE, OnAnimationFadeOutComplete);
        armature.AddEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
      }

    }

    void OnDisable ()
    {

      if (armature != null) {
        //armature.RemoveEventListener (DBE.AnimationEvent.FADE_OUT, OnAnimationFadeOut);
        // armature.RemoveEventListener (DBE.AnimationEvent.FADE_OUT_COMPLETE, OnAnimationFadeOutComplete);
        armature.RemoveEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
      }

    }

    #endregion

    #region Other Events

    private void OnAnimationFadeOut (Com.Viperstudio.Events.Event e)
    {

      string movementId = armature.Animation.MovementID;

      Debug.Log ("movementId fade out - "+movementId);

      if (movementId.Equals (HeroStatus.attack.ToString ())) {


        // 播放完起手动作后，播放打击。如果有特效，放出特效

      }

    }

    private void OnAnimationFadeOutComplete (Com.Viperstudio.Events.Event e)
    {
      string movementId = armature.Animation.MovementID;

      Debug.Log ("movementId fade out complete - "+movementId);

      if (movementId.Equals (HeroStatus.attack.ToString ())) {

        //statusBhvr.Status = HeroStatus.idle;

      }

    }

    private void OnAnimationLoopComplete(Com.Viperstudio.Events.Event e){

      string movementId = armature.Animation.MovementID;

      Debug.Log ("movementId fade out complete - "+movementId);

      if (movementId.Equals (HeroStatus.attack.ToString ())) {

        statusBhvr.Status = HeroStatus.idle;

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

      this.skill = skill;
      statusBhvr.Status = HeroStatus.attack;

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

    #region Tang Callback

    /// <summary>
    /// 状态开始回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusStart (HeroStatus status)
    {
      switch (status) {
      case HeroStatus.attack:
        dbBhvr.GotoAndPlay (status.ToString ());
        break;
      default:
        dbBhvr.GotoAndPlay (status.ToString ());
        break;
      }
    }

    #endregion
  }
}

