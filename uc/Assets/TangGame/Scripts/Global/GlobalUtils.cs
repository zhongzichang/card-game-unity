using UnityEngine;
using System.Collections;

public class GlobalUtils {

  /// <summary>
  /// 转换成金钱格式，即每3位一个逗号，无小数点处理
  /// </summary>
  /// <returns>The format.</returns>
  /// <param name="str">String.</param>
  public static string MoneyFormat(string str){
    int length = str.Length;
    int total = length / 3;
    string result = "";
    string temp = "";
    int count = 0;
    temp = str.Substring(count, length - total * 3);
    count += length - total * 3;
    result += temp;
    for(int i = 0; i < total; i++){
      temp = str.Substring(count, 3);
      if(count == 0){
        result += temp;
      }else{
        result += "," + temp;
      }
      count += 3;
    }
    return result;
  }

}
