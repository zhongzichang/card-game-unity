using System;
using UnityEngine;
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class HeroRecordBhvr : MonoBehaviour
  {

    private HeroStatusBhvr statusBhvr;
    private HeroBhvr heroBhvr;
    private Transform myTransform;

    private TP.HeroAnimation anim;
    private TP.Frame<TP.Action> actionFrame;
    private TP.RunAction runAction;

    public TP.HeroAnimation Anim {
      get {
        return anim;
      }
    }


    void OnEnable ()
    {
      // status behaviour
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
      }
      statusBhvr.statusChangedHandler += OnStatusChange;

      // hero behaviour
      if (heroBhvr == null) {
        heroBhvr = GetComponent<HeroBhvr> ();
      }

      if (myTransform == null) {
        myTransform = transform;
      }

      // animation
      anim = new TP.HeroAnimation (heroBhvr.hero.id);

    }

    void OnDisable ()
    {
      if (statusBhvr != null) {
        statusBhvr.statusChangedHandler -= OnStatusChange;
      }
    }

    /// <summary>
    /// 录像结束前要调用一次
    /// </summary>
    public void EndRecord ()
    {
      if (runAction != null) {
        // 补充目标值
        runAction.stopx = myTransform.position.x;
        runAction = null;
        actionFrame = null;
      }

    }

    private void OnStatusChange (HeroStatus status)
    {

      int frameIndex = RecorderBhvr.frameIndex;


      switch (status) {
      case HeroStatus.running:
        // 英雄开始移动
        // run ation
        runAction = new TP.RunAction (myTransform.position.x);
        actionFrame = new TP.Frame<TP.Action> ();
        actionFrame.index = frameIndex;
        actionFrame.val = runAction;
        anim.actionTimeline.frames.Add (actionFrame);

        break;

      case HeroStatus.dead:
        actionFrame = new TP.Frame<TP.Action> ();
        actionFrame.index = frameIndex;
        actionFrame.val = new TP.DeadAction ();
        anim.actionTimeline.frames.Add (actionFrame);
        break;
      }

      switch (statusBhvr.beforeStatus) {
      case HeroStatus.running:

        // 英雄移动结束
        if (runAction != null) {
          runAction.stopx = myTransform.position.x;
          runAction = null;
          actionFrame = null;
        }
        break;
      }

    }

  }
}

