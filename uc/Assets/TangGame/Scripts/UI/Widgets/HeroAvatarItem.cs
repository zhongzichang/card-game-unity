using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
using TangGame.Xml;

namespace TangGame.UI
{
	public class HeroAvatarItem : MonoBehaviour
	{
		/// <summary>
		/// 英雄icon图标
		/// </summary>
		public UISprite heroIcon;
		/// <summary>
		/// The hero frames.
		/// 英雄品质边框
		/// </summary>
		public UISprite heroFrames;
		/// <summary>
		/// The starlist.
		/// 英雄星星为零
		/// </summary>
		public StarList starlist;

		public HeroBase heroBase;


		/// <summary>
		/// Flush the specified herobase.
		/// 当已拥有此英雄就更新英雄星级以及品质
		/// </summary>
		/// <param name="herobase">Herobase.</param>
		public void Flush(HeroBase herobase){
			this.heroBase = herobase;
			this.heroIcon.spriteName = herobase.Xml.avatar;
			int upgrade = herobase.Net.upgrade;
			this.heroFrames.spriteName = Global.GetHeroIconFrame (upgrade);
			this.starlist.count = herobase.Net.evolve;
			this.starlist.Flush ();
		}
		/// <summary>
		/// Flush the specified xml.
		/// 没有英雄只更新图片
		/// </summary>
		/// <param name="xml">Xml.</param>
		public void Flush(HeroXml xml){
			this.heroIcon.spriteName = xml.avatar;
		}
	}
}