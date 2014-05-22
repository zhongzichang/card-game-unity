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
	}
}