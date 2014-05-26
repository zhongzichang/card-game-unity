
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class MapData
	{ 
		/// 地图编号
		public int id;
		/// 开放等级限制
		public int team_lv_constraint;
		/// 类型
		public int type;
		/// 地图名称
		public string name;
		/// 地图美术资源
		public string resource;
	}
}