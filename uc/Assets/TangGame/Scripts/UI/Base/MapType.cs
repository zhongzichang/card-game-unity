using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 地图类型，标示是否为普通，精英等
  /// </summary>
  public enum MapType {
    /// 普通
    None = 0,
    /// 精英
    Normal = 1,
    /// 精英
    Elite,
    /// 经验副本
    Exp,
    /// 金钱副本
    Money,
    /// 道具副本1
    Props1,
    /// 道具副本2
    Props2,
    /// 道具副本3
    Props3,
  }
}