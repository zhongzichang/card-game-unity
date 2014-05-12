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
			PropsBase prop = new PropsBase();
			prop.Xml = new TangGame.Xml.PropsXml();
			prop.Xml.name = "测试道具";
			prop.Xml.type = 2;
			prop.Count = 2;
			prop.Xml.icon = "104";
			prop.Xml.level = 5;
			prop.Xml.info = "使用后可以获得一个小萝莉";
			prop.Xml.description = "这是测试道具，大家都懂得";

			propInfoItem.data = prop;
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