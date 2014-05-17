using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame
{
	public class InputController : MonoBehaviour
	{
		public GameObject grassLayer;
		public GameObject mountainLayer;
		public GameObject skyLayer;
		public Transform pressed;
		private float dragScale = 0.03f;

		void OnDrag (DragGesture gesture)
		{
			if (UICamera.hoveredObject != null) {
				return;
			}

			float new_pos = grassLayer.transform.localPosition.x + gesture.DeltaMove.x * dragScale;
			if (new_pos >= -10.24 && new_pos <= 0) {
				skyLayer.transform.Translate (Vector3.right * (gesture.DeltaMove.x * dragScale / 4));
				mountainLayer.transform.Translate (Vector3.right * (gesture.DeltaMove.x * dragScale / 2));
				grassLayer.transform.Translate (Vector3.right * gesture.DeltaMove.x * dragScale);
			}
		}

		void OnTap (TapGesture gesture)
		{
			if (UICamera.hoveredObject != null) {
				return;
			}

			Ray ray = Camera.main.ScreenPointToRay (gesture.Position);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Debug.DrawLine (ray.origin, hit.point);
				if (hit.collider != null) {
					GameObject obj = hit.collider.gameObject;
					pressed.position = obj.transform.position + Vector3.forward;
					Debug.Log ("OnTap " + obj.name);
					if (obj.name == "Enhance") {
						Debug.Log ("Boom!");
//					TangGame.UIContext.mgrCoC.LazyOpen ();
						UIContext.mgrCoC.LazyOpen (UIContext.ENCHANTS_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.TEXTURE);
					}else if(obj.name.Equals("Pve")){

						UIContext.mgrCoC.LazyOpen (UIContext.BATTLE_PVE_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.TEXTURE);
					}
				}
			}
		}
	}
}