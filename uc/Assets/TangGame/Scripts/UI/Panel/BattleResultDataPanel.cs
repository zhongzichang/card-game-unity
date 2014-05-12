using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
	public class BattleResultDataPanel : MonoBehaviour {

		public const string NAME = "BattleResultDataPanel";

		public UIEventListener closeBtn;
		public BattleResultDataItem heroLeftItem;
		public BattleResultDataItem heroRightItem;
		public UILabel leftLabel;
		public UILabel rightLabel;
	

		private object mParam;
		private bool started;
		private List<BattleResultDataItem> items = new List<BattleResultDataItem>();

		void Start(){
			leftLabel.text = UIPanelLang.BATTLE_OUR_DAMAGE;
			rightLabel.text = UIPanelLang.BATTLE_ENEMY_DAMAGE;
			heroLeftItem.gameObject.SetActive(false);
			heroRightItem.gameObject.SetActive(false);
			started = true;

			BattleResultData data = new BattleResultData();
			data.herosList.Add(new BattleResultHeroData());
			data.herosList.Add(new BattleResultHeroData());

			data.enemysList.Add(new BattleResultHeroData());
			data.enemysList.Add(new BattleResultHeroData());
			data.enemysList.Add(new BattleResultHeroData());

			this.mParam = data;

			UpdateData();
		}

		public object param{
			get{return this.mParam;}
			set{this.mParam = value;UpdateData();}
		}

		private void UpdateData(){
			if(!started){return;}
			foreach(BattleResultDataItem item in items){
				GameObject.Destroy(item.gameObject);
			}
			items.Clear();
			if(this.mParam == null){return;}
			CalculateMaxDamage();
			BattleResultData data = this.mParam as BattleResultData;
			Vector3 tempPosition = this.heroLeftItem.transform.localPosition;
			foreach(BattleResultHeroData heroData in data.herosList){
				GameObject go = UIUtils.Duplicate(this.heroLeftItem.gameObject, this.gameObject);
				go.transform.localPosition = tempPosition;
				tempPosition.y -= 80;
				BattleResultDataItem item = go.GetComponent<BattleResultDataItem>();
				item.data = heroData;
				items.Add(item);
			}
			tempPosition = this.heroRightItem.transform.localPosition;
			foreach(BattleResultHeroData heroData in data.enemysList){
				GameObject go = UIUtils.Duplicate(this.heroRightItem.gameObject, this.gameObject);
				go.transform.localPosition = tempPosition;
				tempPosition.y -= 80;
				BattleResultDataItem item = go.GetComponent<BattleResultDataItem>();
				item.data = heroData;
				items.Add(item);
			}
		}

		/// 找出最大伤害
		private void CalculateMaxDamage(){
			int maxDamage = 0;
			BattleResultData data = this.mParam as BattleResultData;
			foreach(BattleResultHeroData heroData in data.herosList){
				if(heroData.damage > maxDamage){
					maxDamage = heroData.damage;
				}
			}
			foreach(BattleResultHeroData heroData in data.enemysList){
				if(heroData.damage > maxDamage){
					maxDamage = heroData.damage;
				}
			}

			foreach(BattleResultHeroData heroData in data.herosList){
				heroData.maxDamage = maxDamage;
			}
			foreach(BattleResultHeroData heroData in data.enemysList){
				heroData.maxDamage = maxDamage;
			}
		}
	}
}