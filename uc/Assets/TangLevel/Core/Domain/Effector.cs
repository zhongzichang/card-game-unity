﻿using System;
using UnityEngine;

namespace TangLevel
{
  public class Effector
  {

    #region Self
    // -- 自身属性 --
    public string specialName;

    public Effector[] subEffectors;

    #endregion

    #region Runtime
    // -- 运行时属性 --
    public Skill skill;
    #endregion

  }
}
