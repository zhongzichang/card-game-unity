using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class PositionHelper
  {
    /// <summary>
    /// 为英雄找一个最好的位置
    /// </summary>
    /// <returns>The hero best position.</returns>
    /// <param name="x">The x coordinate.</param>
    public static Vector3 FindHeroBestPos (GameObject source)
    {
      Vector3 pos = source.transform.localPosition;

      const float accuracy = 3F;

      List<int> remainy = new List<int> ();
      remainy.AddRange (Config.HORIZONTAL_LINES);

      // 找出 X = pos.x 上的所有对象的 Y 值，然后从 remainy 中去掉
      foreach (GameObject gobj in LevelContext.AliveEnemyGobjs) {
        if (gobj != source) {
          Vector3 tpos = gobj.transform.localPosition;
          if (Mathf.Abs (tpos.x - pos.x) < accuracy) {
            int y = Mathf.RoundToInt (tpos.y);
            if (remainy.Contains (y)) {
              remainy.Remove (y);
            }
          }
        }
      }
      foreach (GameObject gobj in LevelContext.AliveSelfGobjs) {
        if (gobj != source) {
          Vector3 tpos = gobj.transform.localPosition;
          if (Mathf.Abs (tpos.x - pos.x) < accuracy) {
            int y = Mathf.RoundToInt (tpos.y);
            if (remainy.Contains (y)) {
              remainy.Remove (y);
            }
          }
        }
      }
      int besty = Mathf.RoundToInt (pos.y);
      float mindiff = 100F;
      if (remainy.IndexOf (besty) < 0 && remainy.Count > 0) {
        foreach (int y in remainy) {
          float diff = Mathf.Abs (pos.y - y);
          if (diff < mindiff) {
            mindiff = diff;
            besty = y;
          }
        }
        return new Vector3 (pos.x, (float) besty, Config.HERO_POS_MIN_Z + (besty-Config.BOTTOM_BOUND)*10);
      } else {
        return pos;
      }
    }
  }
}

