using System;
using System.Collections.Generic;
using TG = TangGame;
using UnityEngine;

namespace TangLevel.Playback
{
  public class TextAnimationController
  {

    private static TextAnimation mAnim;
    private static Frame<TG.BattleTxt> nextBattleTxtFrame;
    private static IEnumerator<Frame<TG.BattleTxt>> battleTxtEnumer;

    public static TextAnimation Anim {
      get {
        return mAnim;
      }
      set {
        mAnim = value;
        if (mAnim != null) {
          battleTxtEnumer = mAnim.battleTimeline.frames.GetEnumerator ();
        }
      }
    }

    // 录像
    public static void OnTextBubbing (TangGame.BattleTxt txt)
    {
      Frame<TG.BattleTxt> frame = new Frame<TG.BattleTxt> (RecorderBhvr.frameIndex, txt);
      mAnim.battleTimeline.frames.Add (frame);
    }

    // 回放
    public static void OnFrameIndexChange (int index)
    {

      if (index == 0) {
        nextBattleTxtFrame = null;
        if (battleTxtEnumer.MoveNext ()) {
          nextBattleTxtFrame = battleTxtEnumer.Current;
        }
      }

      while (nextBattleTxtFrame != null && nextBattleTxtFrame.index <= index) {

        // 冒字
        BattleTextController.Bubbling (nextBattleTxtFrame.val);

        if (battleTxtEnumer.MoveNext ()) {
          nextBattleTxtFrame = battleTxtEnumer.Current;
        } else {
          nextBattleTxtFrame = null;
        }
      }

    }

  }
}

