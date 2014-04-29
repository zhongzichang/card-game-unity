using System;

namespace TangLevel
{
  public class Effect
  {

    public int type;
    public int times;

    public Effect(){
    }

    public Effect (int type, int times)
    {
      this.type = type;
      this.times = times;
    }
  }
}

