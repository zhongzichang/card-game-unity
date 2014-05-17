using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
	/// 战斗结果面板类型，标示结果
	public enum BattleResultType{
    Lose = 0,
		Timeout,
		Win,
		Star1,
		Star2,
		Star3,
	}

	/// 战斗结果面板需要的数据对象
	public class BattleResultData {

		/// 结果类型
		public BattleResultType type = BattleResultType.Lose;
    /// 战队等级
    public int level;
    /// 战队获得经验
    public int exp;
    /// 获得金钱
    public int gold;

		/// 获得的道具
		public List<Props> propsList = new List<Props>();
		/// 英雄数据
		public List<BattleResultHeroData> herosList = new List<BattleResultHeroData>();
		/// 敌方英雄数据
		public List<BattleResultHeroData> enemysList = new List<BattleResultHeroData>();
	}
}