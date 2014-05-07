using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class SelectHeroPanel : MonoBehaviour {

    public HeroListGrid allGrid;
    public HeroListGrid frontGrid;
    public HeroListGrid middleGrid;
    public HeroListGrid backGrid;

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

      HeroListGrid heroGrid = GetHeroGrid (data);
      if (heroGrid) {
        HeroItemObject obj = heroGrid.CreateHeroItemObj(data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }

      {
        HeroItemObject obj = allGrid.CreateHeroItemObj (data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }

      {
        HeroItemObject obj = selectedGrid.CreateHeroItemObj (data);
        UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;
      }
    }

    private HeroListGrid GetHeroGrid(HeroItemData data){
      if (data.IsFront()) return frontGrid;
      if (data.IsMiddle()) return middleGrid;
      if (data.IsBack()) return backGrid;
      return null;
    }

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> (); 
      HeroItemData data = hero.GetData ();
      if (data != null) {
        data.Toggle ();
      }
    }

    void OnGUI(){
      if (GUILayout.Button ("AddHero")) {
        {
          HeroItemData data = new HeroItemData ();
          data.id = "AV";
          data.order = 26;
          data.rank = 7;
          data.level = 40;
          data.stars = 2;
          data.lineType = 2;
          AddHero (data);
        }
        {
          HeroItemData data = new HeroItemData ();
          data.id = "BatRider";
          data.order = 15;
          data.rank = 2;
          data.level = 50;
          data.stars = 3;
          data.lineType = 1;
          AddHero (data);
        }
        {
          HeroItemData data = new HeroItemData ();
          data.id = "Axe";
          data.order = 8;
          data.rank = 8;
          data.level = 66;
          data.stars = 5;
          data.lineType = 0;
          AddHero (data);
        }
      }
      if (GUILayout.Button ("SortNewHero")) {
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