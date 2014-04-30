using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using TangGame.UI.Base;

namespace TangGame.Xml
{
	public class PropsXml
	{
		//		<!-- 物品编号 -->
		//		<id>1001</id>
		public int id;
		//		<!-- 物品名称 -->
		//		<name>牛顿的不粘锅</name>
		public string name;
		//		<!-- 物品类型，装备碎片卷轴 -->
		//		<type>1</type>
		public short type;
		/// <summary>
		/// The upgrade.
		/// 道具的品质
		/// </summary>
		public short upgrade;
		//		<!-- 属性加成 -->
		//		<!-- 力量 -->
		//		<strength>21</strength>
		public int strength;
		//		<!-- 智力 -->
		//		<intellect>42</intellect>
		public int intellect;
		//		<!-- 敏捷 -->
		//		<agile>2</agile>
		public int agile;
		//		<!-- 生命最大 -->
		//		<hpMax>132</hpMax>
		public int hpMax;
		//		<!-- 攻击强度 -->
		//		<attack_damage>23</attack_damage>
		public int attack_damage;
		//		<!-- 法术强度 -->
		//		<spell_power>123</spell_power>
		public int spell_power;
		//		<!-- 物理防御 -->
		//		<physical_defense>321</physical_defense>
		public int physical_defense;
		//		<!-- 法术防御 -->
		//		<spell_defense>123</spell_defense>
		public int spell_defense;
		//		<!-- 物理爆击 -->
		//		<physical_crit>12</physical_crit>
		public int physical_crit;
		//		<!-- 法术爆击 -->
		//		<spell_crit>21</spell_crit>
		public int spell_cirt;
		//		<!-- 生命回复 -->
		//		<hp_re>12</hp_re>
		public int hp_re;
		//		<!-- 能量回复 -->
		//		<energy_re>21</energy_re>
		public int energy_re;
		//		<!-- 物理穿透 -->
		//		<physical_penetrate>12</physical_penetrate>
		public int physical_penetrate;
		//		<!-- 法术穿透 -->
		//		<spell_penetrate>21</spell_penetrate>
		public int spell_penetrate;
		//		<!-- 吸血等级 -->
		//		<bloodsucking_lv>12</bloodsucking_lv>
		public int bloodsucking_lv;
		//		<!-- 闪避 -->
		//		<dodge>21</dodge>
		public int dodge;
		//		<!-- 治疗效果 -->
		//		<addition_treatment>21</addition_treatment>
		public int addtition_treatment;
		//		<!-- 需求等级 -->
		//		<level>12</level>
		public int level;
		//		<!-- 合成道具 -->
		//		<synthetic_props>12</synthetic_props>
		public string synthetic_props;
		//		<!-- 合成花费 -->
		//		<synthetic_spend>21</synthetic_spend>
		public int synthetic_spend;
		//		<!-- 买入价格 -->
		//		<price_list>12</price_list>
		public string price_list;
		//		<!-- 卖出价格 -->
		//		<selling_price></selling_price>
		public int selling_price;
		//		<!-- 附魔点数 -->
		//		<enchant_points></enchant_points>
		public int enchant_points;
		//		<!-- 经验 -->
		//		<experience></experience>
		public int experience;
		//		<!-- 物品图标 -->
		//		<icon></icon>
		public string icon;
		/// <summary>
		/// 物品信息
		/// </summary>
		public string info;
		//		<!-- 物品描述 -->
		//		<description></description>
		public string description;
		/// <summary>
		/// The synthetic properties table.
		/// </summary>
		private Dictionary<int,int> syntheticPropsTable = new Dictionary<int, int> ();

		/// <summary>
		/// Gets the synthetic properties table.
		/// 合成所需的物品列表
		/// </summary>
		/// <value>The synthetic properties table.</value>
		public Dictionary<int, int> SyntheticPropsTable {
			get {
				return syntheticPropsTable;
			}
		}
	}

	[XmlRoot ("root")]
	[XmlLate ("props")]
	public class PropsRoot
	{
		[XmlElement ("value")]
		public List<PropsXml> items = new List<PropsXml> ();

		public static void LateProcess (object obj)
		{
			PropsRoot root = obj as PropsRoot;
			foreach (PropsXml item in root.items) {
				Config.propsXmlTable [item.id] = item;
				ResolveSyntheticProps (item);
			}
		}

		/// <summary>
		/// Resolves the synthetic properties.
		/// </summary>
		/// <param name="item">Item.</param>
		private static void ResolveSyntheticProps (PropsXml item)
		{
			if (item.synthetic_props != null) {
				string[] strs = (string[])Utils.SplitStrByBraces (item.synthetic_props).ToArray (typeof(string));
				foreach (string str in strs) {
					int configId = Utils.SplitStrByCommaToInt (str) [0];
					int count = Utils.SplitStrByCommaToInt (str) [1];
					item.SyntheticPropsTable.Add (configId, count);
					Config.addPropsPropsRelationship (configId, item.id);
				}
			}
		}
	}
}

