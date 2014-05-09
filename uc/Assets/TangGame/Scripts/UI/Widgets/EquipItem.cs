﻿
using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
using TangGame.Xml;
using TangGame.Net;
using TangUI;

namespace TangGame.UI
{
	public class EquipItem : MonoBehaviour
	{
		public int index;
		public UISprite equipIcon;
		public UISprite equipFrames;
		public StarList starList;
		public UILabel label;
		HeroBase hero;
		EquipBase equipBase = null;
		public void Flush(HeroBase hero){
			this.hero = hero;
			if (hero.Net == null)
				return;
			if (hero.Net.equipList != null && hero.EquipBases.Length >= index) {
				equipBase = hero.EquipBases [index - 1];
			}
			this.starList.countMax = 15;
			this.starList.cellWidth = 15;
			this.starList.cellHeight = 12;
			this.starList.showBackground = false;
			if (equipBase.Net != null) {
				this.equipIcon.color = Color.white;
				this.equipIcon.alpha = 1f;
				this.equipIcon.spriteName = equipBase.Xml.icon;
				this.equipFrames.spriteName = "equip_frame_" + HeroBase.GetRankColorStr((RankEnum)equipBase.Xml.upgrade);
				this.label.text = "";
				this.starList.count = equipBase.Net.enchantsLv;
				this.starList.Flush ();
			} else {
				this.equipIcon.color = Color.gray;
				this.equipIcon.alpha = 0.4f;
				this.equipIcon.spriteName = equipBase.Xml.icon;
				this.equipFrames.spriteName = "equip_frame_" + HeroBase.GetRankColorStr(RankEnum.WHITE);
				this.label.text = "未装备";
				this.starList.count = 0;
				this.starList.Flush ();
			}
		}

		void OnClick(){
			PropsDetailsPanelBean bean = new PropsDetailsPanelBean ();
			bean.hero = this.hero;
			if (equipBase.Net == null) {
				bean.props = equipBase;
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.PROPS_DETAILS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE,bean,true);
			} else {
				bean.props = equipBase; 
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.EQUIP_INFO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE,bean);
			}
		}
	}
}