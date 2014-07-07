using System;
using UnityEngine;
using TP = TangLevel.Playback;
using System.Collections.Generic;


namespace TangLevel
{
  public class RecorderBhvr : MonoBehaviour
  {

    public const int FRAME_RATE = 24;

    public static int frameIndex = 0;

    private bool recording = false;
    private TP.LevelRecord record; // 当前关卡录像
    private TP.SubLevelRecord subLevelRecord; // 当前子关卡录像
    private List<GameObject> attackGobjs = new List<GameObject>();
    private List<GameObject> defenseGobjs = new List<GameObject>();
    private float time;
    private float secondsPerFrame  = 1F/FRAME_RATE;

    // 测试用
    private static int recordCounter = 1;


    void Start ()
    {

      time = 0;
      frameIndex = 0;

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
        time += Time.deltaTime;
        frameIndex = (int)(time / secondsPerFrame);
      }
    }

    /// <summary>
    /// 进入关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnEnterLevelSuccess (object sender, EventArgs args)
    {
      record = new TP.LevelRecord (recordCounter++);
      record.attackGroup = LevelContext.attackGroup.DeepCopy();
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

      //string jsontext = Newtonsoft.Json.JsonConvert.SerializeObject (record);

      //Debug.Log (jsontext);
    }

    /// <summary>
    /// 进入子关卡
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnEnterSubLevel (object sender, EventArgs args)
    {
      // 子关卡录像初始化
      recording = true;
      time = 0;
      frameIndex = 0;

      // 创建当前子关卡录像
      subLevelRecord = new TP.SubLevelRecord ();
      // 子关卡背景
      subLevelRecord.bg = LevelContext.CurrentSubLevel.resName;
      // 子关卡防守小组
      subLevelRecord.defenseGroup = LevelContext.CurrentSubLevel.defenseGroup.DeepCopy ();

      attackGobjs.Clear ();
      foreach (GameObject g in LevelContext.AliveSelfGobjs) {
        attackGobjs.Add (g);
      }
      defenseGobjs.Clear ();
      foreach (GameObject g in LevelContext.AliveDefenseGobjs) {
        defenseGobjs.Add (g);
      }

      // 对每一个攻方英雄进行录像
      foreach (GameObject heroGobj in attackGobjs) {
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr == null) {
          hrBhvr = heroGobj.AddComponent<HeroRecordBhvr> ();
        }
      }

      // 对每一个守方英雄进行录像
      foreach (GameObject heroGobj in defenseGobjs) {
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

      // 获取每一个英雄的录像 ---

      // 攻方英雄
      Debug.Log ("attackGobjs.Count:"+attackGobjs.Count);
      foreach (GameObject heroGobj in attackGobjs) {
        HeroBhvr heroBhvr = heroGobj.GetComponent<HeroBhvr> ();
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr.Anim != null) {
          hrBhvr.EndRecord ();
          subLevelRecord.attackerAnims.Add (heroBhvr.hero.id, hrBhvr.Anim);
        }
      }

      // 守方英雄
      Debug.Log ("defenseGobjs.Count:"+defenseGobjs.Count);
      foreach (GameObject heroGobj in defenseGobjs) {
        HeroBhvr heroBhvr = heroGobj.GetComponent<HeroBhvr> ();
        HeroRecordBhvr hrBhvr = heroGobj.GetComponent<HeroRecordBhvr> ();
        if (hrBhvr.Anim != null) {
          hrBhvr.EndRecord ();
          subLevelRecord.defenseAnims.Add (heroBhvr.hero.id, hrBhvr.Anim);
        }
      }

      record.subLevelRecords.Add (subLevelRecord);
    }
  }
}

