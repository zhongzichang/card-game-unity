using System;

namespace TangLevel.Playback
{
  public class Action
  {

    public HeroStatus status = HeroStatus.none;

    public Action (HeroStatus status)
    {
      this.status = status;
    }

  }
}

