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
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class LevelContext
  {
    #region Attributes

    private static Level m_currentLevel = null;

    #endregion

    // -- 关卡 --

    #region Level

    /// <summary>
    /// 正在挑战，第一个子关卡的所有资源加载后
    /// </summary>
    /// <value><c>true</c> if challenging; otherwise, <c>false</c>.</value>
    public static bool Challenging {
      get;
      set;
    }

    /// <summary>
    /// 是否在关卡里面，玩家点击挑战按钮后，该状态设置为 true，离开关卡后，该状态设置为 false
    /// </summary>
    /// <value><c>true</c> if in level; otherwise, <c>false</c>.</value>
    public static bool InLevel {
      get;
      set;
    }

    /// <summary>
    /// 当前关卡
    /// </summary>
    /// <value>The current level.</value>
    public static Level CurrentLevel {
      get{ return m_currentLevel; }
      set{ m_currentLevel = value; CurrentSubLevel = value.subLevels [0]; }
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
          if (CurrentSubLevel != null) { // 挑战已经开始，不是第一个子关卡
            int nextIndex = CurrentSubLevel.index + 1;
            if (m_currentLevel.subLevels.Length > nextIndex) {
              return m_currentLevel.subLevels [nextIndex];
            }
          } else { // 挑战还没开始，第一个子关卡
            if (m_currentLevel.subLevels.Length > 0) {
              return m_currentLevel.subLevels [0];
            }
          }
        } 
        return null;
      }
    }

    /// <summary>
    /// 我方小组(model)
    /// </summary>
    public static Group attackGroup;


    /// <summary>
    /// 我方小组(model)
    /// </summary>
    public static Group attackGroupBackup;

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
    public static List<GameObject> defenseGobjs = new List<GameObject> ();
    /// <summary>
    /// 我方所有 GameObject
    /// </summary>
    public static List<GameObject> attackGobjs = new List<GameObject> ();
    /// <summary>
    /// 活着的敌方英雄
    /// </summary>
    private static List<GameObject> aliveDefenseGobjs = new List<GameObject> ();

    public static List<GameObject> AliveDefenseGobjs {
      get {
        aliveDefenseGobjs.Clear ();
        if (defenseGobjs.Count > 0) {
          foreach (GameObject gobj in defenseGobjs) {
            HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
            if (heroBhvr != null) {
              if (heroBhvr.hero.hp > 0)
                aliveDefenseGobjs.Add (gobj);
            }
          }
        }
        return aliveDefenseGobjs;
      }
    }

    /// <summary>
    /// 活着的我方英雄
    /// </summary>
    private static List<GameObject> aliveSelfGobjs = new List<GameObject> ();

    public static List<GameObject> AliveSelfGobjs {
      get {
        aliveSelfGobjs.Clear ();
        if (attackGobjs.Count > 0) {
          foreach (GameObject gobj in attackGobjs) {
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
    /// <returns>The alive attack gobj.</returns>
    /// <param name="id">Identifier.</param>
    public static GameObject GetAliveSelfGobj (int id)
    {
      GameObject g = null;
      foreach (GameObject gobj in attackGobjs) {
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
    public static T GetHeroComponent<T> (int id) where T : Component
    {

      T t = null;
      // find in attack group
      foreach (GameObject gobj in attackGobjs) {
        HeroBhvr hb = gobj.GetComponent<HeroBhvr> ();
        if (hb != null && hb.hero.id == id) {
          t = gobj.GetComponent<T> ();
          break;
        }
      }
      // find in defense group if h is null
      if (t == null) {
        foreach (GameObject gobj in defenseGobjs) {
          HeroBhvr hb = gobj.GetComponent<HeroBhvr> ();
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

