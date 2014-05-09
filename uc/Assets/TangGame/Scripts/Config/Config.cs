﻿using TangGame.Xml;
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
		public static Dictionary<int,LevelUpXml> levelUpXmlTable = new Dictionary<int,LevelUpXml> ();
		public static Dictionary<int,SkillXml> skillXmlTable = new Dictionary<int, SkillXml> ();
		public static Dictionary<int, PropsXml> propsXmlTable = new Dictionary<int, PropsXml> ();
		public static Dictionary<int,EvolveXml> evolveXmlTable = new Dictionary<int, EvolveXml> ();
		public static Dictionary<int,EnchantsConsumedXml> enchantsConsumedXmlTable = new Dictionary<int, EnchantsConsumedXml>();
		/// <summary>
		/// The properties heroes relationship.
		/// 道具和英雄之间的关联
		/// </summary>
		public static Dictionary <int, ArrayList> propsHeroesRelationship = new Dictionary<int, ArrayList> ();
		/// <summary>
		/// The properties levels relationship.
		/// 道具和关卡之间的关联
		/// </summary>
		public static  Dictionary <int, ArrayList> propsLevelsRelationship = new Dictionary<int, ArrayList> ();
		/// <summary>
		/// The properties properties relationship.
		/// 道具和道具之间的关联
		/// 可合成的道具列表
		/// </summary>
		public static Dictionary <int, ArrayList> propsPropsRelationship = new Dictionary<int, ArrayList> ();

		/// <summary>
		/// Adds the properties properties relationship.
		/// </summary>
		/// <param name="propsId1">Properties id1.</param>
		/// <param name="propsId2">Properties id2.</param>
		public static void addPropsPropsRelationship (int propsXmlId1, int propsXmlId2)
		{
			if (!propsPropsRelationship.ContainsKey (propsXmlId1)) {
				propsPropsRelationship.Add (propsXmlId1, new ArrayList ());
			}
			ArrayList array = propsPropsRelationship [propsXmlId1];
			if (!array.Contains (propsXmlId2))
				array.Add (propsXmlId2);
		}

		/// <summary>
		/// Adds the properties heroes relationship.
		/// </summary>
		/// <param name="propsXmlId">Properties xml identifier. 道具id</param>
		/// <param name="heroXmlId">Hero xml identifier. 英雄id</param>
		public static void addPropsHeroesRelationship (int propsXmlId, int heroXmlId)
		{
			if (!propsPropsRelationship.ContainsKey (propsXmlId)) {
				propsPropsRelationship.Add (propsXmlId, new ArrayList ());
			}
			ArrayList array = propsPropsRelationship [propsXmlId];
			if (!array.Contains (heroXmlId))
				array.Add (heroXmlId);
		}
	}
}
