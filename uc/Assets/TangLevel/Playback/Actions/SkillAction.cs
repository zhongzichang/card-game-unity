using System;

namespace TangLevel.Playback
{
  public class SkillAction : Action
  {
    public const HeroStatus STATUS = HeroStatus.charge;

    public int skillId;

    public SkillAction (int skillId) : base(STATUS)
    {
      this.skillId = skillId;
    }
  }
}

