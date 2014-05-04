using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class DirectedNavAgent : MonoBehaviour
  {
    public delegate void PositionChangeHandler (Vector3 screenPosition);

    public PositionChangeHandler raisePositionChange;
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
        if (distance - stoppingDistance < 0.01F) {
          ResetPath ();
          //Debug.Log (distance + "-" + stoppingDistance);
        } else {

          // 移动
          float fraction = Time.deltaTime * speed / distance;
          float offsetx = Mathf.Lerp (localPosition.x, destination, fraction);
          //Debug.Log (""+"offsetx="+offsetx);
          Vector3 npos = new Vector3 (offsetx, localPosition.y, localPosition.z);
          myTransform.localPosition = npos;
          if (raisePositionChange != null) {
            raisePositionChange (Camera.main.WorldToScreenPoint (npos));
          }

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