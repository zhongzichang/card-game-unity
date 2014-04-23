using UnityEngine;
using System.Collections;
using TangGame.UI;
public class UITableByHeroFragments : UITable
{
	
	static public int SortByFragments (Transform a, Transform b)
	{
		//		return b.GetComponent<HeroItem> ().Data.FragmentsCount.CompareTo (a.GetComponent<HeroItem> ().Data.FragmentsCount); TODO 需要道具功能的支持
		return 0; 
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortByFragments);
	}
}
