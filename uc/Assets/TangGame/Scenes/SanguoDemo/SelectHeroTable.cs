using UnityEngine;
using System.Collections;

namespace TangGame.UI
{

  public class SelectHeroTable : MonoBehaviour {

    private UITable tableHero;
    // Use this for initialization
  	void Start () {
      tableHero = GetComponent<UITable> ();
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

    public void UpdateHero(HeroItemData data){
      if (data.selected) {
        Debug.Log ("UnselectHero");
        DestoryHero (data);
      } else {
        CreateHero (data);
      }
      tableHero.Reposition ();
    }

    private void DestoryHero(HeroItemData data){
      foreach (Transform t in tableHero.children) {
        HeroItemObject tmp = (HeroItemObject)t.GetComponent<HeroItemObject> ();
        if(tmp.icon.spriteName.Equals(data.icon)){
          GameObject.Destroy (t.gameObject);
        }
      }
    }

    private void CreateHero(HeroItemData data){
      GameObject hero = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PvE/HeroItem"));
      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      obj.UpdateHeroItem (data);
      tableHero.children.Add (hero.transform);
    }
  }

}