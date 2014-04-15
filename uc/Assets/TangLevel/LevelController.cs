using System;
using UnityEngine;

namespace TangLevel
{

  /// <summary>
  /// Level controller.
  /// </summary>
  public class LevelController
  {

    /// <summary>
    /// Occurs when raise sub level loaded event.
    /// </summary>
    public static event EventHandler RaiseSubLevelLoadedEvent;

    private static int m_currentLevelId = 0;

    public static int CurrentLevelId {
      get{
        return m_currentLevelId;
      }
      set{
        m_currentLevelId = value;
      }
    }

    /// <summary>
    /// 预加载指定关卡的资源
    /// </summary>
    /// <param name="levelId">Level identifier.</param>
    public static void LoadNextSubLevel(){

    }

    private static void OnSubLevelLoaded(){
      if (RaiseSubLevelLoadedEvent != null)
        RaiseSubLevelLoadedEvent (null, EventArgs.Empty);
    }

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    public static void EnterNextSubLevel(){

    }

    /// <summary>
    /// Lefts the current level.
    /// </summary>
    public static void LeftLevel(){

      m_currentLevelId = 0;
      // 发出离开关卡通知

    }

    /// <summary>
    /// 暂停当前关卡
    /// </summary>
    public static void Pause(){

    }

    /// <summary>
    /// 恢复当前关卡
    /// </summary>
    public static void Resume(){
    }

    /// <summary>
    /// Determines if is last sub level.
    /// </summary>
    /// <returns><c>true</c> if is last sub level; otherwise, <c>false</c>.</returns>
    public static bool IsLastSubLevel(){
      return false;
    }

  }
}

