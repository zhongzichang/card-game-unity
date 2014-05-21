using UnityEngine;
using System.Collections;
using TangUI;
using TangPlace;

namespace TangGame
{
  public class MainUI : MonoBehaviour
  {
    /// <summary>
    /// The anchor center or center.
    /// </summary>
    public UIAnchor anchorCoC;
    // Use this for initialization
    void Start ()
    {
      // 设置当前位置为 home
      PlaceController.Place = Place.home;

      if (anchorCoC != null) {
        UIPanelNodeManager mgr = new UIPanelNodeManager (anchorCoC);
        mgr.LazyOpen (TangGame.UIContext.MAIN_POPUP_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE);
        mgr.LazyOpen (TangGame.UIContext.MAIN_HEAD_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE);
				mgr.LazyOpen (TangGame.UIContext.MAIN_STATUS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE);
        UIContext.mgrCoC = mgr;
      }
    }
  }
}