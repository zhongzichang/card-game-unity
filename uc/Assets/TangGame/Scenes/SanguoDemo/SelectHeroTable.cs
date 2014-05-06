using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class SelectHeroTable : MonoBehaviour {

    private UITable tableHero;
    // Use this for initialization
  	void Start () {
      tableHero = GetComponent<UITable> ();
      selectedHero = new BetterList<HeroItemObject> ();
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

    public void UpdateHero(HeroItemData data){
      HeroItemObject obj = FindHeroItem (data);
      if (obj == null) {
        obj = CreateHeroItem (data);
        selectedHero.Add (obj);
      } else {
        obj.gameObject.SetActive (true);
      }
      tableHero.Reposition ();
    }

    private BetterList<HeroItemObject> selectedHero ;

    private HeroItemObject FindHeroItem(HeroItemData data){
      foreach( HeroItemObject t in selectedHero){
        if(t.icon.spriteName.Equals(data.icon)){
          return t;
        }
      }
      return null;
    }

    private HeroItemObject CreateHeroItem(HeroItemData data){
      GameObject hero = NGUITools.AddChild (tableHero.gameObject, (GameObject)Resources.Load("Prefabs/PvE/HeroItem"));
      UIEventListener.Get(hero).onClick += OnHeroItemClicked;

      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      obj.UpdateHeroItem (data);

      return obj;
    }

    private void OnHeroItemClicked(GameObject obj){
      obj.SetActive (false);
    }
  }

}