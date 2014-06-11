using System;
using System.Collections;

namespace TangLevel
{
  [EffectAttribute(CODE)]
  public class ChangeEvaEffect : Effect
  {
    public const int CODE = 25;

    public ChangeEvaEffect() : base(CODE) {

    }

    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

