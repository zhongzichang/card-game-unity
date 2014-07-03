using System;
using System.Collections.Generic;
using UnityEngine;
using TangUtils;

namespace TangLevel.Playback
{
  [Serializable]
  public class LevelRecord
  {

    public int frameRate = 24;
    // 每秒多少帧
    public string version = "1.0.0";

    // ID
    public int id;

    // 子关卡录像
    public List<SubLevelRecord> subLevelRecords = new List<SubLevelRecord> ();

    public LevelRecord (int id)
    {
      this.id = id;
    }


    /// <summary>
    /// 从 LevelRecord 中生成 Level
    /// </summary>
    /// <returns>The level.</returns>
    /// <param name="record">Record.</param>
    public static Level MakeLevel (LevelRecord record)
    {


      Level level = new Level ();
      List<SubLevel> subLevels = new List<SubLevel> ();
      int indexCounter = 0;

      // 生成攻击小组

      Debug.Log ("record.subLevelRecords [0].attackerAnims:" + record.subLevelRecords [0].attackerAnims.Count);
      int[] attackerIds = CollectionUtil.KeysToArray (record.subLevelRecords [0].attackerAnims);
      Group attackerGroup = DomainHelper.GetInitGroup (attackerIds);

      foreach (SubLevelRecord subRecord in record.subLevelRecords) {

        // 生成防守小组
        int[] defenseIds = CollectionUtil.KeysToArray (subRecord.defenseAnims);
        //Group defenseGroup = DomainHelper.GetInitGroup (defenseIds);
        Group defenseGroup = DomainHelper.BuildMonsterGroup (defenseIds);

        SubLevel subLevel = new SubLevel ();
        subLevel.index = indexCounter; // 索引
        subLevel.resName = subRecord.bg; // 资源
        subLevel.defenseGroup = defenseGroup; // 防守小组

        subLevels.Add (subLevel);

        indexCounter++;
      }
      level.subLevels = subLevels;

      return level;

    }

  }
}

