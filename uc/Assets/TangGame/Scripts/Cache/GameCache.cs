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

    /// 游戏的字符串版本号
    public string version;
    /// 游戏的数字版本号
    public int versionCode;

  }
}