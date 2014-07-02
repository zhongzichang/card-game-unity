using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel.Playback
{
  [Serializable]
  public class LevelRecord
  {

    public int frameRate = 24; // 每秒多少帧
    public string version = "1.0.0";

    // ID
    public int id;

    // 关卡ID
    public int levelId;

    // 子关卡录像
    public List<SubLevelRecord> subLevelRecords = new List<SubLevelRecord>();

    public LevelRecord(int id, int levelId){
      this.id = id;
      this.levelId = levelId;
    }

  }
}

