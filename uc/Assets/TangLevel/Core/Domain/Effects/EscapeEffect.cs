using System;
using System.Collections;

namespace TangLevel
{
  /* 参数为毫秒，让一个目标完全脱离战场。*/
  [EffectAttribute(CODE)]
  public class EscapeEffect : Effect
  {
    public const int  CODE = 16;
    public EscapeEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

