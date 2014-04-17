using System;
using UnityEngine;
using TS = TangScene;

namespace TangLevel
{
  public class HeroGobjManager
  {
    public static void LazyLoad (string name)
    {

    }

    /// <summary>
    /// 获取一个没有被使用的英雄游戏对象
    /// </summary>
    /// <returns>The unused.</returns>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (Hero hero)
    {
      // 因为资源名称定义没有标准化，先强制使用测试资源 centaur/charactor
      string heroResName = "centaur/charactor";
      GameObject gobj = TangDragonBones.CharacterManager.FetchUnused (heroResName);
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
        // HeroBhvr
        HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
        if (heroBhvr == null) {
          heroBhvr = gobj.AddComponent<HeroBhvr> ();
          heroBhvr.hero = hero;
        }

      }

      return gobj;

    }
  }
}

