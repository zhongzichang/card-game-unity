using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class HeroListGrid : MonoBehaviour {

    private UIGrid grid;
    private UIScrollView scrollView;
    private Hashtable heroObjs = new Hashtable();

    void Start () {
      grid = gameObject.GetComponent<UIGrid> ();
      grid.sorting = UIGrid.Sorting.Custom;
      grid.onCustomSort = CompareHeroItem;

      scrollView = NGUITools.FindInParents<UIScrollView>(gameObject);
    }
      
    public void AddHeroItemObject(HeroItemObject hero){
      heroObjs [hero.HeroId] = hero;
    }

    public void UpdateToggle (string heroId){
      HeroItemObject obj = FindHeroItemObject (heroId);
      if (obj == null)
        return;

      obj.ToggleTick ();
    }

    private HeroItemObject FindHeroItemObject(string heroId){
      return (HeroItemObject) heroObjs [heroId];
    }

    private int CompareHeroItem (Transform left, Transform right){
      HeroItemObject leftObj = left.gameObject.GetComponent<HeroItemObject> ();
      HeroItemObject rightObj = right.gameObject.GetComponent<HeroItemObject> ();

      if (leftObj == null || rightObj == null)
        return 0;

      // 根据等级，星级和品阶依次排序
      int ret = rightObj.HeroData.level.CompareTo(leftObj.HeroData.level);
      if (ret == 0) {
        ret = rightObj.HeroData.stars.CompareTo(leftObj.HeroData.stars);
        if (ret == 0) {
          return  rightObj.HeroData.rank.CompareTo(leftObj.HeroData.rank);
        }
      }
      return ret;
    }

  }

}