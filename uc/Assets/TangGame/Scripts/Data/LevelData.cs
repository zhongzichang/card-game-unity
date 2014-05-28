using System.Xml;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using TangUtils;
using TangGame.UI;

namespace TangGame.Xml
{
	public class LevelData
	{ 
		/// 关卡序列号
		public int id;
		/// 关卡地图
		public int map_id;
		/// 关卡重复
    public int repeat;
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
		public int lv1_dialogue;
		/// 二层对话编号
		public int lv2_dialogue;
		/// 三层对话编号
		public int lv3_dialogue;
		/// 关卡美术资源
		public string resources;


    [XmlRoot ("root")]
    [XmlLate ("levels")]
    public class LevelsRoot
    {
      [XmlElement ("value")]
      public List<LevelData> items = new List<LevelData> ();
      
      public static void LateProcess (object obj)
      {
        LevelsRoot root = obj as LevelsRoot;
        int i = 0;
        foreach (LevelData item in root.items) {
          Config.levelsXmlTable[item.id] = item;
          LevelCache.instance.AddLevelData(item);
          PropsCache.instance.AddPropsLevelRelation(item);
        }
      }
    }

	}
}