using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{

  public class HeroSortData{
    public int location;
    public int hero_id;
  }

  [XmlRoot ("root")]
  [XmlLate ("hero_sort")]
  public class HeroSortRoot
  {
    [XmlElement ("value")]
    public List<HeroSortData> items = new List<HeroSortData> ();

    public static void LateProcess (object obj)
    {
      HeroSortRoot root = obj as HeroSortRoot;
      foreach (HeroSortData item in root.items) {
        Config.heroSortTable [item.hero_id] = item.location;
      }
    }
  }
}