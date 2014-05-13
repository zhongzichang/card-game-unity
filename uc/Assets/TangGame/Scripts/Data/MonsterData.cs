
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class MonsterData
	{ 
		/// 怪物编号
		public int id;
		/// 怪物等级
		public int lv;
		/// 怪物进阶等级
		public int up_lv;
		/// 怪物技能等级
		public int skill_lvs;
		/// 力量
		public float strength;
		/// 智力
		public float intellect;
		/// 敏捷
		public float agile;
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
		/// 技能等级
		public int skill_lv;
	}
}