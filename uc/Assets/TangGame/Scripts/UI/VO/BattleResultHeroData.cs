using System.Collections;
using UnityEngine;

namespace TangGame.UI{
	/// 战斗结果面板英雄数据
	public class BattleResultHeroData {
		/// ID
		public int id;
		/// 等级
		public int level = 1;
		/// 当前经验
		public int exp;
    /// 当前最大经验
    public int maxExp;
		/// 是否升级
		public bool levelUp;
		/// 输出伤害
		public int damage;
		/// 战斗中的最大输出伤害
		public int maxDamage;
		/// 品阶
		public int rank = 1;
		/// 星阶
		public int evolve = 1;

		public BattleResultHeroData(){
			level = Random.Range(1, 99);
			evolve = Random.Range(1, 5);
			damage = Random.Range(1, 5000);
		}

	}
}