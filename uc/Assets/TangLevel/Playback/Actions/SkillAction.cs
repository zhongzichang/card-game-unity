using System;

namespace TangLevel.Playback
{
  public class SkillAction : Action
  {
    public const HeroStatus STATUS = HeroStatus.charge;

    public int skillId;
    public int targetId;

    public SkillAction (int skillId, int targetId) : base(STATUS)
    {
      this.skillId = skillId;
      this.targetId = targetId;
    }
  }
}

