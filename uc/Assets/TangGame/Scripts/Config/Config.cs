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
		public static Dictionary<int,HeroData> heroXmlTable = new Dictionary<int, HeroData> ();
		public static Dictionary<int,LevelUpData> levelUpXmlTable = new Dictionary<int,LevelUpData> ();
		public static Dictionary<int,SkillData> skillXmlTable = new Dictionary<int, SkillData> ();
		public static Dictionary<int, PropsData> propsXmlTable = new Dictionary<int, PropsData> ();
		public static Dictionary<int,EvolveData> evolveXmlTable = new Dictionary<int, EvolveData> ();
		public static Dictionary<int,EnchantsConsumedData> enchantsConsumedXmlTable = new Dictionary<int, EnchantsConsumedData>();
		/// <summary>
		/// The properties heroes relationship.
		/// 道具和英雄之间的关联
		/// </summary>
		public static Dictionary <int, List<HeroData>> propsHeroesRelationship = new Dictionary<int, List<HeroData>> ();
		/// <summary>
		/// The properties properties relationship.
		/// 道具和道具之间的关联
		/// 可合成的道具列表
		/// </summary>
		public static Dictionary <int, List<PropsData>> propsPropsRelationship = new Dictionary<int, List<PropsData>> ();



	}
}
