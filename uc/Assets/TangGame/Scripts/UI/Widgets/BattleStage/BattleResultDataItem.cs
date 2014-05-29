using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI{
	/// 战斗结束界面的数据显示项
	public class BattleResultDataItem : ViewItem {
		public UISprite bar;
		public UILabel damageLabel;
		public UISprite frame;
		public UISprite icon;
		public UILabel levelLabel;
		public UISprite star;

		private float currDamage;
		private float damage;
		private float maxDamage;
		/// 数字显示速度，默认200
		private float step = 200;

		public override void Start (){
			base.Start ();
			UpdateData();
		}

		public override void UpdateData (){
			if(!this.started){return;}
			if(this.data == null){return;}

			BattleResultHeroData tempData = this.data as BattleResultHeroData;

			if(Config.heroXmlTable.ContainsKey(tempData.id)){
				HeroData hero = Config.heroXmlTable[tempData.id];
				icon.spriteName = hero.avatar;
			}

			levelLabel.text = tempData.level.ToString();
			frame.spriteName = Global.GetHeroIconFrame(tempData.rank);
			star.width = Global.GetHeroStar(tempData.evolve) * 20;
			currDamage = 0;
			damage = tempData.damage;
			maxDamage = tempData.maxDamage;
			if(maxDamage < damage){
				maxDamage = damage;
			}
			step = maxDamage / 1.5f;
		}

		void Update(){
			if(currDamage < damage){
				currDamage += step * Time.deltaTime;
				if(currDamage >= damage){
					currDamage = damage;
				}
				damageLabel.text = ((int)currDamage).ToString();
				bar.fillAmount = currDamage / maxDamage;
			}
		}
	}
}