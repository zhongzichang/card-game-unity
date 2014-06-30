using System;
using UnityEngine;
using TPA = TangLevel.Playback.Adv;

namespace TangLevel
{
  public class RecorderBhvr : MonoBehaviour
  {

    public const int SAMPLES = 120;
    private TPA.LevelRecorder recorder;
    private TPA.Frame currentFrame;
    private float timeInSecond;
    private int frameIndex = 0;
    private int preFrameIndex = 0;
    private float secondsPerFrame;

    void Start ()
    {

      recorder = TPA.LevelRecorder.NewInstance ();
      currentFrame = new TPA.Frame ();
      frameIndex = 0;
      preFrameIndex = 0;
      secondsPerFrame = 60F / SAMPLES; 

      LevelController.RaiseChallengeStart += OnChallengeStart;
      LevelController.RaiseChallengeSuccess += OnChallengeSuccess;
      LevelController.RaiseChangengeFailure += OnChallengeFailure;

    }

    void Update ()
    {
      if (recorder.IsRecording) {
        timeInSecond += Time.deltaTime;
        // 设置当前帧索引
        frameIndex = (int)(timeInSecond / secondsPerFrame);
      }
    }


    private void OnChallengeStart (object sender, EventArgs args)
    {
      recorder.Start (LevelContext.attackGobjs, LevelContext.defenseGobjs, LevelContext.CurrentLevel);

      foreach (GameObject gobj in LevelContext.attackGobjs) {

        // 监听攻方英雄的状态变化
        HeroStatusBhvr sttsBhvr = gobj.GetComponent<HeroStatusBhvr> ();
        sttsBhvr.changeHandler += OnHeroStatusChange;
      }
      foreach (GameObject gobj in LevelContext.defenseGobjs) {

        // 监听守方英雄的状态变化
        HeroStatusBhvr sttsBhvr = gobj.GetComponent<HeroStatusBhvr> ();
        sttsBhvr.changeHandler += OnHeroStatusChange;
      }

      timeInSecond = 0;
    }

    private void OnChallengeSuccess (object sender, EventArgs args)
    {
      CheckFrame ();

      recorder.Stop ();
      recorder.Save ();
    }

    private void OnChallengeFailure (object sender, EventArgs args)
    {
      CheckFrame ();
      recorder.Stop ();
      recorder.Save ();
    }

    private void OnHeroStatusChange (object sender, EventArgs args)
    {

      CheckFrame ();

      HeroStatusEvent hsArgs = (HeroStatusEvent)args;

      HeroStatusBhvr sttsBhvr = (HeroStatusBhvr)sender;
      if (sttsBhvr != null) {
        HeroBhvr heroBhvr = sttsBhvr.GetComponent<HeroBhvr> ();
        TPA.Action action = new TPA.StatusChange (heroBhvr.hero.id, hsArgs.Status);
        currentFrame.actions.Add (action);
      }

    }

    private void CheckFrame(){

      if (frameIndex != preFrameIndex) {

        Debug.Log ("frame create ...");

        currentFrame.duration = frameIndex - preFrameIndex;
        recorder.AddKeyFrame (currentFrame);
        currentFrame = new TPA.Frame ();
        preFrameIndex = frameIndex;
      }

    }
  }
}

