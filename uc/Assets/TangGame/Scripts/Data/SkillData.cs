
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class SkillData
	{ 
		/// 编号
		public int id;
		/// 技能名称
		public string name;
		/// cd时间
		public int cool_time;
		/// 类型
		public int type;
		/// 前摇
		public float boot_time;
		/// 后摇
		public float after_time;
		/// 动画循环
		public int play_loop;
		/// 是否大招
		public int isUltimate;
		/// 施法距离
		public int cast_range;
		/// 技能系数基本参数
		public int skill_coefficient;
		/// 升级提升参数
		public int up_add;
		/// 目标类型
		public int target_type;
		/// 范围类型
		public int range_type;
		/// 作用器次数
		public int effector_times;
		/// 多重作用器序列
		public int effector_sequence;
		/// 作用器编号
		public int effector_id;
		/// 是否被伤害打断
		public int being_interrupted_by_injuries;
		/// 是否是被动加属性技能
		public int is_attribute_addition;
		/// 覆盖技能编号
		public int code_coverage_skill_id;
		/// 动画编号
		public int animation_id;
		/// 前摇特效
		public int singing_effect;
		/// 后摇特效
		public int play_effect;
		/// 技能图标
		public int skill_icon;
		/// 文字描述
		public int desc;
		/// 黄字描述
		public int desc_y;
	}
}