
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class LevelsData
	{ 
		/// 关卡序列号
		public int id;
		/// 关卡类型
		public int type;
		/// 一层怪物序列
		public string lv1_monster_ids;
		/// 二层怪物序列
		public string lv2_monster_ids;
		/// 三层怪物序列
		public string lv3_monster_ids;
		/// 体力消耗
		public int energy_consumption;
		/// 产出经验
		public int team_exp;
		/// 英雄经验
		public int hero_exp;
		/// 掉落约束
		public string drop_constraint;
		/// 同id关卡次数限制
		public int lv_constraint_num;
		/// 同类型关卡次数限制
		public int type_constraint_num;
		/// 开放等级限制
		public int team_lv;
		/// 关卡名
		public string name;
		/// 怪物一览
		public string monster_ids;
		/// 掉落一览
		public string props_ids;
		/// 描述
		public string description;
		/// 一层对话编号
		public string lv1_dialogue;
		/// 二层对话编号
		public string lv2_dialogue;
		/// 三层对话编号
		public string lv3_dialogue;
		/// 关卡美术资源
		public string resources;
	}
}