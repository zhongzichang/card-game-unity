using System;
using System.Collections;
using UnityEngine;

namespace TangUtils
{
  public class TypeUtil
  {
    /// <summary>
    /// 将字符串转换成整形数组
    /// </summary>
    /// <returns>The to int array</returns>
    /// <param name="text">Text.</param>
    /// <param name="seperator">Seperator.</param>
    public static int[] StringToIntArray (string text, char seperator)
    {
      string[] splits = text.Split (new char[]{ seperator });
      if (splits != null && splits.Length > 0) {
        int[] ret = new int[splits.Length];
        for (int i = 0; i < splits.Length; i++) {
          ret [i] = int.Parse (splits [i]);
        }
        return ret;
      }
      return null;
    }

    public static ArrayList DoubleToInt(ArrayList list){
      ArrayList nlist = new ArrayList ();
      foreach (object o in list) {
        nlist.Add ( Convert.ToInt32(o) );
      }
      return nlist;
    }

    /// <summary>
    /// 将对象数组转换成
    /// </summary>
    /// <returns>The int array.</returns>
    /// <param name="objs">Objects.</param>
    public static T[] ToArray<S, T> ( S list) where S : System.Collections.IList
    {
      T[] array = new T[list.Count];
      for (int i = 0; i < array.Length; i++) {
        array [i] = (T)list [i];
      }
      return array;
    }
  }
}

