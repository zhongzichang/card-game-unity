using System;
using UnityEngine;
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class RecorderBhvr : MonoBehaviour
  {

    private TP.LevelRecorder recorder;
    private int frameIndex = 0;
    private int lastKeyFrameIndex = 0;
    private int keyFrameCounter = 0;

    //private TP.Frame currentFrame;

    void Start ()
    {

      recorder = new TP.LevelRecorder();
      //currentFrame = new TP.Frame ();
      frameIndex = 0;
      lastKeyFrameIndex = 0;

      LevelController.RaiseBattleStart += OnBattleStart;
      LevelController.RaiseChallengeSuccess += OnChallengeSuccess;
      LevelController.RaiseChangengeFailure += OnChallengeFailure;

    }

    void Update ()
    {
      if (recorder.IsRecording) {
        // 设置当前帧索引
        frameIndex++;
      }
    }


    private void OnBattleStart (object sender, EventArgs args)
    {
      //recorder.Start (LevelContext.attackGobjs, LevelContext.defenseGobjs, LevelContext.CurrentLevel);


      frameIndex = 0;
      keyFrameCounter = 0;
    }

    private void OnChallengeSuccess (object sender, EventArgs args)
    {
      CheckFrame ();

      //recorder.Stop ();
      //recorder.Save ();

      //Debug.Log (Procurios.Public.JSON.JsonEncode (recorder.Record));
    }

    private void OnChallengeFailure (object sender, EventArgs args)
    {
      CheckFrame ();

      //recorder.Stop ();
      //recorder.Save ();

      //Debug.Log (Procurios.Public.JSON.JsonEncode (recorder.Record));
      //Debug.Log (recorder.Record.timelines [0].frames.Count);
    }

    private void OnHeroStatusChange (object sender, EventArgs args)
    {

      CheckFrame ();

      HeroStatusEvent hsArgs = (HeroStatusEvent)args;

      HeroStatusBhvr sttsBhvr = (HeroStatusBhvr)sender;
      if (sttsBhvr != null) {
        HeroBhvr heroBhvr = sttsBhvr.GetComponent<HeroBhvr> ();
        //TP.Action action = new TPA.StatusChange (heroBhvr.hero.id, hsArgs.Status);
        //currentFrame.actions.Add (action);

        switch (hsArgs.Status) {
        case HeroStatus.running:
          // 开始移动
          //action = new TPA.PosChange (heroBhvr.hero.id, sttsBhvr.transform.position.x);
          //currentFrame.AddAction (action);
          break;
        }

        switch (sttsBhvr.beforeStatus) {
        case HeroStatus.running:
          // 停止移动
          //action = new TPA.PosChange (heroBhvr.hero.id, sttsBhvr.transform.position.x);
          //currentFrame.AddAction (action);
          break;
        }

      }

    }

    private void CheckFrame ()
    {
      if (frameIndex != lastKeyFrameIndex) {

        // 设置当前帧持续帧数
        //currentFrame.duration = frameIndex - lastKeyFrameIndex;
        //recorder.AddKeyFrame (currentFrame);

        // 创建新帧，并且设置新帧为当前帧
        //currentFrame = new TPA.Frame ();

        // 前一个关键帧索引
        lastKeyFrameIndex = frameIndex;
        // 关键帧计数++
        keyFrameCounter++;
      }

    }
  }
}

