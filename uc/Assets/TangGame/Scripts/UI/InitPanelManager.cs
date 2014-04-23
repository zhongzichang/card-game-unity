using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame
{
	public class InitPanelManager : MonoBehaviour
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
			UIContext.mgrCoC = mgr;
		}
	}
}