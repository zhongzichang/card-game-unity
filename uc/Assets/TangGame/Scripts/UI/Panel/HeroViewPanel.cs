using UnityEngine;
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

		}

		void OnEnable(){
			foreach (HeroItem item in heroItems.Values) {
				item.Flush (item.Data);
			}
		}

		// Use this for initialization
		void Start ()
		{
			UIEventListener.Get (ToggleAll.gameObject).onClick += OnToggleAllClick;
			UIEventListener.Get (ToggleBefore.gameObject).onClick += OnToggleBeforeClick;
			UIEventListener.Get (ToggleLater.gameObject).onClick += OnToggleLaterClick;
			UIEventListener.Get (ToggleMedium.gameObject).onClick += OnToggleMediumClick;
			this.LoadHeroAll ();
		}

		/// <summary>
		/// 加载所有的英雄
		/// </summary>
		void LoadHeroAll(){
      foreach(HeroBase herobase in HeroCache.instance.heroBeseTable.Values){
				this.UpHeroItem (herobase);
			}
			this.repositionNow ();
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
				if (item.Data.Xml.location.Equals ((int)em)) {
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

		void OnGUI ()
		{
			if (GUILayout.Button ("refash")) {
				this.repositionNow ();
			}
		}

		/// <summary>
		/// 更新某个
		/// </summary>
		/// <param name="herobase">Herobase.</param>
		void UpHeroItem(HeroBase herobase){
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
			HeroItem item = Resources.Load<HeroItem> (UIContext.getWidgetsPath(UIContext.HERO_ITEM_NAME));
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

		public void ItemOnClick(GameObject obj){
			HeroItem item = obj.GetComponent<HeroItem> ();
			if (item == null)
				return;
			if (!item.Data.Islock) {
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.HERO_INFO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.SPRITE, item.Data,true);
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