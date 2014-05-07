using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class SelectHeroTable : MonoBehaviour {
    private UITable table;
    private Hashtable heroObjs = new Hashtable();

    void Start () {
      table = gameObject.GetComponent<UITable> ();
    }

    public HeroItemObject CreateHeroItemObj(HeroItemData data){
      GameObject hero = NGUITools.AddChild (gameObject, (GameObject)Resources.Load("Prefabs/PvE/HeroItem"));

      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      heroObjs[data.icon] = obj;
      data.onToggleChanged += ToggleChanged;
      obj.Update (data);

      if (data.toggled) {
        if (table) {
          table.Reposition ();
        }
      } else {
        hero.SetActive (false);
      }

      return obj;
    }

    public void ToggleChanged (string key){
      HeroItemObject obj = (HeroItemObject) heroObjs [key];
      if (obj) {
        obj.Hide ();
      }
      if (table) {
        table.Reposition ();
      }
    }
  }

}