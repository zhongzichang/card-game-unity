using System;
using System.Collections;

namespace TangLevel
{
  /* 参数为百分比，提高／降低移动速度。负数表示减速。 */
  [EffectAttribute(CODE)]
  public class SpeedEffect : Effect
  {
    public const int CODE = 12;
    public SpeedEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

