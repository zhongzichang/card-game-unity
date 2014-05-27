using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class PositionMonitor : MonoBehaviour
  {

    private Transform myTransform = null;
    private Vector3 offset = new Vector3 (-18, 150, 0);

    // Use this for initialization
    void Awake ()
    {
      myTransform = transform;
    }

    public void OnChange(Vector3 screenPosition){

      Vector3 pos = UICamera.mainCamera.ScreenToWorldPoint (screenPosition );
      myTransform.position = pos;

      Vector3 localPos = myTransform.localPosition;
      myTransform.localPosition = new Vector3(localPos.x, localPos.y, -10F) + offset;
    }

  }
}