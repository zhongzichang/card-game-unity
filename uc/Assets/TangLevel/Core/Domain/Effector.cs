﻿using System;
using UnityEngine;

namespace TangLevel
{
  [Serializable]
  public class Effector
  {

    #region Self
    // -- 自身属性 --
    public int id;
    public int times; // 作用次数
    public float timeSpan; // 时间间隔
    public float probability; // 概率
    public int type; // 类型
    public int radius; // 作用范围
    public string specialName;
    public Effect effect; // 效果
    public string[] scripts; // 脚本，该作用器需要悬挂的脚本
    #endregion

    #region Runtime
    public Effector[] subEffectors; // 子作用器
    #endregion

  }
}

