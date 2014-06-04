using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

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
		public int boot_time;
		/// 后摇
		public int after_time;
		/// 动画循环
		public int play_loop;
		/// 是否大招
    	public bool isUltimate;
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
		/// 是否被伤害打断
		public bool being_interrupted_by_injuries;
		/// 是否是被动加属性技能
		public bool is_attribute_addition;
		/// 覆盖技能编号
		public int code_coverage_skill_id;
    /// 前摇特效s
    public string singing_effects;
    /// 后摇特效s
    public string play_effects;
		/// 技能图标
		public string skill_icon;
		/// 文字描述
		public string desc;
		/// 黄字描述
		public string desc_y;
		/// 前摇动画
		public string boot_animation;
		/// 后摇动画
    public string after_animation;
    /// 作用器编号
    public string effector_ids;
    /// 前摇作用器编号
    public string boot_effector_ids;
    /// 后摇作用器编号
    public string after_effector_ids;
	}
	[XmlRoot ("root")]
	[XmlLate("skill")]
	public class SkillRoot
	{
		[XmlElement ("value")]
		public List<SkillData> items = new List<SkillData> ();

		public static void LateProcess (object obj)
		{
			SkillRoot root = obj as SkillRoot;
			foreach (SkillData item in root.items) {
				Config.skillXmlTable [item.id] = item;
			}
		}
	}
}

