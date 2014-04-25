// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using DragonBones;
using DragonBones.Factorys;
using DragonBones.Animation;
using DragonBones.Objects;
using DragonBones.Display;
using DragonBones.Textures;
using Com.Viperstudio.Utils;
using UnityEngine;

namespace TangDragonBones
{
  /// <summary>
  /// DragonBones 游戏对象管理器
  /// </summary>
  public class DbgoManager : MonoBehaviour
  {
    /// <summary>
    /// 需要预加载的骨架资源
    /// </summary>
    public LoadPair[] preloadArmatures;

    public delegate void ResEventHandler (object sender,ResEventArgs a);

    /// <summary>
    /// 资源加载后将执行
    /// </summary>
    public static event ResEventHandler RaiseLoadedEvent;

    /// <summary>
    /// 需加载的资源名称队列
    /// </summary>
    private static Queue<string> requireQueue = new Queue<string> ();
    /// <summary>
    /// Armature Factory
    /// </summary>
    private UnityFactory factory = null;

    #region Mono Methods

    void Start ()
    {
      //Application.targetFrameRate = 30;
      factory = new UnityFactory ();

      // 预加载
      if (preloadArmatures != null) {
        foreach (LoadPair p in preloadArmatures) {
          PreResourceLoad (p);
        }
      }
    }
    // Update is called once per frame
    void Update ()
    {

      if (requireQueue.Count > 0) {

        string name = requireQueue.Dequeue ();

        // 确认数据和资源在缓存中
        if (Cache.atlasDataTable.ContainsKey (name) &&
            Cache.textureTable.ContainsKey (name) &&
            Cache.skeletonDataTable.ContainsKey (name)) {

          // 创建一个游戏对象
          Create (name);

          // 发出通知事件，游戏对象已经准备完毕
          if (RaiseLoadedEvent != null)
            RaiseLoadedEvent (null, new ResEventArgs (name));

        } else {

          Debug.LogError ("DragonBones res " + name + " not ready.");

        }
      }


      WorldClock.Clock.AdvanceTime (Time.deltaTime);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// 创建一个游戏对象
    /// </summary>
    /// <param name="name">Name.</param>
    private void Create (string name)
    {

      SkeletonData skeletonData = Cache.skeletonDataTable [name];
      if (factory.GetSkeletonData (name) == null) {
        factory.AddSkeletonData (skeletonData, skeletonData.Name);
      }

      AtlasData atlasData = Cache.atlasDataTable [name];
      Texture texture = Cache.textureTable [name];
      if (factory.GetTextureAtlas (name) == null) {
        factory.AddTextureAtlas (new TextureAtlas (texture, atlasData));
      }

      Armature armature = factory.BuildArmature (name, null, name);
      GameObject heroObj = new GameObject ();
      heroObj.name = armature.Name;
      GameObject armatureGobj = (armature.Display as UnityArmatureDisplay).Display;
      armatureGobj.name = "body";
      armatureGobj.transform.parent = heroObj.transform;
      armatureGobj.transform.localPosition = Vector3.zero;
      armatureGobj.transform.localRotation = Quaternion.identity;
      DragonBonesBhvr bhvr = heroObj.AddComponent<DragonBonesBhvr> ();
      bhvr.armature = armature;
      heroObj.SetActive (false);
      AddToCache (heroObj);

    }

    #endregion

    #region GameObject Methods

    /// <summary>
    /// 加载角色游戏对象
    /// </summary>
    /// <param name="name">Name.</param>
    public static void LazyLoad (string name)
    {

      if (Cache.atlasDataTable.ContainsKey (name) &&
          Cache.textureTable.ContainsKey (name) &&
          Cache.skeletonDataTable.ContainsKey (name)) {

        // 有了
        OnResReady (name);

      } else if (Config.use_packed_res) {

        // 从资源包下载
        Tang.AssetBundleLoader.LoadAsync (name, OnAbLoaded);

      } else {

        // 本地读取
        ResourceLoad (name);
      }
    }

    /// <summary>
    /// 加载多个英雄游戏对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="count">Count.</param>
    public static void LazyLoad (string name, int count)
    {
      for (int i = 0; i < count; i++)
        LazyLoad (name);
    }

    /// <summary>
    /// 从 AssetBundle 加载资源
    /// </summary>
    /// <param name="ab">Ab.</param>
    private static void OnAbLoaded (AssetBundle ab)
    {
      //Debug.Log ("OnAbLoaded");
      if (ab != null) {


        TextAsset atlasAssets = null;
        Texture textureAssets = null;
        TextAsset skeletonAssets = null;

        if (!Cache.atlasDataTable.ContainsKey (ab.name)) {
          string atlasFilepath = ab.name + "_atlas";
          atlasAssets = ab.Load (atlasFilepath, typeof(TextAsset)) as TextAsset;
        }
        if (!Cache.textureTable.ContainsKey (ab.name)) {
          string textureFilepath = ab.name + "_texture";
          textureAssets = ab.Load (textureFilepath, typeof(Texture)) as Texture;
        }
        if (!Cache.skeletonDataTable.ContainsKey (ab.name)) {
          string skeletonFilepath = ab.name + "_skeleton";
          skeletonAssets = ab.Load (skeletonFilepath, typeof(TextAsset)) as TextAsset;
        }

        SetUpAndCreate (ab.name, atlasAssets, textureAssets, skeletonAssets);
      }

    }

    /// <summary>
    /// 从本地 Resources 文件夹中加载资源
    /// </summary>
    /// <param name="name">Name.</param>
    private static void ResourceLoad (string name)
    {

      //Debug.Log ("ResourceLoad");

      TextAsset atlasAssets = null;
      Texture textureAssets = null;
      TextAsset skeletonAssets = null;

      if (!Cache.atlasDataTable.ContainsKey (name)) {
        string atlasFilepath = Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_atlas";
        atlasAssets = Resources.Load (atlasFilepath, typeof(TextAsset)) as TextAsset;
      }
      if (!Cache.textureTable.ContainsKey (name)) {
        string textureFilepath = Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_texture";
        textureAssets = Resources.Load (textureFilepath, typeof(Texture)) as Texture;
      }
      if (!Cache.skeletonDataTable.ContainsKey (name)) {
        string skeletonFilepath = Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_skeleton";
        skeletonAssets = Resources.Load (skeletonFilepath, typeof(TextAsset)) as TextAsset;
      }

      SetUpAndCreate (name, atlasAssets, textureAssets, skeletonAssets); 

    }

    /// <summary>
    /// 预加载需要的数据，用于优化
    /// </summary>
    /// <param name="pair">Pair.</param>
    private static void PreResourceLoad (LoadPair pair)
    {

      string name = pair.name;

      if (name != null) {

        TextAsset atlasAssets = null;
        Texture textureAssets = null;
        TextAsset skeletonAssets = null;

        if (!Cache.atlasDataTable.ContainsKey (name)) {
          string atlasFilepath = Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_atlas";
          atlasAssets = Resources.Load (atlasFilepath, typeof(TextAsset)) as TextAsset;
          Debug.Log (atlasFilepath);
        }
        if (!Cache.skeletonDataTable.ContainsKey (name)) {
          string skeletonFilepath = Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_skeleton";
          skeletonAssets = Resources.Load (skeletonFilepath, typeof(TextAsset)) as TextAsset;
          Debug.Log (skeletonFilepath);
        }

        if (pair.includeTexture && !Cache.textureTable.ContainsKey (name)) {
          string textureFilepath = Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_texture";
          textureAssets = Resources.Load (textureFilepath, typeof(Texture)) as Texture;
          Debug.Log (textureFilepath);
        }

        SetUp (name, atlasAssets, textureAssets, skeletonAssets);
      }
    }

    private static void SetUpAndCreate (string name, TextAsset atlasAssets, 
                                        Texture textureAssets, TextAsset skeletonAssets)
    {

      SetUp (name, atlasAssets, textureAssets, skeletonAssets);
      OnResReady (name);

    }

    private static void OnResReady (string name)
    {

      // 将游戏对象创建请求放入队列中，在 Update 方法中完成创建
      requireQueue.Enqueue (name);
    }

    private static void SetUp (string name, TextAsset atlasAssets, 
                               Texture textureAssets, TextAsset skeletonAssets)
    {

      //Debug.Log ("SetUp");

      // 内容加载成功

      TextReader reader = null;

      // 分析 Atlas 数据
      // read and parse texture atlas josn into TextureAtlas
      if (atlasAssets != null) {
        reader = new StringReader (atlasAssets.text);
        Dictionary<string, System.Object> atlasRawData = Json.Deserialize (reader) as Dictionary<string, System.Object>;
        AtlasData atlasData = AtlasDataParser.ParseAtlasData (atlasRawData);
        if (!Cache.atlasDataTable.ContainsKey (name)) {
          Cache.atlasDataTable.Add (name, atlasData);
        } else {
          Debug.LogWarning ("DragonBones atlas data table already contains " + name);
        }
      }

      if (textureAssets != null) {
        if (!Cache.textureTable.ContainsKey (name)) {
          Cache.textureTable.Add (name, textureAssets);
        } else {
          Debug.LogWarning ("DragonBones texture table already contains " + name);
        }
      }

      // 分析 skeleton 数据
      // read and parse skeleton josn into SkeletonData
      if (skeletonAssets != null) {
        reader = new StringReader (skeletonAssets.text);
        Dictionary<string, System.Object> skeletonRawData = Json.Deserialize (reader) as Dictionary<string, System.Object>;
        SkeletonData skeletonData = ObjectDataParser.ParseSkeletonData (skeletonRawData);
        if (!Cache.skeletonDataTable.ContainsKey (name)) {
          Cache.skeletonDataTable.Add (name, skeletonData);
        } else {
          Debug.LogWarning ("DragonBones skeleton data table already contains " + name);
        }
      }

    }

    /// <summary>
    /// 获取一个游戏对象－没有被使用的
    /// </summary>
    /// <param name="name">Name.</param>
    public static GameObject FetchUnused (string name)
    {

      if (Cache.gobjTable.ContainsKey (name)) {
        foreach (GameObject gobj in Cache.gobjTable[name]) {
          if (gobj != null && !gobj.activeSelf) {
            gobj.SetActive (true);
            return gobj;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// Add the specified gobj.
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    public static void AddToCache (GameObject gobj)
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
        // 从对象表中删除
        if (Cache.gobjTable.ContainsKey (name)) {
          Cache.gobjTable [name].Remove (gobj);
          // 如果该资源对应的游戏对象都已经删除了，则清空相应的资源
          if (Cache.gobjTable [name].Count == 0) {
            Cache.gobjTable.Remove (name);
            Cache.textureTable.Remove (name);
          }
        }
        // 销毁对象
        GameObject.Destroy (gobj);
        // 销毁资源包
        Tang.AssetBundleLoader.Unload (name, all);
      }
    }

    /// <summary>
    /// 有多少个游戏对象可用
    /// </summary>
    /// <param name="name">对象名称</param>
    public static int SizeOfUnused (string name)
    {
      if (Cache.gobjTable.ContainsKey (name)) {
        int size = 0;
        List<GameObject> list = Cache.gobjTable [name];
        foreach (GameObject o in list) {
          if (!o.activeSelf)
            size++;
        }
        return size;
      }
      return 0;
    }

    /// <summary>
    /// 有多少个 GameObject
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOf (string name)
    {
      if (Cache.gobjTable.ContainsKey (name)) {
        List<GameObject> list = Cache.gobjTable [name];
        return Cache.gobjTable [name].Count;
      }
      return 0;
    }

    public static void Clear(){
      Cache.gobjTable.Clear ();
    }

    #endregion
  }
}

