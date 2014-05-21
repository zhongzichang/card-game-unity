using UnityEngine;
using System.Collections;
using TangUI;

public class Block : MonoBehaviour
{
	public GameObject sprite;
	public GameObject texture;
	public GameObject backButton;
	public UIPanelNodeManager currentMgr;
	UIPanelNode.BlockMode mMode;
	// Use this for initialization
	void Start ()
	{
		if (UIPanelNodeContext.mgrUseStack.Count > 0) {
			currentMgr = UIPanelNodeContext.mgrUseStack.Peek () as UIPanelNodeManager;
		}
		UIWidget uiwidget = this.GetComponent<UIWidget> ();
		UICamera uicamera = NGUITools.FindInParents<UICamera> (this.gameObject);
		if (uicamera != null) {
			uiwidget.SetAnchor (uicamera.gameObject, -10, -10, 10, 10);
		}
		UIEventListener.Get (backButton).onClick += BackButtonOnClick;
	}

	void  OnClick ()
	{
		if (currentMgr != null && texture.activeSelf == false) {
			currentMgr.Back ();
		}
	}

	void BackButtonOnClick (GameObject obj)
	{
		this.Back ();
	}

	public void Back(){
		if (currentMgr != null) {
			currentMgr.Back ();
			if (this.mMode == UIPanelNode.BlockMode.ADDSTATUS) {
				currentMgr.Back ();
			}
			if (this.mMode == UIPanelNode.BlockMode.ADDSTATUSANDPOPUP) {
				currentMgr.Back ();
				currentMgr.Back ();
			}
		}
	}

	public void SetBlockMode (UIPanelNode.BlockMode mode)
	{
		this.mMode = mode;
		NGUITools.SetActive (this.texture, false);
		NGUITools.SetActive (this.sprite, false);
		NGUITools.SetActive (this.backButton, false);
		BoxCollider collider = this.GetComponent<BoxCollider> ();
		if (collider != null) {
			collider.enabled = false;
		}

		if (mMode == UIPanelNode.BlockMode.SPRITE) {
			NGUITools.SetActive (this.sprite, true);
			collider.enabled = true;
		}

		if (mMode == UIPanelNode.BlockMode.TEXTURE) {
			NGUITools.SetActive (this.texture, true);
			NGUITools.SetActive (this.backButton, true);
		}

		if (mMode == UIPanelNode.BlockMode.ADDSTATUS) {
			NGUITools.SetActive (this.texture, true);
			NGUITools.SetActive (this.backButton, true);
			TangGame.UIContext.mgrCoC.LazyOpen (TangGame.UIContext.MAIN_STATUS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.NONE,null,true);

		}
		if (mMode == UIPanelNode.BlockMode.ADDSTATUSANDPOPUP) {
			NGUITools.SetActive (this.texture, true);
			NGUITools.SetActive (this.backButton, true);
			TangGame.UIContext.mgrCoC.LazyOpen (TangGame.UIContext.MAIN_STATUS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.NONE,null,true);
			TangGame.UIContext.mgrCoC.LazyOpen (TangGame.UIContext.MAIN_POPUP_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.NONE,null,true);
		}

	}
}
