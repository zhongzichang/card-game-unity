using System;
using UnityEngine;
using TS = TangScene;
using TDB = TangDragonBones;
using DragonBones;


namespace TangLevel
{
  public class HeroArmatureManager
  {

    public const string BODY_NAME = "body";

    /// <summary>
    /// 加载一个英雄游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    public static void LazyLoad (string name)
    {
      TDB.ArmatureManager.LazyLoad (name);
    }

    /// <summary>
    /// 加载多个英雄游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="count">Count.</param>
    public static void LazyLoad (string name, int count)
    {
      TDB.ArmatureManager.LazyLoad (name, count);
    }

    /// <summary>
    /// 获取一个没有被使用的英雄游戏对象
    /// </summary>
    /// <returns>The unused.</returns>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (Hero hero)
    {

      GameObject gobj = new GameObject ();
      gobj.SetActive (false);

      Armature armature = TDB.ArmatureManager.FetchUnused (hero.resName);
      if (armature != null) {

        gobj.name = armature.Name;
        GameObject body = TDB.ArmatureManager.Gobj (armature);
        if (body != null) {
          body.name = BODY_NAME;
          body.transform.parent = gobj.transform;
          body.transform.localPosition = Vector3.zero;
          body.transform.localRotation = Quaternion.identity;
          if (!body.activeSelf) {
            body.SetActive (true);
          }
        }
      }

      TDB.ArmatureBhvr armatureBhvr = gobj.AddComponent<TDB.ArmatureBhvr> ();
      armatureBhvr.armature = armature;

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
      // AI
      if (hero.ai != null) {
        foreach (string scriptName in hero.ai) {
          Component component = gobj.AddComponent (scriptName);
          if (component == null) {
            Debug.LogWarning ("Fail to add AI Component - " + scriptName);
          }
        }
      }

      //gobj.SetActive (true);

      return gobj;

    }

    /// <summary>
    /// 释放英雄游戏对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="all">If set to <c>true</c> 释放所占有的资源.</param>
    public static void Release (GameObject gobj, bool all)
    {

      // 解脱与 body 的关系
      TDB.ArmatureBhvr armatureBhvr = gobj.GetComponent<TDB.ArmatureBhvr> ();
      if (armatureBhvr != null) {
        Transform body = gobj.transform.FindChild (BODY_NAME);
        if (body != null) {
          body.parent = null;
        }
        if (armatureBhvr.armature != null) {
          TDB.ArmatureManager.Release (armatureBhvr.armature, all);
        }

      }

      // 销毁自身
      GameObject.Destroy (gobj);

    }

    /// <summary>
    /// 获取可用英雄对象的数量
    /// </summary>
    /// <returns>The of unused.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOfUnused (string name)
    {
      return TDB.ArmatureManager.SizeOfUnused (name);
    }

    /// <summary>
    /// 获取已有英雄对象的数量
    /// </summary>
    /// <param name="name">Name.</param>
    public static int Size (string name)
    {
      return TDB.ArmatureManager.SizeOfAll (name);
    }
  }
}

