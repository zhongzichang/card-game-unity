using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class StageDetailPanel : MonoBehaviour {

    public UILabel title;
    public UILabel desc;
    public UILabel vitCost;
    public UISprite start;
    public UIGrid enemies;
    public UIGrid rewards;

    private StageItemData stageData;
  	// Use this for initialization
  	void Start () {
      UIEventListener.Get (start.gameObject).onClick += OnButtonClicked;
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

    public void Refresh(StageItemData stage){
      stageData = stage;
      title.text = stage.name;
      desc.text = stage.desc;
      vitCost.text = stage.vitCost.ToString();

      foreach (string enemyId in stage.enemies.Split(',')) {
        HeroItemData enemy = null;
        CreateEnemyItemObj (enemies.gameObject, enemy);
      }
      enemies.Reposition ();
      foreach (string rewardId in stage.rewards.Split(',')) {
        RewardItemData item = null;
        CreateRewardItemObj (rewards.gameObject, item);
      }
      rewards.Reposition ();
    }

    private HeroItemObject CreateEnemyItemObj(GameObject parent, HeroItemData data){
      GameObject obj = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/HeroItemObj"));

      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> ();
      hero.Refresh (data);
      hero.ShowLevel = false;
      return hero;
    }

    private RewardItemObject CreateRewardItemObj(GameObject parent, RewardItemData data){
      GameObject obj = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/RewardItemObj"));

      RewardItemObject item = (RewardItemObject)obj.GetComponent<RewardItemObject> ();
      item.Refresh (data);
      return item;
    }

    private void OnButtonClicked(GameObject obj){
      Debug.Log ("OnButtonClicked" + stageData.chapterId + stageData.id);
      // 进入关卡
    }

    void OnGUI(){
      if (GUILayout.Button ("Refresh")) {
        StageItemData  data = TestDataStore.Instance.RandomStage(0, 1);
        Refresh (data);
      }
      if (GUILayout.Button ("Add Enemy Item")) {
        CreateEnemyItemObj (enemies.gameObject, TestDataStore.Instance.RandomHero());
      }
      if (GUILayout.Button ("Add Reward Item")) {
        CreateRewardItemObj (rewards.gameObject, TestDataStore.Instance.RandRewardItem());
      }
    }
  }
}