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
    public static void LazyLoad( string name, int count){
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
        // HeroBhvr
        HeroBhvr heroBhvr = gobj.GetComponent<HeroBhvr> ();
        if (heroBhvr == null) {
          heroBhvr = gobj.AddComponent<HeroBhvr> ();
        }
        heroBhvr.hero = hero;
        // Directional
        Directional directional = gobj.GetComponent<Directional> ();
        if (directional == null) {
          directional = gobj.AddComponent<Directional> ();
        }
        directional.Direction = hero.battleDirection;


      }

      return gobj;

    }

    /// <summary>
    /// 释放英雄游戏对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="all">If set to <c>true</c> 释放所占有的资源.</param>
    public static void Release(GameObject gobj, bool all ){
      TD.DbgoManager.Release (gobj, all);
    }

    /// <summary>
    /// 获取可用英雄对象的数量
    /// </summary>
    /// <returns>The of unused.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOfUnused(string name){
      return TD.DbgoManager.SizeOfUnused (name);
    }

    /// <summary>
    /// 获取已有英雄对象的数量
    /// </summary>
    /// <param name="name">Name.</param>
    public static int Size(string name){
      return TD.DbgoManager.SizeOf (name);
    }
  }
}

