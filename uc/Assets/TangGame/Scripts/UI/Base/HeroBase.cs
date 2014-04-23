﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;
using TangGame.Net;
namespace TangGame.UI.Base
{
	public class HeroBase
	{
		private HeroNet net;
		private HeroXml xml;

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
				xml = Config.heroXml [net.configId];
				net = value;
			}
		}
		/// <summary>
		/// 来自配置表的数据
		/// </summary>
		/// <value>The xml.</value>
		public HeroXml Xml {
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
		/// <summary>
		/// 战队战斗力
		/// </summary>
		public int Score{
			get{
				//TODO 修正算法
				return net.level * (net.evolve + net.upgrade);
			}
		}
		/// <summary>
		/// 这个英雄是否解锁
		/// </summary>
		/// <value><c>true</c> if this instance is lock; otherwise, <c>false</c>.</value>
		public bool Islock{
			get{ 
				if (net.id == 0) {
					return true;
				} else {
					return false;
				}
			}
		}

		public static HeroesRankEnum GetHeroesRankEnum(int rank){
			float val = (float)Mathf.Sqrt ((float)(2 * rank + 0.25)) - (float)0.5;
			return (HeroesRankEnum)(int)val;
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

	public enum HeroesRankEnum
	{
		WHITE,
		GREEN,
		BLUE,
		PURPLE,
		ORANGE
	}
}