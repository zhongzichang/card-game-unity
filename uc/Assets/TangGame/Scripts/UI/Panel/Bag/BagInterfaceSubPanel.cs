﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;

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
		public GameObject PropsPictorialBtn;

		public GameObject HeroScrolBar;
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
			UIEventListener.Get (PropsPictorialBtn.gameObject).onClick += PropsPictorialBtnOnClick;

			if(bagPanel == null)
				bagPanel = NGUITools.FindInParents<BagPanel> (this.gameObject);
			LoadAllPropsItem ();
		}
		/// <summary>
		/// Loads all properties item.
		/// 加载所有的物品对象到列表中去
		/// </summary>
		public void LoadAllPropsItem(){
			StartCoroutine (LoadAllPropsItemAtMount());
		}

		IEnumerator LoadAllPropsItemAtMount(){
			PropsItem item = Resources.Load<PropsItem> (UIContext.getWidgetsPath (UIContext.PROPS_ITEM_NAME));
			foreach (Props propsBase in PropsCache.instance.propsTable.Values) {
				item = NGUITools.AddChild (PropsTable.gameObject, item.gameObject).GetComponent<PropsItem> ();
				item.Flush (propsBase);
				item.gameObject.name = propsBase.data.id.ToString ();
				propsItems.Add (item.data.data.id, item);
				UIEventListener.Get (item.gameObject).onClick += PropsItemOnClick;
				PropsTable.GetComponent<UITable> ().repositionNow = true;
				yield return new WaitForFixedUpdate ();
			}
			this.PlayTweenItemAlpha (item.gameObject);
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
					this.PlayTweenItemAlpha (item.gameObject);
				}
			}
			this.PropsTableReposition ();

		}
		void PropsTableReposition(){
			this.PropsTable.GetComponent<UITable> ().repositionNow = true;
			BackToTop ();
		}

		/// <summary>
		/// Toggles the equip on click.
		/// 只显示装备
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleEquipOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsType.EQUIP);
		}

		/// <summary>
		/// Toggles the scroll on click.
		/// 显示卷轴
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleScrollOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsType.SCROLLS);
		}

		/// <summary>
		/// Toggles the soul rock on click.
		/// 显示灵魂石
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleSoulRockOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsType.SOULROCK);
		}

		/// <summary>
		/// Toggles the consumables on click.
		/// 显示消耗品
		/// </summary>
		/// <param name="obj">Object.</param>
		void ToggleConsumablesOnClick (GameObject obj)
		{
			this.ShowItemsByPropsType (PropsType.EXP, PropsType.ENCHANT);
		}
		/// <summary>
		/// Propertieses the pictorial button on click.
		/// </summary>
		void PropsPictorialBtnOnClick(GameObject obj){
			UIContext.mgrCoC.LazyOpen (UIContext.PROPS_PICTORIAL_Panel,TangUI.UIPanelNode.OpenMode.ADDITIVE,TangUI.UIPanelNode.BlockMode.TEXTURE);
		}
		void ShowItemsByPropsType (PropsType type)
		{
			foreach (PropsItem item in propsItems.Values) {
				if (item.data != null) {
					if ((PropsType)item.data.data.type == type) {
						item.gameObject.SetActive (true);
						this.PlayTweenItemAlpha (item.gameObject);
					} else {
						item.gameObject.SetActive (false);
					}
				}
			}
			this.PropsTableReposition ();
		}
		/// <summary>
		/// 让view回到最顶层
		/// </summary>
		void BackToTop ()
		{
			this.HeroScrolBar.GetComponent <UIScrollBar> ().value = 0;
		}
		/// <summary>
		/// Shows the type of the items by properties.
		/// 根据道具的类型显示item
		/// </summary>
		/// <param name="type1">Type1.</param>
		/// <param name="type2">Type2.</param>
		void ShowItemsByPropsType (PropsType type1, PropsType type2)
		{
			foreach (PropsItem item in propsItems.Values) {
				if (item.data != null) {
					if ((PropsType)item.data.data.type == type1 || (PropsType)item.data.data.type == type2) {
						item.gameObject.SetActive (true);
						this.PlayTweenItemAlpha (item.gameObject);
					} else {
						item.gameObject.SetActive (false);
					}
				}
			}
			this.PropsTableReposition ();
		}
		void PlayTweenItemAlpha(GameObject obj){
			TweenAlpha al = obj.GetComponent<TweenAlpha> ();
			al.ResetToBeginning ();
			al.Play ();
		}
	}
}