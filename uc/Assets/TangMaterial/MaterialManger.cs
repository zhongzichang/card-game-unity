using System;
using UnityEngine;

namespace TangMaterial
{
  public class MaterialManger
  {

    /// <summary>
    /// 获取一个没有被使用的材质
    /// </summary>
    /// <returns>The unused.</returns>
    /// <param name="name">Name.</param>
    public static Material FetchUnused(string name){
      return null;
    }

    /// <summary>
    /// 获取一个共享材质
    /// </summary>
    /// <returns>The shared.</returns>
    /// <param name="name">Name.</param>
    public static Material FetchShared(string name){
      return null;
    }


    /// <summary>
    /// 释放材质
    /// </summary>
    /// <param name="mat">Mat.</param>
    public static void Release(Material mat){

    }

    /// <summary>
    /// 释放材质
    /// </summary>
    /// <param name="name">Name.</param>
    public static void Release(string name){

    }

  }
}

