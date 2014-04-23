using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class HeroXml
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		public int id;
		/// <summary>
		/// 英雄名称
		/// </summary>
		public string name;
		/// <summary>
		/// 是否再列表显示
		/// </summary>
		public short showView;
		/// <summary>
		/// 初始星级
		/// </summary>
		public int evolve;
		/// <summary>
		/// 英雄阵营
		/// </summary>
		public int camp;
		/// <summary>
		/// 英雄性别
		/// </summary>
		public int gender;
		/// <summary>
		/// 英雄属性类型，力量，智力，敏捷
		/// </summary>
		public int attribute_type;
		/// <summary>
		/// 前排，中排，后排
		/// </summary>
		public int location;
		/// <summary>
		/// 力量成长率
		/// </summary>
		public float strength_growth;
		/// <summary>
		/// 智力成长率
		/// </summary>
		public float intellect_growth;
		/// <summary>
		/// 敏捷成长率
		/// </summary>
		public float agile_growth;
		/// <summary>
		/// 力量
		/// </summary>
		public int strength;
		/// <summary>
		/// 智力
		/// </summary>
		public int intellect;
		/// <summary>
		/// 敏捷
		/// </summary>
		public int agile;
		/// <summary>
		/// 生命最大值
		/// </summary>
		public int hpMax;
		/// <summary>
		/// 物理攻击强度
		/// </summary>
		public int attack_damage;
		/// <summary>
		/// 法术攻击强度
		/// </summary>
		public int ability_power;
		/// <summary>
		/// 物理防御
		/// </summary>
		public int physical_defense;
		/// <summary>
		/// 法术防御
		/// </summary>
		public int magic_defense;
		/// <summary>
		/// 生命回复
		/// </summary>
		public int hp_recovery;
		/// <summary>
		/// 能量回复
		/// </summary>
		public int energy_recovery;
		/// <summary>
		/// 物理穿透
		/// </summary>
		public int physical_penetration;
		/// <summary>
		/// 法术穿透
		/// </summary>
		public int spell_penetration;
		/// <summary>
		/// 吸血等级
		/// </summary>
		public int bloodsucking_lv;
		/// <summary>
		/// 闪避
		/// </summary>
		public int dodge;
		/// <summary>
		/// 治疗加成
		/// </summary>
		public int addition_treatment;
		/// <summary>
		/// 进阶装备id列表
		/// </summary>
		public string equip_id_list;
		/// <summary>
		/// 技能id数组
		/// </summary>
		public string skill_ids;
		/// <summary>
		/// 技能出手先后顺序
		/// </summary>
		public string shot_order;
		/// <summary>
		/// 英雄简介
		/// </summary>
		public string hero_info;
		/// <summary>
		/// 英雄标签
		/// </summary>
		public string hero_tip;
		/// <summary>
		/// 英雄头像资源名字
		/// </summary>
		public string avatar;
		/// <summary>
		/// 图鉴的名字
		/// </summary>
		public string portrait;
	}

	[XmlRoot("root")]
	public class HeroRoot
	{
		[XmlElement("value")]
		public List<HeroXml>
			items = new List<HeroXml> ();

		public static void LateProcess (object obj)
		{
			HeroRoot root = obj as HeroRoot;
			foreach (HeroXml item in root.items) {
				Config.heroXml [item.id] = item;
				//TODO 先写到这个地方到时候再改
				TangGame.UI.Base.HeroBase herobase = 	new TangGame.UI.Base.HeroBase ();
				herobase.Xml = item;
				TangGame.UI.Base.BaseCache.heroBeseDic.Add (item.id,herobase);
			}
		}
	}
}