using System;
using System.Collections.Generic;

namespace TangUtils
{
  public class CollectionUtil
  {
    /// <summary>
    /// 将 Dictionary 所有的键转化为数组
    /// </summary>
    /// <returns>The to array.</returns>
    /// <param name="dic">Dic.</param>
    /// <typeparam name="K">The 1st type parameter.</typeparam>
    /// <typeparam name="V">The 2nd type parameter.</typeparam>
    public static K[] KeysToArray<K,V>(Dictionary<K,V> dic){

      return KeysToList(dic).ToArray ();
    }

    /// <summary>
    /// 将 Dictionary 所有的键转化为列表
    /// </summary>
    /// <returns>The to list.</returns>
    /// <param name="dic">Dic.</param>
    /// <typeparam name="K">The 1st type parameter.</typeparam>
    /// <typeparam name="V">The 2nd type parameter.</typeparam>
    public static List<K> KeysToList<K,V>(Dictionary<K,V> dic){
      List<K> list = new List<K> ();
      foreach (K k in dic.Keys) {
        list.Add (k);
      }
      return list;
    }
  }
}

