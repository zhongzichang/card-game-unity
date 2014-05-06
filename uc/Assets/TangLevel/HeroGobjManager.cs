using System;
using UnityEngine;
using TS = TangScene;
using TD = TangDragonBones;

namespace TangLevel
{
  public class HeroGobjManager
  {
    /// <summary>
    /// 加载一个英雄游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    public static void LazyLoad (string name)
    {
      TD.DbgoManager.LazyLoad (name);
    }

    /// <summary>
    /// 加载多个英雄游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="count">Count.</param>
    public static void LazyLoad (string name, int count)
    {
      Debug.Log ("HeroGobjManager.LazyLoad " + name + " " + count);
      TD.DbgoManager.LazyLoad (name, count);
    }

    /// <summary>
    /// 获取一个没有被使用的英雄游戏对象
    /// </summary>
    /// <returns>The unused.</returns>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (Hero hero)
    {
      GameObject gobj = TD.DbgoManager.FetchUnused (hero.resName);
      if (gobj != null) {
        // DirectedNavigable
        DirectedNavigable navigable = gobj.GetComponent<DirectedNavigable> ();
        if (navigable == null) {
          navigable = gobj.AddComponent<DirectedNavigable> ();
        }
        // CharacterStatusBhvr
        TS.CharacterStatusBhvr statusBhvr = gobj.GetComponent<TS.CharacterStatusBhvr> ();
        if (statusBhvr == null) {
          statusBhvr = gobj.AddComponent<TS.CharacterStatusBhvr> ();
        }
        // Directional
        Directional directional = gobj.GetComponent<Directional> ();
        if (directional == null) {
          directional = gobj.AddComponent<Directional> ();
        }
        directional.Direction = hero.battleDirection;
        // Skill
        SkillBhvr skillBhvr = gobj.GetComponent<SkillBhvr> ();
        if (skillBhvr == null) {
          skillBhvr = gobj.AddComponent<SkillBhvr> ();
        }
        // HeroBhvr
        HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
        if (heroBhvr == null) {
          heroBhvr = gobj.AddComponent<HeroBhvr> ();
        }
        heroBhvr.hero = hero;
        // DemoBhvr , make disable
        TD.DemoBhvr demoBhvr = gobj.GetComponent<TD.DemoBhvr> ();
        if (demoBhvr != null && demoBhvr.enabled) {
          demoBhvr.enabled = false;
        }
        // AI
        if (hero.ai != null) {
          foreach (string scriptName in hero.ai) {
            Component component = gobj.GetComponent (scriptName);
            if (component == null) {
              component = gobj.AddComponent (scriptName);
            }
            if (component == null) {
              Debug.LogWarning ("Fail to add AI Component - " + scriptName);
            }
          }
        }


      }

      return gobj;

    }

    /// <summary>
    /// 释放英雄游戏对象 - 不释放资源，只设置为 unactive
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    public static void Release (GameObject gobj)
    {
      TD.DbgoManager.Release (gobj, false);
    }

    /// <summary>
    /// 释放英雄游戏对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="all">If set to <c>true</c> 释放所占有的资源.</param>
    public static void Release (GameObject gobj, bool all)
    {
      TD.DbgoManager.Release (gobj, all);
    }

    /// <summary>
    /// 获取可用英雄对象的数量
    /// </summary>
    /// <returns>The of unused.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOfUnused (string name)
    {
      return TD.DbgoManager.SizeOfUnused (name);
    }

    /// <summary>
    /// 获取已有英雄对象的数量
    /// </summary>
    /// <param name="name">Name.</param>
    public static int Size (string name)
    {
      return TD.DbgoManager.SizeOf (name);
    }

    public static void Clear ()
    {
      TD.DbgoManager.Clear ();
    }
  }
}

