using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
using TangGame.Xml;

namespace TangGame.UI
{
	public class HeroAvatarItem : MonoBehaviour
	{
		public UISprite heroIcon;
		public UISprite heroFrames;
		public StarList starlist;

		public void Flush(HeroBase herobase){
			Flush (herobase.Xml);
			int rank = herobase.Net.upgrade;
//			this.heroFrames.spriteName = Global
		}
		public void Flush(HeroXml xml){
			this.heroIcon.spriteName = xml.avatar;
		}
	}
}