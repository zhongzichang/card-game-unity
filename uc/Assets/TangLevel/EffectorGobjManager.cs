using System;
using UnityEngine;
using TS = TangScene;
using TD = TangDragonBones;

namespace TangLevel
{
  /// <summary>
  /// 作用器游戏对象管理器
  /// </summary>
  public class EffectorGobjManager
  {
    /// <summary>
    /// 加载一个作用器游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    public static void LazyLoad (string name)
    {
      TD.DbgoManager.LazyLoad (name);
    }

    /// <summary>
    /// 加载多个作用器游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="count">Count.</param>
    public static void LazyLoad (string name, int count)
    {
      Debug.Log ("EffectorGobjManager.LazyLoad " + name + " " + count);
      TD.DbgoManager.LazyLoad (name, count);
    }

    /// <summary>
    /// 获取一个没有被使用的作用器游戏对象
    /// </summary>
    /// <returns>The unused.</returns>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (Effector effector)
    {
      GameObject gobj = TD.DbgoManager.FetchUnused (effector.specialName);
      if (gobj != null) {

        // Scripts
        if (effector.scripts != null) {
          foreach (string scriptName in effector.scripts) {
            Component component = gobj.GetComponent (scriptName);
            if (component == null) {
              component = gobj.AddComponent (scriptName);
            }
            if (component == null) {
              Debug.LogWarning ("Fail to add Component - " + scriptName);
            }
          }
        }
      }
      return gobj;
    }

    /// <summary>
    /// 释放作用器游戏对象 - 不释放资源，只设置为 unactive
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    public static void Release (GameObject gobj)
    {
      TD.DbgoManager.Release (gobj, false);
    }

    /// <summary>
    /// 释放作用器游戏对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="all">If set to <c>true</c> 释放所占有的资源.</param>
    public static void Release (GameObject gobj, bool all)
    {
      TD.DbgoManager.Release (gobj, all);
    }

    /// <summary>
    /// 获取可用作用器对象的数量
    /// </summary>
    /// <returns>The of unused.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOfUnused (string name)
    {
      return TD.DbgoManager.SizeOfUnused (name);
    }

    /// <summary>
    /// 获取已有对象的数量
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

