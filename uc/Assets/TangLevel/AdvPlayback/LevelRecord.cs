using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel.Playback.Adv
{
  public class LevelRecord
  {
    public int id;
    public int frameRate = 24; // 每秒多少帧
    public List<GameObject> attackGroup; // 进攻方
    public List<GameObject> defenseGroup; // 防守方
    public Level level; // 防守方
    public Timeline timeline = new Timeline(); // 时间线
    public string version = "1.0.0";

  }
}

