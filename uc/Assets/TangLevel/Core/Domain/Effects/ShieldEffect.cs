using System;
using System.Collections;

namespace TangLevel
{
  /*{类型，技能系数，升级提升参数}，类型分为4类：
1.吸收物理伤害。
2.吸收魔法伤害。
3.吸收任意伤害。
4.吸收伤害并回血
吸收伤害达到限额后，盾会失效。吸收具体的值与魔法伤害效果算法相同，通常由技能表para 传入。*/
  [EffectAttribute(CODE)]
  public class ShieldEffect : Effect
  {
    public const int CODE = 19;
    public ShieldEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

