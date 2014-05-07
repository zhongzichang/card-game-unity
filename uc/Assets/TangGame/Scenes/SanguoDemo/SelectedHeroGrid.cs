using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class SelectedHeroGrid : MonoBehaviour {
    private UIGrid grid;
    private Hashtable heroObjs = new Hashtable();

    void Start () {
      grid = gameObject.GetComponent<UIGrid> ();
    }

    public HeroItemObject CreateHeroItemObj(HeroItemData data){
      GameObject hero = NGUITools.AddChild (gameObject, (GameObject)Resources.Load("Prefabs/PvE/HeroItemObj"));

      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      heroObjs[data.id] = obj;
      data.onToggleChanged += ToggleChanged;
      obj.Update (data);

      if (data.toggled) {
        if (grid) {
          grid.Reposition ();
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
      if (grid) {
        grid.Reposition ();
      }
    }
  }

}