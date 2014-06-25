using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  /// <summary>
  /// 战队状态控制
  /// </summary>
  public class GroupController
  {

    /// <summary>
    /// 修改团队状态
    /// </summary>
    /// <param name="heros">Heros.</param>
    /// <param name="status">Status.</param>
    public static void ChagneStatus(List<GameObject> heros, GroupStatus status)
    {

      foreach(GameObject gobj in heros){

        GroupBhvr gbhvr = gobj.GetComponent<GroupBhvr> ();
        if (gbhvr != null) {
          gbhvr.Status = status;
        }
      }

    }

    /// <summary>
    /// 团队移动
    /// </summary>
    /// <param name="heros">Heros.</param>
    /// <param name="x">The x coordinate.</param>
    public static void NavTo(List<GameObject> heros, float x, float stoppingDistance){

      float startX = GetGroupX (heros);

      // 移动每一个英雄
      foreach (GameObject gobj in heros) {
        DirectedNavigable nav = gobj.GetComponent<DirectedNavigable> ();
        if (nav != null) {
          float fixedX = x - (startX - gobj.transform.position.x);
          nav.NavTo (fixedX, stoppingDistance);
        }
      }

    }


    /// <summary>
    /// 获取最前面的英雄的x坐标
    /// </summary>
    /// <returns>The group x.</returns>
    /// <param name="heros">Heros.</param>
    public static float GetGroupX(List<GameObject> heros){

      float startX = 0;
      int count = 0;
      foreach (GameObject gobj in heros) {

        HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
        if (heroBhvr != null) {
          if (heroBhvr.hero.battleDirection == BattleDirection.RIGHT) {
            if (gobj.transform.position.x > startX) {
              startX = gobj.transform.position.x;
            }
          } else {
            if (count == 0) {
              startX = float.MaxValue;
            }
            if (gobj.transform.position.x < startX) {
              startX = gobj.transform.position.x;
            }
          }
        }
        count++;
      }
      return startX;
    }

  }
}

