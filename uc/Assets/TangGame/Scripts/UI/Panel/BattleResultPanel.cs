using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame{
	public class BattleResultPanel : MonoBehaviour {

		public const string NAME = "BattleResultPanel";

		public GameObject winGroup;
		public UIEventListener winNextBtn;
		public UIEventListener winReplayBtn;
		public UISprite[] winStars;
		public UIEventListener winDataBtn;
		public BattleResultHeroItem heroItem;
		public PropInfoItem propInfoItem;

		public GameObject loseGroup;
		public UIEventListener loseDataBtn;
		public UIEventListener loseNextBtn;
		public UISprite[] loseSprites;

		private object mParam;

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
			winGroup.SetActive(true);
			Props props = new Props();
			props.data = new TangGame.Xml.PropsData();
			props.data.name = "测试道具";
			props.data.type = 2;
			props.count = 2;
			props.data.icon = "104";
			props.data.level = 5;
			props.data.info = "使用后可以获得一个小萝莉";
			props.data.description = "这是测试道具，大家都懂得";

			propInfoItem.data = props;
		}

		public object param{
			get{return this.mParam;}
			set{this.mParam = value;UpdateData();}
		}

		private void UpdateData(){

		}


		private void NextBtnHandler(GameObject go){

		}

		private void ReplayBtnHandler(GameObject go){
			
		}

		private void DataBtnHandler(GameObject go){
			
		}



	}
}