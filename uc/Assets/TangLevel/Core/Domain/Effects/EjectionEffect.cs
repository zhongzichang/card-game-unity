using System;

namespace TangLevel
{
  /* 参数为次数，表示作用器弹射的长度。
   * 弹射规则为，寻找离自己最近的下一个（如果之前是敌方，下一个目标也是敌方，如果之前目标是友方，那下一个目标也是友方）。*/
  public class EjectionEffect : Effect
  {

    public const int TYPE = 5;

    public override void Arise ()
    {
      throw new NotImplementedException ();
    }

  }
}

