using System;
using System.Collections;

namespace TangLevel
{
  /* 参数为概率，10000表示100%。被魅惑的目标会转而攻击友方。在ai控制下，不会施放任何技能。*/
  [EffectAttribute(CODE)]
  public class CharmEffect : Effect
  {
    public const int CODE = 11;
    public CharmEffect(): base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

