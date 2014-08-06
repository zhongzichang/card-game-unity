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
      // 回放时不能修改英雄数据
      if (w.effector.effect != null
          && !LevelContext.isPlayback) {
        EffectEjector.Instance.Arise (w.effector.effect, w);
      }

    }
  }
}

