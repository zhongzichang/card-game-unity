using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{

  /// 英雄相关
  public class HeroCache  {

    private static HeroCache mInstance;
    
    public static HeroCache instance {
      get{
        if(null == mInstance){
          mInstance = new HeroCache();
        }
        return mInstance; 
      }
    }

    /// <summary>
    /// 英雄数据数组
    /// </summary>
    public Dictionary<int, HeroBase> heroBeseTable = new Dictionary<int, HeroBase>();
    
    /// 获取英雄数据
    public HeroBase GetHero(int id){
      if(heroBeseTable.ContainsKey(id)){
        return heroBeseTable[id];
      }
      return null;
    }

    /// 灵魂石ID与英雄的关联
    private Dictionary<int, HeroData> soulStoneRelations = new Dictionary<int, HeroData>();
    /// <summary>
    /// 添加灵魂石关联
    /// </summary>
    /// <param name="id">灵魂石ID</param>
    /// <param name="data">英雄数据</param>
    public void AddSoulStoneRelation(HeroData data){
      if(!soulStoneRelations.ContainsKey(data.soul_rock_id)){
        soulStoneRelations[data.soul_rock_id] = data;
      }
    }

    /// 获取灵魂石关联的英雄数据
    public HeroData GetSoulStoneRelation(int id){
      if(soulStoneRelations.ContainsKey(id)){
        return soulStoneRelations[id];
      }
      return null;
    }

    /// 获取英雄数据
    public HeroData GetHeroData(int id){
      if(Config.heroXmlTable.ContainsKey(id)){
        return Config.heroXmlTable[id];
      }
      return null;
    }

  }
}