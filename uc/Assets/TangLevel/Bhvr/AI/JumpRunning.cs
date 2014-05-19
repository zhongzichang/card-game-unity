using System;
using UnityEngine;
using TDB = TangDragonBones;
using DragonBones;
using DBE = DragonBones.Events;
using System.Collections.Generic;

namespace TangLevel
{
  public class JumpRunning : MonoBehaviour
  {
    public static readonly string SPEED_ZERO = "speed_zero";
    public static readonly string SPEED_RESUME = "speed_resume";
    private DirectedNavigable navigable;
    private DirectedNavAgent agent;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private TDB.DragonBonesBhvr dbBhvr;
    private Armature armature;
    private bool speedResume = false;

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

      // status behaviour
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
        if (statusBhvr == null) {
          statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
        }
        statusBhvr.statusChangedHandler += OnStatusChanged;
      }

      // DragonBonesBhvr
      if (dbBhvr == null) {
        // dragonbones behaviour
        dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
        armature = dbBhvr.armature;
      }
      // armature
      if (armature != null) {
        armature.AddEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
        armature.AddEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
      }

      // transform
      myTransform = transform;
    }

    void Update ()
    {

      if (speedResume) {
        if (agent.speed < navigable.Speed) {
          agent.speed += (navigable.Speed) * Time.deltaTime * 4;
        } else {
          speedResume = false;
        }
      }

    }

    #endregion

    private void OnAnimationLoopComplete (Com.Viperstudio.Events.Event e)
    {
      Debug.Log ("OnAnimationLoopComplete");
      if (statusBhvr.Status == HeroStatus.running) {
        agent.speed = 0;
      }

    }

    private void OnAnimationFrameEvent (Com.Viperstudio.Events.Event e)
    {

      Debug.Log (e.EventType);
      DBE.FrameEvent fe = e as DBE.FrameEvent;
      if (fe != null && statusBhvr.Status == HeroStatus.running) {

        if (SPEED_ZERO.Equals (fe.FrameLabel)) {
          agent.speed = 0;
        } else if (SPEED_RESUME.Equals (fe.FrameLabel)) {
          speedResume = true;
        }
      }
    }

    /// <summary>
    /// 状态回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusChanged (HeroStatus status)
    {
      if (statusBhvr.beforeStatus == HeroStatus.running) {
        // 恢复速度
        agent.speed = navigable.Speed;
        speedResume = false;
      }
    }
  }
}

