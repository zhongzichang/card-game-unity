using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame{
	public class UIBehaviour : MonoBehaviour {

		public UIAnchor uiRoot;
		public Camera uiCamera;

		void Awake(){
			UIContext.manger = new UIPanelNodeManager (uiRoot);
		}
	}
}