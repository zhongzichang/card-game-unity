using System;

namespace TangLevel.Playback
{
  public class RunAction : Action
  {

    public const HeroStatus STATUS = HeroStatus.running;

    public float startx;
    public float stopx;

    public RunAction (float startx) : base(STATUS)
    {
      this.startx = startx;
    }

  }
}

