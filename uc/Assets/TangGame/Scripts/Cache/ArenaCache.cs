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
    /// 拥有的英雄列表
    public List<ArenaHero> ownList = new List<ArenaHero>();

    /// 拥有的英雄列表
    public List<ArenaHero> GetOwnList(){

      if(ownList.Count < 1){
        ArenaHero hero = new ArenaHero();
        hero.id = 14;
        ownList.Add(hero);
        
        hero = new ArenaHero();
        hero.id = 27;
        ownList.Add(hero);
        
        hero = new ArenaHero();
        hero.id = 20;
        ownList.Add(hero);
        
        hero = new ArenaHero();
        hero.id = 23;
        ownList.Add(hero);

        
        //ownList.Sort(SortOrder);
      }
      return ownList;
    }

    /// ArenaAdjustHeroItem的排序
    private int SortOrder(ArenaHero item1, ArenaHero item2){
      return item1.sortOrder.CompareTo(item2.sortOrder);
    }

  }
}