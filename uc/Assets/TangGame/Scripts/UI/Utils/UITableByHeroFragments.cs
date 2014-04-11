using UnityEngine;
using System.Collections;

public class UITableByHeroFragments : UITable
{
	
	static public int SortByFragments (Transform a, Transform b)
	{
		return b.GetComponent<HeroItem> ().Data.FragmentsCount.CompareTo (a.GetComponent<HeroItem> ().Data.FragmentsCount);
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortByFragments);
	}
}
