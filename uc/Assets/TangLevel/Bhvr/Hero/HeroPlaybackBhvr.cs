using System;
using System.Collections.Generic;
using UnityEngine;
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class HeroPlaybackBhvr : MonoBehaviour
  {

    private TP.HeroAnimation mAnim;
    private IEnumerator<TP.Frame<float>> posxEnumer;
    private IEnumerator<TP.Frame<HeroStatus>> statusEnumer;
    private IEnumerator<TP.Frame<TP.Action>> actionEnumer;
    private int frameIndex;
    // posx frame
    private TP.Frame<float> prePosxFrame;
    private TP.Frame<float> nextPosxFrame;
    // status frame
    private TP.Frame<HeroStatus> preStatusFrame;
    private TP.Frame<HeroStatus> nextStatusFrame;
    // action frame
    private TP.Frame<TP.Action> preActionFrame;
    private TP.Frame<TP.Action> nextActionFrame;
    // bhvrs
    private DirectedNavigable nav;
    private HeroBhvr heroBhvr;

    void Start ()
    {
      nav = GetComponent<DirectedNavigable> ();
      heroBhvr = GetComponent<HeroBhvr> ();
    }

    public TP.HeroAnimation Anim {
      get{ return mAnim; }
      set {
        mAnim = value;
        posxEnumer = mAnim.posxTimeline.frames.GetEnumerator ();
        statusEnumer = mAnim.statusTimeline.frames.GetEnumerator ();
        actionEnumer = mAnim.actionTimeline.frames.GetEnumerator ();
      }
    }

    public void OnFrameIndexChange (int frameIndex)
    {
      this.frameIndex = frameIndex;

      CheckAction ();
    }

    private void CheckAction(){

      if (frameIndex == 0) {

        preActionFrame = null;
        nextActionFrame = null;

        if (actionEnumer.MoveNext ()) {
          nextActionFrame = actionEnumer.Current;
        }

      }

      if (nextActionFrame != null && nextActionFrame.index == frameIndex) {
        preActionFrame = nextActionFrame;
        if (actionEnumer.MoveNext ()) {
          nextActionFrame = actionEnumer.Current;
        }
        switch (preActionFrame.val.status) {
        case HeroStatus.running:
          TP.RunAction runAction = preActionFrame.val as TP.RunAction;
          nav.NavTo (runAction.stopx);
          break;
        case HeroStatus.dead:
          heroBhvr.Die ();
          break;
        }
      }
    }

    private void CheckPos ()
    {

      if (frameIndex == 0) {

        // 第一帧
        prePosxFrame = null;
        nextPosxFrame = null;
        if (posxEnumer.MoveNext ()) {
          nextPosxFrame = posxEnumer.Current;
        }
      }

      if (nextPosxFrame != null && nextPosxFrame.index == frameIndex) {
        prePosxFrame = nextPosxFrame;
        if (posxEnumer.MoveNext ()) {
          nextPosxFrame = posxEnumer.Current;
          if (Mathf.Abs (prePosxFrame.val - nextPosxFrame.val) > 1F) {
            // 需要移动
            nav.NavTo (nextPosxFrame.val);
          }
        } else {
          nextPosxFrame = null;
        }
      }
    }

    private void CheckStatus ()
    {

      if (frameIndex == 0) {
        preStatusFrame = null;
        nextStatusFrame = null;
        if (statusEnumer.MoveNext ()) {
          nextStatusFrame = statusEnumer.Current;
        }
      }

      if (nextStatusFrame != null && nextStatusFrame.index == frameIndex) {
        preStatusFrame = nextStatusFrame;
        if (statusEnumer.MoveNext ()) {
          nextStatusFrame = statusEnumer.Current;
        } else {
          nextStatusFrame = null;
        }
      }
    }

  }
}

