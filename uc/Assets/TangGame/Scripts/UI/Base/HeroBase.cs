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
		private EquipBase[] equipBases;

		/// <summary>
		/// Ups the equip bases.
		/// 更新装备相关数据
		/// </summary>
		private void UpEquipBases ()
		{
			if (xml != null) {
				if (equipBases == null) {
					equipBases = new EquipBase[Equip_Ids.Length];
				}
				for (int i = 0; i < Equip_Ids.Length; i++) {
					EquipBase equipBaseTmp = equipBases [i];
					if (equipBaseTmp == null) {
						equipBaseTmp = new EquipBase ();
						equipBaseTmp.Xml = Config.propsXmlTable [Equip_Ids [i]];
						equipBases [i] = equipBaseTmp;
					}
				}
				if (net != null && net.equipList != null) {
					for (int i = 0; i < net.equipList.Length; i++) {
						equipBases [i].Net = net.equipList [i];
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

		public EquipBase[] EquipBases {
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
				return xml.strength_growth;
			}
		}

		/// <summary>
		/// 智力成长率
		/// </summary>
		public float Intellect_growth {
			get {
				return xml.intellect_growth;
			}
		}

		/// <summary>
		/// 敏捷成长率
		/// </summary>
		public float Agile_growth {
			get {
				return xml.agile_growth;
			}
		}

		/// <summary>
		/// 力量
		/// </summary>
		public int Strength {
			get {
				return xml.strength;
			}
		}

		/// <summary>
		/// 智力
		/// </summary>
		public int Intellect {
			get {
				return xml.intellect;
			}
		}

		/// <summary>
		/// 敏捷
		/// </summary>
		public int Agile {
			get {
				return xml.agile;
			}
		}

		/// <summary>
		/// 生命最大值
		/// </summary>
		public int HpMax {
			get {
				return xml.hpMax;
			}
		}

		/// <summary>
		/// 物理攻击强度
		/// </summary>
		public int Attack_damage {
			get {
				return xml.attack_damage;
			}
		}

		/// <summary>
		/// 法术攻击强度
		/// </summary>
		public int Ability_power {
			get {
				return xml.ability_power;
			}
		}

		/// <summary>
		/// 物理防御
		/// </summary>
		public int Physical_defense {
			get {
				return xml.physical_defense;
			}
		}

		/// <summary>
		/// 法术防御
		/// </summary>
		public int Magic_defense {
			get {
				return xml.magic_defense;
			}
		}

		/// <summary>
		/// 生命回复
		/// </summary>
		public int Hp_recovery {
			get {
				return xml.hp_recovery;
			}
		}

		/// <summary>
		/// 能量回复
		/// </summary>
		public int Energy_recovery {
			get {
				return xml.energy_recovery;
			}
		}

		/// <summary>
		/// 物理穿透
		/// </summary>
		public int Physical_penetration {
			get {
				return xml.physical_penetration;
			}
		}

		/// <summary>
		/// 法术穿透
		/// </summary>
		public int Spell_penetration {
			get {
				return xml.spell_penetration;
			}
		}

		/// <summary>
		/// 法术爆击
		/// </summary>
		/// <value>The spell crit.</value>
		public int Magic_Crit {
			get { 
				return xml.magic_crit;
			}
		}

		/// <summary>
		/// 物理爆击
		/// </summary>
		/// <value>The physical_ crit.</value>
		public int Physical_Crit {
			get { 
				return xml.physical_crit;
			}
		}

		/// <summary>
		/// 吸血等级
		/// </summary>
		public int Bloodsucking_lv {
			get {
				return xml.bloodsucking_lv;
			}
		}

		/// <summary>
		/// 闪避
		/// </summary>
		public int Dodge {
			get {
				return xml.dodge;
			}
		}

		/// <summary>
		/// 治疗加成
		/// </summary>
		public int Addition_treatment {
			get {
				return xml.addition_treatment;
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

		/// <summary>
		/// 返回品质的颜色
		/// </summary>
		/// <returns>The rank color string.</returns>
		/// <param name="rank">Rank.</param>
		public static string GetRankColorStr (int rank)
		{
			return GetRankColorStr (GetHeroesRankEnum (rank));
		}

		public static string GetRankColorStr (RankEnum rank)
		{
			if (rank.Equals (RankEnum.WHITE)) {
				return "white";
			} else if (rank.Equals (RankEnum.GREEN)) {
				return "green";
			} else if (rank.Equals (RankEnum.BLUE)) {
				return "blue";
			} else if (rank.Equals (RankEnum.PURPLE)) {
				return "purple";
			} else {
				return "white";
			}
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