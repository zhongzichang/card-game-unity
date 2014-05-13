using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{
	/// 道具关联，道具掉落配置和英雄可装备配置，根据关卡配置表中的解析而来
	public class PropsRelationData {
		/// 道具ID
		public int id;
		/// 可掉落该物品的关卡列表
		public List<LevelsData> levels = new List<LevelsData>();
		/// 可装备该道具的英雄列表
		public List<PropsHeroEquipData> heros = new List<PropsHeroEquipData>();
		/// 可合成需要该道具的道具列表
		public List<PropsXml> synthetics = new List<PropsXml>();
	}
}