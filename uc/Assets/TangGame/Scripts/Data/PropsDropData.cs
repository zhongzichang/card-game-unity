using System.Collections.Generic;

namespace TangGame.UI{
	/// 道具掉落配置，根据关卡配置表中的解析而来
	public class PropsDropData {
		/// 道具ID
		public int id;
		/// 掉落该物品的关卡列表
		public List<object> levels = new List<object>();
		
	}
}