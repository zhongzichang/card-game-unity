using System;

namespace TangLevel.Playback
{
  public class IdleAction : Action
  {
    public const HeroStatus STATUS = HeroStatus.idle;

    public IdleAction () : base(STATUS)
    {
    }
  }
}

