using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{
  public class NameCache  {
    
    private static NameCache mInstance;
    
    public static NameCache instance {
      get{
        if(null == mInstance){
          mInstance = new  NameCache();
        }
        return mInstance; 
      }
    }

    private string[] name1 = new string[]{};
    private string[] name2 = new string[]{};

    /// 第一个和最后一个可能是空串
    public void Parse(NameData data){
      name1 = data.name1.Split('\n');
      name2 = data.name2.Split('\n');
    }

    /// 获取随机名称
    public string GetRandomName(){
      string result = "";
      if(name1.Length < 2 || name2.Length < 2){
        Global.LogError(">> NameCache name is null");
      }else{
        int temp1 = Random.Range(1, name1.Length - 1);
        int temp2 = Random.Range(1, name2.Length - 1);
        result =  name1[temp1] + name2[temp2];
      }
      return result;
    }

  }
}