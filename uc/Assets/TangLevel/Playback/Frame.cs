using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel.Playback
{
  public class Frame
  {
    public int duration;
    public List<Update> updates = new List<Update>();
  }
}

