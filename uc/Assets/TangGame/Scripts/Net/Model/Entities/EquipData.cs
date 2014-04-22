using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.Net
{
	public class EquipData
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		public long id;
		/// <summary>
		/// 装备的配置表id
		/// </summary>
		public int configId;
		/// <summary>
		/// 装备的附魔等级
		/// </summary>
		public int level;
		/// <summary>
		/// 装备的附魔经验
		/// </summary>
		public long exp;
	}
}