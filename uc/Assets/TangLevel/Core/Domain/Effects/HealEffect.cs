using System;
using System.Collections;

namespace TangLevel
{
  /*同魔法伤害，{技能系数，升级提升参数}，通常由技能表para 传入。治疗效果不会超出最大生命值。*/
  [EffectAttribute(CODE)]
  public class HealEffect : Effect
  {
    public const int CODE = 4;
    public HealEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

