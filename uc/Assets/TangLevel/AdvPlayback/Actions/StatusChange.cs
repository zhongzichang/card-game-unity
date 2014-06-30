using System;

namespace TangLevel.Playback.Adv
{
  public class StatusChange : Action
  {
    public int heroId;
    public HeroStatus status;

    public StatusChange(int heroId, HeroStatus status){
      this.heroId = heroId;
      this.status = status;
    }

    public override string ToString ()
    {
      return string.Format ("[StatusChange{heroId:"+heroId+";status:"+status+";}]");
    }
   
  }
}

