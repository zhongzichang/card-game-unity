using System;
using System.Collections;

namespace TangLevel
{
  /* 更改目标的显示模型 */
  [EffectAttribute(CODE)]
  public class ChangeModelEffect : Effect
  {
    public const int CODE = 23;
    public ChangeModelEffect() : base(CODE) {

    }

    // 作用开始
    public static void Arise (Effect effect, EffectorWrapper w)
    {

    }

  }
}

