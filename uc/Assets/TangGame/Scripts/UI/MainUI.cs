using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame
{
  public class MainUI : MonoBehaviour
  {
    /// <summary>
    /// The anchor Top Right.
    /// </summary>
    public UIAnchor anchorTR;
    /// <summary>
    /// The anchor Top left.
    /// </summary>
    public UIAnchor anchorTL;
    /// <summary>
    /// The anchor top or top.
    /// </summary>
    public UIAnchor anchorToT;
    /// <summary>
    /// The anchor center or center.
    /// </summary>
    public UIAnchor anchorCoC;
    // Use this for initialization
    void Start ()
    {
      // 设置当前位置为 home
      PlaceController.Place = Place.home;

      if (anchorTR != null) {
        UIPanelNodeManager mgr = new UIPanelNodeManager (anchorTR);
        mgr.LazyOpen (TangGame.UIContext.MAIN_POPUP_PANEL_NAME, UIPanelNode.OpenMode.OVERRIDE);
        UIContext.mgrTR = mgr;
      }
      if (anchorCoC != null) {
        UIPanelNodeManager mgr = new UIPanelNodeManager (anchorCoC);
        UIContext.mgrCoC = mgr;
      }
      if (anchorTL != null) {
        UIPanelNodeManager mgr = new UIPanelNodeManager (anchorTL);
        UIContext.mgrTL = mgr;
      }
      if (anchorToT != null) {
        UIPanelNodeManager mgr = new UIPanelNodeManager (anchorToT);
        mgr.LazyOpen (TangGame.UIContext.MAIN_STATUS_PANEL_NAME, UIPanelNode.OpenMode.OVERRIDE);
        UIContext.mgrToT = mgr;
      }
    }

    void OnGUI ()
    {
      if (GUI.Button (new Rect (10, 10, 50, 60), "Back")) {

        if (TangGame.UIContext.mgrCoC != null) {
          TangGame.UIContext.mgrCoC.Back ();
        }

      }
  }
}