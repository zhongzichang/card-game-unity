using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{

	/// 关卡缓存
	public class LevelCache {
		private static LevelCache mInstance;
		
		public static LevelCache instance {
			get{
				if(null == mInstance){
					mInstance = new  LevelCache();
				}
				return mInstance;	
			}
		}


		/// 关卡
		public Dictionary<int, Level> levels = new Dictionary<int, Level>();
    /// 地图关卡关联
    private Dictionary<int, List<Level>> mapLevels = new Dictionary<int, List<Level>>();

    /// 添加关卡配置数据
    public void AddLevelData(LevelData data){
      Level level = new Level();
      level.data = data;
      levels[data.id] = level;
      List<Level> list = null;
      if(!mapLevels.ContainsKey(data.map_id)){
        list = new List<Level>();
        mapLevels[data.map_id] = list;
      }else{
        list = mapLevels[data.map_id];
      }
      list.Add(level);
    }

    /// 添加地图中的关卡列表
    public List<Level> GetMapLevels(int mapId){
      if(mapLevels.ContainsKey(mapId)){
        return mapLevels[mapId];
      }
      return null;
    }
		
    /// 获取怪物数据
    public MonsterData GetMonsterData(int id){
      if(Config.monsterXmlTable.ContainsKey(id)){
        return Config.monsterXmlTable[id];
      }
      return null;
    }

    /// 获取关卡数据
    public Level GetLevel(int id){
      if(levels.ContainsKey(id)){
        return levels[id];
      }
      return null;
    }

    /// 获取地图数据
    public MapData GetMapData(int id){
      if(Config.mapXmlTable.ContainsKey(id)){
        return Config.mapXmlTable[id];
      }
      return null;
    }
	}
}