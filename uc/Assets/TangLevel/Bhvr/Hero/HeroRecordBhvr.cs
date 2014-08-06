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

    // 行走
    private TP.Frame<TP.RunAction> runActionFrame;
    private TP.RunAction runAction;

    // 其他动作
    private TP.HeroAnimation anim;
    private TP.Frame<TP.Action> actionFrame;

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
      heroBhvr.hero.raiseHpChange += OnHpChange;
      heroBhvr.hero.raiseMpChange += OnMpChange;

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
      if (heroBhvr != null) {
        heroBhvr.hero.raiseMpChange -= OnMpChange;
        heroBhvr.hero.raiseMpChange -= OnMpChange;
      }
    }

    /// <summary>
    /// 录像结束前要调用一次
    /// </summary>
    public void EndRecord ()
    {
      if (runAction != null && runActionFrame != null) {
        // 补充目标值
        runAction.stopx = myTransform.position.x;
        anim.runActionTimeline.frames.Add (runActionFrame);

        runAction = null;
        runActionFrame = null;
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
        runActionFrame = new TP.Frame<TP.RunAction> (frameIndex, runAction);
        break;

      case HeroStatus.charge:
        // 攻击
        int skillId = heroBhvr.skill.id;
        int targetId = 0;
        if (heroBhvr.target != null) {
          HeroBhvr tgtHeroBhvr = heroBhvr.target.GetComponent<HeroBhvr> ();
          targetId = tgtHeroBhvr.hero.id;
        }
        TP.SkillAction skillAction = new TP.SkillAction (heroBhvr.skill.id, targetId);
        TP.Frame<TP.SkillAction> skillActionFrame = new TP.Frame<TP.SkillAction> (frameIndex, skillAction);
        anim.skillActionTimeline.frames.Add (skillActionFrame);
        break;

      default:
        // 其他动作的记录
        actionFrame = TP.FrameFactory.NewActionFrame (frameIndex, status);
        if (actionFrame != null) {
          anim.actionTimeline.frames.Add (actionFrame);
        }
        break;
      }

      // 前一个动作是什么
      switch (statusBhvr.beforeStatus) {
      case HeroStatus.running:

        // 英雄移动结束，或者英雄移动的目标位置
        if (runAction != null && runActionFrame != null) {
          runAction.stopx = myTransform.position.x;
          anim.runActionTimeline.frames.Add (runActionFrame);

          runAction = null;
          runActionFrame = null;
        }
        break;
      }

    }

    private void OnHpChange (int val, int max)
    {
      TP.Frame<int> frame = new TP.Frame<int> (RecorderBhvr.frameIndex, val);
      anim.hpTimeline.frames.Add (frame);
    }

    private void OnMpChange (int val, int max)
    {
      TP.Frame<int> frame = new TP.Frame<int> (RecorderBhvr.frameIndex, val);
      anim.mpTimeline.frames.Add (frame);
    }

  }
}

