using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  public class TaskCache  {
    
    private static TaskCache mInstance;
    
    public static TaskCache instance {
      get{
        if(null == mInstance){
          mInstance = new  TaskCache();
        }
        return mInstance; 
      }
    }

    public List<Task> list = new List<Task>();

  }
}