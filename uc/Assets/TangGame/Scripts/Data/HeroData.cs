using System.Xml;
using UnityEngine;
using TangUtils;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using TangGame.UI;

namespace TangGame.Xml
{
	public class HeroData
	{ 
		/// 编号
		public int id;
		/// 英雄名称
		public string name;
		/// 是否显示到英雄列表
		public int showView;
		/// 碎片ID
		public int soul_rock_id;
		/// 初始星级
		public int evolve;
		/// 英雄阵营
		public int camp;
		/// 英雄性别
		public int gender;
		/// 英雄类型
		public int attribute_type;
		/// 英雄位置
		public int location;
		/// 力量成长
		public float strength_growth;
		/// 智力成长
		public float intellect_growth;
		/// 敏捷成长
		public float agile_growth;
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
		/// 法术暴击
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
		/// 进阶装备配置
		public string equip_id_list;
		/// 技能编号
		public string skill_ids;
		/// 出手序列
		public string shot_order;
		/// 英雄介绍
		public string hero_info;
		/// 标签介绍
		public string hero_tip;
		/// 头像资源名
		public string avatar;
		/// 图鉴资源名字
		public string portrait;
		List<PropsData> equipList;

		/// <summary>
		/// Currents the equip list by rank.
		/// 根据阶级获取当前装备的xml对象
		/// </summary>
		/// <returns>The equip list by rank.</returns>
		/// <param name="rank">Rank.</param>
		public List<PropsData> CurrentEquipListByRank(int heroUpgrade){
			if (equipList == null) {
				equipList = new List<PropsData> ();
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
		public List<HeroData> items = new List<HeroData> ();
		public static void LateProcess (object obj)
		{
			HeroRoot root = obj as HeroRoot;
			int i = 0;
			foreach (HeroData item in root.items) {
				Config.heroXmlTable [item.id] = item;
        PropsCache.instance.AddPropsHeroRelation(item);
				//TODO 先写到这个地方到时候再改，测试使用数据
				TangGame.UI.HeroBase herobase = new TangGame.UI.HeroBase ();
				if (item.id == 1) {
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