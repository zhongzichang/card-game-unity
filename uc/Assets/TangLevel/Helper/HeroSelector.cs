using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  public class HeroSelector
  {
    /// <summary>
    /// 找距离最近的目标
    /// </summary>
    /// <returns>The target.</returns>
    /// <param name="sourceGobj">Source gobj.</param>
    public static GameObject FindClosestTarget (HeroBhvr sourceHeroBhvr)
    {

      GameObject sgobj = sourceHeroBhvr.gameObject;
      List<GameObject> ol = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT 
        ? LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;

      return FindClosestTarget (sgobj, ol);
    }

    /// <summary>
    /// 获取距离最近的可攻击目标
    /// </summary>
    /// <returns>The closest target.</returns>
    /// <param name="sgobj">Sgobj.</param>
    /// <param name="ol">Ol.</param>
    public static GameObject FindClosestTarget (GameObject sgobj, List<GameObject>  ol)
    {
      // 源对象的 x 坐标
      float posx = sgobj.transform.localPosition.x;
      // 最接近的对象
      GameObject closestGobj = null;
      // 最短的距离
      float closestDistance = 0;
      foreach (GameObject gobj in ol) {

        if (closestGobj == null)
          closestGobj = gobj;
        else {
          float distance = Mathf.Abs (posx - gobj.transform.localPosition.x);
          if (distance < closestDistance) {
            closestDistance = distance;
            closestGobj = gobj;
          }
        }
      }

      return closestGobj;
    }

    /// <summary>
    /// 以某一个点为中心，找出在宽度为 width ，高度不限制的目标
    /// </summary>
    public static List<GameObject> FindTargetsWithWidth (List<GameObject> ol, Vector3 center, float width)
    {
      float left = center.x - width / 2;
      float right = center.x + width / 2;
      List<GameObject> ret = new List<GameObject> ();
      foreach (GameObject g in ol) {
        Vector3 pos = g.transform.localPosition;
        if (pos.x > left && pos.x < right) {
          ret.Add (g);
        }
      }
      return ret;
    }


    /// <summary>
    /// 以某一个点为中心，找出在宽度为 width ，高度不限制的英雄
    /// </summary>
    public static List<HeroBhvr> FindHerosWithWidth (List<GameObject> ol, Vector3 center, float width)
    {
      float left = center.x - width / 2;
      float right = center.x + width / 2;
      List<HeroBhvr> ret = new List<HeroBhvr> ();
      foreach (GameObject g in ol) {
        Vector3 pos = g.transform.localPosition;
        if (pos.x > left && pos.x < right) {
          ret.Add (g.GetComponent<HeroBhvr>());
        }
      }
      return ret;
    }
  }
}

