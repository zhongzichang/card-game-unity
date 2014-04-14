using System;
using UnityEngine;

namespace TangLevel
{

  /// <summary>
  /// Level controller.
  /// </summary>
  public class LevelController
  {

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

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    public static void NextSubLevel(){

    }

  }
}

