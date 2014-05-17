using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame.UI
{
  public class StageDetailPanel : MonoBehaviour {

    public UILabel title;
    public UILabel desc;
    public UILabel vitCost;
    public UISprite start;
    public UIGrid enemies;
    public UIGrid rewards;

		object mParam;

		public object param {
			get {
				return mParam;
			}
			set {
				Debug.Log ("Show StageDetailPanel with " + param);
				mParam = value;
			}
		}

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

      foreach (string enemyId in  stage.GetEnemyIds()) {
        HeroItemData enemy = TestDataStore.Instance.RandomEnemy(enemyId);
        CreateEnemyItemObj (enemies.gameObject, enemy);
      }

      Debug.Log ("boss -- " + stage.bossId);
      HeroItemData boss = TestDataStore.Instance.RandomEnemy(stage.bossId);
      EnemyItemObject bossObj = CreateEnemyItemObj (enemies.gameObject, boss);
      // 先自动排列，再调整boss位置
      enemies.Reposition ();
      bossObj.ShowBoss(true);
      bossObj.gameObject.transform.Translate(new Vector3(0f, 20f, 0f));
      bossObj.gameObject.transform.localScale = new Vector3 (1.2f, 1.2f, 1.0f);

      foreach (string rewardId in stage.GetRewardIds()) {
        RewardItemData item = TestDataStore.Instance.RandRewardItem(rewardId);
        CreateRewardItemObj (rewards.gameObject, item);
      }
      rewards.Reposition ();
    }

    private EnemyItemObject CreateEnemyItemObj(GameObject parent, HeroItemData data){
      GameObject obj = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/EnemyItemObj"));

      EnemyItemObject enemy = (EnemyItemObject)obj.GetComponent<EnemyItemObject> ();
      enemy.Refresh (data);
      return enemy;
    }

    private RewardItemObject CreateRewardItemObj(GameObject parent, RewardItemData data){
      GameObject obj = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/RewardItemObj"));

      RewardItemObject item = (RewardItemObject)obj.GetComponent<RewardItemObject> ();
      item.Refresh (data);
      return item;
    }

    private void OnButtonClicked(GameObject obj){
			Debug.Log ("OnButtonClicked" + obj);
			// 进入选择英雄界面
			UIContext.mgrCoC.LazyOpen (UIContext.BATTLE_SELECT_HERO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.TEXTURE);
    }
  }
}