using System;
using UnityEngine;

namespace TangLevel
{
  [Serializable]
  public class Effector
  {

    #region Self
    // -- 自身属性 --
    public int times;
    public float timeSpan;
    public float rate;
    public int type;
    public int radius;
    public string specialName;

    public Effector[] subEffectors;

    #endregion


  }
}

