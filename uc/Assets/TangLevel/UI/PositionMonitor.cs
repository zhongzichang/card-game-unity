using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class PositionMonitor : MonoBehaviour
  {

    public Camera camera;
    private Transform myTransform = null;

    // Use this for initialization
    void Start ()
    {
      myTransform = transform;
    }

    void OnChange(Vector3 screenPosition){

      camera.ScreenToWorldPoint (screenPosition);
      //myTransform.
    }

  }
}