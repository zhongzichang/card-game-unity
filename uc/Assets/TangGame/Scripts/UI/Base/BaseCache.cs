/// <summary>
///   Config for UI
/// </summary>
using System.Collections.Generic;
namespace TangGame.UI
{
	public class BaseCache
	{
		/// <summary>
		/// 英雄数据数组
		/// </summary>
		public static Dictionary<int,HeroBase> heroBeseTable = new Dictionary<int, HeroBase>();
		/// <summary>
		/// 道具数据数组
		/// </summary>
		public static Dictionary<int,PropsBase> propsBaseTable = new Dictionary<int, PropsBase>();

		/// 获取英雄数据
		public static HeroBase GetHero(int id){
			if(heroBeseTable.ContainsKey(id)){
				return heroBeseTable[id];
			}
			return null;
		}

	}
}