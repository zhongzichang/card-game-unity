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

    private void AddHero(HeroItemData data){
      if(HeroStore.Instance.HasHero(data.id)){
        return;
      }
      AddHeroToList (data);
    }

    private void AddHeroToList(HeroItemData data){
      HeroStore.Instance.AddHero (data);
      HeroItemUpdateHandler handler = HeroStore.Instance.GetUpdateHandler(data.id);

      HeroListGrid heroGrid = GetHeroGrid (data);
      if (heroGrid) {
        HeroItemObject obj = CreateHeroItemObj(heroGrid.gameObject, data);
        heroGrid.AddHeroItemObject (obj);
        handler.updateToggle += heroGrid.UpdateToggle;
      }

      {
        HeroItemObject obj = CreateHeroItemObj (allGrid.gameObject, data);
        allGrid.AddHeroItemObject (obj);
        handler.updateToggle += allGrid.UpdateToggle;
      }

      {
        HeroItemObject obj = CreateHeroItemObj (selectedGrid.gameObject, data);
        selectedGrid.AddHeroItemObject (obj);
        handler.updateToggle += selectedGrid.UpdateToggle;
      }
    }

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> (); 
      HeroStore.Instance.UpdateToggle (hero.HeroId);
    }

    private HeroItemObject CreateHeroItemObj(GameObject parent, HeroItemData data){
      GameObject hero = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/HeroItemObj"));

      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      obj.Refresh (data);

      UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;

      UIGrid grid = parent.GetComponent<UIGrid> ();
      grid.Reposition ();

      return obj;
    }

    private HeroListGrid GetHeroGrid(HeroItemData data){
      if (data.IsFront()) return frontGrid;
      if (data.IsMiddle()) return middleGrid;
      if (data.IsBack()) return backGrid;
      return null;
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