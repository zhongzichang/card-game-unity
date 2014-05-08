using UnityEngine;
using System.Collections;
using TangGame.UI;
using TangGame.UI.Base;

namespace TangGame{
	public class BattleHeroItem : ViewItem {
		
		public UISprite hpBar;
		public UISprite hpYellow;
		public UISprite mpBar;
		public UISprite frame;
		public UISprite icon;
		public UIEventListener frameBtn;
		/// 点击分派的事件
		public ViewItemDelegate onClick;

		private int heroId;
		private int hp;
		private int maxHp;
		private int mp;
		private int maxMp;

		/// 用于HP的减少动画
		private float hpFillAmount;
		/// 用于MP的减少动画
		private float mpFillAmount;
		/// 用于动画的速度
		private float speed = 3;

		public override void Start (){
			hpFillAmount = hpYellow.fillAmount = hpBar.fillAmount = 1;
			mpFillAmount = mpBar.fillAmount = 0;
			frameBtn.onClick += FrameBtnClickHandler;
			this.started = true;
			UpdateHero();
			UpdateHp();
			UpdateMp();
		}

		void Update(){
			if(hpFillAmount != hpYellow.fillAmount){
				float temp = hpYellow.fillAmount - speed * Time.deltaTime;
				if(temp < hpFillAmount){
					temp = hpFillAmount;
				}
				hpYellow.fillAmount = temp;
			}
			if(mpFillAmount != mpBar.fillAmount){
				float temp = mpBar.fillAmount - speed * Time.deltaTime;
				if(temp < mpFillAmount){
					temp = mpFillAmount;
				}
				mpBar.fillAmount = temp;
			}
		}
		
		public override void OnDestroy (){
			base.OnDestroy ();
			frameBtn.onClick -= FrameBtnClickHandler;
			onClick = null;
		}

		/// 设置英雄的ID，用于显示英雄的头像和品质边框
		public void SetHeroId(int id){
			this.heroId = id;
			if(!this.started){return;}
			UpdateHero();
		}

		/// 更新HP
		public void SetHp(int hp, int maxHp){
			this.hp = hp;
			this.maxHp = maxHp;
			if(!this.started){return;}
			UpdateHp();
		}

		/// 更新MP
		public void SetMp(int mp, int maxMp){
			this.mp = mp;
			this.maxMp = maxMp;
			if(!this.started){return;}
			UpdateMp();
		}

		/// 更新英雄
		private void UpdateHero(){

		}

		/// 更新英雄HP
		private void UpdateHp(){
			float percent = (float)this.hp / (float)this.maxHp;
			if(hpBar.fillAmount < percent){
				hpYellow.fillAmount = percent;
			}
			hpFillAmount = percent;
			hpBar.fillAmount = percent;
		}

		/// 更新英雄Mp
		private void UpdateMp(){
			float percent = (float)this.mp / (float)this.maxMp;
			if(mpBar.fillAmount < percent){
				mpBar.fillAmount = percent;
			}
			mpFillAmount = percent;
			if(percent >= 1){//可以释放技能了
			}
		}

		/// 点击
		private void FrameBtnClickHandler(GameObject go){
			if(onClick != null){
				onClick(this);
			}
		}
	}
}