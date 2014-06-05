using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  public class DailyCache  {
    
    private static DailyCache mInstance;
    
    public static DailyCache instance {
      get{
        if(null == mInstance){
          mInstance = new  DailyCache();
        }
        return mInstance; 
      }
    }

    public List<Daily> list = new List<Daily>();

  }
}