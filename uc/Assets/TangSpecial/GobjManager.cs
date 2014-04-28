using System.Collections.Generic;
using UnityEngine;

namespace TangSpecial
{
  public class GobjManager
  {

    /// <summary>
    /// 获取一个没有被使用的游戏对象
    /// </summary>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (string name)
    {
      GameObject result = null;

      if (Cache.gobjTable.ContainsKey (name)) {

        // 查找是否存在没被使用的对象
        foreach (GameObject gobj in Cache.gobjTable[name]) {
          if (!gobj.activeSelf) {
            gobj.SetActive (true);
            result = gobj;
          }
        }
        // 如果找不到能用的，则克隆一个对象
        if (result == null) {
          GameObject source = Cache.gobjTable [name] [0];
          result = GameObject.Instantiate (source, Vector3.zero, Quaternion.identity) as GameObject;
        }
      }

      return result;
    }

    /// <summary>
    /// 增加一个对象到缓存
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
    /// <param name="all">该值为 true 时，完全释放该对象占用的资源；为false时，仅将对象设置为 unactive</param>
    public static void Release (GameObject gobj, bool all)
    {
      gobj.SetActive (false);

      if (all) {

        string name = gobj.name;

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

