using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame{
	public class UIBehaviour : MonoBehaviour {

		public UIAnchor uiRoot;
		public Camera uiCamera;

		void Awake(){
			Global.uiRoot = uiRoot.gameObject;
			UIContext.manger = new UIPanelNodeManager (uiRoot);
		}
	}
}