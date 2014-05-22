using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{
	/// 道具详情面板
	public class PropsDetailsPanel : MonoBehaviour {
		public static string name = "PropsDetailsPanel";
		public UIEventListener closeBtn;
		public UISprite frame;
		public UISprite icon;

		public UIScrollView scrollView;
		public GameObject group;

		public UISprite getGroup;
		public UISprite heroGroup;
		public UISprite syntheticGroup;

		public UILabel getLabel;
		public UILabel heroLabel;
		public UILabel syntheticLabel;

		public PropsDetailsGetItem getItem;
		public PropsDetailsHeroItem heroItem;
		public PropsDetailsSyntheticItem syntheticItem;

		private bool started;
		private object mParam;
		// 存储全部的Item，便于销毁时使用
		private List<ViewItem> items = new List<ViewItem>();

		void Start(){
			closeBtn.onClick += CloseBtnHandler;
			getLabel.text = UIPanelLang.PROPS_DETAILS_GET;
			heroLabel.text = UIPanelLang.PROPS_DETAILS_HERO;
			syntheticLabel.text = UIPanelLang.PROPS_DETAILS_SYNTHETIC;
			getItem.gameObject.SetActive(false);
			heroItem.gameObject.SetActive(false);
			syntheticItem.gameObject.SetActive(false);
			started = true;
			UpdateData();
		}

		public object param{
			get{return this.mParam;}
			set{this.mParam = value;UpdateData();}
		}

		private void CloseBtnHandler(GameObject go){
      UIContext.mgrCoC.Back();
		}

		private void UpdateData(){

      scrollView.SetDragAmount(0, 0, false);

			if(!this.started){return;}
			foreach(ViewItem item in items){
				GameObject.Destroy(item.gameObject);
			}
			items.Clear();
			float y = 0;
			int height = 0;

      bool isShow = true;

      PropsData props = null;
      PropsRelationData data = null;
      if(this.mParam != null){
        props = (PropsData)this.mParam;
        data = PropsCache.instance.GetPropsRelationData(props.id);
      }else{
        return;
      }

      frame.spriteName = Global.GetPropFrameName((PropsType)props.type, props.upgrade);
      icon.spriteName = props.icon;

      isShow = data != null;

      if(!isShow){
        UIUtils.SetY(getLabel.transform.parent.gameObject, -y);
        y += 65;
        UIUtils.SetY(getGroup.gameObject, -y);
        //getGroup.height = 100;
        
        heroLabel.transform.parent.gameObject.SetActive(false);
        heroGroup.gameObject.SetActive(false);
        
        syntheticLabel.transform.parent.gameObject.SetActive(false);
        syntheticGroup.gameObject.SetActive(false);
				return;
			}

			GameObject go = null;
			int count = 0;

			//========================================合成========================================
			int length = data.synthetics.Count;
      syntheticLabel.transform.parent.gameObject.SetActive(length > 0);
			syntheticGroup.gameObject.SetActive(length > 0);
			UIUtils.SetY(syntheticLabel.transform.parent.gameObject, -y);
			if(length > 0){
				y += 65;
				UIUtils.SetY(syntheticGroup.gameObject, -y);
				height = Mathf.FloorToInt((length + 1) / 2) * 70 + 20;
				y += height;
				//syntheticGroup.height = height;
				count = 0;
				foreach(PropsData propsData in data.synthetics){
					go = UIUtils.Duplicate(syntheticItem.gameObject, syntheticGroup.gameObject);
					go.transform.localPosition = new Vector3((count % 2) * 280, -10 + Mathf.FloorToInt(count / 2) * -70,0);
					PropsDetailsSyntheticItem item = go.GetComponent<PropsDetailsSyntheticItem>();
					item.data = propsData;
					items.Add(item);
					count++;
				}
			}

			//========================================装备========================================
			length = data.heros.Count;
      heroLabel.transform.parent.gameObject.SetActive(length > 0);
			heroGroup.gameObject.SetActive(length > 0);
			UIUtils.SetY(heroLabel.transform.parent.gameObject, -y);
			if(length > 0){
				y += 65;
				UIUtils.SetY(heroGroup.gameObject, -y);
				height = Mathf.FloorToInt((length + 1) / 2) * 70 + 20;
				y += height;
				//heroGroup.height = height;
				count = 0;
				foreach(PropsHeroEquipData propsHeroEquipData in data.heros){
					go = UIUtils.Duplicate(heroItem.gameObject, heroGroup.gameObject);
					go.transform.localPosition = new Vector3((count % 2) * 280, -10 + Mathf.FloorToInt(count / 2) * -70,0);
					PropsDetailsHeroItem item = go.GetComponent<PropsDetailsHeroItem>();
					item.data = propsHeroEquipData;
					items.Add(item);
					count++;
				}
			}

			//========================================获得途径,不管有不有数据都要显示========================================
			length = data.levels.Count;
			UIUtils.SetY(getLabel.transform.parent.gameObject, -y);
			y += 65;
			UIUtils.SetY(getGroup.gameObject, -y);
			if(length > 0){
				height = Mathf.FloorToInt((length + 1) / 2) * 70 + 20;
				y += height;
				//getGroup.height = height;
				count = 0;
				foreach(LevelData levelData in data.levels){
					go = UIUtils.Duplicate(getItem.gameObject, getGroup.gameObject);
					go.transform.localPosition = new Vector3((count % 2) * 280, -10 + Mathf.FloorToInt(count / 2) * -70,0);
					PropsDetailsGetItem item = go.GetComponent<PropsDetailsGetItem>();
					item.data = levelData;
					items.Add(item);
					count++;
				}
			}else{
				//getGroup.height = 100;
			}
		}
	}
}