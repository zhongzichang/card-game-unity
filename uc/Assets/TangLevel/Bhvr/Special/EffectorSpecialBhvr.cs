using System;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using TG = TangGame;

namespace TangLevel
{
  public abstract class EffectorSpecialBhvr : SpecialBhvr
  {

    public static readonly Vector3 HURT_TEXT_OFFSET = new Vector3 (0, 160, 0);

    public EffectorWrapper w;

    /// <summary>
    /// 命中目标
    /// </summary>
    public void Hit ()
    {

      if (w.effector.effect != null) {
        EffectEjector.Instance.Arise (w.effector.effect, w);
      }

    }
  }
}

