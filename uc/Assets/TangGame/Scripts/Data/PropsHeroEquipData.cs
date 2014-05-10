using System.Collections.Generic;

namespace TangGame.UI{
	/// 英雄装备道具对应的数据对象，主要有进阶对应
	public class PropsHeroEquipData {
		/// 英雄ID
		public int id;
		/// 英雄进阶列表，包括哪个阶段
		public List<int> upgrades = new List<int>();
	}
}