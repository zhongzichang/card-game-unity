using TangGame.Xml;
using UnityEngine;
using System.Collections.Generic;

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
	}
}
