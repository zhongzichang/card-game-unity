using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using TangGame.UI;

namespace TangGame.Xml
{

  public class NameData {

    public string name1;
	  public string name2;
  }

  [XmlRoot ("root")]
  [XmlLate("name")]
  public class NameRoot
  {
    [XmlElement ("value")]
    public List<NameData> items = new List<NameData> ();
    
    public static void LateProcess (object obj)
    {
      NameRoot root = obj as NameRoot;
      foreach (NameData item in root.items) {
        NameCache.instance.Parse(item);
        break;
      }
    }
  }
}