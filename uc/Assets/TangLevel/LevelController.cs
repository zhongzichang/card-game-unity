using System;
using UnityEngine;

namespace TangLevel
{
  /// <summary>
  /// Level controller.
  /// </summary>
  public class LevelController
  {
    /// <summary>
    /// Occurs when raise sub level loaded event.
    /// 子关卡加载完毕事件处理
    /// </summary>
    public static event EventHandler RaiseSubLevelLoadedEvent;

    private static int m_currentLevelId = 0;

    public static int CurrentLevelId {
      get {
        // 获取当前关卡ID
        return m_currentLevelId;
      }
      set {
        // 设置当前关卡
        if (Config.levelTable.ContainsKey (value)) {
          m_currentLevelId = value;
          LevelContext.CurrentLevel = Config.levelTable [m_currentLevelId];
        }
      }
    }

    /// <summary>
    /// 预加载指定关卡的资源
    /// </summary>
    /// <param name="levelId">Level identifier.</param>
    public static void LoadTargetSubLevelRes ()
    {

      SubLevel subLevel = LevelContext.TargetSubLevel;

      if (Cache.gobjTable.ContainsKey (subLevel.resName)) {
        // 资源已准备完毕
        OnSubLevelResReady ();
      } else if (Config.use_packed_res) {
        string name = LevelContext.CurrentSubLevel.resName;
        string path = Tang.ResourceUtils.GetAbFilePath (name);
        Tang.AssetBundleLoader.LoadAsync (name, OnSubLevelLoaded);
      } else {
        OnSubLevelLoaded (null);
      }

    }

    /// <summary>
    /// 资源加载后
    /// </summary>
    /// <param name="ab">Ab.</param>
    private static void OnSubLevelLoaded (AssetBundle ab)
    {

      Debug.Log ("OnSubLevelLoaded +");

      UnityEngine.Object assets = null;
      if (ab == null) {
        string filepath = Config.BATTLE_BG_PATH + Tang.Config.DIR_SEP + LevelContext.TargetSubLevel.resName;
        Debug.Log (filepath);
        assets = Resources.Load (filepath);
      } else
        assets = ab.Load (ab.name);

      if (assets != null) {
        GameObject gobj = GameObject.Instantiate (assets) as GameObject;
        gobj.SetActive (false);
        gobj.name = assets.name;
        GameObjectManager.Add (gobj);
        // 资源已准备完毕
        OnSubLevelResReady ();
      }

    }

    /// <summary>
    /// 发出子关卡资源已准备好的事件
    /// </summary>
    private static void OnSubLevelResReady ()
    {
      if (RaiseSubLevelLoadedEvent != null)
        RaiseSubLevelLoadedEvent (null, EventArgs.Empty);
    }

    /// <summary>
    /// 进入下一个子关卡
    /// </summary>
    public static void EnterNextSubLevel ()
    {

      // 创建背景
      GameObject bgGobj = GameObjectManager.FetchUnused (LevelContext.TargetSubLevel.resName);
      if (bgGobj != null)
        bgGobj.SetActive (true);

      // 创建人物

      LevelContext.InLevel = true;
    }

    /// <summary>
    /// Lefts the current level.
    /// </summary>
    public static void LeftLevel ()
    {

      // 发出离开关卡通知

      LevelContext.InLevel = false;
    }

    /// <summary>
    /// 暂停当前关卡
    /// </summary>
    public static void Pause ()
    {

    }

    /// <summary>
    /// 恢复当前关卡
    /// </summary>
    public static void Resume ()
    {
    }

    /// <summary>
    /// Determines if is last sub level.
    /// </summary>
    /// <returns><c>true</c> if is last sub level; otherwise, <c>false</c>.</returns>
    public static bool IsLastSubLevel ()
    {
      return false;
    }
  }
}

