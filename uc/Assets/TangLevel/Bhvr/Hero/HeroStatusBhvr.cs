using System;
using UnityEngine;

namespace TangLevel
{
  public class HeroStatusBhvr : MonoBehaviour
  {
    public delegate void StatusChange (HeroStatus status);

    public delegate void PauseChange (bool pause);
    // 状态结束时回调
    public StatusChange statusChangedHandler;
    public PauseChange pauseChangedHandler;
    /// <summary>
    ///   角色状态，在*inspector*中使用
    /// </summary>
    public HeroStatus m_status = HeroStatus.none;
    /// <summary>
    ///   上一个状态
    /// </summary>
    public HeroStatus beforeStatus = HeroStatus.none;
    private HeroStatus newStatus = HeroStatus.none;

    /// <summary>
    ///   角色状态，被脚本使用，暂停的情况下不能修改状态
    /// </summary>
    public HeroStatus Status {
      get {
        return m_status;
      }
      set {
        if (!m_isPause && newStatus != value) {
          newStatus = value;
        }
      }
    }
    // 暂停 ----
    public bool m_isPause = false;
    private bool newIsPause = false;

    public bool IsPause {
      get {
        return m_isPause;
      }
      set {
        newIsPause = value;
      }
    }
    // 大招 ----
    public bool m_isBigMove = false;

    public bool IsBigMove {
      get {
        return m_isBigMove;
      }
      set {
        m_isBigMove = value;
      }
    }

    void Update ()
    {

      // 暂停改变通知
      if (m_isPause != newIsPause) {
        m_isPause = newIsPause;
        if (pauseChangedHandler != null) {
          pauseChangedHandler (m_isPause);
        }
      }

      // 状态改变通知
      else if (m_status != newStatus) {
        beforeStatus = m_status; // 保留状态
        m_status = newStatus; // 切换到新状态
        if (statusChangedHandler != null) {
          statusChangedHandler (m_status);
        }
      } 


    }

    void OnEnable(){

      m_status = HeroStatus.none;
      beforeStatus = HeroStatus.none;
      newStatus = HeroStatus.none;

      m_isPause = false;
      newIsPause = false;

      m_isBigMove = false;
    }
  }
}

