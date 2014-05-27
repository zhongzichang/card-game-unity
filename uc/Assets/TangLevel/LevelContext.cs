// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  public class LevelContext
  {
    // -- 关卡 --

    #region Level

    /// <summary>
    /// 是否在关卡里面
    /// </summary>
    /// <value><c>true</c> if in level; otherwise, <c>false</c>.</value>
    public static bool InLevel {
      get;
      set;
    }

    private static Level m_currentLevel = null;

    /// <summary>
    /// 当前关卡
    /// </summary>
    /// <value>The current level.</value>
    public static Level CurrentLevel {
      get{ return m_currentLevel; }
      set{ m_currentLevel = value; }
    }

    /// <summary>
    /// 当前子关卡
    /// </summary>
    /// <value>The current sub level.</value>
    public static SubLevel CurrentSubLevel {
      get;
      set;
    }

    /// <summary>
    /// 下一个子关卡
    /// </summary>
    /// <value>The next sub level.</value>
    public static SubLevel NextSubLevel {
      get {
        if (m_currentLevel != null) {
          if (InLevel) { // 在关卡里面
            int nextIndex = CurrentSubLevel.index + 1;
            if (m_currentLevel.subLeves.Length > nextIndex) {
              return m_currentLevel.subLeves [nextIndex];
            }
          } else { // 在关卡外面
            if (m_currentLevel.subLeves.Length > 0) {
              return m_currentLevel.subLeves [0];
            }
          }
        } 
        return null;
      }
    }

    /// <summary>
    /// 目标子关卡
    /// </summary>
    /// <value>The target sub level.</value>
    public static SubLevel TargetSubLevel {
      get {
        return NextSubLevel;
      }
    }

    /// <summary>
    /// 我方小组(model)
    /// </summary>
    public static Group selfGroup;


    /// <summary>
    /// 我方小组(model)
    /// </summary>
    public static Group selfGroupBackup;

    #endregion

    // -- 场景 --

    #region Scene

    /// <summary>
    /// 游戏背景
    /// </summary>
    public static GameObject background;
    /// <summary>
    /// 敌方所有 GameObject
    /// </summary>
    public static List<GameObject> enemyGobjs = new List<GameObject> ();
    /// <summary>
    /// 我方所有 GameObject
    /// </summary>
    public static List<GameObject> selfGobjs = new List<GameObject> ();
    /// <summary>
    /// 活着的敌方英雄
    /// </summary>
    private static List<GameObject> aliveEnemyGobjs = new List<GameObject> ();

    public static List<GameObject> AliveEnemyGobjs {
      get {
        aliveEnemyGobjs.Clear ();
        if (enemyGobjs.Count > 0) {
          foreach (GameObject gobj in enemyGobjs) {
            HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
            if (heroBhvr != null) {
              if (heroBhvr.hero.hp > 0)
                aliveEnemyGobjs.Add (gobj);
            }
          }
        }
        return aliveEnemyGobjs;
      }
    }

    /// <summary>
    /// 活着的我方英雄
    /// </summary>
    private static List<GameObject> aliveSelfGobjs = new List<GameObject> ();

    public static List<GameObject> AliveSelfGobjs {
      get {
        aliveSelfGobjs.Clear ();
        if (selfGobjs.Count > 0) {
          foreach (GameObject gobj in selfGobjs) {
            HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
            if (heroBhvr != null) {
              if (heroBhvr.hero.hp > 0)
                aliveSelfGobjs.Add (gobj);
            }
          }
        }
        return aliveSelfGobjs;
      }
    }

    /// <summary>
    /// 根据ID获取活着的我方英雄对象
    /// </summary>
    /// <returns>The alive self gobj.</returns>
    /// <param name="id">Identifier.</param>
    public static GameObject GetAliveSelfGobj (int id)
    {
      GameObject g = null;
      foreach (GameObject gobj in selfGobjs) {
        HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
        if (heroBhvr != null) {
          if (heroBhvr.hero.id == id) {
            g = gobj;
            break;
          }
        }
      }
      return g;
    }

    /// <summary>
    /// 根据英雄ID获取 Component
    /// </summary>
    /// <returns>The hero bhvr.</returns>
    /// <param name="id">Identifier.</param>
    public static T GetHeroComponent<T>(int id) where T : Component {

      T t = null;
      // find in self group
      foreach (GameObject gobj in selfGobjs) {
        HeroBhvr hb = gobj.GetComponent<HeroBhvr>();
        if (hb != null && hb.hero.id == id) {
          t = gobj.GetComponent<T> ();
          break;
        }
      }
      // find in enemy group if h is null
      if( t == null )
        {
          foreach (GameObject gobj in enemyGobjs) {
            HeroBhvr hb = gobj.GetComponent<HeroBhvr>();
            if (hb != null && hb.hero.id == id) {
              t = gobj.GetComponent<T> ();
              break;
            }
          }
        }
      return t;
    }

    /// <summary>
    /// 子关卡开始时活着的英雄
    /// </summary>
    public static List<GameObject> SubLevelBeganGobjs = new List<GameObject> ();
    public static bool isPause = false;

    #endregion
  }
}

