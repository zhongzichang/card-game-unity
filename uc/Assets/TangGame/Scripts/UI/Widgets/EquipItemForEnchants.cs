using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
using TangGame.Xml;
using TangGame.Net;
using TangUI;

namespace TangGame.UI
{
	public class EquipItemForEnchants : MonoBehaviour
	{
		public UISprite equipIcon;
		public UISprite equipFrames;
		public StarList starList;
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
				this.equipIcon.spriteName = xml.icon;
				this.equipFrames.spriteName = "equip_frame_" + HeroBase.GetRankColorStr((RankEnum)xml.upgrade);
				this.starList.count = propsNet.enchantsLv;
				this.starList.Flush ();
			} else {
				this.equipIcon.spriteName = "";
				this.equipFrames.spriteName = "gocha";
			}
		}
	}
}