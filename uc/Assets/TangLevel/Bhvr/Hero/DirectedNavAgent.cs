using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class DirectedNavAgent : MonoBehaviour
  {
    private Transform myTransform;
    // speed
    public float speed;
    // stopping distance
    public float stoppingDistance;
    // destination
    public float destination {
      private set;
      get;
    }

    public bool hasPath {
      private set;
      get;
    }
    // Use this for initialization
    void Start ()
    {
      myTransform = transform;
    }
    // Update is called once per frame
    void Update ()
    {

      if (hasPath) {

        // 如果已经到达目标位置
        // 目标位置 - 当前位置 < stoppingDistance
        Vector3 localPosition = myTransform.localPosition;
        float distance = Mathf.Abs (destination - localPosition.x);
        if (distance - stoppingDistance < 0.01F ) {
          ResetPath ();
          //Debug.Log (distance + "-" + stoppingDistance);
        } else {
          float fraction = Time.deltaTime * speed / distance;
          float offsetx = Mathf.Lerp (localPosition.x, destination, fraction);
          //Debug.Log (""+"offsetx="+offsetx);
          myTransform.localPosition = new Vector3( offsetx, 
            localPosition.y, localPosition.z);
        }

      }
    }

    public void ResetPath ()
    {
      hasPath = false;
    }

    public void SetDestination (float destination)
    {
      this.destination = destination;
      hasPath = true;
    }
  }
}