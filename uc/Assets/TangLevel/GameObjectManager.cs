using System;
using UnityEngine;

namespace TangLevel
{

  /// <summary>
  /// Game object manager.
  /// </summary>
  public class GameObjectManager
  {
    public GameObjectManager ()
    {
    }

    /// <summary>
    /// 获取一个游戏对象－没有被使用的
    /// </summary>
    /// <param name="name">Name.</param>
    public GameObject FetchUnused (string name)
    {
      return null;
    }

    /// <summary>
    /// 释放游戏对象所占用的资源
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="all">If set to <c>true</c> all.</param>
    public void Release (GameObject gobj, bool all)
    {

    }
  }
}

