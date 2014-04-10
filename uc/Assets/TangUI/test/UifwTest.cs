﻿using UnityEngine;
using System.Collections;

namespace TangUI
{
	public class UifwTest : MonoBehaviour
	{

		UIAnchor anchor;
		UIPanelNodeManager mgr;

		// Use this for initialization
		void Start ()
		{

			anchor = GetComponent<UIAnchor> ();

			if (anchor != null) {
				mgr = new UIPanelNodeManager (anchor);

			}

  
		}
  
		void OnGUI ()
		{
			if (GUI.Button (new Rect (10, 10, 150, 100), "Back")) {

				if (mgr != null) {
					TangGame.UIContext.mgr.Back ();
				}

			}

			if (GUI.Button (new Rect (200, 10, 150, 100), "New Main Popup Panel")) {
				if (mgr != null) {
					mgr.LazyOpen ("MainPopupPanel", UIPanelNode.OpenMode.ADDITIVE, "test param");
				}
			}

		}

	}
}

