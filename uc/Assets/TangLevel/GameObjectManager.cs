using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  /// <summary>
  /// Game object manager.
  /// </summary>
  public class GameObjectManager
  {
    /// <summary>
    /// 获取一个游戏对象－没有被使用的
    /// </summary>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (string name)
    {
      GameObject result = null;
      if (Cache.gobjTable.ContainsKey (name)) {
        foreach (GameObject gobj in Cache.gobjTable[name]) {
          if (!gobj.activeSelf) {
            gobj.SetActive (true);
            result = gobj;
          }
        }
      }

      return result;
    }

    /// <summary>
    /// Add the specified gobj.
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    public static void Add (GameObject gobj)
    {
      if (Cache.gobjTable.ContainsKey (gobj.name)) {
        Cache.gobjTable [gobj.name].Add (gobj);
      } else {
        List<GameObject> list = new List<GameObject> ();
        list.Add (gobj);
        Cache.gobjTable.Add (gobj.name, list);
      }
    }

    /// <summary>
    /// 释放游戏对象所占用的资源
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="all">If set to <c>true</c> all.</param>
    public static void Release (GameObject gobj, bool all)
    {
      string name = gobj.name;
      gobj.SetActive (false);
      if (all) {
        if (Cache.gobjTable.ContainsKey (name)) {
          Cache.gobjTable [name].Remove (gobj);
          if (Cache.gobjTable [name].Count == 0) {
            Cache.gobjTable.Remove (name);
          }
        }
        GameObject.Destroy (gobj);
      }
    }
  }
}

