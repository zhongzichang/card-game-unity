using System.Xml;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using TangUtils;
using TangGame.UI;

namespace TangGame.Xml
{
	public class MapData
	{ 
		/// 地图编号
		public int id;
    /// 章节
    public int index;
		/// 开放等级限制
		public int team_lv_constraint;
		/// 类型
		public int type;
		/// 地图名称
		public string name;
		/// 地图美术资源
		public string resource;

    [XmlRoot ("root")]
    [XmlLate ("map")]
    public class MapRoot
    {
      [XmlElement ("value")]
      public List<MapData> items = new List<MapData> ();
      
      public static void LateProcess (object obj)
      {
        MapRoot root = obj as MapRoot;
        foreach (MapData item in root.items) {
          Config.mapXmlTable[item.id] = item;
        }
      }
    }
	}
}