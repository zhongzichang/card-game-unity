using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{

  public class DailyData {

    public int id;
    public string name;
    /// 条件
    public string condition;
    /// 开始等级
    public string  level;
    /// 计数器
    public int count;
    /// 开始，结束时间
    public string time;
    /// 战队经验
    public int exp;
    /// 金钱
    public int moeny;
    /// 元宝
    public int ingot;
    /// 体力
    public int vitality;
    /// 物品奖励
    public string award;
    /// 前往打开面板的类型
    public int openPanelType;
    /// 前往打开面板的传递值
    public string openPanelValue;
  }

  [XmlRoot ("root")]
  [XmlLate("daily")]
  public class DailyRoot
  {
    [XmlElement ("value")]
    public List<DailyData> items = new List<DailyData> ();
    
    public static void LateProcess (object obj)
    {
      DailyRoot root = obj as DailyRoot;
      foreach (DailyData item in root.items) {
        Config.dailyTable[item.id] = item;
      }
    }
  }
}