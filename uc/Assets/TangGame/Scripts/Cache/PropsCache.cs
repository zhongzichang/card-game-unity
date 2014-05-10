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

		/// 道具掉落数据，存储道具掉落所在的场景中
		public Dictionary<int, PropsRelationData> propsRelations = new Dictionary<int, PropsRelationData>();

	}
}