using System;
using System.Collections;

namespace TangLevel
{
  /* 改变角色贴图颜色 */
  [EffectAttribute(CODE)]
  public class ChangeColorEffect: Effect
  {
    public const int CODE = 22;

    public ChangeColorEffect() : base(CODE) {

    }

    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

