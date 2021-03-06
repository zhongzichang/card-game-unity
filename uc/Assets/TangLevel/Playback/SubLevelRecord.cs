﻿using System;
using System.Collections.Generic;

namespace TangLevel.Playback
{
  public class SubLevelRecord
  {

    /// <summary>
    /// 背景图片
    /// </summary>
    public string bg;

    public Group defenseGroup;

    // 攻击方英雄动画
    public Dictionary<int, HeroAnimation> attackerAnims = new Dictionary<int, HeroAnimation>();

    // 防守方英雄动画
    public Dictionary<int, HeroAnimation> defenseAnims = new Dictionary<int, HeroAnimation>();

    // 文字动画
    public TextAnimation textAnimation;

  }
}

