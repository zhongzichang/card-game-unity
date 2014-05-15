// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace TangLevel
{
  [Serializable]
  public class Group
  {

    public Hero[] heros;

    public List<Hero> aliveHeros = new List<Hero>();

    #region Constructor

    public Group ()
    {
    }

    #endregion


    #region PublicMethods

    /// <summary>
    /// Arrange this instance.
    /// </summary>
    public void Arrange(){
      aliveHeros.Clear ();
      for (int i = 0; i < heros.Length; i++) {
        if (heros [i].hp > 0) {
          aliveHeros.Add (heros [i]);
        }
      }
    }

    public Group ShallowCopy ()
    {
      return (Group)this.MemberwiseClone ();
    }

    public Group DeepCopy ()
    {
      Group other = (Group)this.MemberwiseClone ();

      other.heros = new Hero[heros.Length];
      for (int i = 0; i < heros.Length; i++) {
        other.heros [i] = heros [i].ShallowCopy ();
      }

      return other;
    }

    #endregion

  }
}

