using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class SelectHeroPanel : MonoBehaviour {

    public HeroListTable heroTableAll;
    public HeroListTable heroTableFront;
    public HeroListTable heroTableMiddle;
    public HeroListTable heroTableBack;

    public SelectHeroTable selectedHeroTable;

    // Use this for initialization
  	void Start () {

  	}
  	
  	// Update is called once per frame
  	void Update () {
  	  
  	}

    void AddHero(HeroItemData data){
      if (data.IsFront()) {
        UpdateHero(heroTableFront, data);
      } else if (data.IsMiddle()) {
        UpdateHero(heroTableMiddle, data);
      } else if (data.IsBack()) {
        UpdateHero(heroTableBack, data);
      }
      UpdateHero(heroTableAll, data);
    }

    void UpdateHero(HeroListTable table, HeroItemData data){
        HeroItemObject obj = table.FindHeroItem (data);
        if (obj == null) {
          obj = table.CreateHeroItem (data);
          UIEventListener.Get(obj.gameObject).onClick += OnHeroViewItemClicked;
        } else {
          obj.gameObject.SetActive (true);
        }
    }

    void SelectHero(SelectHeroTable table, HeroItemData data){
      HeroItemObject obj = table.FindHeroItem (data);
      if (obj == null) {
        obj = table.CreateHeroItem (data);
        UIEventListener.Get(obj.gameObject).onClick += OnSelectHeroItemClicked;
      } else {
        obj.gameObject.SetActive (true);
      }
    }

    private void OnSelectHeroItemClicked(GameObject obj){
      obj.SetActive (false);
    }

    private void OnHeroViewItemClicked(GameObject obj){
      HeroItemObject hero = obj.GetComponent<HeroItemObject>();
      if(hero){
        Debug.Log ("ToggleTick.");
        hero.ToggleTick();
      }
    }

    void OnGUI(){
      if (GUILayout.Button ("AddHero")) {
        {
          HeroItemData data = new HeroItemData ();
          data.icon = "AV";
          data.iconFrame = "hero_icon_frame_7";
          data.level = "40";
          data.stars = 2;
          data.camp = 2;
          AddHero (data);
        }
        {
          HeroItemData data = new HeroItemData ();
          data.icon = "BatRider";
          data.iconFrame = "hero_icon_frame_2";
          data.level = "50";
          data.stars = 3;
          data.camp = 1;
          AddHero (data);
        }
        {
          HeroItemData data = new HeroItemData ();
          data.icon = "Axe";
          data.iconFrame = "hero_icon_frame_10";
          data.level = "66";
          data.stars = 5;
          data.camp = 0;
          AddHero (data);
        }
      }
      if (GUILayout.Button ("SelectHero")) {
        HeroItemData data = new HeroItemData ();
        data.icon = "Axe";
        data.iconFrame = "hero_icon_frame_2";
        data.level = "36";
        data.stars = 4;
        data.camp = 2;
        data.selected = false;
        SelectHero (selectedHeroTable, data);
      }
    }
  }
}