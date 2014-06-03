using System;

namespace TangLevel
{
  /* 参数为百分比，同时提高／降低对象当前的施法前摇，后摇，技能的冷却时间。负数表示减速。*/
  public class AttackSpeedEffect : Effect
  {
    public const int TYPE = 13;
    public override void Arise ()
    {
    }
  }
}

