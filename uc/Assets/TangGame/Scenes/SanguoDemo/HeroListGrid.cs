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
      scrollView = NGUITools.FindInParents<UIScrollView>(gameObject);
    }

    public HeroItemObject CreateHeroItemObj(HeroItemData data){
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

//    public HeroItemObject FindHeroItem(HeroItemData data){
//      foreach( HeroItemObject t in heroList){
//        if(t.icon.spriteName.Equals(data.icon)){
//          return t;
//        }
//      }
//      return null;
//    }

//    public HeroItemObject CreateHeroItem(HeroItemData data){
//      GameObject hero = NGUITools.AddChild (gameObject, (GameObject)Resources.Load("Prefabs/PvE/HeroItem"));
//
//      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
//      obj.UpdateHeroItem (data);
//      heroList.Add (obj);
//      if (tableHero) {
//        tableHero.Reposition ();
//      }
//      return obj;
//    }

  }

}