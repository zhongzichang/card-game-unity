using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class Level
  {
    // -- 关卡属性 --

    #region Level Attributes

    public int id;
    public string name;
    public SubLevel[] subLeves;

    #endregion

    // -- 场景属性 --

    #region Scene Attributes

    #endregion

    #region Constructor

    public Level ()
    {
    }

    public Level (int id, string name)
    {
      this.id = id;
      this.name = name;
    }

    #endregion

    #region PublicMethods

    public Level ShallowCopy ()
    {
      return (Level)this.MemberwiseClone ();
    }

    public Level DeepCopy ()
    {
      Level other = (Level)this.MemberwiseClone ();

      other.subLeves = new SubLevel[ subLeves.Length ];

      return other;
    }

    #endregion
  }
}