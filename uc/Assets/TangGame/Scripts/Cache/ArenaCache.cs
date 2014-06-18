using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场
  /// </summary>
  public class ArenaCache  {
    
    private static ArenaCache mInstance;
    
    public static ArenaCache instance {
      get{
        if(null == mInstance){
          mInstance = new  ArenaCache();
        }
        return mInstance; 
      }
    }

    /// 选中上阵的英雄列表
    public List<ArenaHero> selectedList = new List<ArenaHero>();
    
  }
}