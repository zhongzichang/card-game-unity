﻿using System;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using DragonBones.Animation;
using DragonBones.Display;

namespace TangDragonBones
{
  public class DragonBonesBhvr : MonoBehaviour
  {
    public Armature armature;

    #region MonoBehaviour

    void OnDisable ()
    {
      //Debug.Log ("disable");
      if (armature != null && WorldClock.Clock.Contains (armature)) {
        WorldClock.Clock.Remove (armature);
      }
    }

    void OnEnable ()
    {
      if (armature != null &&  ! (WorldClock.Clock.Contains(armature)) ) {
        WorldClock.Clock.Add (armature);
      }
    }

    void OnDestroy ()
    {
      //Debug.Log ("destroy");
      if (armature != null)
        armature.Dispose ();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 播放指定动作
    /// </summary>
    /// <param name="movementId">Movement identifier.</param>
    public void GotoAndPlay (string movementId)
    {
      if (armature != null && gameObject.activeSelf) {
        //armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay (movementId, -1, -1, 0);
      }
    }

    /// <summary>
    /// 暂停动画播放
    /// </summary>
    public void Pause(){
      if (armature != null) {
        if (WorldClock.Clock.Contains (armature)) {
          WorldClock.Clock.Remove (armature);
        }
      }
    }

    /// <summary>
    /// 恢复动画播放
    /// </summary>
    public void Resume(){
      if (armature != null) {
        if (!WorldClock.Clock.Contains (armature)) {
          WorldClock.Clock.Add (armature);
        }
      }
    }
    #endregion
  }
}

