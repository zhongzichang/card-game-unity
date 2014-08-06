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
    private IEnumerator<TP.Frame<TP.RunAction>> runActionEnumer;
    private IEnumerator<TP.Frame<int>> hpEnumer;
    private IEnumerator<TP.Frame<int>> mpEnumer;
    private IEnumerator<TP.Frame<TP.SkillAction>> skillActionEnumer;
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
    // run ation frame
    private TP.Frame<TP.RunAction> preRunActionFrame;
    private TP.Frame<TP.RunAction> nextRunActionFrame;
    // hp and mp frame
    private TP.Frame<int> nextHpFrame;
    private TP.Frame<int> nextMpFrame;
    // skill action
    private TP.Frame<TP.SkillAction> nextSkillActionFrame;

    // bhvrs
    private DirectedNavigable nav;
    private HeroBhvr heroBhvr;
    private SkillBhvr skillBhvr;

    void Start ()
    {
      nav = GetComponent<DirectedNavigable> ();
      heroBhvr = GetComponent<HeroBhvr> ();
      skillBhvr = GetComponent<SkillBhvr> ();
    }

    public TP.HeroAnimation Anim {
      get{ return mAnim; }
      set {
        mAnim = value;
        posxEnumer = mAnim.posxTimeline.frames.GetEnumerator ();
        statusEnumer = mAnim.statusTimeline.frames.GetEnumerator ();
        actionEnumer = mAnim.actionTimeline.frames.GetEnumerator ();
        runActionEnumer = mAnim.runActionTimeline.frames.GetEnumerator ();
        hpEnumer = mAnim.hpTimeline.frames.GetEnumerator ();
        mpEnumer = mAnim.mpTimeline.frames.GetEnumerator ();
        skillActionEnumer = mAnim.skillActionTimeline.frames.GetEnumerator ();
      }
    }

    /// <summary>
    /// 当帧索引改变时的回调 PlaybackBhvr.frameIndex
    /// </summary>
    /// <param name="frameIndex">Frame index.</param>
    public void OnFrameIndexChange (int frameIndex)
    {
      this.frameIndex = frameIndex;
      CheckRunAction ();
      CheckAction ();
      CheckSkillAction ();
      CheckHp ();
      CheckMp ();
    }

    /// <summary>
    /// 检查普通动作
    /// </summary>
    private void CheckAction ()
    {

      if (frameIndex == 0) {

        // 如果是第一帧

        preActionFrame = null;
        nextActionFrame = null;
        if (actionEnumer.MoveNext ()) {
          nextActionFrame = actionEnumer.Current;
        }
      }

      if (nextActionFrame != null && nextActionFrame.index == frameIndex) {

        // 下一帧到达

        preActionFrame = nextActionFrame;
        if (actionEnumer.MoveNext ()) {
          nextActionFrame = actionEnumer.Current;
        } else {
          nextActionFrame = null;
        }
        switch (preActionFrame.val.status) {
        case HeroStatus.dead:
          // 死亡
          heroBhvr.Die ();
          break;
        /*
        case HeroStatus.beat:
          heroBhvr.BeBeat ();
          break;*/
        }
      }
    }

    /// <summary>
    /// 检查跑步动作
    /// </summary>
    private void CheckRunAction ()
    {

      if (frameIndex == 0) {
        preRunActionFrame = null;
        nextRunActionFrame = null;
        if (runActionEnumer.MoveNext ()) {
          nextRunActionFrame = runActionEnumer.Current;
        }
      }

      if (nextRunActionFrame != null && nextRunActionFrame.index == frameIndex) {
        preRunActionFrame = nextRunActionFrame;
        if (runActionEnumer.MoveNext ()) {
          nextRunActionFrame = runActionEnumer.Current;
        } else {
          nextRunActionFrame = null;
        }
        nav.NavTo (preRunActionFrame.val.stopx);
      }
    }

    /// <summary>
    /// 检查HP
    /// </summary>
    private void CheckHp ()
    {
      if (frameIndex == 0) {
        nextHpFrame = null;
        if (hpEnumer.MoveNext ()) {
          nextHpFrame = hpEnumer.Current;
        }
      }

      while (nextHpFrame != null && nextHpFrame.index == frameIndex) {

        // 修改英雄的HP
        heroBhvr.hero.hp = nextHpFrame.val;

        if (hpEnumer.MoveNext ()) {
          nextHpFrame = hpEnumer.Current;
        } else {
          nextHpFrame = null;
        }
      }
    }

    private void CheckMp ()
    {
      if (frameIndex == 0) {
        nextMpFrame = null;
        if (mpEnumer.MoveNext ()) {
          nextMpFrame = mpEnumer.Current;
        }
      }

      while (nextMpFrame != null && nextMpFrame.index == frameIndex) {

        // 修改英雄的HP
        heroBhvr.hero.mp = nextMpFrame.val;

        if (mpEnumer.MoveNext ()) {
          nextMpFrame = mpEnumer.Current;
        } else {
          nextMpFrame = null;
        }
      }
    }

    private void CheckSkillAction ()
    {

      if (frameIndex == 0) {
        // 如果是第一帧
        nextSkillActionFrame = null;
        if (skillActionEnumer.MoveNext ()) {
          nextSkillActionFrame = skillActionEnumer.Current;
        }
      }

      if (nextSkillActionFrame != null && nextSkillActionFrame.index <= frameIndex) {

        // 攻击
        TP.SkillAction skillAction = nextSkillActionFrame.val;
        int skillId = skillAction.skillId;
        int targetId = skillAction.targetId;
        if (skillBhvr.skills.ContainsKey (skillId)) {
          // 释放技能
          Skill skill = skillBhvr.skills [skillId];
          GameObject target = null;
          if (targetId != 0 && LevelContext.heroGobjs.ContainsKey (targetId)) {
            target = LevelContext.heroGobjs [targetId];
          }
          heroBhvr.Attack (target, skill);
        }

        // 下一帧
        if (skillActionEnumer.MoveNext ()) {
          nextSkillActionFrame = skillActionEnumer.Current;
        } else {
          nextSkillActionFrame = null;
        }
      }
    }

    /*
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
    }*/

  }
}

