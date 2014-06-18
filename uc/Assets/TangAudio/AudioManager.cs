using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangAudio
{
  public class AudioManager
  {
    /// <summary>
    /// 加载完毕后回调该委派
    /// </summary>

    public delegate void LoadEventHandler (object sender,LoadResultEventArgs args);

    public static event LoadEventHandler RaiseLoadEvent;

    /// <summary>
    /// 声音资源表
    /// </summary>
    public static Dictionary<string, AudioClip> audioTable = new Dictionary<string, AudioClip> ();


    public static bool HasHandler (LoadEventHandler func)
    {
      bool ret = false;
      if (RaiseLoadEvent != null) {
        Delegate[] delegates = RaiseLoadEvent.GetInvocationList ();
        for (int i = 0; i < delegates.Length; i++) {
          if (delegates [i] == func) {
            ret = true;
          }
        }
      }
      return ret;
    }

    /// <summary>
    /// 加载声音资源
    /// </summary>
    /// <param name="name">Name.</param>
    public static void LazyLoad (string name)
    {
      if (audioTable.ContainsKey (name)) {
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
    /// 获取声音资源
    /// </summary>
    /// <param name="name">Name.</param>
    public static AudioClip Fetch (string name)
    {
      if (audioTable.ContainsKey (name)) {
        return audioTable [name];
      } else {
        return null;
      }
    }


    /// <summary>
    /// 从 Assetbundle 加载后的回调
    /// </summary>
    /// <param name="ab">Ab.</param>
    private static void OnAbLoadedCompleted (AssetBundle ab)
    {
      AudioClip audio = ab.Load (ab.name) as AudioClip;
      if (audio != null && !audioTable.ContainsKey (ab.name)) {
        audioTable.Add (audio.name, audio);
        Notify (audio.name, true);
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
      string filepath = Config.AudioPath (name);
      AudioClip audio = Resources.Load (filepath) as AudioClip;
      if (audio != null) {
        audioTable.Add (audio.name, audio);
        Notify (audio.name, true);
      } else {
        Notify (name, false);
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

  }
}

