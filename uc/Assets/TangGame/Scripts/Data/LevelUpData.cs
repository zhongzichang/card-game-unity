using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class LevelUpData
	{ 
		public int lv;
		public long val;
	}

	[XmlRoot ("root")]
	[XmlLate("level_up")]
	public class LevelUpRoot
	{
		[XmlElement ("value")]
		public List<LevelUpData> items = new List<LevelUpData> ();

		public static void LateProcess (object obj)
		{
			LevelUpRoot root = obj as LevelUpRoot;
			foreach (LevelUpData item in root.items) {
				Config.levelUpXmlTable [item.lv] = item;
			}
		}
	}
}