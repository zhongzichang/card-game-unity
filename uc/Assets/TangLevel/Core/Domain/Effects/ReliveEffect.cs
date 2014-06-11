using System;
using System.Collections;

namespace TangLevel
{
  /* 死亡后3秒，可以复活，参数为复活后的恢复的血量。 */
  [EffectAttribute(CODE)]
  public class ReliveEffect : Effect
  {
    public const int CODE = 15;
    public ReliveEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

