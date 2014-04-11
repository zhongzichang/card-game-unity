using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class HeroXml
	{

//				一 一级属性
//				英雄以一级属性区分职业特性，分别对应以下三项
		public int strength;
//				力量：增加物理攻击、生命、护甲
		public int intelligence;
//				智力：增加物理攻击、法术强度、魔抗
		public int agility;
//				敏捷：增加物理攻击、护甲、暴击
//				
//				二 延伸属性
		public int hp;
//				生命值：英雄初始生命值
		public int attack_power;
//				物理攻击力：英雄初始物理攻击力
		public int spell_power;
//				魔法强度：英雄初始魔法强度
		public int physical_defense;
//				物理护甲：英雄初始物理护甲
		public int magic_defense;
//				魔法抗性：英雄初始魔法抗性
		public int physical_crit;
//				物理暴击：英雄初始物理暴击
		public int hp_re;
//				生命回复：英雄初始生命回复
		public int energy_re;
//				能量回复：英雄初始能量回复
		//TODO 升级经验
//				升级经验：英雄初始经验及每级升级所需经验值
		public int level;
//				英雄等级：英雄初始初始等级
		public int score;
//				战斗力：英雄初始战斗力显示
//				
//				三 额外需求
		public int id;
//				编号：英雄编号
		public string name;
//				名称：英雄名字
		public string info;
//				描述：英雄的中文描述
		public float atk_speed;
//				攻击速度：英雄的攻击速度
		public int atk_distance;
//				攻击距离：英雄的攻击距离
		public int location;
//				英雄位置：英雄战斗中的位置，分为前，中，后排
		public string pictorial;
//				图鉴编号：调用英雄图鉴图片的编号
		public int animation_id;
//				动作编号：调用在查看英雄属性时的角色
		public int ai_id;
//				AI调用：调用英雄的技能释放AI
		public string skill_ids;
//				技能配置：英雄所拥有的技能编号
	}

	[XmlRoot("root")]
	public class HeroRoot
	{
		[XmlElement("value")]
		public List<HeroXml>
			items = new List<HeroXml> ();

		public static void LateProcess (object obj)
		{
			HeroRoot root = obj as HeroRoot;
			foreach (HeroXml item in root.items) {
				Config.heroXml [item.id] = item;
			}
		}
	}
}