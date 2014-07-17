using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  public class HeroSelector
  {
    /// <summary>
    /// 找与指定英雄距离最近的敌方目标
    /// </summary>
    public static GameObject FindClosestTarget (HeroBhvr sourceHeroBhvr)
    {

      GameObject sgobj = sourceHeroBhvr.gameObject;
      List<GameObject> ol = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT 
        ? LevelContext.AliveDefenseGobjs : LevelContext.AliveSelfGobjs;

      return FindClosestTarget (sgobj, ol);
    }

    /// <summary>
    ///   找与指定英雄距离最近的友方目标
    /// </summary>
    public static GameObject FindclosestFriend (HeroBhvr sourceHeroBhvr)
    {
      GameObject sgobj = sourceHeroBhvr.gameObject;
      List<GameObject> ol = sourceHeroBhvr.hero.battleDirection == BattleDirection.LEFT
        ? LevelContext.AliveDefenseGobjs : LevelContext.AliveSelfGobjs;

      return FindClosestTarget (sgobj, ol);
 
    }

    /// <summary>
    /// 获取距离最近的目标
    /// </summary>
    /// <returns>与指定英雄距离最近的目标</returns>
    /// <param name="sgobj">当前英雄游戏对象</param>
    /// <param name="ol">要计算距离的英雄列表</param>
    public static GameObject FindClosestTarget (GameObject sgobj, List<GameObject>  ol)
    {
      // 源对象的 x 坐标
      float posx = sgobj.transform.localPosition.x;
      // 最接近的对象
      GameObject closestGobj = null;
      // 最短的距离
      float closestDistance = 0;
      foreach (GameObject gobj in ol) {

        if (gobj == sgobj)
          continue;

        if (closestGobj == null) {
          closestGobj = gobj;
          closestDistance = Mathf.Abs (posx - gobj.transform.localPosition.x);
        } else {
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
    /// 以某一个点为中心，在指定的对象列表中，找出在宽度为 width ，高度不限制的所有目标
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
    public static List<T> FindHerosWithWidth<T> (List<GameObject> ol, Vector3 center, float width) 
      where T : Component
    {
      float left = center.x - width / 2;
      float right = center.x + width / 2;
      List<T> ret = new List<T> ();
      foreach (GameObject g in ol) {
        Vector3 pos = g.transform.localPosition;
        if (pos.x > left && pos.x < right) {
          ret.Add (g.GetComponent<T> ());
        }
      }
      return ret;
    }

    /// <summary>
    /// 找以为个点为中心，半径为 width 的敌人
    /// </summary>
    /// <returns>The enemies with width.</returns>
    /// <param name="self">Self.</param>
    /// <param name="center">Center.</param>
    /// <param name="width">Width.</param>
    public static List<GameObject> FindEnemiesWithWidth (GameObject self, Vector3 center, float width)
    {

      List<GameObject> ol = null;
      HeroBhvr heroBhvr = self.GetComponent<HeroBhvr> ();
      if (heroBhvr != null) {
        if (heroBhvr.hero.battleDirection == BattleDirection.RIGHT) {
          // 进攻方
          ol = LevelContext.AliveDefenseGobjs;
        } else {
          // 防守方
          ol = LevelContext.AliveSelfGobjs;
        }
        return FindTargetsWithWidth (ol, center, width);
      }
      return null;
    }

    public static List<T> FindEnemiesWithWidth<T> (GameObject self, Vector3 center, float width) 
      where T : Component
    {
      List<GameObject> ol = null;
      HeroBhvr heroBhvr = self.GetComponent<HeroBhvr> ();
      if (heroBhvr != null) {
        if (heroBhvr.hero.battleDirection == BattleDirection.RIGHT) {
          // 进攻方
          ol = LevelContext.AliveDefenseGobjs;
        } else {
          // 防守方
          ol = LevelContext.AliveSelfGobjs;
        }
        return FindHerosWithWidth<T> (ol, center, width);
      }
      return null;
    }

    /// <summary>
    /// 找在某个点上的英雄，没有则返回 null
    /// </summary>
    /// <returns>The <see cref="UnityEngine.GameObject"/>.</returns>
    /// <param name="point">Point.</param>
    public static GameObject FindGobjAt (Vector3 point)
    {
      const float accuracy = 0.1F;
      foreach (GameObject gobj in LevelContext.AliveDefenseGobjs) {
        Vector3 tpos = gobj.transform.localPosition;
        if (tpos.x - point.x < accuracy && tpos.y - point.y < accuracy) {
          return gobj;
        }
      }
      foreach (GameObject gobj in LevelContext.AliveSelfGobjs) {
        Vector3 tpos = gobj.transform.localPosition;
        if (tpos.x - point.x < accuracy && tpos.y - point.y < accuracy) {
          return gobj;
        }
      }
      return null;
    }

    /// <summary>
    /// 找 X = xval 的垂直线上所有的英雄对象
    /// </summary>
    /// <returns>The gobjs at vertical line.</returns>
    /// <param name="x">The x coordinate.</param>
    public static List<GameObject> FindGobjsAtVerticalLine (float xval)
    {
      const float accuracy = 0.1F;
      List<GameObject> ret = new List<GameObject> ();
      foreach (GameObject gobj in LevelContext.AliveDefenseGobjs) {
        Vector3 tpos = gobj.transform.localPosition;
        if (Mathf.Abs (tpos.x - xval) < accuracy) {
          ret.Add (gobj);
        }
      }
      foreach (GameObject gobj in LevelContext.AliveSelfGobjs) {
        Vector3 tpos = gobj.transform.localPosition;
        if (Mathf.Abs (tpos.x - xval) < accuracy) {
          ret.Add (gobj);
        }
      }
      return ret;
    }


    /// <summary>
    ///   找指定英雄的己方队伍中最虚弱的英雄
    /// </summary>
    public static GameObject FindSelfWeakest (Hero hero)
    {
      List<GameObject> list = hero.battleDirection == BattleDirection.RIGHT ?
        LevelContext.AliveSelfGobjs : LevelContext.AliveDefenseGobjs;
      
      return FindWeakest (list);
        
    }

    /// <summary>
    /// 获取战队里面最虚弱的英雄对象
    /// </summary>
    /// <returns>The weakest.</returns>
    /// <param name="list">List.</param>
    public static GameObject FindWeakest (List<GameObject> list)
    {
      GameObject ret = null;
      //int minHp = int.MaxValue;
      float minHpPercent = 1F;
      foreach (GameObject g in list) {
        Hero hero = g.GetComponent<HeroBhvr> ().hero;
        float hpPercent = (float)hero.hp / (float)hero.maxHp;
        if (hpPercent <= minHpPercent && hpPercent > 0) {
          minHpPercent = hpPercent;
          ret = g;
        }
      }
      return ret;
    }
  }
}

