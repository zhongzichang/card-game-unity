using System;

namespace TangLevel.Playback
{
  public class HeroAnimation
  {

    public int heroId;

    public Timeline<HeroStatus> statusTimeline = new Timeline<HeroStatus>();
    public Timeline<float> posxTimeline = new Timeline<float>();
    public Timeline<Action> actionTimeline = new Timeline<Action> ();
    public Timeline<RunAction> runActionTimeline = new Timeline<RunAction> ();
    public Timeline<int> hpTimeline = new Timeline<int> ();
    public Timeline<int> mpTimeline = new Timeline<int> ();
    public Timeline<SkillAction> skillActionTimeline = new Timeline<SkillAction> ();

    public HeroAnimation (int heroId)
    {
      this.heroId = heroId;
    }
  }
}

