using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame{
	
	/// 战斗界面英雄信息面板
	public class BattleHeroPanel : MonoBehaviour {
		public UISprite background;
		public GameObject itemGroup;
		public BattleHeroItem heroItem;

		private object mParam;
		private bool started;
		/// 对象列表
		private List<BattleHeroItem> itemList = new List<BattleHeroItem>();

		void Start(){
			heroItem.gameObject.SetActive(false);
			started = true;
			UpdateData();
		}

		/// 面板的参数对象
		public object param{
			get{return this.mParam;}
			set{this.mParam = value;this.UpdateData();}
		}

		private void UpdateData(){
			if(!started){return;}
			if(this.mParam == null){return;}
			BattleHeroPanelData data = this.mParam as BattleHeroPanelData;
			foreach(BattleHeroItem item in itemList){
				GameObject.Destroy(item.gameObject);
			}
			itemList.Clear();

			float gap = 140;
			this.background.width = (int)(40 + gap * data.heros.Count);
			float startX = -(data.heros.Count - 1) / 2 * gap;
			foreach(int id in data.heros){
				GameObject go = GameObject.Instantiate(heroItem.gameObject) as GameObject;
				go.name = "HeroItem_" + id;
				go.SetActive(true);
				go.transform.parent = itemGroup.transform;
				go.transform.localScale = Vector3.one;
				go.transform.localPosition = new Vector3(startX, 0, 0);
				startX += gap;
				BattleHeroItem item = go.GetComponent<BattleHeroItem>();
				itemList.Add(item);
			}
		}

	}
}