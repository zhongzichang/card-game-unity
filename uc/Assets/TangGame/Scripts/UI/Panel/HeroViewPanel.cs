﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using TangUI;
using TangGame.UI;

namespace TangGame.UI
{
	public class HeroViewPanel : MonoBehaviour
	{

		public GameObject ToggleAll;
		public GameObject ToggleBefore;
		public GameObject ToggleLater;
		public GameObject ToggleMedium;
		public GameObject HeroScrolBar;
		public GameObject HeroTable;
		public GameObject HeroLocking;
		public GameObject HeroUnlock;
		public GameObject LineLabel;
		private UIToggle lastToggle = null;
		private Dictionary<int,HeroItem> heroItems = new Dictionary<int, HeroItem> ();
		private HeroViewPanelMediator mediator;

		void Awake ()
		{
			mediator = new HeroViewPanelMediator ();
			Facade.Instance.RegisterMediator (mediator);
		}

		public class HeroViewPanelMediator : Mediator
		{
			//TODO监听
		}

		void OnEnable ()
		{
			foreach (HeroItem item in heroItems.Values) {
				item.Flush (item.heroBase);
			}
		}


		// Use this for initialization
		void Start ()
		{
			UIEventListener.Get (ToggleAll.gameObject).onClick += OnToggleAllClick;
			UIEventListener.Get (ToggleBefore.gameObject).onClick += OnToggleBeforeClick;
			UIEventListener.Get (ToggleLater.gameObject).onClick += OnToggleLaterClick;
			UIEventListener.Get (ToggleMedium.gameObject).onClick += OnToggleMediumClick;
			StartCoroutine (LoadHeroAll ());
		}

		/// <summary>
		/// 加载所有的英雄
		/// </summary>
		IEnumerator LoadHeroAll ()
		{

			UILabel lineLab = LineLabel.GetComponent<UILabel> ();
			lineLab.text = "";
			List<HeroBase> herobaseList = new List<HeroBase> ();
			foreach (HeroBase herobase in HeroCache.instance.MHeroBeseTable.Values) {
				herobaseList.Add (herobase);
			}
			herobaseList.Sort (UITableByHeroFragments.SortById);
			herobaseList.Sort (UITableByHeroFragments.SortByFragments);
			herobaseList.Sort (UITableByHeroLevel.SortByLevel);
			foreach (HeroBase herobase in herobaseList) {
				yield return new WaitForFixedUpdate ();
				this.UpHeroItem (herobase);
				HideAllItem (false);
				this.repositionNow ();
			}
			lineLab.text = UIPanelLang.NOT_SUMMON_HERO;
		}
	



		// Update is called once per frame
		void Update ()
		{
			
		}
		/// <summary>
		/// 根据英雄location属性来显示相应的item
		/// </summary>
		/// <param name="em">Em.</param>
		void ShowItemByHeroLocation (HeroLocationEnum em)
		{
			foreach (HeroItem item in heroItems.Values) {
				if (item.heroBase.Xml.location.Equals ((int)em)) {
					item.gameObject.SetActive (true);
				}
			}
			BackToTop ();
		}
		/// <summary>
		/// 隐藏所有的item
		/// </summary>
		/// <param name="value">If set to <c>true</c> value.</param>
		void HideAllItem (bool value)
		{
			foreach (HeroItem item in heroItems.Values) {
				item.gameObject.SetActive (!value);
			}
			BackToTop ();
		}
		/// <summary>
		/// 让view回到最顶层
		/// </summary>
		void BackToTop ()
		{
			this.HeroScrolBar.GetComponent <UIScrollBar> ().value = 0;
		}


	#region UIEventListener
		/// <summary>
		/// 显示中排按钮被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnToggleMediumClick (GameObject obj)
		{
			UIToggle tg = obj.GetComponent<UIToggle> ();
			if (isLastToggle (obj) || tg == null)
				return;
			HideAllItem (true);
			ShowItemByHeroLocation (HeroLocationEnum.MEDIUM);
			this.repositionNow ();
			this.lastToggle = tg;
		}

		/// <summary>
		/// 显示后排按钮被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnToggleLaterClick (GameObject obj)
		{
			UIToggle tg = obj.GetComponent<UIToggle> ();
			if (isLastToggle (obj) || tg == null)
				return;
		
			HideAllItem (true);
			ShowItemByHeroLocation (HeroLocationEnum.LATER);
			this.repositionNow ();
			this.lastToggle = tg;
		}

		/// <summary>
		/// 显示前排按钮被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnToggleBeforeClick (GameObject obj)
		{
			UIToggle tg = obj.GetComponent<UIToggle> ();
			if (isLastToggle (obj) || tg == null)
				return;
		
			HideAllItem (true);
			ShowItemByHeroLocation (HeroLocationEnum.BEFORE);
			this.repositionNow ();
			this.lastToggle = tg;
		}
		/// <summary>
		/// 显示全部按钮被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnToggleAllClick (GameObject obj)
		{
			UIToggle tg = obj.GetComponent<UIToggle> ();
			if (isLastToggle (obj) || tg == null)
				return;
			HideAllItem (false);
			this.repositionNow ();
			this.lastToggle = tg;
		}
	#endregion

		/// <summary>
		/// 更新某个
		/// </summary>
		/// <param name="herobase">Herobase.</param>
		void UpHeroItem (HeroBase herobase)
		{
			if (heroItems.ContainsKey (herobase.Xml.id)) {
				HeroItem item = heroItems [herobase.Xml.id];
				if (item.transform.parent == HeroLocking.transform && !herobase.Islock) {
					item.transform.parent = HeroUnlock.transform;
				}
				item.Flush (herobase);
			} else {
				AddHeroItem (herobase);
			}
		}
		/// <summary>
		/// Adds the hero item.
		/// </summary>
		/// <param name="data">Data.</param>
		void AddHeroItem (HeroBase data)
		{
			HeroItem item = Resources.Load<HeroItem> (UIContext.getWidgetsPath (UIContext.HERO_VIEW_ITEM_NAME));
			if (data.Islock) {
				item = NGUITools.AddChild (HeroLocking, item.gameObject).GetComponent<HeroItem> ();

			} else {
				item = NGUITools.AddChild (HeroUnlock, item.gameObject).GetComponent<HeroItem> ();
			}
			heroItems.Add (data.Xml.id, item);
			item.Flush (data);
			UIEventListener.Get (item.gameObject).onClick -= ItemOnClick;
			UIEventListener.Get (item.gameObject).onClick += ItemOnClick;
		}

		public void ItemOnClick (GameObject obj)
		{
			HeroItem item = obj.GetComponent<HeroItem> ();
			if (item == null)
				return;
			if (!item.heroBase.Islock) {
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.HERO_INFO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE, item.heroBase, true);
			} else {
				if (item.canCall) {
					//TODO 发送消息给服务器召唤此英雄
				} else {
					UIContext.mgrCoC.LazyOpen (UIContext.SOUL_StTONE_DROP_PANEL, TangUI.UIPanelNode.OpenMode.ADDITIVE, item.heroBase);
				}
			}
		}
		//Reposition the children on the next Update().
		void repositionNow ()
		{
			HeroUnlock.GetComponent<UITable> ().repositionNow = true;
			HeroLocking.GetComponent<UITable> ().repositionNow = true;
			HeroTable.GetComponent<UITable> ().repositionNow = true;
		}
		// Check whether the current object is last uitoggle !
		bool isLastToggle (GameObject obj)
		{
			if (obj == null) {
				return false;
			}
			if (obj.GetComponent<UIToggle> () == null) {
				return false;		
			}
			if (obj.GetComponent<UIToggle> () != lastToggle) {
				return false;		
			}
			return true;
		}

	}
}