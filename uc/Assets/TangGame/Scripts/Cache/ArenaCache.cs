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
        hero.isSelected = true;
        ownList.Add(hero);
        
        hero = new ArenaHero();
        hero.id = 20;
        ownList.Add(hero);
        
        hero = new ArenaHero();
        hero.id = 23;
        hero.isSelected = true;
        ownList.Add(hero);

        
        //ownList.Sort(SortOrder);
      }
      return ownList;
    }

    /// ArenaAdjustHeroItem的排序
    public int SortOrder(ArenaHero item1, ArenaHero item2){
      return item1.sortOrder.CompareTo(item2.sortOrder);
    }

    /// ArenaAdjustHeroItem的排序降序
    public int SortOrderByDescending(ArenaHero item1, ArenaHero item2){
      return item2.sortOrder.CompareTo(item1.sortOrder);
    }
  }
}