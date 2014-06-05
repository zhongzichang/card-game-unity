using UnityEngine;
using System.Collections;
using TangGame.UI;

public class UITableByHeroFragments : UITable
{
	
	private int SortByFragments (Transform a, Transform b)
	{
		HeroBase aBase = a.GetComponent<HeroItem> ().heroBase;
		HeroBase bBase = b.GetComponent<HeroItem> ().heroBase;
	
		return SortByFragments (aBase, bBase);
	}

	static public int SortByFragments (HeroBase aBase, HeroBase bBase)
	{
		int aCount = 0;
		int bCount = 0;
		if (PropsCache.instance.propsTable.ContainsKey (aBase.Xml.soul_rock_id)) {
			aCount = PropsCache.instance.propsTable [aBase.Xml.soul_rock_id].net.count;
		}
		if (PropsCache.instance.propsTable.ContainsKey (bBase.Xml.soul_rock_id)) {
			bCount = PropsCache.instance.propsTable [bBase.Xml.soul_rock_id].net.count;
		}
		int val = bCount.CompareTo (aCount);
		if (val == 0) {
			val = SortById(aBase,bBase);
		}
		return val;
	}

	static public int SortById (HeroBase aBase, HeroBase bBase)
	{
		int val = aBase.Xml.id.CompareTo (bBase.Xml.id);
		return val;
	}

	private int SortById (Transform a, Transform b)
	{
		HeroBase aBase = a.GetComponent<HeroItem> ().heroBase;
		HeroBase bBase = b.GetComponent<HeroItem> ().heroBase;
		return SortById (aBase, bBase);
	}

	protected override void Sort (System.Collections.Generic.List<Transform> list)
	{
		list.Sort (SortById);
		list.Sort (SortByFragments);
	}
}
