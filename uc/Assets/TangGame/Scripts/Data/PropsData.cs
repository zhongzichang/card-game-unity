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
		public float strength;
		/// 智力
		public float intellect;
		/// 敏捷
		public float agile;
		/// 最大生命值
		public float hpMax;
		/// 物理攻击
		public float attack_damage;
		/// 魔法强度
		public float ability_power;
		/// 物理护甲
		public float physical_defense;
		/// 魔法抗性
		public float magic_defense;
		/// 物理暴击
		public float physical_crit;
		/// 魔法爆击
		public float magic_crit;
		/// 生命回复
		public float hp_recovery;
		/// 能量回复
		public float energy_recovery;
		/// 破甲值
		public float physical_penetration;
		/// 无视魔抗
		public float spell_penetration;
		/// 吸血等级
		public float bloodsucking_lv;
		/// 闪避
		public float dodge;
		/// 治疗效果
		public float addition_treatment;
		/// 能量消耗降低
		public float reduce_energy;
		/// 需求等级
		public int level;
		/// 合成物
		public string synthetic_props;
		/// 合成花费
		public int synthetic_spend;
		/// 买入价格
		public string price_list;
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
				if (item.id % 2 == 0)
					net.count = 20;
				else
					net.count = 5;
				props.data = item;
				props.net = net;
				if (item.id % 3 == 1)
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
					TangGame.UI.PropsCache.instance.AddPropsSyntheticsRelation (configId, item);//道具关联
				}
			}
		}
	}
}

