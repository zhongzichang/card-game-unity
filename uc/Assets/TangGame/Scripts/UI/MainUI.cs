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

		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
			if (GUI.Button (new Rect (10, 10, 150, 100), "Back")) {

				if (TangGame.UIContext.mgrCoC != null) {
					TangGame.UIContext.mgrCoC.Back ();
				}

			}

			//			if (GUI.Button (new Rect (200, 10, 150, 100), "New Main Popup Panel")) {
			//				if (mgr != null) {
			//					mgr.LazyOpen (TangGame.UIContext.MAIN_POPUP_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, "test param");
			//				}
			//			}

		}
	}
}