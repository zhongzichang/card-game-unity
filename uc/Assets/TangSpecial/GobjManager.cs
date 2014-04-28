using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangSpecial
{
  public class GobjManager
  {
    #region Resource Load

    public delegate void LoadEventHandler (object sender,LoadResultEventArgs args);

    public static event LoadEventHandler RaiseLoadEvent;

    /// <summary>
    /// Load the specified Resource.
    /// </summary>
    /// <param name="name">Name.</param>
    public static void LazyLoad (string name)
    {

      if (Cache.gobjTable.ContainsKey (name)) {

        // 资源已准备完毕
        Notify (name, true);

      } else if (Config.use_packed_res) {

        // 使用 Assetbundle
        Tang.AssetBundleLoader.LoadAsync (name, OnAbLoadedCompleted, OnAbLoadedFailure);

      } else {

        LoadLocalResources (name);
      }
    }

    /// <summary>
    /// 从 Assetbundle 加载后的回调
    /// </summary>
    /// <param name="ab">Ab.</param>
    private static void OnAbLoadedCompleted (AssetBundle ab)
    {

      UnityEngine.Object asset = ab.Load (ab.name);
      GameObject gobj = null;
      if (asset != null) {
        AdvanceInstantiate (asset);
      }

    }

    /// <summary>
    /// Asset Bundle 加载失败
    /// </summary>
    /// <param name="name">Name.</param>
    private static void OnAbLoadedFailure (string name)
    {
      // 资源加载失败
      Notify (name, false);
    }

    /// <summary>
    /// 加载本地 Resource 文件夹的资源
    /// </summary>
    /// <param name="name">Name.</param>
    private static void LoadLocalResources (string name)
    {
      string filepath = Config.SpecialPath (name);
      UnityEngine.Object assets = Resources.Load (filepath);
      GameObject gobj = null;
      if (assets != null) {
        AdvanceInstantiate (assets);
      }
    }

    /// <summary>
    /// 自定义实例化
    /// </summary>
    /// <param name="asset">Asset.</param>
    private static void AdvanceInstantiate (UnityEngine.Object asset)
    {

      GameObject gobj = GameObject.Instantiate (asset) as GameObject;

      if (gobj != null) {

        // 修饰
        gobj.SetActive (false);
        gobj.name = asset.name;

        // 添加到缓存中
        GobjManager.Add (gobj);

        // 资源已准备完毕
        Notify (asset.name, true);

      } else {

        // 资源加载失败
        Notify (asset.name, false);

      }

    }

    /// <summary>
    /// 通知加载结果
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    private static void Notify (string name, bool success)
    {

      if (RaiseLoadEvent != null) {
        RaiseLoadEvent (null, new LoadResultEventArgs (name, success));
      }
    }

    #endregion

    #region Manage Game Object

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
    /// 释放游戏对象所占用的资源，不完全释放，调用了 Release(gobj,false);
    /// </summary>
    public static void Release (GameObject gobj)
    {
      Release (gobj, false);
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

    #endregion
  }
}

