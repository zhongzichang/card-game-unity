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
    /// 用于升级使用
    public UISprite levelUpSprite;

    public override void Start (){
			expFullLabel.text = UIPanelLang.BATTLE_FULL_EXP;
			expFullLabel.gameObject.SetActive(false);
      levelUpSprite.gameObject.SetActive(false);
			this.started = true;
      UpdateData();
		}

    public override void UpdateData(){
      if(!this.started){return;}
      if(this.data == null){return;}
      BattleResultHeroData battleResultHeroData = this.data as BattleResultHeroData;

      levelUpSprite.gameObject.SetActive(battleResultHeroData.levelUp);
      expLabel.text = "[cfa654]EXP+[-][f7e4c4]" + battleResultHeroData.exp + "[-]";
      levelLabel.text = battleResultHeroData.level.ToString();

      float percent = battleResultHeroData.exp / battleResultHeroData.maxExp;
      expFullLabel.gameObject.SetActive(percent >= 1);
      if(percent < 1){//换经验条名称
        expBar.spriteName = "resultExpBar";
      }else{
        expBar.spriteName = "resultExpFullBar";
      }
      expBar.fillAmount = percent;
    }
    
    public override void OnDestroy (){
      base.OnDestroy ();
    }

	}
}
