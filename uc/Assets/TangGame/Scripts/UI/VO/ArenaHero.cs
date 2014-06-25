using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场英雄数据对象
  /// </summary>
  public class ArenaHero  {
    /// 英雄的ID
    private int mId;
    /// 英雄数据对象
    public HeroBase heroBase = null;
    /// 选中上阵
    public bool isSelected;
    /// 排序使用
    public int sortOrder;

    public int id{
      get{return mId;}
      set{this.mId = value;UpdateHero();}
    }

    public void UpdateHero(){
      if(this.heroBase == null){
        this.heroBase = HeroCache.instance.GetHeroByID(id);
        if(this.heroBase == null){
          Global.LogWarning(">> ArenaHero heroBase is null, id = " + id);
        }
      }
      if(Config.heroSortTable.ContainsKey(id)){
        sortOrder = Config.heroSortTable[id];
      }
    }

    public int GetHeroLocation(){
      if(this.heroBase != null){
        return this.heroBase.Xml.location;
      }
      return -1;
    }


  }
}