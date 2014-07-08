using System;

namespace TangLevel.Playback
{
  public class HeroAnimation
  {

    public int heroId;

    public Timeline<HeroStatus> statusTimeline = new Timeline<HeroStatus>();
    public Timeline<float> posxTimeline = new Timeline<float>();
    public Timeline<Action> actionTimeline = new Timeline<Action> ();

    public HeroAnimation (int heroId)
    {
      this.heroId = heroId;
    }
  }
}

