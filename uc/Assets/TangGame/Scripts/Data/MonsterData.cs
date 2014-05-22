using System.Xml;
using TangUtils;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using TangGame.UI;

namespace TangGame.Xml
{
	public class MonsterData
	{ 
		/// 怪物编号
		public int id;
    /// 怪物名称
    public string name;
		/// 怪物等级
		public int level;
    /// 怪物技能
    public string skill;
    /// 出手序列
    public string shot_order;
    /// 星级
    public int evolve;
    /// 品阶
    public int upgrade;

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
    /// 模型资源名
    public string model;
    /// 头像资源名
    public string avatar;
    /// 怪物简介
    public string desc;

    [XmlRoot ("root")]
    [XmlLate ("monster")]
    public class MonsterRoot
    {
      [XmlElement ("value")]
      public List<MonsterData> items = new List<MonsterData> ();
      
      public static void LateProcess (object obj)
      {
        MonsterRoot root = obj as MonsterRoot;
        foreach (MonsterData item in root.items) {
          
        }
      }
    }
	}
}