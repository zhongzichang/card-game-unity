using System;
using System.Collections;
using UnityEngine;

namespace TangLevel
{
  /* 改变角色贴图颜色 */
  [EffectAttribute(CODE)]
  public class ChangeColorEffect: Effect
  {
    public const int CODE = 22;

    public ChangeColorEffect() : base(CODE) {

    }

    public static void Arise (Effect effect, EffectorWrapper w)
    {

      Color color = (Color) effect.paramList [0];

      GameObject target = w.target;

      target.GetComponent<MaterialBhvr> ().color = color;

    }
  }
}

