using System;
using System.Collections;

namespace TangLevel
{
  /* 减少X点物理伤害，负数表示增加受到的伤害。10000表示免疫。 */
  [EffectAttribute(CODE)]
  public class DescPhysicalEffect : Effect
  {

    public const int CODE = 8;
    public DescPhysicalEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

