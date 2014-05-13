using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class LevelUpXml
	{
		public int lv;
		public long val;
	}

	[XmlRoot ("root")]
	[XmlLate("level_up")]
	public class LevelUpRoot
	{
		[XmlElement ("value")]
		public List<LevelUpXml> items = new List<LevelUpXml> ();

		public static void LateProcess (object obj)
		{
			LevelUpRoot root = obj as LevelUpRoot;
			foreach (LevelUpXml item in root.items) {
				Config.levelUpXmlTable [item.lv] = item;
			}
		}
	}
}