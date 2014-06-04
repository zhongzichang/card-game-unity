using UnityEngine;
using System.Collections;
using TangGame.UI;

public class UITableByHeroLevel : UITable
{
	
	private int SortByLevel (Transform a, Transform b)
	{
		HeroBase Adata = a.GetComponent<HeroItem> ().heroBase;
		HeroBase Bdata = b.GetComponent<HeroItem> ().heroBase;
		return SortByLevel(Adata,Bdata);
	}
	static public int SortByLevel (HeroBase aBase, HeroBase bBase)
	{
		int val = 0;
		if (bBase.Net.Equals (null) || aBase.Net.Equals (null) || aBase.Xml.Equals (null) || bBase.Xml.Equals (null))
			return val;
		val = bBase.Net.level.CompareTo (aBase.Net.level);
		if (val == 0) {
			val = bBase.Net.rank.CompareTo (aBase.Net.rank);
		}
		if (val == 0) {
			val = UITableByHeroFragments.SortByFragments(aBase,bBase);
		}
		return val;
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortByLevel);
	}
}
