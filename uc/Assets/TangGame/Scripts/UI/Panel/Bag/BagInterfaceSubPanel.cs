﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class BagInterfaceSubPanel : MonoBehaviour
	{
		/// <summary>
		/// The toggle all.
		/// 显示全部物品按钮的对象
		/// </summary>
		public GameObject ToggleAll;
		/// <summary>
		/// The toggle equip.
		/// 显示装备按钮的对象
		/// </summary>
		public GameObject ToggleEquip;
		/// <summary>
		/// The toggle scroll.
		/// 显示卷轴按钮的对象
		/// </summary>
		public GameObject ToggleScroll;
		/// <summary>
		/// The toggle soul rock.
		/// 显示灵魂石按钮的对象
		/// </summary>
		public GameObject ToggleSoulRock;
		/// <summary>
		/// The toggle consumables.
		/// 显示消耗品按钮的对象
		/// </summary>
		public GameObject ToggleConsumables;
		public GameObject PropsTable;
		private BagPanel bagPanel;
		private Dictionary<int, PropsItem> propsItems = new Dictionary<int, PropsItem> ();
		// Use this for initialization
		void Start ()
		{
			UIEventListener.Get (ToggleAll.gameObject).onClick += ToggleAllOnClick;
			UIEventListener.Get (ToggleEquip.gameObject).onClick += ToggleEquipOnClick;
			UIEventListener.Get (ToggleScroll.gameObject).onClick += ToggleScrollOnClick;
			UIEventListener.Get (ToggleSoulRock.gameObject).onClick += ToggleSoulRockOnClick;
			UIEventListener.Get (ToggleConsumables.gameObject).onClick += ToggleConsumablesOnClick;

			if(bagPanel == null)
				bagPanel = NGUITools.FindInParents<BagPanel> (this.gameObject);
			//TODO 测试数据
			PropsItem item = Resources.Load<PropsItem> (UIContext.getWidgetsPath (UIContext.PROPS_ITEM_NAME));
			foreach (PropsBase propsBase in TangGame.UI.Base.BaseCache.propsBaseTable.Values) {
				item = NGUITools.AddChild (PropsTable.gameObject, item.gameObject).GetComponent<PropsItem> ();
				item.Flush (propsBase);
				propsItems.Add (item.data.Xml.id, item);
				UIEventListener.Get (item.gameObject).onClick += PropsItemOnClick;

			}
			PropsTable.GetComponent<UITable> ().repositionNow = true;
			//TODO 测试数据


		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		/// <summary>
		/// Propertieses the item on click.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void PropsItemOnClick(GameObject obj){
			PropsItem item = obj.GetComponent<PropsItem> ();
			bagPanel.UpBagPropsInfoSubPanel (item.data);
		}
		/// <summary>
		/// Toggles all on click.
		/// 显示全部
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleAllOnClick (GameObject obj)
		{
			foreach (PropsItem item in propsItems.Values) {
				if (item.data != null) {
					item.gameObject.SetActive (true);
				}
			}
		}

		/// <summary>
		/// Toggles the equip on click.
		/// 只显示装备
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleEquipOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsTypeEnum.EQUIP);
		}

		/// <summary>
		/// Toggles the scroll on click.
		/// 显示卷轴
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleScrollOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsTypeEnum.SCROLLS);
		}

		/// <summary>
		/// Toggles the soul rock on click.
		/// 显示灵魂石
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleSoulRockOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsTypeEnum.SOULROCK);
		}

		/// <summary>
		/// Toggles the consumables on click.
		/// 显示消耗品
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleConsumablesOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsTypeEnum.EXP, PropsTypeEnum.ENCHANT);
		}

		void ShowItemsByPropsType (PropsTypeEnum type)
		{
			foreach (PropsItem item in propsItems.Values) {
				if (item.data != null) {
					if ((PropsTypeEnum)item.data.Xml.type == type) {
						item.gameObject.SetActive (true);
					} else {
						item.gameObject.SetActive (false);
					}
				}
			}
		}
		/// <summary>
		/// Shows the type of the items by properties.
		/// 根据道具的类型显示item
		/// </summary>
		/// <param name="type1">Type1.</param>
		/// <param name="type2">Type2.</param>
		void ShowItemsByPropsType (PropsTypeEnum type1, PropsTypeEnum type2)
		{
			foreach (PropsItem item in propsItems.Values) {
				if (item.data != null) {
					if ((PropsTypeEnum)item.data.Xml.type == type1 || (PropsTypeEnum)item.data.Xml.type == type2) {
						item.gameObject.SetActive (true);
					} else {
						item.gameObject.SetActive (false);
					}
				}
			}
		}
	}
}