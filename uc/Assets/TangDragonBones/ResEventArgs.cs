using System;

namespace TangDragonBones
{
  public class ResEventArgs : EventArgs
  {
    public string Name {
      get;
      private set;
    }
    public ResEventArgs (string name) : base()
    {
      this.Name = name;
    }
  }
}

