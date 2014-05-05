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
        heroTableFront.AddHero (data);
      } else if (data.IsMiddle()) {
        heroTableMiddle.AddHero (data);
      } else if (data.IsBack()) {
        heroTableBack.AddHero (data);
      }
      heroTableAll.AddHero (data);
    }

    void UpdateHero(HeroItemData data){
      selectedHeroTable.UpdateHero (data);
    }

    void OnGUI(){
      if (GUILayout.Button ("AddHero")) {
          HeroItemData data = new HeroItemData ();
          data.icon = "BatRider";
          data.iconFrame = "hero_icon_frame_2";
          data.level = "56";
          data.stars = 3;
          data.camp = 1;
          data.selected = false;
          AddHero (data);
      }
      if (GUILayout.Button ("UpdateHero")) {
        HeroItemData data = new HeroItemData ();
        data.icon = "BatRider";
        data.iconFrame = "hero_icon_frame_2";
        data.level = "56";
        data.stars = 3;
        data.camp = 1;
        data.selected = true;
        UpdateHero (data);
      }
    }
  }
}