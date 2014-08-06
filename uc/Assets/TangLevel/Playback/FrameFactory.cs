using System;
using TLP = TangLevel.Playback;
namespace TangLevel.Playback
{
  public class FrameFactory
  {
    public static Frame<TLP.Action> NewActionFrame (int index, HeroStatus status)
    {
      Frame<TLP.Action> actionFrame = null;
      switch (status) {
      case HeroStatus.dead:
        actionFrame = new TLP.Frame<TLP.Action> (index, new TLP.DeadAction ());
        break;
      case HeroStatus.idle:
        actionFrame = new TLP.Frame<TLP.Action> (index, new TLP.IdleAction ());
        break;
      case HeroStatus.vertigo:
        actionFrame = new TLP.Frame<TLP.Action> (index, new TLP.VertigoAction ());
        break;
      case HeroStatus.beat:
        actionFrame = new TLP.Frame<TLP.Action> (index, new TLP.BeatAction ());
        break;
      }
      return actionFrame;
    }

  }
}

