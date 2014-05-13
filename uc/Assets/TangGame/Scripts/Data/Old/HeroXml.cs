using TangUtils;
using System.Xml;
using UnityEngine;
using System.Xml.Serialization;
using System.Collections;
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
		/// 是否再列表显示
		public short showView;
		/// <summary>
		/// 灵魂石头id
		/// </summary>
		public int soul_rock_id;
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
		/// 物理爆击
		/// </summary>
		public int physical_crit;
		/// <summary>
		/// 法术爆击
		/// </summary>
		public int magic_crit;
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
		public string portrait = null;

		List<PropsXml> equipList;

		/// <summary>
		/// Currents the equip list by rank.
		/// 根据阶级获取当前装备的xml对象
		/// </summary>
		/// <returns>The equip list by rank.</returns>
		/// <param name="rank">Rank.</param>
		public List<PropsXml> CurrentEquipListByRank(int heroUpgrade){
			if (equipList == null) {
				equipList = new List<PropsXml> ();
				ArrayList equipStrList = Utils.SplitStrByBraces (this.equip_id_list);
				string equipStr = "";
				if (equipStrList.Count >= heroUpgrade - 1) {
					equipStr = equipStrList [heroUpgrade - 1].ToString();
				}
				int[] equipIds = Utils.SplitStrByCommaToInt (equipStr);
				if(equipStr != null){
					foreach (int id in equipIds) {
					if (Config.propsXmlTable.ContainsKey (id))
						equipList.Add (Config.propsXmlTable [id]);
					else
							Debug.LogWarning (string.Format("找不到物品，道具id:{0}",id));
					}
				}
			}
			return equipList;
		}
	}

	[XmlRoot("root")]
	[XmlLate ("hero")]
	public class HeroRoot
	{
		[XmlElement("value")]
		public List<HeroXml> items = new List<HeroXml> ();
		public static void LateProcess (object obj)
		{
			HeroRoot root = obj as HeroRoot;
			int i = 0;
			foreach (HeroXml item in root.items) {
				Config.heroXmlTable [item.id] = item;
				Config.addPropsHeroesRelationship (item.soul_rock_id,item);
				//TODO 先写到这个地方到时候再改，测试使用数据
				TangGame.UI.HeroBase herobase = new TangGame.UI.HeroBase ();
				if (item.id == 1001) {
					herobase.Xml = item;
					herobase.Net = new TangGame.Net.HeroNet ();
					herobase.Net.configId = item.id;
					herobase.Net.evolve = 2;
					herobase.Net.exp = 40;
					herobase.Net.upgrade = 3;
					herobase.Net.id = 10001;
					herobase.Net.level = 5;
					herobase.Net.equipList = new TangGame.Net.EquipNet[6];
					TangGame.Net.EquipNet equip;
					equip = new TangGame.Net.EquipNet ();
					equip.configId = 1003;
					equip.enchantsLv = 1;
					equip.enchantsExp = 30;
					herobase.Net.equipList [3] = equip;
				} else {
					herobase.Xml = item;
				}
				TangGame.UI.BaseCache.heroBeseTable.Add (item.id, herobase);
			}
		}
	}
}