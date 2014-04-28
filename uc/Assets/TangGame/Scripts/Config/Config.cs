using TangGame.Xml;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace TangGame
{
	public class Config
	{

    // XML 配置 ------
    // 所有加载后的XML对象
    public static Dictionary<string, object> xmlObjTable = new Dictionary<string, object> ();

		public static Dictionary<int,HeroXml> heroXmlTable = new Dictionary<int, HeroXml> ();

		public static Dictionary<int,LevelUpXml> levelUpXmlTable = new Dictionary<int,LevelUpXml>();

		public static Dictionary<int,SkillXml> skillXmlTable = new Dictionary<int, SkillXml> ();

		public static Dictionary<int, PropsXml> propsXmlTable = new Dictionary<int, PropsXml>();



		/// <summary>
		/// The properties heroes relationship.道具和英雄之间的关联
		/// </summary>
		public Dictionary <int, ArrayList> propsHeroesRelationship = new Dictionary<int, ArrayList>();
		/// <summary>
		/// The properties levels relationship.道具和关卡之间的关联
		/// </summary>
		public Dictionary <int, ArrayList> propsLevelsRelationship = new Dictionary<int, ArrayList>();
		/// <summary>
		/// The properties properties relationship.道具和道具之间的关联
		/// </summary>
		public Dictionary <int, ArrayList> propsPropsRelationship = new Dictionary<int, ArrayList>();
	}
}
