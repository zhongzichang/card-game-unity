using UnityEngine;
using System.Collections;
using TangGame.UI;

public class UITableByHeroLevel : UITable
{
	
	static public int SortByLevel (Transform a, Transform b)
	{

		HeroBase Adata = a.GetComponent<HeroItem> ().Data;
		HeroBase Bdata = b.GetComponent<HeroItem> ().Data;

		int val = 0;
		if (Bdata.Net.Equals(null) || Adata.Net.Equals(null) || Adata.Xml.Equals(null) || Bdata.Xml.Equals(null) )
			return val;
		val = Bdata.Net.level.CompareTo (Adata.Net.level);
		if (val == 0) {
			val = Bdata.Net.upgrade.CompareTo(Adata.Net.upgrade);
		}

		return val;
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortByLevel);
	}
}
