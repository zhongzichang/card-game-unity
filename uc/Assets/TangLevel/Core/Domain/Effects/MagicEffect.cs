using System;

namespace TangLevel
{
  /*
   * {技能系数，升级提升参数}，通常由技能表para 传入。
魔法伤害公式：
魔法伤害＝魔法强度＊技能系数＋升级提升参数＊技能等级
最终伤害＝魔法伤害＊(1-0.01*魔抗／（1+0.01*魔抗）)
*/
  public class MagicEffect : Effect
  {
    public const int TYPE = 2;

    public override void Arise ()
    {
      throw new NotImplementedException ();
    }
  }
}

