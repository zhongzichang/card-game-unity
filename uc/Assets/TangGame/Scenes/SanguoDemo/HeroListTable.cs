using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class HeroListTable : MonoBehaviour {

    private UITable table;
    private Hashtable heroObjs = new Hashtable();

    void Start () {
      table = gameObject.GetComponent<UITable> ();
    }

    public HeroItemObject CreateHeroItemObj(HeroItemData data){
      GameObject hero = NGUITools.AddChild (gameObject, (GameObject)Resources.Load("Prefabs/PvE/HeroItem"));

      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      heroObjs[data.icon] = obj;
      obj.Update (data);
      data.onToggleChanged += ToggleChanged;

      if (table) {
        table.Reposition ();
      }
      return obj;
    }

    public void ToggleChanged (string key){
      if (!heroObjs.ContainsKey (key))
        return;
      HeroItemObject obj = (HeroItemObject) heroObjs [key];
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