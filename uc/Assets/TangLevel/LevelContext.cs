// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;

namespace TangLevel
{
  public class LevelContext
  {

    // -- 关卡 --
    #region Level

    public static bool InLevel {
      get;
      set;
    }

    // 当前关卡
    private static Level m_currentLevel = null;

    public static Level CurrentLevel {
      get{ return m_currentLevel; }
      set{ m_currentLevel = value; }
    }

    // 当前子关卡
    private static int m_currentSubLevelIndex = 0;
    private static int m_currentSubLevelId = 0;

    public static SubLevel CurrentSubLevel {
      get {
        if (m_currentLevel != null && m_currentLevel.subLeves.Length > m_currentSubLevelIndex) {
          return m_currentLevel.subLeves [m_currentSubLevelIndex];
        }
        return null;
      }
    }

    // 下一个子关卡
    public static SubLevel NextSubLevel {
      get {
        if (m_currentLevel != null && m_currentLevel.subLeves.Length > m_currentSubLevelIndex + 1) {
          return m_currentLevel.subLeves [m_currentSubLevelIndex + 1];
        }
        return null;
      }
    }

    public static SubLevel TargetSubLevel{
      get{
        return InLevel ? NextSubLevel : CurrentSubLevel;
      }
    }

    //
    public static bool IsNextSubLevelResReady {
      get;
      set;
    }

    public static bool IsWaitingForEnten {
      get;
      set;
    }

    #endregion

    // -- 场景 --
    #region Scene

    #endregion
  }
}

