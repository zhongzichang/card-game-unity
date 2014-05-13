
using System.Xml;
using UnityEngine;

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
		/// 生命回复
		public int magic_crit;
		/// 能量回复
		public int hp_recovery;
		/// 破甲值
		public int energy_recovery;
		/// 无视魔抗
		public int physical_penetration;
		/// 吸血等级
		public int spell_penetration;
		/// 闪避
		public int bloodsucking_lv;
		/// 治疗效果
		public int dodge;
		/// 能量消耗降低
		public int addition_treatment;
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
		public int icon;
		/// 物品描述1
		public string info;
		/// 物品描述2
		public string description;
	}
}