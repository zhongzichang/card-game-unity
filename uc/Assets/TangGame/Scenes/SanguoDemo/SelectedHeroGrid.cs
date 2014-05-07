using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class SelectedHeroGrid : MonoBehaviour {
    private UIGrid grid;
    private Hashtable heroObjs = new Hashtable();

    void Start () {
      grid = gameObject.GetComponent<UIGrid> ();
      grid.sorting = UIGrid.Sorting.Custom;
      grid.onCustomSort = CompareHeroItem;
    }


    public void AddHeroItemObject(HeroItemObject hero){
      heroObjs [hero.HeroId] = hero;
      // 默认隐藏
      hero.gameObject.SetActive (false);
    }

    public void UpdateToggle (string heroId){
      HeroItemObject obj = FindHeroItemObject (heroId);
      if (obj == null)
        return;
      obj.ToggleActive ();
      grid.Reposition ();
    }

    private HeroItemObject FindHeroItemObject(string heroId){
      return (HeroItemObject) heroObjs [heroId];
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