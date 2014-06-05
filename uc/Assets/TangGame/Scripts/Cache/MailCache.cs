using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  public class MailCache  {
    
    private static MailCache mInstance;
    
    public static MailCache instance {
      get{
        if(null == mInstance){
          mInstance = new  MailCache();
        }
        return mInstance; 
      }
    }

    public List<Mail> list = new List<Mail>();

    /// List排序使用
    private int SortLevel(Mail a1, Mail a2){
      return a1.time.CompareTo(a2.time);
    }
  }
}