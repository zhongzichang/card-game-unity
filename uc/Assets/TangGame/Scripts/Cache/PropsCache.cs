using System.Collections;
using System.Collections.Generic;

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

		/// 道具管理数据，存储道具掉落所在的场景等
		public Dictionary<int, PropsRelationData> propsRelations = new Dictionary<int, PropsRelationData>();

		/// 获取道具关联数据
		public PropsRelationData GetPropsRelationData(int id){
			if(propsRelations.ContainsKey(id)){
				return propsRelations[id];
			}
			return null;
		}
	}
}