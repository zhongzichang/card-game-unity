using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class EvolveXml
	{
		public int lv;
		public int val;
	}

	[XmlRoot ("root")]
	[XmlLate("evolve")]
	public class EvolveRoot
	{
		[XmlElement ("value")]
		public List<EvolveXml> items = new List<EvolveXml> ();

		public static void LateProcess (object obj)
		{
			EvolveRoot root = obj as EvolveRoot;
			foreach (EvolveXml item in root.items) {
				Config.evolveXmlTable [item.lv] = item;
			}
		}
	}
}