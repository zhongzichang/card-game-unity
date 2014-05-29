using UnityEngine;
using System.Collections.Generic;
using System;
using TangUtils;

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

    public Transform myTransform;
    // speed
    public float speed;
    // stopping distance
    public float stoppingDistance;
    private Vector3 lastScreenPos = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
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
        if (distance - stoppingDistance < 0.05F) {

          ResetPath ();

          // 获取最佳位置
          myTransform.localPosition = PositionHelper.FindHeroBestPos (gameObject);

        } else {

          // 移动
          float fraction = Time.deltaTime * speed / distance;
          float offsetx = Mathf.Lerp (localPosition.x, destination, fraction);

          Vector3 npos = new Vector3 (offsetx, localPosition.y, 
                           Config.HERO_POS_MIN_Z + (localPosition.y - Config.BOTTOM_BOUND + 1) * 10);
          myTransform.localPosition = npos;

        }

      }

      Vector3 pos = myTransform.localPosition;

      // 比较xy值，判断角色的位置是否发生变化
      if (!VectorUtil.EqualsIntXY (pos, lastPos)) {

        // 屏幕位置改变通知
        if (raiseScrPosChanged != null) {
          Vector3 screePos = Camera.main.WorldToScreenPoint (pos);
          if (screePos != lastScreenPos) {
            raiseScrPosChanged (screePos);
            lastScreenPos = screePos;
          }
        }

        // 位置改变通知
        if (RaisePosChanged != null) {
          RaisePosChanged (this, EventArgs.Empty);
        }

        lastPos = pos;
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