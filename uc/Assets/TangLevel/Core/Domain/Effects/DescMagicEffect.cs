using System;

namespace TangLevel
{
  /* 减少X点魔法伤害，负数表示增加受到的伤害。10000表示免疫。*/
  public class DescMagicEffect : Effect
  {
    public const int TYPE = 9;

    public override void Arise ()
    {
      throw new NotImplementedException ();
    }
  }
}

