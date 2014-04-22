using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using TangUI;

namespace TangGame
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

		// Use this for initialization
		void Start ()
		{
			UIEventListener.Get (ToggleAll.gameObject).onClick += OnToggleAllClick;
			UIEventListener.Get (ToggleBefore.gameObject).onClick += OnToggleBeforeClick;
			UIEventListener.Get (ToggleLater.gameObject).onClick += OnToggleLaterClick;
			UIEventListener.Get (ToggleMedium.gameObject).onClick += OnToggleMediumClick;

			this.TestData ();

		}

		void TestData ()
		{
			HeroBase data;
			for (int i=0; i<50; i++) {
				data = new HeroBase ();
				data.HeroName = "god is a girl";
				data.ConfigId = i;
				data.HeroPropertyType = (HeroPropertyEnum)(i % 3 + 1);
				data.HeroLocation = (HeroLocationEnum)(i % 3 + 1);
				data.Evolve = i%5 + 1;
				data.FragmentsCount = i;
				data.FragmentsCountMax = 50;
				if (i > 40) {
					data.Level = i % 4 + 1;
					data.HeroesRank = (HeroesRankEnum)(i % 10 + 1);
					data.Islock = false;
				} else {
					data.Islock = true;
				}
				this.AddHeroItem (data);
			}
			this.repositionNow ();
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
	
		void ShowItemByHeroLocation (HeroLocationEnum em)
		{
			foreach (HeroItem item in heroItems.Values) {
				if (item.Data.HeroLocation.Equals (em)) {
					item.gameObject.SetActive (true);
				}
			}
			BackToTop ();
		}

		void HideAllItem (bool value)
		{
			foreach (HeroItem item in heroItems.Values) {
				item.gameObject.SetActive (!value);
			}
			BackToTop ();
		}

		void BackToTop ()
		{
			this.HeroScrolBar.GetComponent <UIScrollBar> ().value = 0;
		}


	#region UIEventListener
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

		void AddHeroItem (HeroBase data)
		{
			HeroItem item = Resources.Load<HeroItem> (UIContext.getWidgetsPath(UIContext.HERO_ITEM_NAME));
			if (data.Islock) {
				item = NGUITools.AddChild (HeroLocking, item.gameObject).GetComponent<HeroItem> ();

			} else {
				item = NGUITools.AddChild (HeroUnlock, item.gameObject).GetComponent<HeroItem> ();
			}
			heroItems.Add (data.ConfigId, item);
			item.Flush (data);
			UIEventListener.Get (item.gameObject).onClick += ItemOnClick;
		}

		public void ItemOnClick(GameObject obj){
			HeroItem item = obj.GetComponent<HeroItem> ();
			if (item == null)
				return;
			if (!item.Data.Islock) {
				TangGame.UIContext.mgrCoC.LazyOpen (UIContext.HERO_INFO_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, item.Data);
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