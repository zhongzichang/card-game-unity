using System;
using System.Collections.Generic;

namespace TangLevel.Playback.Adv
{
  public class Timeline
  {

    private List<Frame> frames = new List<Frame> ();

    public void AddFrame (Frame frame)
    {
      frames.Add (frame);
    }

  }
}

