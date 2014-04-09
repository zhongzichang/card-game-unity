using UnityEngine;
using System.Collections;

public class HeroViewPanel : MonoBehaviour {

	public GameObject ToggleAll;
	public GameObject ToggleBefore;
	public GameObject ToggleLater;
	public GameObject ToggleMedium;

	private UIToggle lastToggle = null;

	// Use this for initialization
	void Start () {
		UIEventListener.Get (ToggleAll.gameObject).onClick += OnToggleAllClick;
		UIEventListener.Get (ToggleBefore.gameObject).onClick += OnToggleBeforeClick;
		UIEventListener.Get (ToggleLater.gameObject).onClick += OnToggleLaterClick;
		UIEventListener.Get (ToggleMedium.gameObject).onClick += OnToggleMediumClick;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	#region UIEventListener
	void OnToggleMediumClick(GameObject obj){
		UIToggle tg = obj.GetComponent<UIToggle> ();
		if (isLastToggle(obj) || tg == null)
			return;
		
		//TODO Show Medium hero list
		this.lastToggle = tg;
	}
	void OnToggleLaterClick(GameObject obj){
		UIToggle tg = obj.GetComponent<UIToggle> ();
		if (isLastToggle(obj) || tg == null)
			return;
		
		//TODO Show Later hero list
		this.lastToggle = tg;
	}

	void OnToggleBeforeClick(GameObject obj){
		UIToggle tg = obj.GetComponent<UIToggle> ();
		if (isLastToggle(obj) || tg == null)
			return;
		
		//TODO Show Before hero list
		this.lastToggle = tg;
	}

	void OnToggleAllClick(GameObject obj){
		UIToggle tg = obj.GetComponent<UIToggle> ();
		if (isLastToggle(obj) || tg == null)
			return;

		//TODO Show All hero list
		this.lastToggle = tg;
	}
	#endregion


	// Check whether the current object is last uitoggle !
	bool isLastToggle(GameObject obj){
		if(obj == null){
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
