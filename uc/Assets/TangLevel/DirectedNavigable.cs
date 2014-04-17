using UnityEngine;
using System.Collections;
using TS = TangScene;

namespace TangLevel
{
  [RequireComponent (typeof(DirectedNavAgent))]
  public class DirectedNavigable : MonoBehaviour
  {
    public const float CACHE_DISTANCE = 10F;
    // 距离目标人物等于小于这个距离的时候人物动作由跑动改为站立(run=>idle)
    public static readonly Vector2 NOTIFIED_RANGE = new Vector2 (32F, 16F);
    // 摇杆操作，角色移动超过这个距离发通知（如果需要 nextPositionChangeHandle != null）
    public float m_speed = 240F;
    private DirectedNavAgent agent;
    private TS.CharacterStatusBhvr statusBhvr;
    private Transform myTransform;

    public float Speed {
      get {
        return m_speed;
      }
      set {
        m_speed = value;
        if (agent != null) {
          agent.speed = value;
        }
      }
    }

    /// <summary>
    ///   Navigate to a destination
    /// </summary>
    public void NavTo (float x)
    {
      NavTo (x, 0F);
    }

    /// <summary>
    ///   Navigate to a destination with stopping distance
    /// </summary>
    public void NavTo (float x, float stoppingDistance)
    {
      
      if (agent != null && agent.enabled) {

        Vector3 fixedPosition = new Vector3 (x, transform.localPosition.z, transform.localPosition.z);
        agent.SetDestination (fixedPosition);
        agent.stoppingDistance = stoppingDistance;
      }
      
    }

    #region mono

    void Start ()
    {
      
      // agent
      agent = GetComponent<DirectedNavAgent> ();
      if (agent == null)
        agent = gameObject.AddComponent<DirectedNavAgent> ();
      
      // initialize agent
      agent.speed = m_speed;
      agent.stoppingDistance = 0F;
      
      // character status bhvr
      statusBhvr = GetComponent<TS.CharacterStatusBhvr> ();

      myTransform = transform;

    }

    void Update ()
    {
      
      if (agent.hasPath) {
        
        // status(run/idle) checking ------
        
        if (Mathf.Abs (agent.destination.x - transform.localPosition.x) - agent.stoppingDistance
            < CACHE_DISTANCE) {
          
          if (statusBhvr != null) {
            if (statusBhvr.Status == TS.CharacterStatus.run) {
              statusBhvr.Status = TS.CharacterStatus.idle;
            }
          }
        } else {
          if (statusBhvr != null) {
            if (statusBhvr.Status != TS.CharacterStatus.run) {
              statusBhvr.Status = TS.CharacterStatus.run;
            }
          }
        }

        
      } // if agent.hasPath
      
      else {

        if (statusBhvr != null) {
          if (statusBhvr.Status == TS.CharacterStatus.run)
            statusBhvr.Status = TS.CharacterStatus.idle;
        }
      }


    }

    void LateUpdate(){
      Vector3 pos = myTransform.localPosition;
      myTransform.localPosition = new Vector3 (pos.x, pos.y, -pos.y);
    }

    #endregion
  }
}