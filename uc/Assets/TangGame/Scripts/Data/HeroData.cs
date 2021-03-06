﻿using System.Xml;
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
		public string strength_growth;
		/// 智力成长
		public string intellect_growth;
		/// 敏捷成长
		public string agile_growth;
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
		/// 法术暴击
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
		/// 模型资源名
		public string model;
		/// <summary>
		/// 装备列表
		/// </summary>
		List<PropsData> equipList;

		/// <summary>
		/// Currents the equip list by rank.
		/// 根据阶级获取当前装备的xml对象
		/// </summary>
		/// <returns>The equip list by rank.</returns>
		/// <param name="rank">Rank.</param>
		public List<PropsData> CurrentEquipListByRank (int heroUpgrade)
		{
			if (equipList == null) {
				equipList = new List<PropsData> ();
				ArrayList equipStrList = Utils.SplitStrByBraces (this.equip_id_list);
				string equipStr = "";
				if (equipStrList.Count >= heroUpgrade - 1) {
					equipStr = equipStrList [heroUpgrade - 1].ToString ();
				}
				int[] equipIds = Utils.SplitStrByCommaToInt (equipStr);
				if (equipStr != null) {
					foreach (int id in equipIds) {
						if (Config.propsXmlTable.ContainsKey (id))
							equipList.Add (Config.propsXmlTable [id]);
						else
							Debug.LogWarning (string.Format ("找不到物品，道具id:{0}", id));
					}
				}
			}
			return equipList;
		}

		/// <summary>
		/// Gets the strength growth.
		/// 获取力量成长
		/// </summary>
		/// <returns>The strength growth.</returns>
		/// <param name="evolve">Evolve.</param>
		public float GetStrengthGrowth (int evolve)
		{
			if (Utils.isXMLStr_Null (strength_growth))
				return 0;
			string str = Utils.SplitStrByBraces (strength_growth) [0].ToString ();
			string floatStr = Utils.SplitStrByComma (str) [evolve];
			float fl = float.Parse (floatStr);
			return fl;
		}

		/// <summary>
		/// 获取智力成长
		/// </summary>
		/// <returns>The intellect growth.</returns>
		/// <param name="evolve">Evolve.</param>
		public float GetIntellectGrowth (int evolve)
		{
			if (Utils.isXMLStr_Null (intellect_growth))
				return 0;
			string str = Utils.SplitStrByBraces (intellect_growth) [0].ToString ();
			string floatStr = Utils.SplitStrByComma (str) [evolve];
			float fl = float.Parse (floatStr);
			return fl;
		}

		/// <summary>
		/// 获取敏捷成长
		/// </summary>
		/// <returns>The agile growth.</returns>
		/// <param name="evolve">Evolve.</param>
		public float GetAgileGrowth (int evolve)
		{
			if (Utils.isXMLStr_Null (agile_growth))
				return 0;
			string str = Utils.SplitStrByBraces (agile_growth) [0].ToString ();
			string floatStr = Utils.SplitStrByComma (str) [evolve];
			float fl = float.Parse (floatStr);
			return fl;
		}
	}

	[XmlRoot ("root")]
	[XmlLate ("hero")]
	public class HeroRoot
	{
		[XmlElement ("value")]
		public List<HeroData> items = new List<HeroData> ();

		public static void LateProcess (object obj)
		{
			HeroRoot root = obj as HeroRoot;
			int i = 0;
			foreach (HeroData item in root.items) {
				Config.heroXmlTable [item.id] = item;
				HeroCache.instance.AddSoulStoneRelation (item);
				PropsCache.instance.AddPropsHeroRelation (item);
				HeroCache.instance.UpdataMyHeroBaseTable (item);

			}
		}
	}
}