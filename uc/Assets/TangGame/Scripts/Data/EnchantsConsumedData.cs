
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class EnchantsConsumedData
	{ 
		/// 品质
		public int upgrade;
		/// 每级消耗金币数组
		public string exp_spend;
		/// 每点消耗金币
		public int gold_spend;
		/// 每点消耗砖石
		public int gem_spend;
	}
}