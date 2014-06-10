using System;
using System.Collections;

namespace TangLevel
{
  /*
   * 普通攻击默认命中率为100%
改变命中后，普通攻击命中率＝（命中值－闪避值）／100，命中率大于1表示一定会命中。
技能命中率见做作用器的概率属性。*/
  [EffectAttribute(CODE)]
  public class ChangHitEffect : Effect
  {
    public const int CODE = 24;
    public ChangHitEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }
  }
}

