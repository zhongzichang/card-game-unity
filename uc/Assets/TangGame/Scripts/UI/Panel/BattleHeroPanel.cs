using UnityEngine;
using System.Collections;

namespace TangGame{
	
	/// 战斗界面英雄信息面板
	public class BattleHeroPanel : MonoBehaviour {
		public UISprite background;
		public BattleHeroItem heroItem;

		public UIEventListener hpBtn;
		public UIEventListener mpBtn;

		private int hp;
		private int mp;

		void Start(){
			//heroItem.gameObject.SetActive(false);
			hpBtn.onClick += HpBtnClickHandler;
			mpBtn.onClick += MpBtnClickHandler;
		}

		private void HpBtnClickHandler(GameObject go){
			int temp = Random.Range(1,1000);
			heroItem.SetHp(temp, 1000);
		}

		private void MpBtnClickHandler(GameObject go){
			int temp = Random.Range(1,1000);
			heroItem.SetMp(temp, 1000);
		}

	}
}