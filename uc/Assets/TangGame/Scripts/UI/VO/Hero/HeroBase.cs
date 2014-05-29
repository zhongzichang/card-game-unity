using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI
{
	public class HeroBase
	{
		private HeroNet net;
		private HeroData xml;
		private SkillBase[] skillBases;
		private Equip[] equipBases;

		/// <summary>
		/// Ups the equip bases.
		/// 更新装备相关数据
		/// </summary>
		private void UpEquipBases ()
		{
			if (xml != null) {
				if (equipBases == null) {
					equipBases = new Equip[Equip_Ids.Length];
				}
				for (int i = 0; i < Equip_Ids.Length; i++) {
					Equip equipBaseTmp = equipBases [i];
					if (equipBaseTmp == null) {
						equipBaseTmp = new Equip ();
						equipBaseTmp.data = Config.propsXmlTable [Equip_Ids [i]];
						equipBases [i] = equipBaseTmp;
					}
				}
				if (net != null && net.equipList != null) {
					for (int i = 0; i < net.equipList.Length; i++) {
						equipBases [i].net = net.equipList [i];
					}
				}
			}
		}

		/// <summary>
		/// Ups the skill bases. 更新技能列表
		/// </summary>
		private void UpSkillBases ()
		{
			if (xml != null) {
				if (skillBases == null) {
					skillBases = new SkillBase[Skill_Ids.Length];
				}
				for (int i = 0; i < Skill_Ids.Length; i++) {
					if (skillBases [i] == null) {
						skillBases [i] = new SkillBase ();
						skillBases [i].Xml = Config.skillXmlTable [Skill_Ids [i]];
					}
				}
			}
			if (net != null && net.skillLevel != null) {
				if (skillBases == null) {
					skillBases = new SkillBase[net.skillLevel.Length];
				}
				for (int i = 0; i < net.skillLevel.Length; i++) {
					if (skillBases == null) {
						skillBases [i] = new SkillBase ();
					}
					skillBases [i].Level = net.skillLevel [i];
				}
			}
		}

		/// <summary>
		/// Gets the skill bases.技能实体数据数组
		/// </summary>
		/// <value>The skill bases.</value>
		public SkillBase[] SkillBases {
			get {
				UpSkillBases ();
				return skillBases;
			}
		}

		public Equip[] EquipBases {
			get {
				UpEquipBases ();
				return equipBases;
			}
		}

		/// <summary>
		/// 来自网络的数据
		/// </summary>
		/// <value>The net.</value>
		public HeroNet Net {
			get {
				if (net == null) {
					return new HeroNet ();
				}
				return net;
			}
			set {
				net = value;
			}
		}

		/// <summary>
		/// 来自配置表的数据
		/// </summary>
		/// <value>The xml.</value>
		public HeroData Xml {
			get {
				return xml;
			}
			set { 
				xml = value;
			}
		}
		//TODO skillList ,
		//TODO propslist ,
		/// <summary>
		/// 力量成长率
		/// </summary>
		public float Strength_growth {
			get {
				return xml.GetStrengthGrowth (net.evolve);
			}
		}

		/// <summary>
		/// 智力成长率
		/// </summary>
		public float Intellect_growth {
			get {
				return xml.GetIntellectGrowth (net.evolve);
			}
		}

		/// <summary>
		/// 敏捷成长率
		/// </summary>
		public float Agile_growth {
			get {
				return xml.GetAgileGrowth (net.evolve);
			}
		}

		public AttributeTypeEnum Attribute_Type {
			get { 
				return (AttributeTypeEnum)xml.attribute_type;
			}
		}

		/// <summary>
		/// 战队战斗力
		/// </summary>
		public int Score {
			get {
				//TODO 修正算法
				return Net.level * (Net.evolve + Net.upgrade);
			}
		}

		/// <summary>
		/// 英雄技能id 数组
		/// </summary>
		/// <value>The skill_ identifiers.</value>
		private int[] Skill_Ids {
			get { 
				string[] strs = (string[])Utils.SplitStrByBraces (xml.skill_ids).ToArray (typeof(string));
				return Utils.SplitStrByCommaToInt (strs [0]);
			}
		}

		/// <summary>
		/// Sets the equip_id_list.
		/// 获取当前品阶的装备列表
		/// </summary>
		/// <value>The equip_id_list.</value>
		private int[] Equip_Ids {
			get { 
				string[] strs = (string[])Utils.SplitStrByBraces (xml.equip_id_list).ToArray (typeof(string));
				return Utils.SplitStrByCommaToInt (strs [net.upgrade - 1]);
			}
		}

		/// <summary>
		/// 这个英雄是否解锁
		/// </summary>
		/// <value><c>true</c> if this instance is lock; otherwise, <c>false</c>.</value>
		public bool Islock {
			get { 
				if (Net.id == 0) {
					return true;
				} else {
					return false;
				}
			}
		}

		public static RankEnum GetHeroesRankEnum (int rank)
		{
			float val = (float)Mathf.Sqrt ((float)(2 * rank + 0.25)) - (float)0.5;
			return (RankEnum)(int)(val - 1);
		}
	}

	public enum AttributeTypeEnum
	{
		NONE,
		STR,
		INT,
		AGI
	}

	public enum HeroLocationEnum
	{
		NONE,
		BEFORE,
		MEDIUM,
		LATER
	}

	public enum RankEnum
	{
		NONE,
		WHITE,
		GREEN,
		BLUE,
		PURPLE,
		ORANGE
	}
}