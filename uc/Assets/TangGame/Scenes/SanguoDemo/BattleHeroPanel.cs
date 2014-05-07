using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class BattleHeroPanel : MonoBehaviour {

    public SelectedHeroGrid selectedGrid;

    private Hashtable heroItems = new Hashtable();
    // Use this for initialization
  	void Start () {

  	}
  	
  	// Update is called once per frame
  	void Update () {
  	  
  	}

    void AddHero(HeroItemData data){
      heroItems [data.id] = data;

      {
        HeroItemObject obj = selectedGrid.CreateHeroItemObj (data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }
    }

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> (); 
      HeroItemData data = hero.GetData ();
      if (data != null) {
        Debug.Log("OnItemClicked.");
      }
    }

    void OnGUI(){
      if (GUILayout.Button ("AddHero1")) {
        {
          HeroItemData data = new HeroItemData();
          data.id = "AV";
          data.order = 26;
          data.rank = 7;
          data.level = 40;
          data.stars = 2;
          data.lineType = 2;
          AddHero(data);
        }
      }
      if (GUILayout.Button ("AddHero2")) {
        {
          HeroItemData data = new HeroItemData ();
          data.order = 17;
          data.id = "CM";
          data.rank = 3;
          data.level = 42;
          data.stars = 5;
          data.lineType = 1;
          AddHero (data);
        }
      }
    }
  }
}