// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Tang
{
  public class AssetBundleCache
  {

    private Dictionary<string,AssetBundle> table = new Dictionary<string,AssetBundle> ();

    public AssetBundleCache ()
    {
    }

    public bool Contains (string name)
    {
      return table.ContainsKey(name);
    }

    public bool Contains (AssetBundle ab)
    {
      return table.ContainsKey(ab.name);
    }



    public void Add (AssetBundle ab)
    {
      table.Add (ab.name, ab);
    }

    public void Remove(string name){
      table.Remove (name);
    }

    public AssetBundle this [string name]
    {
      get{
        return table[name];
      } 
      set{
        table[name] = value;
      }
    }
  }
}

