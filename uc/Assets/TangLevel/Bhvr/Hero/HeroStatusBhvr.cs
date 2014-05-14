﻿using System;
using UnityEngine;

namespace TangLevel
{
  public class HeroStatusBhvr : MonoBehaviour
  {
    public delegate void StatusChange (HeroStatus status);

    // 状态结束时回调
    public StatusChange statusChangedHandler;

    /// <summary>
    ///   角色状态，在*inspector*中使用
    /// </summary>
    public HeroStatus m_status = HeroStatus.idle;

    /// <summary>
    ///   上一个状态
    /// </summary>
    public HeroStatus beforeStatus = HeroStatus.idle;


    private bool changed = false;

    void Update ()
    {
      if (changed) {

        changed = false;

        if (statusChangedHandler != null)
          statusChangedHandler (m_status);
      }
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

          beforeStatus = m_status;
          changed = true;
          m_status = value;
        }
      }
    }
  }
}

