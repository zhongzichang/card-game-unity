using System;

namespace TangLevel.Playback
{
  public class VertigoAction : Action
  {
    public const HeroStatus STATUS = HeroStatus.vertigo;

    public VertigoAction () : base(STATUS)
    {
    }
  }
}

