using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	public GameObject grassLayer;
	public GameObject mountainLayer;
	public GameObject skyLayer;

	void OnDrag( DragGesture gesture ) 
	{
		Debug.Log ("OnDrag");
		float new_pos = grassLayer.transform.localPosition.x + gesture.DeltaMove.x;
		if(new_pos >=-10.24 && new_pos<=0)
		{
      skyLayer.transform.Translate(Vector3.right * (gesture.DeltaMove.x /40));
      mountainLayer.transform.Translate(Vector3.right * (gesture.DeltaMove.x/20));
      grassLayer.transform.Translate(Vector3.right * gesture.DeltaMove.x/10);
		}
	}

	void OnTap( TapGesture gesture ) 
	{
		Debug.Log ("OnTap");
		Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		if(hit.collider != null)
		{
			Debug.Log ("OnTap " + hit.collider.gameObject.name );
			hit.collider.gameObject.SendMessage("OnTap");
		}
	}
}