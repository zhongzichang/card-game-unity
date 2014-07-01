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

    [NonSerialized]
    public List<GameObject> attackGroup; // 进攻方
    [NonSerialized]
    public List<GameObject> defenseGroup; // 防守方

    // ID
    public int id;
    // 关卡
    public Level level;
    // 英雄动画
    public Dictionary<int, HeroAnimation> heroAnims = new Dictionary<int, HeroAnimation>();


  }
}

