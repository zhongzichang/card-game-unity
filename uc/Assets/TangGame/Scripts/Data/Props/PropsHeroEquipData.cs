using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{
	/// 英雄装备道具对应的数据对象，主要有进阶对应
	public class PropsHeroEquipData {
		/// 英雄ID
		public HeroData data;
    /// 道具ID
    public int id;
		/// 英雄阶数
		public int rank;
	}
}