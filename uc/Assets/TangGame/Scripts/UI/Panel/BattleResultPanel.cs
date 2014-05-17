using UnityEngine;
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
          winStars[1].gameObject.SetActive(true);
        }else if(data.type == BattleResultType.Star3){
          winStars[2].gameObject.SetActive(true);
        }

        Vector3 tempPosition = Vector3.zero;
        foreach(Props props in data.propsList){
          GameObject go = UIUtils.Duplicate(this.propsInfoItem.gameObject, this.propsInfoItem.transform.parent.gameObject);
          go.transform.localPosition = tempPosition;
          tempPosition.x += 100;
          PropsInfoItem item = go.GetComponent<PropsInfoItem>();
          item.data = props;
          items.Add(go);
        }
        
        tempPosition = Vector3.zero;
        foreach(BattleResultHeroData battleResultHeroData in data.herosList){
          GameObject go = UIUtils.Duplicate(this.heroItem.gameObject, this.heroItem.transform.parent.gameObject);
          go.transform.localPosition = tempPosition;
          tempPosition.x += 128;
          BattleResultHeroItem item = go.GetComponent<BattleResultHeroItem>();
          item.data = battleResultHeroData;
          items.Add(go);
        }
      }
		}


		private void NextBtnHandler(GameObject go){

		}

		private void ReplayBtnHandler(GameObject go){
			
		}

		private void DataBtnHandler(GameObject go){
			
		}



	}
}