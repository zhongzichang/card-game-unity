using System;
using System.Collections;

namespace TangLevel
{
  /* 眩晕，参数为概率，10000表示100% */
  [EffectAttribute(CODE)]
  public class VertigoEffect : Effect
  {
    public const int CODE = 10;
    public VertigoEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

