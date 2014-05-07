using UnityEngine;
using System.Collections;

namespace TangGame{
	public class BattleResultPanel : MonoBehaviour {

		public GameObject winGroup;
		public UIEventListener winNextBtn;
		public UIEventListener winReplayBtn;
		public UISprite[] winStars;
		public UIEventListener winDataBtn;
		public BattleResultHeroItem heroItem;

		public GameObject loseGroup;
		public UIEventListener loseDataBtn;
		public UIEventListener loseNextBtn;
		public UISprite[] loseSprites;

		public UIEventListener tempBtn;


		void Awake(){
			winNextBtn.onClick += NextBtnHandler;
			winReplayBtn.onClick += ReplayBtnHandler;
			winDataBtn.onClick += DataBtnHandler;

			loseNextBtn.onClick += NextBtnHandler;
			loseDataBtn.onClick += DataBtnHandler;

			tempBtn.onPress += TempBtnHandler;

			winGroup.SetActive(false);
			loseGroup.SetActive(false);
		}

		void Start () {
			winGroup.SetActive(true);
		}
		
		// Update is called once per frame
		void Update () {
			
		}


		private void NextBtnHandler(GameObject go){

		}

		private void ReplayBtnHandler(GameObject go){
			
		}

		private void DataBtnHandler(GameObject go){
			
		}

		private void TempBtnHandler(GameObject go, bool state){
			if(state){
				PropTips.Show(Vector3.zero, 10, 0);
			}else{
				PropTips.Hiddle();
			}
		}

	}
}