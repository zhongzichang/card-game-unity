using UnityEngine;
using System.Collections;

public class MainPopupPanel : MonoBehaviour {

	public GameObject MainPopupButton;
	public GameObject HeroMenu;
	public GameObject BagMenu;
	public GameObject DebrisMenu;
	public GameObject TaskMenu;
	public GameObject ActivityMenu;

	private UIToggle mainPopupToggle;
	// Use this for initialization
	void Start () {
		UIEventListener.Get (HeroMenu.gameObject).onClick += OnHeroMenuClick;


		mainPopupToggle = MainPopupButton.GetComponent<UIToggle>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// when hero menu click show hero panel
	void OnHeroMenuClick(GameObject obj){
		TangGame.UIContext.mgr.LazyOpen ("HeroViewPanel");
		this.CloseMenu ();
	}

	// Close current Menu!
	void CloseMenu(){
		if (!mainPopupToggle.value) {
			return;		
		}
		mainPopupToggle.value = false;
		MainPopupButton.GetComponent<UIPlayTween> ().Play (true);
	}
}
