using System;
using System.Collections.Generic;
using UnityEngine;
using TP = TangLevel.Playback;

namespace TangLevel
{
  /// <summary>
  /// 回放行为
  /// </summary>
  public class PlaybackBhvr : MonoBehaviour
  {

    public  delegate void FrameIndexChange(int frameIndex);

    public FrameIndexChange frameIndexChangeHandler;

    public const int FRAME_RATE = 24;

    public int lastFrameIndex = 0;
    public int frameIndex = 0;

    private float time;
    private float secondsPerFrame = 1F / FRAME_RATE;
    private bool playing = false;
    private TP.LevelRecord levelRecord;
    private TP.SubLevelRecord subLevelRecord;
    private List<GameObject> attackGobjs = new List<GameObject>();
    private List<GameObject> defenseGobjs = new List<GameObject>();

    void Start ()
    {

      // 进入和离开关卡
      TP.PlaybackController.RaiseEnterLevelSuccess += OnEnterLevelSuccess;
      TP.PlaybackController.RaiseLeftLevel += OnLeftLevel;
      // 进入和离开子关卡
      TP.PlaybackController.RaiseEnterSubLevel += OnEnterSubLevel;
      TP.PlaybackController.RaiseLeftSubLevel += OnLeftSubLevel;
    }

    void Update ()
    {
      if (playing) {
        time += Time.deltaTime;
        frameIndex = (int)(time / secondsPerFrame);
        if (frameIndex > lastFrameIndex && frameIndexChangeHandler != null) {
          frameIndexChangeHandler (frameIndex);
          lastFrameIndex = frameIndex;
        }
      }
    }


    /// <summary>
    /// 进入关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnEnterLevelSuccess (object sender, EventArgs args)
    {
      levelRecord = LevelContext.CurrentLevelRecord;
    }


    /// <summary>
    /// 离开关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnLeftLevel (object sender, EventArgs args)
    {
      levelRecord = null;
    }

    /// <summary>
    /// 进入子关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnEnterSubLevel (object sender, EventArgs args)
    {
      time = 0;
      lastFrameIndex = -1;
      frameIndex = 0;

      subLevelRecord = levelRecord.subLevelRecords[ LevelContext.CurrentSubLevel.index ];

      attackGobjs.Clear ();
      foreach (GameObject g in LevelContext.AliveSelfGobjs) {
        attackGobjs.Add (g);
      }
      defenseGobjs.Clear ();
      foreach (GameObject g in LevelContext.AliveDefenseGobjs) {
        defenseGobjs.Add (g);
      }

      foreach (GameObject gobj in attackGobjs) {
        HeroBhvr hBhvr = gobj.GetComponent<HeroBhvr> ();
        HeroPlaybackBhvr hpBhvr = gobj.GetComponent<HeroPlaybackBhvr> ();
        if (hpBhvr == null) {
          hpBhvr = gobj.AddComponent<HeroPlaybackBhvr> ();
        }
        hpBhvr.Anim = subLevelRecord.attackerAnims[hBhvr.hero.id];
        frameIndexChangeHandler += hpBhvr.OnFrameIndexChange;
      }

      foreach (GameObject gobj in defenseGobjs) {
        HeroBhvr hBhvr = gobj.GetComponent<HeroBhvr> ();
        HeroPlaybackBhvr hpBhvr = gobj.GetComponent<HeroPlaybackBhvr> ();
        if (hpBhvr == null) {
          hpBhvr = gobj.AddComponent<HeroPlaybackBhvr> ();
        }
        hpBhvr.Anim = subLevelRecord.defenseAnims[hBhvr.hero.id];
        frameIndexChangeHandler += hpBhvr.OnFrameIndexChange;
      }

      playing = true;

    }

    /// <summary>
    /// 离开子关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnLeftSubLevel (object sender, EventArgs args)
    {

      playing = false;

      foreach (GameObject gobj in attackGobjs) {
        HeroPlaybackBhvr hpBhvr = gobj.GetComponent<HeroPlaybackBhvr> ();
        frameIndexChangeHandler -= hpBhvr.OnFrameIndexChange;
      }

      foreach (GameObject gobj in defenseGobjs) {
        HeroPlaybackBhvr hpBhvr = gobj.GetComponent<HeroPlaybackBhvr> ();
        frameIndexChangeHandler -= hpBhvr.OnFrameIndexChange;
      }

    }

  }
}

