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
		public int upgrade;
		/// <summary>
		/// 英雄星级
		/// </summary>
		public int evolve;
		/// <summary>
		/// 剩余的技能点
		/// </summary>
		public int skillCount;
		/// <summary>
		/// 最后一次升级技能的时间
		/// </summary>
		public long lastUpSkillTime;
		/// <summary>
		/// 英雄技能等级
		/// </summary>
		public int[] skillLevel;
		/// <summary>
		/// The equip.
		/// </summary>
		public EquipNet[] equipList;
	}
}