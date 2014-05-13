using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{
	public class EvolveData
	{ 
		public int lv;
		public int val;
		public string val_all;
	}

	[XmlRoot ("root")]
	[XmlLate("evolve")]
	public class EvolveRoot
	{
		[XmlElement ("value")]
		public List<EvolveData> items = new List<EvolveData> ();

		public static void LateProcess (object obj)
		{
			EvolveRoot root = obj as EvolveRoot;
			foreach (EvolveData item in root.items) {
				Config.evolveXmlTable [item.lv] = item;
			}
		}
	}
}