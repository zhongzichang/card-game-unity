using UnityEngine;
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
		private Dictionary<int, PropsItem> propsItems = new Dictionary<int, PropsItem> ();
		// Use this for initialization
		void Start ()
		{
			UIEventListener.Get (ToggleAll.gameObject).onClick += ToggleAllOnClick;
			UIEventListener.Get (ToggleEquip.gameObject).onClick += ToggleEquipOnClick;
			UIEventListener.Get (ToggleScroll.gameObject).onClick += ToggleScrollOnClick;
			UIEventListener.Get (ToggleSoulRock.gameObject).onClick += ToggleSoulRockOnClick;
			UIEventListener.Get (ToggleConsumables.gameObject).onClick += ToggleConsumablesOnClick;


			//TODO 测试数据
			PropsItem item = Resources.Load<PropsItem> (UIContext.getWidgetsPath (UIContext.PROPS_ITEM_NAME));
			for (int i = 0; i < 50; i++) {
				item = NGUITools.AddChild (PropsTable.gameObject, item.gameObject).GetComponent<PropsItem> ();
				propsItems.Add (i, item);//
			}
			PropsTable.GetComponent<UITable> ().repositionNow = true;
			//TODO 测试数据


		}
		// Update is called once per frame
		void Update ()
		{
	
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