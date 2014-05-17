using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class SelectHeroPanel : MonoBehaviour {

    public UIScrollView scrollView;
    public HeroListGrid allGrid;
    public HeroListGrid frontGrid;
    public HeroListGrid middleGrid;
    public HeroListGrid backGrid;

    public SelectedHeroGrid selectedGrid;
    public UIButton battleButton;

    private ArrayList selectHeroes = new ArrayList();
    private Hashtable heroObjs = new Hashtable();

    void Start(){
      UIEventListener.Get (battleButton.gameObject).onClick += OnBattleButtonClicked;
    }

    public void AddHero(HeroItemData data){
      if(HeroStore.Instance.HasHero(data.id)){
        return;
      }
      AddHeroToList (data);
    }

    private void OnBattleButtonClicked(GameObject obj){
      int levelId = -1;
      Debug.Log ("===== ChallengeLevel ====");
      // 开始战斗
//      TangLevel.LevelController.ChallengeLevel (levelId, heroes);
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
        // 默认隐藏
        obj.gameObject.SetActive (false);
        heroObjs [obj.HeroId] = obj;
        handler.updateToggle += UpdateSelectedGridToggle;
      }
    }

    public void UpdateSelectedGridToggle (string heroId){
      HeroItemObject obj = (HeroItemObject) heroObjs [heroId];
      if (obj == null)
        return;
      if (obj.gameObject.activeSelf) {
        obj.gameObject.SetActive (false);
      } else {
        selectHeroes.Add (int.Parse (obj.HeroData.id));
        obj.gameObject.SetActive (true);
      }
      UIGrid grid = obj.transform.parent.GetComponent<UIGrid> ();
      grid.Reposition ();
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

      // 添加DragScrollView
      UIDragScrollView dragScrollView = hero.AddComponent<UIDragScrollView> ();
      dragScrollView.scrollView = scrollView;

      // 刷新
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

//    void OnGUI(){
//      if (GUILayout.Button ("AddHero")) {
//        AddHero (TestDataStore.Instance.RandomHero());
//      }
//    }
  }
}