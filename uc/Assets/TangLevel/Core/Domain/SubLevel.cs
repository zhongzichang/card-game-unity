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
namespace TangLevel
{
  [Serializable]
  public class SubLevel
  {
    public int id;

    public int index;

    public string resName;

    public Group defenseGroup;

    public SubLevel ()
    {

    }


    #region PublicMethods

    public SubLevel ShallowCopy ()
    {
      return (SubLevel)this.MemberwiseClone ();
    }

    public SubLevel DeepCopy ()
    {
      SubLevel other = (SubLevel)this.MemberwiseClone ();

      other.defenseGroup = defenseGroup.DeepCopy ();

      return other;
    }

    #endregion
  }
}
