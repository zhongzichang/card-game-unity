using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.Net
{
	public class HeroNet
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		public long id;
		/// <summary>
		/// The config identifier.
		/// </summary>
		public int configId;
		/// <summary>
		/// 英雄等级
		/// </summary>
		public int level;
		/// <summary>
		/// 英雄经验
		/// </summary>
		public long exp;
		/// <summary>
		/// 英雄品质
		/// </summary>
		public short upgrade;
		/// <summary>
		/// 英雄星级
		/// </summary>
		public short evolve;
		/// <summary>
		/// The equip.
		/// </summary>
		public Dictionary<int,EquipData> equip = new Dictionary<int,EquipData>();

	}
}