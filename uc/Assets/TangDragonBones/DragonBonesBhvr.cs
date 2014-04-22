using System;
using UnityEngine;
using DragonBones;
using DragonBones.Animation;
using DragonBones.Display;
using TL = TangLevel;

namespace TangDragonBones
{
  public class DragonBonesBhvr : MonoBehaviour
  {
    public Armature armature;
    //public GameObject armatureGobj;
    public TL.HeroStatusBhvr statusBhvr;

    #region MonoBehaviour

    void Start ()
    {
      //Debug.Log ("start");
      if (armature != null) {

        armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay ("idle", -1, -1, 0);
      }

      statusBhvr = GetComponent<TL.HeroStatusBhvr> ();
      if (statusBhvr == null) {
        statusBhvr = gameObject.AddComponent<TL.HeroStatusBhvr> ();
      }
      statusBhvr.statusStartHandler += OnStatusStart;

    }

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

    #region Tang Callback
    /// <summary>
    /// 状态开始回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusStart(TL.HeroStatus status){
      switch (status) {
      case TL.HeroStatus.attack:
        break;
      default:
        GotoAndPlay (status.ToString ());
        //Debug.Log (status.ToString());
        break;
      }
    }
    #endregion

    #region private Methods

    private void GotoAndPlay (string movementId)
    {
      if (armature != null) {
        //armature.AdvanceTime (0f);
        armature.Animation.GotoAndPlay (movementId, -1, -1, 0);
      }
    }

    #endregion
  }
}

