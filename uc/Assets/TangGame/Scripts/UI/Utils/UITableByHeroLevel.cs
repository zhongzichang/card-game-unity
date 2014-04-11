using UnityEngine;
using System.Collections;

public class UITableByHeroLevel : UITable
{
	
	static public int SortByLevel (Transform a, Transform b)
	{

		HeroBase Adata = a.GetComponent<HeroItem> ().Data;
		HeroBase Bdata = b.GetComponent<HeroItem> ().Data;
		int val = 0;
		val = Bdata.Level.CompareTo (Adata.Level);
		if (val == 0) {
			val = Bdata.HeroesRank.CompareTo(Adata.HeroesRank);
		}

		return val;
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortByLevel);
	}
}
