using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{

	/// 道具数据缓存
	public class PropsCache {
		
		private static PropsCache mInstance;
		
		public static PropsCache instance {
			get{
				if(null == mInstance){
					mInstance = new  PropsCache();
				}
				return mInstance;	
			}
		}

    /// 道具数据数组
    public Dictionary<int, Props> propsTable = new Dictionary<int, Props>();

		/// 道具管理数据，存储道具掉落所在的场景等
		private Dictionary<int, PropsRelationData> propsRelations = new Dictionary<int, PropsRelationData>();

    /// 获取道具数据
    public PropsData GetPropsData(int id){
      if(Config.propsXmlTable.ContainsKey(id)){
        return Config.propsXmlTable[id];
      }
      return null;
    }

		/// 获取道具关联数据
		public PropsRelationData GetPropsRelationData(int id){
			if(propsRelations.ContainsKey(id)){
				return propsRelations[id];
			}
			return null;
		}

		/// 设置道具碎片合成的关联
		public void AddPropsSyntheticsRelation(int id, PropsData data){
      PropsRelationData propsRelationData = null;
      if(!propsRelations.ContainsKey(id)){
        propsRelationData = new PropsRelationData();
        propsRelationData.id = id;
        propsRelations[id] = propsRelationData;
      }else{
        propsRelationData = propsRelations[id];
      }
      propsRelationData.synthetics.Add(data);
		}

    /// 设置英雄可装备道具的关联
    public void AddPropsHeroRelation(HeroData data){
      if(string.IsNullOrEmpty(data.equip_id_list)){return;}
      ArrayList equipStrList = Utils.SplitStrByBraces(data.equip_id_list);
      int count = 1;
      foreach(string equipStr in equipStrList){
        int[] equipIds = Utils.SplitStrByCommaToInt(equipStr);
        foreach(int id in equipIds){
          PropsHeroEquipData propsHeroEquipData = new PropsHeroEquipData();
          propsHeroEquipData.id = id;
          propsHeroEquipData.data = data;
          propsHeroEquipData.rank = count;
          AddPropsHeroEquipData(id, propsHeroEquipData);
        }
        count++;
      }
    }

    /// 设置英雄可装备道具的关联
    private void AddPropsHeroEquipData(int id, PropsHeroEquipData data){
      PropsRelationData propsRelationData = null;
      if(!propsRelations.ContainsKey(id)){
        propsRelationData = new PropsRelationData();
        propsRelationData.id = id;
        propsRelations[id] = propsRelationData;
      }else{
        propsRelationData = propsRelations[id];
      }
      propsRelationData.AddPropsHeroEquipData(data);
    }

    /// 设置道具关卡掉落关联
    public void AddPropsLevelRelation(LevelData data){
      string[] propsIds = data.props_ids.Replace("{", "").Replace("}", "").Split(',');
      foreach(string id in propsIds){
        if(string.IsNullOrEmpty(id)){
          continue;
        }
        AddPropsLevelData(int.Parse(id), data);
      }
    }

    /// 设置道具关卡掉落关联
    private void AddPropsLevelData(int id, LevelData data){
      PropsRelationData propsRelationData = null;
      if(!propsRelations.ContainsKey(id)){
        propsRelationData = new PropsRelationData();
        propsRelationData.id = id;
        propsRelations[id] = propsRelationData;
      }else{
        propsRelationData = propsRelations[id];
      }

      if(!propsRelationData.levels.Contains(data)){
        propsRelationData.levels.Add(data);
      }
    }

	}
}