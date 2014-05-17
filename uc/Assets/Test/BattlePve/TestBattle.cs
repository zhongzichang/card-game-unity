using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame.UI
{
  public class TestBattle : MonoBehaviour {

    public UIAnchor anchorCoC;
  	// Use this for initialization
  	void Start () {
      if (anchorCoC != null) {
        UIPanelNodeManager mgr = new UIPanelNodeManager (anchorCoC);
        mgr.LazyOpen (UIContext.BATTLE_PVE_PANEL_NAME, UIPanelNode.OpenMode.OVERRIDE, UIPanelNode.BlockMode.TEXTURE);
        UIContext.mgrCoC = mgr;
      }
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}
  }
}