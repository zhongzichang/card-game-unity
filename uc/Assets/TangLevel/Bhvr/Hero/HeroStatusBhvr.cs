using System;
using UnityEngine;

namespace TangLevel
{
  public class HeroStatusBhvr : MonoBehaviour
  {
    #region delegates
    public delegate void StatusChange (HeroStatus status);

    public delegate void PauseChange (bool pause);
    // 状态结束时回调
    public StatusChange statusChangedHandler;
    public PauseChange pauseChangedHandler;
    #endregion

    #region attributes
    /// <summary>
    ///   角色状态，在*inspector*中使用
    /// </summary>
    public HeroStatus m_status = HeroStatus.none;
    /// <summary>
    ///   上一个状态
    /// </summary>
    public HeroStatus beforeStatus = HeroStatus.none;
    public HeroStatus newStatus = HeroStatus.none;
    public bool m_isPause = false;
    public bool newIsPause = false;
    public bool m_isBigMove = false;
    #endregion


    #region properties
    /// <summary>
    ///   角色状态，被脚本使用，暂停的情况下不能修改状态
    /// </summary>
    public HeroStatus Status {
      get {
        return m_status;
      }
      set {
        if (m_status != HeroStatus.dead// 死亡后不能修改状态
            && !m_isBigMove// 大招的时候不能修改状态
          && newStatus != value // 状态相同不做修改
        ) {


          switch (m_status) {
          // 眩晕状态下只能改为空闲
          case HeroStatus.vertigo:
            if (value == HeroStatus.idle) {
              newStatus = value;
            }
            break;
          default:
            newStatus = value;
            break;
          }

          
        }
      }
    }
    // 暂停 ----

    public bool IsPause {
      get {
        return m_isPause;
      }
      set {
        /*
        if (m_isBigMove) {
          if (!value) { // 大招时，只有暂停状态下才能恢复，无法避免系统叫暂停的情况
            newIsPause = value;
          }
        } else {
          newIsPause = value;
        }*/
        newIsPause = value;
      }
    }
    // 大招 ----
    public bool IsBigMove {
      get {
        return m_isBigMove;
      }
      set {
        m_isBigMove = value;
      }
    }
    #endregion

    #region monomethods
    void Update ()
    {

      // 暂停改变通知
      if (m_isPause != newIsPause) {
        if( newIsPause && IsBigMove ){
          Debug.Log ("pause when bigmove");
        }
        m_isPause = newIsPause;
        if (pauseChangedHandler != null) {
          pauseChangedHandler (m_isPause);
        }
      }

      // 状态改变通知
      if (m_status != newStatus) {
        beforeStatus = m_status; // 保留状态
        m_status = newStatus; // 切换到新状态
        if (statusChangedHandler != null) {
          statusChangedHandler (m_status);
        }
      } 


    }

    void OnEnable ()
    {

      m_status = HeroStatus.none;
      beforeStatus = HeroStatus.none;
      newStatus = HeroStatus.none;

      m_isPause = false;
      newIsPause = false;

      m_isBigMove = false;
    }

    #endregion
  }
}

