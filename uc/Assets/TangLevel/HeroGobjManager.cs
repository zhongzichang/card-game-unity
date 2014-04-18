using System;
using UnityEngine;
using TS = TangScene;

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
      TangDragonBones.CharacterManager.LazyLoad (name);
    }

    /// <summary>
    /// 加载多个英雄游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="count">Count.</param>
    public static void LazyLoad( string name, int count){
      TangDragonBones.CharacterManager.LazyLoad (name, count);
    }

    /// <summary>
    /// 获取一个没有被使用的英雄游戏对象
    /// </summary>
    /// <returns>The unused.</returns>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (Hero hero)
    {
      GameObject gobj = TangDragonBones.CharacterManager.FetchUnused (hero.resName);
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
    /// 获取可用英雄对象的数量
    /// </summary>
    /// <returns>The of unused.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOfUnused(string name){
      return TangDragonBones.CharacterManager.SizeOfUnused (name);
    }

    /// <summary>
    /// 获取已有英雄对象的数量
    /// </summary>
    /// <param name="name">Name.</param>
    public static int Size(string name){
      return TangDragonBones.CharacterManager.SizeOf (name);
    }
  }
}

