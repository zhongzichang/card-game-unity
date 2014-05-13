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
		public static Dictionary<int,EnchantsConsumedXml> enchantsConsumedXmlTable = new Dictionary<int, EnchantsConsumedXml>();
		/// <summary>
		/// The properties heroes relationship.
		/// 道具和英雄之间的关联
		/// </summary>
		public static Dictionary <int, List<HeroData>> propsHeroesRelationship = new Dictionary<int, List<HeroData>> ();
		/// <summary>
		/// The properties levels relationship.
		/// 道具和关卡之间的关联
		/// </summary>
		public static  Dictionary <int, List<object>> propsLevelsRelationship = new Dictionary<int, List<object>> ();
		/// <summary>
		/// The properties properties relationship.
		/// 道具和道具之间的关联
		/// 可合成的道具列表
		/// </summary>
		public static Dictionary <int, List<PropsData>> propsPropsRelationship = new Dictionary<int, List<PropsData>> ();

		/// <summary>
		/// Adds the properties properties relationship.
		/// </summary>
		/// <param name="propsId1">Properties id1.</param>
		/// <param name="propsId2">Properties id2.</param>
		public static void addPropsPropsRelationship (int propsXmlId, PropsData propsXml)
		{
			if (!propsPropsRelationship.ContainsKey (propsXmlId)) {
				propsPropsRelationship.Add (propsXmlId, new List<PropsData> ());
			}
			List<PropsData> array = propsPropsRelationship [propsXmlId];
			if (!array.Contains (propsXml))
				array.Add (propsXml);
		}

		/// <summary>
		/// Adds the properties heroes relationship.
		/// </summary>
		/// <param name="propsXmlId">Properties xml identifier. 道具id</param>
		/// <param name="heroXmlId">Hero xml identifier. 英雄id</param>
		public static void addPropsHeroesRelationship (int propsXmlId, HeroData heroXml)
		{
			if (!propsHeroesRelationship.ContainsKey (propsXmlId)) {
				propsHeroesRelationship.Add (propsXmlId, new List<HeroData> ());
			}
			List<HeroData> array = propsHeroesRelationship [propsXmlId];
			if (!array.Contains (heroXml))
				array.Add (heroXml);
		}
	}
}
