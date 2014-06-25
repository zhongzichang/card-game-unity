using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 游戏数据存储，比如版本号，登陆相关，服务器列表相关
  /// </summary>
  public class GameCache {

    private static GameCache mInstance;
    
    public static GameCache instance {
      get {
        if (null == mInstance) {
          mInstance = new GameCache ();
        }
        return mInstance; 
      }
    }

    /// 服务器列表链接地址
    public string serverListUrl = "http://192.168.1.100/serverList.txt";
    /// 游戏的字符串版本号
    public string version;
    /// 游戏的数字版本号
    public int versionCode;
    /// 游戏的网管地址
    public string netWorkUrl = "";
    /// 用于标示预下载资源是否下载完成
    public bool isLoadCompleted = false;
  }
}