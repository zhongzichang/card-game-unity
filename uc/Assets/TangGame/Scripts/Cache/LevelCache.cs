using System.Collections;
using System.Collections.Generic;

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

		
	}
}