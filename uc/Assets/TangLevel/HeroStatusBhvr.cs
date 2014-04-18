using System;
using UnityEngine;

namespace TangLevel
{
  public class HeroStatusBhvr : MonoBehaviour
  {
    public delegate void StatusChange (HeroStatus status);
    // 状态开始时回调
    public StatusChange statusStartHandler;
    // 状态结束时回调
    public StatusChange statusEndHandler;
    /// <summary>
    ///   角色状态，在*inspector*中使用
    /// </summary>
    public HeroStatus m_status = HeroStatus.idle;
    // private SpriteAnimate spriteAnimate;
    //private ActorBhvr actorBhvr;

    void Start ()
    {
      /*
      spriteAnimate = GetComponent<SpriteAnimate> ();
      if (spriteAnimate != null) {
        spriteAnimate.lateLastFrameHandler += LateLastFrame;
        spriteAnimate.currentIndexChange += LateCurrentIndexChange;
      }

      actorBhvr = GetComponent<ActorBhvr> ();
*/
    }

    /// <summary>
    ///   角色状态，被脚本使用
    /// </summary>
    public HeroStatus Status {
      get {
        return m_status;
      }
      set {
        if (m_status != value) {

          HeroStatus before = m_status;

          if (statusEndHandler != null)
            statusEndHandler (m_status);

          m_status = value;

          if (statusStartHandler != null)
            statusStartHandler (m_status);

          // send notification for status changed
          /*
          if (actorBhvr != null) {
            Facade.Instance.SendNotification (NtftNames.ACTOR_STATUS_CHANGED,
              new HeroStatusChangedBean (actorBhvr.id, 
                before, 
                m_status));
          }*/

        }
      }
    }

    /// <summary>
    ///   当播放到最后一帧时被调用
    /// </summary>
    //public void LateLastFrame (SpriteAnimation spriteAnimation)
    //{
      /*
      if (Status == HeroStatus.attack)
        Status = HeroStatus.idle;
      else if (Status == HeroStatus.sprintBrake)
        Status = HeroStatus.idle;
      else if (Status == HeroStatus.die)
        spriteAnimate.spriteAnimation.Suspend ();
        */

    // }

    public void LateCurrentIndexChange (int index)
    {
      if (Status == HeroStatus.attack) {
        // 在这一帧发出攻击到达消息
        //if (index == 6)
          //Facade.Instance.SendNotification (NtftNames.ATTACK_HIT, actorBhvr.id);
      }
    }
  }
}

