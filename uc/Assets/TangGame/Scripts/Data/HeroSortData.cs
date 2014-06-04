using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
  [XmlRoot ("root")]
  [XmlLate ("hero_sort")]
  public class HeroSortData
  {
    [XmlElement ("value")]
    public List<int> items = new List<int> ();

    public static void LateProcess (object obj)
    {
      HeroSortData root = obj as HeroSortData;
      int count = 0;
      foreach (int item in root.items) {
        Config.heroSortTable [item] = count;
        count++;
      }
    }
  }
}