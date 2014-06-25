using System;
using UnityEngine;

namespace TangLevel
{
  public class GroupBhvr : MonoBehaviour
  {

    #region delegates

    public delegate void StatusChange (GroupStatus status);
    // 状态结束时回调
    public StatusChange statusChangedHandler;

    #endregion

    #region attributes

    /// <summary>
    ///   战队状态，在*inspector*中使用
    /// </summary>
    public GroupStatus m_status = GroupStatus.none;
    /// <summary>
    ///   上一个状态
    /// </summary>
    public GroupStatus beforeStatus = GroupStatus.none;
    public GroupStatus newStatus = GroupStatus.none;

    #endregion

    #region properties

    /// <summary>
    ///   角色状态，被脚本使用，暂停的情况下不能修改状态
    /// </summary>
    public GroupStatus Status {
      get {
        return m_status;
      }
      set {
        // 状态相同不做修改
        if (newStatus != value) {
          newStatus = value;
        }
      }
    }
    #endregion

    #region monomethods

    void Update ()
    {
      // 状态改变通知
      if (newStatus != GroupStatus.none && m_status != newStatus) {
        beforeStatus = m_status; // 保留状态
        m_status = newStatus; // 切换到新状态
        newStatus = GroupStatus.none; // 新状态设置为 none
        if (statusChangedHandler != null) {
          statusChangedHandler (m_status);
        }
      }
    }

    void OnEnable ()
    {

      m_status = GroupStatus.none;
      beforeStatus = GroupStatus.none;
      newStatus = GroupStatus.none;

    }

    #endregion

  }
}