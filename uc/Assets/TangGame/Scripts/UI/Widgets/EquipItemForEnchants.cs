using UnityEngine;
using System.Collections;
using TangGame.UI;
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
		Equip equipBase = null;

		public Equip EquipBase {
			get {
				return equipBase;
			}
		}



		/// <summary>
		/// Flush the specified hero.
		/// 穿戴在人物身上的装备专用的flush
		/// </summary>
		/// <param name="hero">Hero.</param>
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
			if (equipBase != null && equipBase.net != null) {
				this.equipIcon.spriteName = equipBase.data.Icon;
				this.equipFrames.spriteName = Global.GetPropFrameName(equipBase.data.rank);
				this.starList.count = equipBase.net.enchantsLv;
			} else {
				this.equipIcon.spriteName = "";
        this.equipFrames.spriteName = "item_frame_1";
			}
			this.starList.Flush ();
		}
	}
}