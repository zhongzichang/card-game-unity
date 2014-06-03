using System;

namespace TangLevel
{
  /* {伤害作用器编号，百分比}
与另一个纯粹用作伤害的作用器配合使用。表示在该作用器产生伤害时，按一个百分比恢复自身生命值。*/
  public class SuckBloodEffect : Effect
  {
    public const int TYPE = 14;

    public override void Arise ()
    {
    }
  }
}

