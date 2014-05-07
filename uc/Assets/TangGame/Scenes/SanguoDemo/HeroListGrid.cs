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

    public HeroItemObject CreateHeroItemObj(HeroItemData data){
      if (heroObjs.ContainsKey (data.id)) {
        return (HeroItemObject)heroObjs [data.id];
      }

      GameObject hero = NGUITools.AddChild (gameObject, (GameObject)Resources.Load("Prefabs/PvE/HeroItemObj"));
      UIDragScrollView view = (UIDragScrollView)hero.AddComponent<UIDragScrollView> ();
      view.scrollView = scrollView;

      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      heroObjs[data.id] = obj;

      obj.Update (data);
      data.onToggleChanged += ToggleChanged;

      if (grid) {
        grid.Reposition ();
      }
      return obj;
    }

    public void ToggleChanged (string heroId){
      HeroItemObject obj = (HeroItemObject) heroObjs [heroId];
      if (obj) {
        obj.Toggle ();
      }
    }

    private int CompareHeroItem (Transform left, Transform right){
      HeroItemObject leftObj = left.gameObject.GetComponent<HeroItemObject> ();
      HeroItemObject rightObj = right.gameObject.GetComponent<HeroItemObject> ();

      if (leftObj == null || rightObj == null)
        return 0;

      HeroItemData  leftData = leftObj.GetData ();
      HeroItemData  rightData = rightObj.GetData ();
      // 根据等级，星级和品阶依次排序
      int ret = rightData.level.CompareTo(leftData.level);
      if (ret == 0) {
        ret = rightData.stars.CompareTo(leftData.stars);
        if (ret == 0) {
          return  rightData.rank.CompareTo(leftData.rank);
        }
      }
      return ret;
    }

  }

}