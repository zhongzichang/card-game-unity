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
    public static Dictionary<int, LevelsData> levelsXmlTable = new Dictionary<int, LevelsData> ();
	}
}
