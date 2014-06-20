using UnityEngine;
using System.Collections;
using TangGame;

namespace TangGame.UI{
	/// <summary>
	/// 用于显示简单道具的Item，主要显示图标，边框
	/// </summary>
	public class SimpleHeroItem : ViewItem {

		public UISprite frame;
		public UISprite icon;
    public UISprite star;

		public override void Start (){
			this.started = true;
			UpdateData();
		}

		public override void UpdateData(){
			if(!this.started){return;}
			if(this.data == null){return;}
      HeroBase hero = this.data as HeroBase;
			
      frame.spriteName = Global.GetHeroIconFrame(hero.Net.rank);
      star.width = Global.GetHeroStar(hero.Net.star) * (int)star.localSize.x;
      icon.spriteName = hero.Xml.avatar;
		}

		public override void OnDestroy (){
			base.OnDestroy ();
		}

	}
}