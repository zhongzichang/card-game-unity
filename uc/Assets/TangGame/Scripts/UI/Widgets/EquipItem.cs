using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
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
		Equip equipBase = null;
		/// <summary>
		/// 等级满足
		/// </summary>
		bool lvMeet;
		/// <summary>
		/// 是否拥有装备或可合成装备
		/// </summary>
		bool hasEquip;

		public void Flush (HeroBase hero)
		{
			this.hero = hero;
			if (hero.Net == null)
				return;
			if (hero.Net.equipList != null && hero.EquipBases.Length >= index) {
				equipBase = hero.EquipBases [index - 1];
			}
			this.ContrastLevels ();
			this.ResetStarList ();
			if (equipBase.net != null) {
				this.equipIcon.color = Color.white;
				this.equipIcon.spriteName = equipBase.data.icon;
				this.equipFrames.spriteName = Global.GetPropFrameName (equipBase.data.rank);
				this.label.text = "";
				this.starList.count = equipBase.net.enchantsLv;
				this.starList.Flush ();
			} else {
				if (PropsCache.instance.propsTable.ContainsKey (equipBase.data.id)) {
					this.label.text = UIPanelLang.NOT_EQUIPPED;
					hasEquip = true;
				} else if (CheckSynthesis (equipBase.data.id, 1)) {
					hasEquip = true;
					this.label.text = UIPanelLang.CAN_SYNTHETIC;
				}else {
					hasEquip = false;
					this.label.text = "";
				}

				if (lvMeet) {
					//TODO add 图标为绿色
				} else {
					//TODO add 图标为灰色
				}
				this.equipIcon.color = new Color32 (0, 255, 255, 255);
				this.equipIcon.spriteName = equipBase.data.icon;
				this.equipFrames.spriteName = Global.GetPropFrameName (1);
				this.starList.count = 0;
				this.starList.Flush ();
			}
		}


		bool CheckSynthesis (int propsId, int count)
		{
			PropsData propsData = Config.propsXmlTable [propsId];
			Dictionary<int,int> syntheticPropsTable = propsData.GetSyntheticPropsTable ();
			if (syntheticPropsTable == null || syntheticPropsTable.Count == 0) {
				return false;
			}
			foreach (KeyValuePair<int,int> propsKeyVal in syntheticPropsTable) {
				int num = this.CheckContains (propsKeyVal.Key, propsKeyVal.Value * count);
				if (num < 0) {
					if (CheckSynthesis (propsKeyVal.Key, count))
						return false;
				}
			}
			return true;
		}

		int CheckContains (int propsId, int count)
		{
			if (PropsCache.instance.propsTable.ContainsKey (propsId)) {
				int num = PropsCache.instance.propsTable [propsId].net.count - count;
				return num;
			} else {
				return -1;
			}
		}

		void ResetStarList ()
		{
			this.starList.countMax = 15;
			this.starList.cellWidth = 15;
			this.starList.cellHeight = 12;
			this.starList.showBackground = false;
		}

		/// <summary>
		/// 检查等级
		/// </summary>
		public void ContrastLevels ()
		{
			if (equipBase.data.level <= hero.Net.level) {
				lvMeet = true;
			} else {
				lvMeet = false;
			}
		}

		void OnClick ()
		{
			PropsDetailsPanelBean bean = new PropsDetailsPanelBean ();
			bean.hero = this.hero;
			if (equipBase.net == null) {
				bean.props = equipBase;
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.EQUIP_DETAILS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE, bean, true);
			} else {
				bean.props = equipBase; 
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.EQUIP_INFO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE, bean);
			}
		}

		/// <summary>
		/// 可以穿戴
		/// </summary>
		public bool CanWear {
			get {
				if (hasEquip && lvMeet) {
					return true;
				} else {
					return false;
				}
			}
		}
	}
}