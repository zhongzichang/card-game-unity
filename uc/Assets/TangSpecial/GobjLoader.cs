using System;
using UnityEngine;

namespace TangSpecial
{
  public class GobjLoader : MonoBehaviour
  {
    public static event EventHandler RaiseResourceEvent;

    /// <summary>
    /// Load the specified Resource.
    /// </summary>
    /// <param name="name">Name.</param>
    public static void Load (string name)
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
    public static void OnAbLoadedCompleted (AssetBundle ab)
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
    public static void OnAbLoadedFailure (string name)
    {
      // 资源加载失败
      Notify (name, false);
    }

    /// <summary>
    /// 加载本地 Resource 文件夹的资源
    /// </summary>
    /// <param name="name">Name.</param>
    public static void LoadLocalResources (string name)
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
    public static void AdvanceInstantiate (UnityEngine.Object asset)
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
    public static void Notify (string name, bool success)
    {

      if (RaiseResourceEvent != null) {
        RaiseResourceEvent (null, new LoadResultEventArgs (name, success));
      }
    }
  }
}

