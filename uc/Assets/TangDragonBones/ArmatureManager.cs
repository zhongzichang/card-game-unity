using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Com.Viperstudio.Utils;
using DragonBones;
using DragonBones.Animation;
using DragonBones.Display;
using DragonBones.Factorys;
using DragonBones.Objects;
using DragonBones.Textures;

namespace TangDragonBones
{
  public class ArmatureManager : MonoBehaviour
  {
    public delegate void ResEventHandler (object sender,ResEventArgs a);

    /// <summary>
    /// 需要预加载的骨架资源
    /// </summary>
    public LoadPair[] preloadArmatures;

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

          // 创建一个Armature对象
          Armature armature = Create (name);
          if (armature != null)
            AddToCache (armature);

          Debug.Log ("add armature .....");

          // 发出通知事件，游戏对象已经准备完毕
          if (RaiseLoadedEvent != null)
            RaiseLoadedEvent (null, new ResEventArgs (name));

        } else {

          Debug.LogError ("DragonBones res " + name + " not ready.");

        }
      }

      // 更新世界时钟
      WorldClock.Clock.AdvanceTime (Time.deltaTime);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// 从 AssetBundle 加载资源后回调该方法
    /// </summary>
    /// <param name="ab">Ab.</param>
    private static void OnAbLoaded (AssetBundle ab)
    {
      //Debug.Log ("OnAbLoaded");
      if (ab != null) {

        TextAsset atlasAssets = null;
        Texture textureAssets = null;
        TextAsset skeletonAssets = null;
        string name = ab.name;

        if (!Cache.atlasDataTable.ContainsKey (ab.name)) {
          atlasAssets = ab.Load (ResUtil.AtlasName (name), typeof(TextAsset)) as TextAsset;
        }
        if (!Cache.textureTable.ContainsKey (ab.name)) {
          string textureFilepath = ab.name + "_texture";
          textureAssets = ab.Load (ResUtil.TextureName (name), typeof(Texture)) as Texture;
        }
        if (!Cache.skeletonDataTable.ContainsKey (name)) {
          skeletonAssets = ab.Load (ResUtil.SkeletonName (name), typeof(TextAsset)) as TextAsset;
        }

        SetUp (name, atlasAssets, textureAssets, skeletonAssets);
        OnResReady (name);
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
        atlasAssets = Resources.Load (ResUtil.AtlasPath (name), typeof(TextAsset)) as TextAsset;
      }
      if (!Cache.textureTable.ContainsKey (name)) {
        textureAssets = Resources.Load (ResUtil.TexturePath (name), typeof(Texture)) as Texture;
      }
      if (!Cache.skeletonDataTable.ContainsKey (name)) {
        skeletonAssets = Resources.Load (ResUtil.SkeletonPath (name), typeof(TextAsset)) as TextAsset;
      }

      SetUp (name, atlasAssets, textureAssets, skeletonAssets);
      OnResReady (name);

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
          string atlasFilepath = ResUtil.AtlasPath (name);
          atlasAssets = Resources.Load (atlasFilepath, typeof(TextAsset)) as TextAsset;
          Debug.Log (atlasFilepath);
        }
        if (!Cache.skeletonDataTable.ContainsKey (name)) {
          string skeletonFilepath = ResUtil.SkeletonPath (name);
          skeletonAssets = Resources.Load (skeletonFilepath, typeof(TextAsset)) as TextAsset;
          Debug.Log (skeletonFilepath);
        }

        if (pair.includeTexture && !Cache.textureTable.ContainsKey (name)) {
          string textureFilepath = ResUtil.TexturePath (name);
          textureAssets = Resources.Load (textureFilepath, typeof(Texture)) as Texture;
          Debug.Log (textureFilepath);
        }

        SetUp (name, atlasAssets, textureAssets, skeletonAssets);
      }
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
        Dictionary<string, System.Object> atlasRawData = 
          Json.Deserialize (reader) as Dictionary<string, System.Object>;
        AtlasData atlasData = AtlasDataParser.ParseAtlasData (atlasRawData);
        if (!Cache.atlasDataTable.ContainsKey (name)) {
          Cache.atlasDataTable.Add (name, atlasData);
        } else {
          Debug.LogWarning ("DragonBones atlas data table already contains " + name);
        }
      }

      // 缓存纹理资源
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
        Dictionary<string, System.Object> skeletonRawData = 
          Json.Deserialize (reader) as Dictionary<string, System.Object>;
        SkeletonData skeletonData = ObjectDataParser.ParseSkeletonData (skeletonRawData);

        if (!Cache.skeletonDataTable.ContainsKey (name)) {
          Cache.skeletonDataTable.Add (name, skeletonData);
        } else {
          Debug.LogWarning ("DragonBones skeleton data table already contains " + name);
        }
      }

    }

    /// <summary>
    /// 创建一个游戏对象
    /// </summary>
    /// <param name="name">Name.</param>
    private Armature Create (string name)
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
      if (armature != null) {

        GameObject gobj = Gobj (armature);
        if (gobj != null)
          gobj.SetActive (false);

      }
      return armature;

    }

    /// <summary>
    /// 加入缓存
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    private static void AddToCache (Armature armature)
    {
      if (Cache.armatureTable.ContainsKey (armature.Name)) {
        Cache.armatureTable [armature.Name].Add (armature);
      } else {
        List<Armature> list = new List<Armature> ();
        list.Add (armature);
        Cache.armatureTable.Add (armature.Name, list);
      }
    }

    /// <summary>
    ///   从缓存中删除
    /// </summary>
    /// <param name="armature">Armature.</param>
    private static void RemoveFromCache (Armature armature)
    {
      string name = armature.Name;
      if (Cache.armatureTable.ContainsKey (name)) {

        Cache.armatureTable [name].Remove (armature);

        // 如果该资源对应的对象都已经删除了，则清空相应的资源
        if (Cache.armatureTable [name].Count == 0) {
          Cache.armatureTable.Remove (name);
          Cache.textureTable.Remove (name);
        }
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 加载骨架对象
    /// </summary>
    /// <param name="name">Name.</param>
    public static void LazyLoad (string name)
    {

      if (Cache.atlasDataTable.ContainsKey (name) &&
          Cache.textureTable.ContainsKey (name) &&
          Cache.skeletonDataTable.ContainsKey (name)) {

        // 数据和纹理资源已经被缓存
        OnResReady (name);

      } else if (Config.use_packed_res) {

        // 从 AssetBundle 资源包下载
        Tang.AssetBundleLoader.LoadAsync (name, OnAbLoaded);

      } else {

        // 从 Resources 文件夹中读取
        ResourceLoad (name);
      }
    }

    /// <summary>
    /// 加载多个骨架对象
    /// </summary>
    /// <param name="hero">Hero.</param>
    /// <param name="count">Count.</param>
    public static void LazyLoad (string name, int count)
    {
      for (int i = 0; i < count; i++) {
        LazyLoad (name);
      }
    }

    /// <summary>
    /// 获取一个骨架－没有被使用的
    /// </summary>
    /// <param name="name">Name.</param>
    public static Armature FetchUnused (string name)
    {

      if (Cache.armatureTable.ContainsKey (name)) {
        foreach (Armature armature in Cache.armatureTable[name]) {
          GameObject gobj = Gobj (armature);
          if (gobj != null && !gobj.activeSelf) {
            return armature;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// 释放所占用的资源
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="all">是否删除无用的资源</param>
    public static void Release (Armature armature, bool all)
    {
      string name = armature.Name;

      GameObject gobj = Gobj (armature);
      if (gobj != null) {
        gobj.SetActive (false);
      }

      // 删除无用的资源
      if (all) {

        RemoveFromCache (armature);

        // 销毁对象
        GameObject.Destroy (gobj);
        // 销毁资源包
        Tang.AssetBundleLoader.Unload (name, all);
      }
    }

    /// <summary>
    /// 获取 armature 里面的游戏对象
    /// </summary>
    /// <returns>The gobj.</returns>
    /// <param name="armature">Armature.</param>
    public static GameObject Gobj (Armature armature)
    {
      return (armature.Display as UnityArmatureDisplay).Display;
    }

    /// <summary>
    /// 有多少个骨架对象没有被使用的
    /// </summary>
    /// <param name="name">对象名称</param>
    public static int SizeOfUnused (string name)
    {
      if (Cache.armatureTable.ContainsKey (name)) {
        int size = 0;
        List<Armature> list = Cache.armatureTable [name];
        foreach (Armature a in list) {
          GameObject o = Gobj (a);
          if (o != null && !o.activeSelf)
            size++;
        }
        return size;
      }
      return 0;
    }

    /// <summary>
    /// 总共有多少个骨架
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="name">Name.</param>
    public static int SizeOfAll (string name)
    {
      if (Cache.armatureTable.ContainsKey (name)) {
        int size = 0;
        List<Armature> list = Cache.armatureTable [name];
        foreach (Armature a in list) {
          GameObject o = Gobj (a);
          if (o != null)
            size++;
        }
        return size;
      }
      return 0;
    }
  }
  #endregion
}

