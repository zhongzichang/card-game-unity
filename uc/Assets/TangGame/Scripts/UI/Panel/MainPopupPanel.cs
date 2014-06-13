using UnityEngine;
using System.Collections;
using TangUI;
using TangGame.UI;

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
			UIEventListener.Get (TaskMenu.gameObject).onClick += OnTaskMenuClick;
      UIEventListener.Get (ActivityMenu.gameObject).onClick += OnActivityMenuClick;

			mainPopupToggle = MainPopupButton.GetComponent<UIToggle> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		// when hero menu click show hero panel
		void OnHeroMenuClick (GameObject obj)
		{
			TangGame.UIContext.mgrCoC.LazyOpen (UIContext.HERO_VIEW_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.ADDSTATUS);
			this.CloseMenu ();
		}

		void OnBagMenuClick (GameObject obj)
		{
			TangGame.UIContext.mgrCoC.LazyOpen (UIContext.BAG_PANEL_NAME , UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.ADDSTATUS);
			this.CloseMenu ();
		}

		void OnDebrisMenuClick (GameObject obj)
		{
      TangGame.UIContext.mgrCoC.LazyOpen (FragmentPanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.ADDSTATUS);
			this.CloseMenu ();
		}

    void OnTaskMenuClick (GameObject obj)
    {
      TangGame.UIContext.mgrCoC.LazyOpen (TaskPanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.ADDSTATUS);
      this.CloseMenu ();
    }

    void OnActivityMenuClick (GameObject obj)
    {
      TangGame.UIContext.mgrCoC.LazyOpen (DailyPanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.ADDSTATUS);
      this.CloseMenu ();
    }


		// Close current Menu!
		void CloseMenu ()
		{
			if (!mainPopupToggle.value) {
				return;		
			}
			mainPopupToggle.value = false;
			UIPlayTween[] playTweens = MainPopupButton.GetComponents<UIPlayTween> ();
			foreach (UIPlayTween playTween in playTweens) {
				playTween.Play (true);
			}
		}
	}
}