﻿using UnityEngine;
using System.Collections;
using TangUI;
public class Block : MonoBehaviour {

	public GameObject sprite;
	public GameObject texture;
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

	}

	void  OnClick(){
		if (currentMgr != null && texture.activeSelf == false) {
			currentMgr.Back ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
