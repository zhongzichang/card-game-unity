using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class PositionMonitor : MonoBehaviour
  {

    private Transform myTransform = null;
    private Vector3 offset = new Vector3 (0, 100, 0);

    // Use this for initialization
    void Start ()
    {
      myTransform = transform;
    }

    public void OnChange(Vector3 screenPosition){

      myTransform.localPosition = screenPosition + offset;

    }

  }
}