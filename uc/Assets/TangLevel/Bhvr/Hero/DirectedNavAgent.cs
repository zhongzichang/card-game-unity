using UnityEngine;
using System.Collections;
using System;

namespace TangLevel
{
  public class DirectedNavAgent : MonoBehaviour
  {
    /// 屏幕位置发生变化
    public delegate void ScrPosChanged (Vector3 screenPosition);

    public ScrPosChanged raiseScrPosChanged;

    /// <summary>
    /// 世界位置发生变化
    /// </summary>
    public event EventHandler RaisePosChanged;
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

    public Transform myTransform;
    private Vector3 lastScreenPos = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
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

        } else {

          // 移动
          float fraction = Time.deltaTime * speed / distance;
          float offsetx = Mathf.Lerp (localPosition.x, destination, fraction);

          Vector3 npos = new Vector3 (offsetx, localPosition.y, localPosition.z);
          myTransform.localPosition = npos;

          // 屏幕位置改变通知
          if (raiseScrPosChanged != null) {
            Vector3 screePos = Camera.main.WorldToScreenPoint (npos);
            if (screePos != lastScreenPos) {
              raiseScrPosChanged (screePos);
              lastScreenPos = screePos;
            }
          }
          // 位置改变通知
          if (RaisePosChanged != null) {
            if (npos != lastPos) {
              RaisePosChanged (this, EventArgs.Empty);
              lastPos = npos;
            }
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