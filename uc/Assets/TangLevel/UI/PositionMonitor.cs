using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class PositionMonitor : MonoBehaviour
  {

    private Transform myTransform = null;
    private Vector3 offset = new Vector3 (32, 130, 0);

    // Use this for initialization
    void Awake ()
    {
      myTransform = transform;
    }

    public void OnChange(Vector3 screenPosition){

      Vector3 pixelPos = screenPosition + offset;

      float x = (screenPosition.x + offset.x) *  960 / Screen.width;
      float y = (screenPosition.y + offset.y) * 640 / Screen.height;

      myTransform.localPosition = new Vector3 (x, y, pixelPos.z) ;
      //myTransform.localPosition = pixelPos;

    }

  }
}