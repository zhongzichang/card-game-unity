using System;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using DragonBones.Animation;

namespace TangDragonBones
{
  public class ArmatureBhvr : MonoBehaviour
  {
    public Armature armature;

    #region MonoBehaviour

    void OnDisable ()
    {
      if (armature != null)
        WorldClock.Clock.Remove (armature);
    }

    void OnEnable ()
    {
      if (armature != null) {
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
      if (armature != null) {
        //armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay (movementId, -1, -1, 0);
      }
    }

    /// <summary>
    /// 播放下一个动作
    /// </summary>
    public void GotoAndPlayNext ()
    {
      if (armature != null) {
        List<string> animationList = armature.Animation.AnimationList;
        int index = animationList.IndexOf (armature.Animation.MovementID);
        if (index >= animationList.Count - 1) {
          index = 0;
        } else {
          index++;
        }
        GotoAndPlay (animationList [index]);

      }
    }

    #endregion
  }
}

