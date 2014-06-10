using System;
using System.Collections;

namespace TangLevel
{
  [EffectAttribute(CODE)]
  public class EliminationEffect : Effect
  {
    public const int CODE = 26;
    public EliminationEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

