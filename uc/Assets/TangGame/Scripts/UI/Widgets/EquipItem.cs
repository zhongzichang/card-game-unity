
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
		public UISprite equipIcon;
		public UISprite equipFrames;
		public StarList starList;
		public UILabel label;
		HeroBase hero;
		PropsXml xml;
		EquipNet propsNet = null;
		public void Flush(HeroBase hero,PropsXml xml){
			this.hero = hero;
			this.xml = xml;
			if (hero.Net == null)
				return;
			if (hero.Net.equipList != null) {
				foreach (EquipNet net in hero.Net.equipList) {
					if (net.configId == xml.id)
						propsNet = net;
				}
			}

			this.starList.countMax = 15;
			this.starList.cellWidth = 15;
			this.starList.cellHeight = 12;
			this.starList.showBackground = false;
			if (propsNet != null) {
				this.equipIcon.color = Color.white;
				this.equipIcon.alpha = 1f;
				this.equipIcon.spriteName = xml.icon;
				this.equipFrames.spriteName = "equip_frame_" + HeroBase.GetRankColorStr((RankEnum)xml.upgrade);
				this.label.text = "";
				this.starList.count = propsNet.enchantsLv;
				this.starList.Flush ();
			} else {
				this.equipIcon.color = Color.gray;
				this.equipIcon.alpha = 0.4f;
				this.equipIcon.spriteName = xml.icon;
				this.equipFrames.spriteName = "equip_frame_" + HeroBase.GetRankColorStr(RankEnum.WHITE);
				this.label.text = "未装备";
				this.starList.count = 0;
				this.starList.Flush ();
			}
		}

		void OnClick(){
			PropsDetailsPanelBean bean = new PropsDetailsPanelBean ();
			bean.hero = this.hero;
			if (propsNet == null) {
				bean.props = new PropsBase ();
				bean.props.Xml = xml;
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.PROPS_DETAILS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE,bean);
			} else {
				EquipBase equipBase= new EquipBase ();
				equipBase.Net = propsNet;
				equipBase.Xml = xml;
				bean.props = equipBase; 
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.EQUIP_INFO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE,bean);
			}
		}
	}
}