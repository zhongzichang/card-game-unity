using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel.Playback
{
  public class Frame<T>
  {
    public int index;
    public T val;

    public Frame(int index, T val){
      this.index = index;
      this.val = val;
    }
  }
}

