using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class SelectHeroPanel : MonoBehaviour {

    public HeroListTable heroTableAll;
    public HeroListTable heroTableFront;
    public HeroListTable heroTableMiddle;
    public HeroListTable heroTableBack;

    public SelectHeroTable heroTableSelected;

    private Hashtable heroItems = new Hashtable();
    // Use this for initialization
  	void Start () {

  	}
  	
  	// Update is called once per frame
  	void Update () {
  	  
  	}

    void AddHero(HeroItemData data){
      heroItems [data.icon] = data;

      HeroListTable heroTable = GetHeroTable (data);
      if (heroTable) {
        HeroItemObject obj = heroTable.CreateHeroItemObj(data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }

      {
        HeroItemObject obj = heroTableAll.CreateHeroItemObj (data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }

      {
        HeroItemObject obj = heroTableSelected.CreateHeroItemObj (data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }
    }

    private HeroListTable GetHeroTable(HeroItemData data){
      if (data.IsFront()) return heroTableFront;
      if (data.IsMiddle()) return heroTableMiddle;
      if (data.IsBack()) return heroTableBack;
      return null;
    }

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> (); 
      HeroItemData data = (HeroItemData)heroItems[hero.name];
      if (data != null) {
        data.Toggle ();
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
    }
  }
}