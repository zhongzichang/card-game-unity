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
    public GameObject armatureGobj;

    void Start ()
    {
      Debug.Log ("start");
      if (armature != null) {

        if (armatureGobj == null)
          armatureGobj = (armature.Display as UnityArmatureDisplay).Display;

        armatureGobj.transform.parent = transform;
        armatureGobj.transform.localPosition = Vector3.zero;

        armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay ("run", -1, -1, 0);
      }
    }

    void OnDisable ()
    {
      Debug.Log ("disable");
      if (armature != null)
        WorldClock.Clock.Remove (armature);
    }

    void OnEnable ()
    {
      Debug.Log ("enable");
      if (armature != null) {
        if (armatureGobj == null)
          armatureGobj = (armature.Display as UnityArmatureDisplay).Display;
        armatureGobj.transform.localPosition = Vector3.zero;
        WorldClock.Clock.Add (armature);
      }
    }

    void OnDestroy ()
    {
      Debug.Log ("destroy");
      if (armature != null)
        armature.Dispose ();
    }
  }
}

