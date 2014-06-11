using System;
using System.Collections;

namespace TangLevel
{
  /* 减少X点魔法伤害，负数表示增加受到的伤害。10000表示免疫。*/
  [EffectAttribute(CODE)]
  public class DescMagicEffect : Effect
  {
    public const int CODE = 9;
    public DescMagicEffect(): base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

