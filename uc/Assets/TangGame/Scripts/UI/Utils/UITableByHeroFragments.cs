using UnityEngine;
using System.Collections;
using TangGame.UI;
public class UITableByHeroFragments : UITable
{
	
	static public int SortByFragments (Transform a, Transform b)
	{
		return b.GetComponent<HeroItem> ().SoulStoneCount.CompareTo (a.GetComponent<HeroItem> ().SoulStoneCount); //TODO 需要道具功能的支持
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortByFragments);
	}
}
