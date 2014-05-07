using UnityEngine;
using System.Collections;

namespace TangLevel
{
  [RequireComponent (typeof(DirectedNavAgent), typeof(Directional))]
  public class DirectedNavigable : MonoBehaviour
  {
    public const float CACHE_DISTANCE = 10F;
    // 距离目标人物等于小于这个距离的时候人物动作由跑动改为站立(run=>idle)
    public static readonly Vector2 NOTIFIED_RANGE = new Vector2 (32F, 16F);
    // 摇杆操作，角色移动超过这个距离发通知（如果需要 nextPositionChangeHandle != null）
    public float m_speed = 6F;
    private DirectedNavAgent agent;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private Directional directional;

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

        agent.SetDestination (x);
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
      statusBhvr = GetComponent<HeroStatusBhvr> ();

      myTransform = transform;

      directional = GetComponent<Directional> ();
      if (directional == null)
        directional = gameObject.AddComponent<Directional> ();

    }

    void Update ()
    {
      
      if (agent.hasPath) {
        
        // status(run/idle) checking ------

        if (statusBhvr != null) {
          if (statusBhvr.Status != HeroStatus.running) {
            statusBhvr.Status = HeroStatus.running;
          }
        }

        
      } else {

        if (statusBhvr != null) {
          if (statusBhvr.Status == HeroStatus.running)
            statusBhvr.Status = HeroStatus.idle;
        }
      }


    }


    #endregion
  }
}