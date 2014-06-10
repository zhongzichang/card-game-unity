using System;
using System.Collections;

namespace TangLevel
{
  /*
   * {属性类型，类型，值}，
属性类型包括全部基础属性，当前能量，当前血量。
类型表示是按百分比，还是绝对值。
值为具体值，可能由技能表para传入。
*/
  [EffectAttribute(CODE)]
  public class IncrePropEffect : Effect
  {

    public const int CODE = 3;
    public IncrePropEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

