using System;
using UnityEngine;
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class HeroRecordBhvr : MonoBehaviour
  {

    private HeroStatusBhvr statusBhvr;
    private HeroBhvr heroBhvr;

    private TP.HeroAnimation anim;

    public TP.HeroAnimation Anim{
      get {
        return anim;
      }
    }

    void OnEnable ()
    {
      // status behaviour
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
        statusBhvr.statusChangedHandler += OnStatusChange;
      }

      // hero behaviour
      if (heroBhvr == null) {
        heroBhvr = GetComponent<HeroBhvr> ();
      }

      // animation
      anim = new TP.HeroAnimation (heroBhvr.hero.id);

    }

    void OnDisable(){
      if (statusBhvr != null) {
        statusBhvr.statusChangedHandler -= OnStatusChange;
      }
    }

    private void OnStatusChange (HeroStatus status)
    {

      // 记录状态变化数据
      TP.Frame<HeroStatus> statusFrame = new TP.Frame<HeroStatus> ();
      anim.statusTimeline.frames.Add (statusFrame);

      switch (status) {
      case HeroStatus.running:
        // 英雄开始移动
        TP.Frame<float> frame = new TP.Frame<float> ();
        anim.posxTimeline.frames.Add (frame);
        break;
      }

      switch (statusBhvr.beforeStatus) {
      case HeroStatus.running:
        // 英雄移动结束
        TP.Frame<float> frame = new TP.Frame<float> ();
        anim.posxTimeline.frames.Add (frame);
        break;
      }

    }

  }
}

