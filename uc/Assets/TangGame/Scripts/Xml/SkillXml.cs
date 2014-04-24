using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class SkillXml
	{
//		<!-- 技能id -->
//		<id>101</id>
		public int id;
//		<!-- 技能名字 -->
//		<name>无敌旋风</name>
		public string name;
//		<!-- 触发类型 -->
//		<trigger_type></trigger_type>
		public short trigger_type;
//		<!-- 目标类型 -->
//		<target_type></target_type>
		public short target_type;
//		<!-- 范围类型 -->
//		<range_type></range_type>
		public short range_type;
//		<!-- 最大等级 -->
//		<lv_max></lv_max>
		public int lv_max;
//		<!-- 技能图标 -->
//		<icon_name></icon_name>
		public string icon_name;
	}
	[XmlRoot ("root")]
	[XmlLate("skill")]
	public class SkillRoot
	{
		[XmlElement ("value")]
		public List<SkillXml> items = new List<SkillXml> ();

		public static void LateProcess (object obj)
		{
			SkillRoot root = obj as SkillRoot;
			foreach (SkillXml item in root.items) {
				Config.skillXmlTable [item.id] = item;
			}
		}
	}
}

