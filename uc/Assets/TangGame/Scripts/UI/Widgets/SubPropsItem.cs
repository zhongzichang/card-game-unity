using UnityEngine;
using System.Collections;
using TangGame.Xml;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class SubPropsItem : PropsItem
	{
		public void Flush (PropsXml xml, int count)
		{
			data = new PropsBase ();
			data.Xml = xml;
			this.Flush (data, count);
		}

		public void Flush (PropsBase data, int count)
		{
			this.data = data;
			if (data != null) {
				this.propsIconSprite.spriteName = data.Xml.icon;
				this.frameSprite.spriteName = string.Format ("equip_frame_{0}", HeroBase.GetRankColorStr ((HeroesRankEnum)data.Xml.upgrade));
				if (data.Net != null)
					this.propsCountLabel.text = (data.Net.count + count).ToString ();
				else
					this.propsCountLabel.text = "[FF0000]0[-]/" + count;
			}

		}
	}
}