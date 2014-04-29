/**
 * LevelManager
 * 
 * Author: zzc
 * Date: 2014/4/26
 */

using System;

namespace TangLevel
{
  

  public class LevelManager
  {

    /// <summary>
    ///   通知进入场景是否成功
    /// </summary>
    public event EventHandler RaiseEnterResult;

    /// <summary>
    ///   进入场景
    /// </summary>
    public static void Enter(int id)
    {
      // 判断是否能进入场景
      // 如果可以，加载子场景
      // 否则，发出进入场景失败消息
    }

    /// <summary>
    ///   离开场景
    /// </summary>
    public static void Exit()
    {
      
    }
  }

}