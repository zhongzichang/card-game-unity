using System;

namespace TangLevel.Playback
{
  public class DeadAction : Action
  {
    public const HeroStatus STATUS = HeroStatus.dead;

    public DeadAction () :base (STATUS)
    {
    }
  }
}

