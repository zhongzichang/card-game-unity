using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using DBE = DragonBones.Events;

namespace TangDragonBones
{
  public class DemoBhvr : MonoBehaviour
  {
    private DragonBonesBhvr dbBhvr = null;
    private Armature armature = null;
    private string COMMON_BEHAVIOUR = "idle";
    private int commonBehaviourIndex = 0;
    private int lastIndex = 0;
    private int index = 0;
    private List<string> animationList;

    void Update ()
    {
      if (index == commonBehaviourIndex) {
        if (dbBhvr != null) {
          dbBhvr.GotoAndPlay (COMMON_BEHAVIOUR);
          index = lastIndex;
        }
      }
    }

    void OnEnable ()
    {
      if (armature == null) {

        dbBhvr = GetComponent<DragonBonesBhvr> ();
        if (dbBhvr != null) {
          armature = dbBhvr.armature;
          animationList = armature.Animation.AnimationList;
          commonBehaviourIndex = animationList.IndexOf (COMMON_BEHAVIOUR);
        }
      }

      if (armature != null) {
        armature.AddEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
        dbBhvr.GotoAndPlay (COMMON_BEHAVIOUR);
      }

    }

    void OnDisable ()
    {

      if (armature != null) {
        armature.RemoveEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
      }

    }
    //private void
    private void OnAnimationLoopComplete (Com.Viperstudio.Events.Event e)
    {

      string movementId = armature.Animation.MovementID;

      if (!movementId.Equals (COMMON_BEHAVIOUR)) {
        // 恢复到普通动作
        index = commonBehaviourIndex;
      } 
    }

    /// <summary>
    /// 播放下一个动作
    /// </summary>
    public void GotoAndPlayNext ()
    {

      Debug.Log ("GotoAndPlayNext");

      if (armature != null && gameObject.activeSelf) {

        index = lastIndex + 1;

        if (index == commonBehaviourIndex) {
          index++;
        }

        if (index > animationList.Count - 1) {
          index = 0;
        }

        dbBhvr.GotoAndPlay (animationList [index]);
        lastIndex = index;
      }
    }
  }
}

