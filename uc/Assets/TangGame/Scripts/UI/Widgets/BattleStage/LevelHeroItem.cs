﻿using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame{
  public class LevelHeroItem : ViewItem {
		
		public UISprite hpBar;
		public UISprite hpYellow;
		public UISprite mpBar;
		public UISprite frame;
		public UISprite icon;
		public UISprite star;
		public UIEventListener frameBtn;
		/// 点击分派的事件
		public ViewItemDelegate onClick;
    public UISprite hpEffect;
    public UISprite mpEffect;
    public UISprite hpBarEffect;

		private int mHeroId;
		private int hp = 1;
		private int maxHp = 1;
		private int mp;
		private int maxMp;

		/// 用于HP的减少动画
		private float hpFillAmount;
		/// 用于MP的减少动画
		private float mpFillAmount;
		/// 用于动画的速度
		private float speed = 3;
    /// 用于显示MP特效标示
    private bool showMpEffect = true;

		public override void Start (){
			hpFillAmount = hpYellow.fillAmount = hpBar.fillAmount = 1;
			mpFillAmount = mpBar.fillAmount = 0;
			frameBtn.onClick += FrameBtnClickHandler;
      hpEffect.gameObject.SetActive(false);
      mpEffect.gameObject.SetActive(false);
      hpBarEffect.gameObject.SetActive(false);
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

		/// 英雄的ID
		public int heroId{
			get{return this.mHeroId;}
		}

		/// 设置英雄的ID，用于显示英雄的头像和品质边框
		public void SetHeroId(int id){
			this.mHeroId = id;
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
			if(this.hp < 1){//该处添加判读是用于设置HP和MP顺序问题
				this.mp = 0;
			}
			UpdateMp();
		}

		/// 更新英雄
		private void UpdateHero(){
      HeroBase hero = HeroCache.instance.GetMyHeroByConfigID(this.mHeroId);
			if(hero == null){return;}
			star.width = Global.GetHeroStar(hero.Net.star) * 20;
			icon.spriteName = hero.Xml.avatar;
      frame.spriteName = Global.GetHeroIconFrame(hero.Net.rank);
		}

		/// 更新英雄HP
		private void UpdateHp(){
			float percent = (float)this.hp / (float)this.maxHp;
			if(hpBar.fillAmount < percent){
				hpYellow.fillAmount = percent;
			}
			hpFillAmount = percent;
			hpBar.fillAmount = percent;

      if(percent > 0.2f){
        this.hpEffect.gameObject.SetActive(false);
        this.hpBarEffect.gameObject.SetActive(false);
      }else{
        this.hpEffect.gameObject.SetActive(true);
        this.hpBarEffect.gameObject.SetActive(true);
      }

			if(this.hp < 1){//死亡了，设置MP为0
				this.mp = 0;
				UpdateMp();
				icon.color = new Color32(51, 51, 51, 255);//死亡设置灰色
        this.hpEffect.gameObject.SetActive(false);
        this.mpEffect.gameObject.SetActive(false);
        this.hpBarEffect.gameObject.SetActive(false);
			}
		}

		/// 更新英雄Mp
		private void UpdateMp(){
			float percent = (float)this.mp / (float)this.maxMp;
			if(mpBar.fillAmount < percent){
				mpBar.fillAmount = percent;
			}
			mpFillAmount = percent;
			if(percent >= 1){//可以释放技能了
        this.mpEffect.gameObject.SetActive(true && showMpEffect);
      }else{
        this.mpEffect.gameObject.SetActive(false);
      }
		}

    /// MP的特效开关
    public void SwitchMpEffect(bool showMpEffect){
      this.showMpEffect = showMpEffect;
      this.mpEffect.gameObject.SetActive(this.showMpEffect && (mpBar.fillAmount >= 1));
    }

		/// 点击
		private void FrameBtnClickHandler(GameObject go){
			if(onClick != null){
				onClick(this);
			}
		}
	}
}