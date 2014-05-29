using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.Net
{
	public class HeroNet
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		public int id;
		/// <summary>
		/// The config identifier.
		/// </summary>
		public int configId;
		/// <summary>
		/// 英雄等级
		/// </summary>
		public int level;
		/// <summary>
		/// 英雄经验
		/// </summary>
		public long exp;
		/// <summary>
		/// 英雄品质
		/// </summary>
		public int rank;
		/// <summary>
		/// 英雄星级
		/// </summary>
		public int star;

		public long lastUpSkillTime;
		public int skillCount;

		#region 英雄属性

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


		#endregion

		/// <summary>
		/// 英雄技能等级
		/// </summary>
		public int[] skillLevel;
		/// <summary>
		/// The equip.
		/// </summary>
		public EquipNet[] equipList;

		#region 临时方法，兼容在线与离线

		public float Strength {
			get {
				if (strength == 0)
					return Config.heroXmlTable [configId].strength;
				return strength;
			}
		}

		public float Intellect {
			get {
				if (intellect == 0)
					return Config.heroXmlTable [configId].intellect;
				return intellect;
			}
		}

		public float Agile {
			get { 
				if (agile == 0)
					return Config.heroXmlTable [configId].agile;
				return agile;
			}
		}

		public float HpMax {
			get {
				if (hpMax == 0)
					return Config.heroXmlTable [configId].hpMax;
				return hpMax;
			}
		}

		public float Attack_damage {
			get {
				if (attack_damage == 0)
					return Config.heroXmlTable [configId].attack_damage;
				return attack_damage;
			}
		}

		public float Ability_power {
			get {
				if (ability_power == 0)
					return Config.heroXmlTable [configId].ability_power;
				return ability_power;
			}
		}

		public float Physical_defense {
			get {
				if (physical_defense == 0)
					return Config.heroXmlTable [configId].physical_defense;
				return physical_defense;
			}
		}

		public float Magic_defense {
			get {
				if (magic_defense == 0)
					return Config.heroXmlTable [configId].magic_defense;
				return magic_defense;
			}
		}

		public float Physical_crit {
			get {
				if (physical_crit == 0)
					return Config.heroXmlTable [configId].physical_crit;
				return physical_crit;
			}
		}

		public float Magic_crit {
			get {
				if (magic_crit == 0)
					return Config.heroXmlTable [configId].magic_crit;
				return magic_crit;
			}
		}

		public float Hp_recovery {
			get {
				if (hp_recovery == 0)
					return Config.heroXmlTable [configId].hp_recovery;
				return hp_recovery;
			}
		}

		public float Energy_recovery {
			get {
				if (energy_recovery == 0)
					return Config.heroXmlTable [configId].energy_recovery;
				return energy_recovery;
			}
		}

		public float Physical_penetration {
			get {
				if (physical_penetration == 0)
					return Config.heroXmlTable [configId].physical_penetration;
				return physical_penetration;
			}
		}

		public float Spell_penetration {
			get {
				if (spell_penetration == 0)
					return Config.heroXmlTable [configId].spell_penetration;
				return spell_penetration;
			}
		}

		public float Bloodsucking_lv {
			get {
				if (bloodsucking_lv == 0)
					return Config.heroXmlTable [configId].bloodsucking_lv;
				return bloodsucking_lv;
			}
		}

		public float Dodge {
			get {
				if (dodge == 0)
					return Config.heroXmlTable [configId].dodge;
				return dodge;
			}
		}

		public float Addition_treatment {
			get {
				if (addition_treatment == 0)
					return Config.heroXmlTable [configId].addition_treatment;
				return addition_treatment;
			}
		}

		#endregion
	}
}