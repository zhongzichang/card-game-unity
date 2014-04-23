using System;
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
      //Debug.Log ("destroy");
      if (armature != null)
        armature.Dispose ();
    }

    #endregion


    #region Public Methods

    public void GotoAndPlay (string movementId)
    {
      if (armature != null) {
        //armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay (movementId, -1, -1, 0);
      }
    }

    #endregion
  }
}

