using System;
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
      //if (armature != null && gameObject.activeSelf) {
        //armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay (movementId, -1, -1, 0);
        //}
    }

    public void Stop(){
      armature.Animation.Stop ();
    }

    /// <summary>
    /// 暂停动画播放
    /// </summary>
    public void Pause(){
      if (armature != null) {
        armature.Animation.Stop ();
        //armature.Animation.TimeScale = 0;
        /*
        if (WorldClock.Clock.Contains (armature)) {
          WorldClock.Clock.Remove (armature);
        }*/
      }
    }

    /// <summary>
    /// 恢复动画播放
    /// </summary>
    public void Resume(){
      if (armature != null) {
        armature.Animation.Play ();
        //armature.Animation.TimeScale = 1;
        /*
        if (!WorldClock.Clock.Contains (armature)) {
          WorldClock.Clock.Add (armature);
        }*/
      }
    }

    /// <summary>
    /// Determines whether this instance is play.
    /// </summary>
    /// <returns><c>true</c> if this instance is play; otherwise, <c>false</c>.</returns>
    public bool IsPlay(){
      if (armature != null) {

        return armature.Animation.TimeScale == 1;
      }
      return false;
    }
    #endregion
  }
}

