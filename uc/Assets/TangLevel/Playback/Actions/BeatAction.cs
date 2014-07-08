using System;

namespace TangLevel.Playback
{
  public class BeatAction : Action
  {

    public const HeroStatus STATUS = HeroStatus.beat;

    public BeatAction () : base(STATUS)
    {
    }
  }
}

