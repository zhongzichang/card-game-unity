using System;

namespace TangLevel
{
  /*同魔法伤害，{技能系数，升级提升参数}，通常由技能表para 传入。治疗效果不会超出最大生命值。*/
  public class HealEffect : Effect
  {
    public const int TYPE = 4;

    public override void Arise ()
    {
      throw new NotImplementedException ();
    }
  }
}

