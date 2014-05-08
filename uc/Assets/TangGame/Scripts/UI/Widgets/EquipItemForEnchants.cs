﻿using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
using TangGame.Xml;
using TangGame.Net;
using TangUI;

namespace TangGame.UI
{
	public class EquipItemForEnchants : MonoBehaviour
	{

		public int index;
		public UISprite equipIcon;
		public UISprite equipFrames;
		public StarList starList;
		HeroBase hero;
		EquipBase equipBase = null;

		public EquipBase EquipBase {
			get {
				return equipBase;
			}
		}


		public void Flush(HeroBase hero){
			this.hero = hero;
			if (hero.Net == null)
				return;
			if (hero.Net.equipList != null && hero.EquipBases.Length >= index) {
				equipBase = hero.EquipBases [index - 1];
			}
			this.starList.count = 0;
			this.starList.countMax = 15;
			this.starList.cellWidth = 15;
			this.starList.cellHeight = 12;
			this.starList.showBackground = false;
			if (equipBase != null && equipBase.Net != null) {
				this.equipIcon.spriteName = equipBase.Xml.icon;
				this.equipFrames.spriteName = "equip_frame_" + HeroBase.GetRankColorStr((RankEnum)equipBase.Xml.upgrade);
				this.starList.count = equipBase.Net.enchantsLv;
			} else {
				this.equipIcon.spriteName = "";
				this.equipFrames.spriteName = "gocha";
			}
			this.starList.Flush ();
		}
	}
}