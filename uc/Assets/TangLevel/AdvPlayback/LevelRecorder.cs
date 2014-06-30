using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel.Playback.Adv
{
  public class LevelRecorder
  {

    private LevelRecord record = null;
    private bool recording = false;

    public bool IsRecording {
      get {
        return recording;
      }
    }

    public static LevelRecorder NewInstance ()
    {
      LevelRecorder r = new LevelRecorder ();
      return r;
    }

    /// <summary>
    /// 防止外部通过这个构造器实例化
    /// </summary>
    private LevelRecorder ()
    {
    }

    public void Start (List<GameObject> attackGroup, List<GameObject> defenseGroup, Level level)
    {
      Debug.Log ("recorder.start");
      record = new LevelRecord ();
      record.attackGroup = attackGroup;
      record.level = level;
      recording = true;
    }

    public void Stop ()
    {
      Debug.Log ("recorder.stop");
      recording = false;
    }

    public void Save ()
    {
      Cache.advRecordTable.Add (record.id, record);
    }

    public void Pause ()
    {
      recording = false;
    }

    public void Resume ()
    {
      recording = true;
    }

    public void AddKeyFrame (Frame frame)
    {
      if (recording) {
        record.timeline.AddFrame (frame);
      }
    }

  }
}