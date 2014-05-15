using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	public GameObject grassLayer;
	public GameObject mountainLayer;
	public GameObject skyLayer;

  public Transform pressed;

  private float dragScale = 0.03f;

  void OnDrag( DragGesture gesture ) 
	{
//    Debug.Log ("DeltaMove" + gesture.DeltaMove);
//    Debug.Log ("TotalMove" + gesture.TotalMove);
//    Debug.Log ("Position" + gesture.Position);
    float new_pos = grassLayer.transform.localPosition.x + gesture.DeltaMove.x *dragScale;
    if(new_pos >=-10.24 && new_pos<=0)
		{
      skyLayer.transform.Translate(Vector3.right * (gesture.DeltaMove.x * dragScale/4));
      mountainLayer.transform.Translate(Vector3.right * (gesture.DeltaMove.x * dragScale/2));
      grassLayer.transform.Translate(Vector3.right * gesture.DeltaMove.x * dragScale);
		}
	}

	void OnTap( TapGesture gesture ) 
	{
    if (UICamera.hoveredObject != null) {
      return;
    }

    Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
    RaycastHit hit;
    if(Physics.Raycast(ray, out hit)){
      Debug.DrawLine(ray.origin, hit.point);
      if(hit.collider != null)
  		{
        GameObject obj = hit.collider.gameObject;
        Debug.Log ("OnTap " + obj.name);
        pressed.position = obj.transform.position;
  		}
    }
	}
}