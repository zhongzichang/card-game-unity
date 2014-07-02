﻿using System;
using UnityEngine;
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class RecorderBhvr : MonoBehaviour
  {

    //private TP.LevelRecorder recorder;
    public static int frameIndex = 0;
    private int lastKeyFrameIndex = 0;
    private int keyFrameCounter = 0;
    private bool recording = false;
    private TP.LevelRecord record;

    // 测试用
    private static int recordCounter = 1;

    void Start ()
    {

      //recorder = new TP.LevelRecorder ();
      frameIndex = 0;
      lastKeyFrameIndex = 0;

      // 进入和离开关卡
      LevelController.RaiseEnterLevelSuccess += OnEnterLevelSuccess;
      LevelController.RaiseLeftLevel += OnLeftLevel;
      // 进入和离开子关卡
      LevelController.RaiseEnterSubLevel += OnEnterSubLevel;
      LevelController.RaiseLeftSubLevel += OnLeftSubLevel;

    }

    void Update ()
    {
      if (recording) {
        frameIndex++;
      }
    }

    /// <summary>
    /// 进入关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnEnterLevelSuccess (object sender, EventArgs args)
    {

      record = new TP.LevelRecord (recordCounter++, LevelContext.CurrentLevel.id);

    }


    /// <summary>
    /// 离开关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnLeftLevel (object sender, EventArgs args)
    {
      // 保存录像
      Cache.recordTable.Add (record.id, record);

      Debug.Log ("Cache.recordTable.Count:"+Cache.recordTable.Count);

      string jsontext = Newtonsoft.Json.JsonConvert.SerializeObject (record);

      Debug.Log (jsontext);
    }

    /// <summary>
    /// 进入子关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnEnterSubLevel (object sender, EventArgs args)
    {
      // 子关卡录像初始化
      frameIndex = 0;
      keyFrameCounter = 0;
      recording = true;

      // 对每一个攻方英雄进行录像
      foreach (GameObject heroGobj in LevelContext.AliveSelfGobjs) {
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr == null) {
          hrBhvr = heroGobj.AddComponent<HeroRecordBhvr> ();
        }
      }

      // 对每一个守方英雄进行录像
      foreach (GameObject heroGobj in LevelContext.AliveDefenseGobjs) {
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr == null) {
          hrBhvr = heroGobj.AddComponent<HeroRecordBhvr> ();
        }
      }

    }

    /// <summary>
    /// 离开子关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnLeftSubLevel (object sender, EventArgs args)
    {
      recording = false;

      TP.SubLevelRecord subLevelRecord = new TP.SubLevelRecord ();

      // 获取每一个英雄的录像
      foreach (GameObject heroGobj in LevelContext.attackGobjs) {
        HeroBhvr heroBhvr = heroGobj.GetComponent<HeroBhvr> ();
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr.Anim != null) {
          subLevelRecord.heroAnims.Add (heroBhvr.hero.id, hrBhvr.Anim);
        }
      }

      foreach (GameObject heroGobj in LevelContext.defenseGobjs) {
        HeroBhvr heroBhvr = heroGobj.GetComponent<HeroBhvr> ();
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr.Anim != null) {
          subLevelRecord.heroAnims.Add (heroBhvr.hero.id, hrBhvr.Anim);
        }
      }

      record.subLevelRecords.Add (subLevelRecord);
    }
  }
}

