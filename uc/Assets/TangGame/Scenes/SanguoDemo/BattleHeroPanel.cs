using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class BattleHeroPanel : MonoBehaviour {

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

      {
        HeroItemObject obj = CreateHeroItemObj (selectedGrid.gameObject, data);
        selectedGrid.AddHeroItemObject (obj);
        handler.updateMp += UpdateMp;
      }
    }

    private void UpdateMp (string heroId, int mp){
      HeroItemObject hero = selectedGrid.GetHeroItemObjById (heroId);
      if (hero == null)
        return;
      hero.UpdateMp (mp);
    }

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> (); 
      Debug.Log ("OnItemClicked");
      hero.UpdateMp (0);
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
          data.hp = 10;
          data.hpMax = 100;
          data.mp = 30;
          data.mpMax = 100;
          AddHero (data);
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
          data.hp = 10;
          data.hpMax = 100;
          data.mp = 30;
          data.mpMax = 100;
          AddHero (data);
        }
      }
      if (GUILayout.Button ("UpdateMp")) {
        {
          UpdateMp ("CM", 100);
        }
      }
    }
  }
}