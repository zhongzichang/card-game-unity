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
		public int upgrade;
		/// <summary>
		/// 英雄星级
		/// </summary>
		public int evolve;

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
	}
}