using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class PropsData
	{ 
		/// 物品编号
		public int id;
		/// 物品名称
		public string name;
		/// 类型
		public int type;
		/// 品质
		public int upgrade;
		/// 力量
		public int strength;
		/// 智力
		public int intellect;
		/// 敏捷
		public int agile;
		/// 最大生命值
		public int hpMax;
		/// 物理攻击
		public int attack_damage;
		/// 魔法强度
		public int ability_power;
		/// 物理护甲
		public int physical_defense;
		/// 魔法抗性
		public int magic_defense;
		/// 物理暴击
		public int physical_crit;
		/// 魔法爆击
		public int magic_crit;
		/// 生命回复
		public int hp_recovery;
		/// 能量回复
		public int energy_recovery;
		/// 破甲值
		public int physical_penetration;
		/// 无视魔抗
		public int spell_penetration;
		/// 吸血等级
		public int bloodsucking_lv;
		/// 闪避
		public int dodge;
		/// 治疗效果
		public int addition_treatment;
		/// 能量消耗降低
		public int reduce_energy;
		/// 需求等级
		public int level;
		/// 合成物
		public string synthetic_props;
		/// 合成花费
		public int synthetic_spend;
		/// 买入价格
		public int price_list;
		/// 卖出价格
		public int selling_price;
		/// 附魔点数
		public int enchant_points;
		/// 经验值
		public int experience;
		/// 图标编号
		public string icon;
		/// 物品描述1
		public string info;
		/// 物品描述2
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
		public Dictionary<int, int> GetSyntheticPropsTable ()
		{
			return syntheticPropsTable;

		}

	}

	[XmlRoot ("root")]
	[XmlLate ("props")]
	public class PropsRoot
	{
		[XmlElement ("value")]
		public List<PropsData> items = new List<PropsData> ();

		public static void LateProcess (object obj)
		{
			PropsRoot root = obj as PropsRoot;
			int a = 5;
			foreach (PropsData item in root.items) {
				Config.propsXmlTable [item.id] = item;
				ResolveSyntheticProps (item);

        //TODO 测试数据
        TangGame.UI.Props props = new TangGame.UI.Props ();
				TangGame.Net.PropsNet net = new TangGame.Net.PropsNet ();
				net.configId = item.id;
				net.count = a * a % 123;
        props.data = item;
				props.net = net;
        TangGame.UI.PropsCache.instance.propsTable.Add (item.id, props);
				a++;
			}
		}

		/// <summary>
		/// Resolves the synthetic properties.
		/// </summary>
		/// <param name="item">Item.</param>
		private static void ResolveSyntheticProps (PropsData item)
		{
			if (item.synthetic_props != null) {
				string[] strs = (string[])Utils.SplitStrByBraces (item.synthetic_props).ToArray (typeof(string));
				foreach (string str in strs) {
					int configId = Utils.SplitStrByCommaToInt (str) [0];
					int count = Utils.SplitStrByCommaToInt (str) [1];
					item.GetSyntheticPropsTable ().Add (configId, count);
          TangGame.UI.PropsCache.instance.AddPropsSyntheticsRelation(configId, item);//道具关联
				}
			}
		}
	}
}

