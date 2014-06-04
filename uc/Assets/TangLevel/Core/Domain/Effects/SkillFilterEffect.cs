using System;

namespace TangLevel
{
  /* {类型，技能1，技能2...}。
类型1，表示无法施放这些编号的技能。
类型2，表示无法施放除这些编号以外的技能。*/
  public class SkillFilterEffect : Effect
  {
    public const int TYPE = 7;

    public override void Arise ()
    {
      throw new NotImplementedException ();
    }

  }
}

