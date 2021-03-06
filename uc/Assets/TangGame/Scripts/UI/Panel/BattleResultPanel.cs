﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame;

namespace TangGame.UI{
	public class BattleResultPanel : MonoBehaviour {

		public const string NAME = "BattleResultPanel";

		public GameObject winGroup;
		public UIEventListener winNextBtn;
		public UIEventListener winReplayBtn;
    public UISprite winSprite;
		public UISprite[] winStars;
		public UIEventListener winDataBtn;
		public BattleResultHeroItem heroItem;
		public PropsInfoItem propsInfoItem;

		public GameObject loseGroup;
		public UIEventListener loseDataBtn;
		public UIEventListener loseNextBtn;
    public UISprite loseSprite;
		public UISprite[] loseSprites;

    public UILabel levelLabel;
    public UILabel expLabel;
    public UILabel goldLabel;
    public UILabel winDataBtnLabel;
    public UILabel loseDataBtnLabel;

		private object mParam;
    private bool started;

    /// 存储创建的ITEM，便于删除销毁
    private List<GameObject> items = new List<GameObject>();
    //显示动画使用变量
    private int showPropsInfoItemCount;
    private int showPropsInfoItemTotal;
    private int showBattleResultHeroItemCount;
    private int showBattleResultHeroItemTotal;
    private float time = 0;

		void Awake(){
			winNextBtn.onClick += NextBtnHandler;
			winReplayBtn.onClick += ReplayBtnHandler;
			winDataBtn.onClick += DataBtnHandler;

			loseNextBtn.onClick += NextBtnHandler;
			loseDataBtn.onClick += DataBtnHandler;

			winGroup.SetActive(false);
			loseGroup.SetActive(false);
		}

		void Start () {
      heroItem.gameObject.SetActive(false);
      propsInfoItem.gameObject.SetActive(false);

      started = true;
      UpdateData();
		}

    void Update(){
      time += Time.deltaTime;
      if(time < 0.2f){
        return;
      }else{
        time = 0;
      }

      if(showPropsInfoItemCount < showPropsInfoItemTotal){
        items[showPropsInfoItemCount].gameObject.SetActive(true);
        showPropsInfoItemCount++;
      }

      if(showBattleResultHeroItemCount < showBattleResultHeroItemTotal){
        items[showBattleResultHeroItemCount].gameObject.SetActive(true);
        showBattleResultHeroItemCount++;
      }
    }

		public object param{
			get{return this.mParam;}
			set{this.mParam = value;UpdateData();}
		}

		private void UpdateData(){
      foreach(GameObject go in items){
        GameObject.Destroy(go);
      }
      items.Clear();

      if(!started){return;}
      if(this.mParam == null){return;}
      BattleResultData data = this.mParam as BattleResultData;
      levelLabel.text = "LV:" + data.level;
      expLabel.text = "+" + data.exp;
      goldLabel.text = "+" +data.gold;

      if(data.type == BattleResultType.Lose || data.type == BattleResultType.Timeout){
        winGroup.SetActive(false);
        loseGroup.SetActive(true);
        loseSprite.spriteName = "lose";
      }else{
        winGroup.SetActive(true);
        loseGroup.SetActive(false);
        foreach(UISprite star in winStars){
          star.gameObject.SetActive(false);
        }
        winSprite.gameObject.SetActive(false);

        if(data.type == BattleResultType.Win){
          winSprite.gameObject.SetActive(true);
        }else if(data.type == BattleResultType.Star1){
          winStars[0].gameObject.SetActive(true);
        }else if(data.type == BattleResultType.Star2){
          winStars[0].gameObject.SetActive(true);
          winStars[1].gameObject.SetActive(true);
        }else if(data.type == BattleResultType.Star3){
          winStars[0].gameObject.SetActive(true);
          winStars[1].gameObject.SetActive(true);
          winStars[2].gameObject.SetActive(true);
        }

        showPropsInfoItemCount = 0;

        Vector3 tempPosition = Vector3.zero;
        foreach(Props props in data.propsList){
          GameObject go = UIUtils.Duplicate(this.propsInfoItem.gameObject, this.propsInfoItem.transform.parent.gameObject);
          go.transform.localPosition = tempPosition;
          tempPosition.x += 100;
          go.SetActive(false);
          PropsInfoItem item = go.GetComponent<PropsInfoItem>();
          item.data = props;
          items.Add(go);
        }
        showPropsInfoItemTotal = items.Count;

        showBattleResultHeroItemCount = showPropsInfoItemTotal;
        tempPosition = Vector3.zero;
        foreach(BattleResultHeroData battleResultHeroData in data.herosList){
          GameObject go = UIUtils.Duplicate(this.heroItem.gameObject, this.heroItem.transform.parent.gameObject);
          go.transform.localPosition = tempPosition;
          tempPosition.x += 128;
          go.SetActive(false);
          BattleResultHeroItem item = go.GetComponent<BattleResultHeroItem>();
          item.data = battleResultHeroData;
          items.Add(go);
        }
        showBattleResultHeroItemTotal = items.Count;
      }
		}


		private void NextBtnHandler(GameObject go){

		}

		private void ReplayBtnHandler(GameObject go){
			
		}

		private void DataBtnHandler(GameObject go){
			
		}
    /*
    void OnGUI(){
      if(GUI.Button(new Rect(50, 50, 50, 50), "T")){
        Props props = new Props();
        props.data = new TangGame.Xml.PropsData();
        props.data.name = "测试道具";
        props.data.type = 2;
        props.count = 2;
        props.data.icon = "104";
        props.data.level = 5;
        props.data.info = "使用后可以获得一个小萝莉";
        props.data.description = "这是测试道具，大家都懂得";
        
        BattleResultData data = new BattleResultData();
        data.type = BattleResultType.Star2;
        data.level = 20;
        data.exp = 12;
        data.gold = 807;
        data.propsList.Add(props);
        data.propsList.Add(props);
        data.propsList.Add(props);
        data.propsList.Add(props);

        int[] heroIds = new int[]{1,2,3};
        foreach (int heroId in heroIds) {
          BattleResultHeroData battleResultHeroData = new BattleResultHeroData ();
          battleResultHeroData.id = heroId;
          battleResultHeroData.evolve = 1;
          battleResultHeroData.maxExp = battleResultHeroData.exp = 100;
          battleResultHeroData.level = 20;
          battleResultHeroData.levelUp = false;
          battleResultHeroData.rank = 2;
          data.herosList.Add (battleResultHeroData);
        }
        param = data;
      }
    }*/

	}
}