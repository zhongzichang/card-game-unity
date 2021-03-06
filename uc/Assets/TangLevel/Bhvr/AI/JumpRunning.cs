﻿using System;
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
    public const float ACCELERATION_SCALE = 4F; // 加速比例
    public const float LANDING_SPEED = 5F;
    private DirectedNavigable navigable;
    private DirectedNavAgent agent;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private TDB.DragonBonesBhvr dbBhvr;
    private Armature armature;
    private bool speedResume = false;
    private float mySpeed = 0;
    private float acceleration = 0;

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
        armature.AddEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
      }

      // transform
      myTransform = transform;

      mySpeed = navigable.Speed * 1.3F;
      acceleration = ACCELERATION_SCALE * mySpeed;
    }

    void Update ()
    {

      if (speedResume) {
        if (agent.speed < mySpeed) {
          agent.speed += acceleration * Time.deltaTime;
        } else {
          speedResume = false;
        }
      }

    }

    #endregion

    private void OnAnimationFrameEvent (Com.Viperstudio.Events.Event e)
    {

      DBE.FrameEvent fe = e as DBE.FrameEvent;
      if (fe != null && statusBhvr.Status == HeroStatus.running) {

        if (SPEED_ZERO.Equals (fe.FrameLabel)) {
          agent.speed = LANDING_SPEED;
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
      } else if (statusBhvr.Status == HeroStatus.running) {
        agent.speed = LANDING_SPEED;
        speedResume = true;
      }
    }
  }
}

