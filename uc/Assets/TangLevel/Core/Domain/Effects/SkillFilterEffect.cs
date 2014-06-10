using System;
using System.Collections;

namespace TangLevel
{
  /* {类型，技能1，技能2...}。
类型1，表示无法施放这些编号的技能。
类型2，表示无法施放除这些编号以外的技能。*/
  [EffectAttribute(CODE)]
  public class SkillFilterEffect : Effect
  {
    public const int CODE = 7;
    public SkillFilterEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {
    }

  }
}

