using UnityEngine;
using System.Collections;

namespace TangUI
{
	public class UiStatusTest : MonoBehaviour
	{

		UIAnchor anchor;
		UIPanelNodeManager mgr;

		// Use this for initialization
		void Start ()
		{

			anchor = GetComponent<UIAnchor> ();

			if (anchor != null) {
				mgr = new UIPanelNodeManager (anchor);
				mgr.LazyOpen (TangGame.UIContext.MAIN_STATUS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, "status");
			}
		}

	}
}

