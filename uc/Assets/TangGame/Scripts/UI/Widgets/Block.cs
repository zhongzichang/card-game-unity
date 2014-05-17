using UnityEngine;
using System.Collections;
using TangUI;
public class Block : MonoBehaviour {

	public GameObject sprite;
	public GameObject texture;
	public GameObject backButton;
	public UIPanelNodeManager currentMgr;

	// Use this for initialization
	void Start () {
		if (UIPanelNodeContext.mgrUseStack.Count > 0) {
			currentMgr = UIPanelNodeContext.mgrUseStack.Peek() as UIPanelNodeManager;
		}
		UIWidget uiwidget = this.GetComponent<UIWidget> ();
		UICamera uicamera = NGUITools.FindInParents<UICamera> (this.gameObject);
		if (uicamera != null) {
			uiwidget.SetAnchor (uicamera.gameObject,-10,-10,10,10);
		}
		UIEventListener.Get (backButton).onClick += BackButtonOnClick;
	}

	void  OnClick(){
		if (currentMgr != null && texture.activeSelf == false) {
			currentMgr.Back ();
		}
	}

	void BackButtonOnClick(GameObject obj){
		if (currentMgr != null) {
			currentMgr.Back ();
		}
	}
}
