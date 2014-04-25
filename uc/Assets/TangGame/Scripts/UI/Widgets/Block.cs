using UnityEngine;
using System.Collections;
using TangUI;
public class Block : MonoBehaviour {

	public UIPanelNodeManager currentMgr;
	// Use this for initialization
	void Start () {
		if (UIPanelNodeContext.mgrUseStack.Count > 0) {
			currentMgr = UIPanelNodeContext.mgrUseStack.Peek() as UIPanelNodeManager;
		}

	}

	void  OnClick(){
		if (currentMgr != null && GetComponent<UITexture>().enabled == false) {
			currentMgr.Back ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
