﻿using System;
using UnityEngine;

namespace TangLevel
{
  public class UIManager : MonoBehaviour
  {
    // 血量槽
    public FillMonitor[] greenHpMonitors;
    public FillMonitor[] redHpMonitors;

    // 血条位置
    public PositionMonitor[] greenHpPosMonitors;
    public PositionMonitor[] redHpPosMonitors;

    // 血条显示与隐藏
    public DisplayByHurt[] greenDisplayByHurts;
    public DisplayByHurt[] redDisplayByHurts;

  }
}

