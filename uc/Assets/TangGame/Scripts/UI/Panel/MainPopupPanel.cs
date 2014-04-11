using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame
{
	public class MainPopupPanel : MonoBehaviour
	{

		public GameObject MainPopupButton;
		public GameObject HeroMenu;
		public GameObject BagMenu;
		public GameObject FragmentsMenu;
		public GameObject TaskMenu;
		public GameObject ActivityMenu;
		private UIToggle mainPopupToggle;
		// Use this for initialization
		void Start ()
		{
			UIEventListener.Get (HeroMenu.gameObject).onClick += OnHeroMenuClick;
			UIEventListener.Get (BagMenu.gameObject).onClick += OnBagMenuClick;
			UIEventListener.Get (FragmentsMenu.gameObject).onClick += OnDebrisMenuClick;
//		UIEventListener.Get (TaskMenu.gameObject).onClick += OnTaskMenuClick;

			mainPopupToggle = MainPopupButton.GetComponent<UIToggle> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		// when hero menu click show hero panel
		void OnHeroMenuClick (GameObject obj)
		{
			TangGame.UIContext.mgr.LazyOpen (UINtftNames.HERO_VIEW_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE);
			this.CloseMenu ();
		}

		void OnBagMenuClick (GameObject obj)
		{
			TangGame.UIContext.mgr.LazyOpen ("WindowPanel", UIPanelNode.OpenMode.ADDITIVE);
			this.CloseMenu ();
		}

		void OnDebrisMenuClick (GameObject obj)
		{
			TangGame.UIContext.mgr.LazyOpen ("QuestLogPanel", UIPanelNode.OpenMode.REPLACE);
			this.CloseMenu ();
		}



		// Close current Menu!
		void CloseMenu ()
		{
			if (!mainPopupToggle.value) {
				return;		
			}
			mainPopupToggle.value = false;
			MainPopupButton.GetComponent<UIPlayTween> ().Play (true);
		}
	}
}