using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class SelectedHeroGrid : MonoBehaviour {
    private UIGrid grid;

    void Start () {
      grid = gameObject.GetComponent<UIGrid> ();
      grid.sorting = UIGrid.Sorting.Custom;
      grid.onCustomSort = CompareHeroItem;
    }

    private int CompareHeroItem (Transform left, Transform right){
      HeroItemObject leftObj = left.gameObject.GetComponent<HeroItemObject> ();
      HeroItemObject rightObj = right.gameObject.GetComponent<HeroItemObject> ();

      if (leftObj == null || rightObj == null)
        return 0;

      // 根据英雄排序表
      return rightObj.HeroData.order.CompareTo (leftObj.HeroData.order);
    }
  }

}