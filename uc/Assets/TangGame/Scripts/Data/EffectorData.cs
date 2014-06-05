using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace TangGame.Xml
{
  public class EffectorData
  {
    public int id; // 特效ID
    public int times; // 作用次数
    public int loop_time; // 时间间隔
    public int probability; // 概率
    public int type; // 类型
    public int radius; // 范围半径
    public string special_effect; // 特效资源名称
    public string effect_ids; // 作用效果IDs
  }


  [XmlRoot ("root")]
  [XmlLate("effectors")]
  public class EffectorRoot
  {
    [XmlElement ("value")]
    public List<EffectorData> items = new List<EffectorData> ();

    public static void LateProcess (object obj)
    {
      EffectorRoot root = obj as EffectorRoot;
      foreach (EffectorData item in root.items) {
        Config.effectorXmlTable [item.id] = item;
      }
    }
  }
}

