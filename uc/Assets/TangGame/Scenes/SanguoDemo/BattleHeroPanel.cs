using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class BattleHeroPanel : MonoBehaviour {

    public SelectedHeroGrid selectedGrid;
    private Hashtable heroObjs = new Hashtable();

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
        heroObjs [obj.HeroId] = obj;
        handler.updateMp += UpdateMp;
      }
    }

    private void UpdateMp (string heroId, int mp){
      HeroItemObject hero = (HeroItemObject) heroObjs [heroId];
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
        AddHero (TestDataStore.Instance.RandomHero());
      }
      if (GUILayout.Button ("UpdateMp")) {
          UpdateMp ("CM", 100);
        }
    }
  }
}