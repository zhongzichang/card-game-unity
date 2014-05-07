using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame{
	
	/// 用于战斗结果界面显示英雄获得经验等相关信息
	public class BattleResultHeroItem : ViewItem {

		/// 经验条
		public UISprite expBar;
		/// 品质框
		public UISprite frame;
		/// 头像图标
		public UISprite icon;
		/// 满经验显示的文本
		public UILabel expFullLabel;
		/// 经验文本，显示获得的经验
		public UILabel expLabel;
		/// 用于显示等级的文本
		public UILabel levelLabel;

		void Start () {
			expLabel.text = "EXP+20";
			expFullLabel.text = UIPanelLang.BATTLE_FULL_EXP;
			expFullLabel.gameObject.SetActive(false);

			expBar.fillAmount = 0.3f;
		}

	}
}
