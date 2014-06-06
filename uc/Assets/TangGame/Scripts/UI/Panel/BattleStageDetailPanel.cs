using UnityEngine;
using System.Collections;
using TangUI;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI
{
  public class BattleStageDetailPanel : MonoBehaviour {

    public const string NAME = "BattleStageDetailPanel";

    public UILabel vitLabel;
    public UILabel enemyLabel;
    public UILabel rewardLabel;
    public UILabel sweepLabel;
    public UILabel sweepTenLabel;
    public UILabel ticketLabel;

    public UILabel titleLabel;
    public UILabel descLabel;
    public UILabel sweepNumLabel;
    public UILabel vitCost;
    public UIEventListener startBtn;
    public UIEventListener sweepBtn;
    public UIEventListener sweepTenBtn;
    public UISprite[] stars;

    public StageMonsterItem stageMonsterItem;
    public PropsInfoItem propsInfoItem;

    private object mParam;
    private bool started;
    private List<GameObject> items = new List<GameObject>();

		public object param {
			get {return mParam;}
			set {mParam = value;}
		}

  	void Start () {
      startBtn.onClick += OnButtonClicked;
      this.vitLabel.text = UIPanelLang.STAGE_VIT;
      this.enemyLabel.text = UIPanelLang.STAGE_ENEMY;
      this.rewardLabel.text = UIPanelLang.STAGE_REWARD;
      this.sweepLabel.text = UIPanelLang.STAGE_SWEEP;
      this.sweepTenLabel.text = UIPanelLang.STAGE_SWEEP_TEN;
      this.ticketLabel.text = UIPanelLang.STAGE_SWEEP_TICKET;
      stageMonsterItem.gameObject.SetActive(false);
      propsInfoItem.gameObject.SetActive(false);
      started = true;

      Level level = LevelCache.instance.GetLevel(1001);
      this.mParam = level;
      UpdateData();
  	}

    private void UpdateData(){
      if(!started){return;}

      foreach(GameObject go in items){
        GameObject.Destroy(go);
      }
      items.Clear();

      if(mParam == null){
        return;
      }

      Level level = this.mParam as Level;

      titleLabel.text = level.data.name;
      descLabel.text = level.data.description;
      vitCost.text = level.data.energy_consumption.ToString();

      string[] list = new string[]{};
      if(!string.IsNullOrEmpty(level.data.monster_ids)){
        list = level.data.monster_ids.Replace("[", "").Replace("]", "").Split(',');
      }

      int index = list.Length - 5;
      index = index < 0 ? 0 : index;//只取最后5个，因为只能显示5个

      Vector3 tempPosition = this.stageMonsterItem.transform.localPosition;
      for(int i = index, length = list.Length; i < length; i++){
        int id = int.Parse(list[i]);
        MonsterData monsterData = LevelCache.instance.GetMonsterData(id);
        if(monsterData != null){
          GameObject go = UIUtils.Duplicate(this.stageMonsterItem.gameObject, this.stageMonsterItem.transform.parent.gameObject);
          if(i == length - 1){
            tempPosition.x += 10;
            tempPosition.y = 8;
            go.transform.localScale = new Vector3(1, 1, 1);
          }else{
            go.transform.localScale = new Vector3(0.8f, 0.8f, 1);
          }
          go.transform.localPosition = tempPosition;
          tempPosition.x += 110;
          StageMonsterItem item = go.GetComponent<StageMonsterItem>();
          item.data = monsterData;
          if(i == length - 1){
            item.ShowBossIcon();
          }
          items.Add(go);
        }
      }

      list = new string[]{};
      if(!string.IsNullOrEmpty(level.data.props_ids)){
        list = level.data.props_ids.Replace("[", "").Replace("]", "").Split(',');
      }
      tempPosition = this.propsInfoItem.transform.localPosition;
      for(int i = 0, length = list.Length; i < length; i++){
        int id = int.Parse(list[i]);
        Props props = new Props();
        props.data = PropsCache.instance.GetPropsData(id);
				props.net.count = 1;

        GameObject go = UIUtils.Duplicate(this.propsInfoItem.gameObject, this.propsInfoItem.transform.parent.gameObject);
        go.transform.localPosition = tempPosition;
        tempPosition.x += 110;
        PropsInfoItem item = go.GetComponent<PropsInfoItem>();
        item.data = props;
        items.Add(go);

      }

      //Debug.Log ("boss -- " + data.bossId);
      //HeroItemData boss = TestDataStore.Instance.RandomEnemy(data.bossId);
      //EnemyItemObject bossObj = CreateEnemyItemObj (enemies.gameObject, boss);
      // 先自动排列，再调整boss位置
     // enemies.Reposition ();
     // bossObj.ShowBoss(true);
     // bossObj.gameObject.transform.Translate(new Vector3(0f, 20f, 0f));
     // bossObj.gameObject.transform.localScale = new Vector3 (1.2f, 1.2f, 1.0f);

      //foreach (string rewardId in data.GetRewardIds()) {
      //  RewardItemData item = TestDataStore.Instance.RandRewardItem(rewardId);
        //CreateRewardItemObj (rewards.gameObject, item);
      //}
     // rewards.Reposition ();
    }



    private void OnButtonClicked(GameObject obj){
			Debug.Log ("OnButtonClicked" + obj);
			// 进入选择英雄界面
      UIContext.mgrCoC.LazyOpen (UIContext.BATTLE_SELECT_HERO_PANEL_NAME, UIPanelNode.OpenMode.OVERRIDE, UIPanelNode.BlockMode.TEXTURE);
    }
  }
}