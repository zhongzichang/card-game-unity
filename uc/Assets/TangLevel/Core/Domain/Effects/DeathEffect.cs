using System;
using System.Collections;

namespace TangLevel
{
  /* 参数为系数，伤害值为对方损失的生命值＊系数。*/
  [EffectAttribute(CODE)]
  public class DeathEffect : Effect
  {
    public const int CODE = 18;

    public DeathEffect(): base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

