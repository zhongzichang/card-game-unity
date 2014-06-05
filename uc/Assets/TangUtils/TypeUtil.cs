using System;

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
          ret [i] = int.Parse(splits [i]);
        }
        return ret;
      }
      return null;
    }
  }
}

