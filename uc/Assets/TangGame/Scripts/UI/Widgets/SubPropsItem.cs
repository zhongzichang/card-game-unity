using UnityEngine;
using System.Collections;
using TangGame.Xml;
using TangGame.UI;

namespace TangGame.UI
{
	public class SubPropsItem : PropsItem
	{
		public void Flush (PropsData xml, int count)
		{
			data = new Props ();
			data.data = xml;
			this.Flush (data, count);
		}

		public void Flush (Props data, int count)
		{
			this.data = data;
			if (data != null) {
        this.propsIconSprite.spriteName = data.data.icon;
				this.frameSprite.spriteName = Global.GetPropFrameName(data.data.upgrade);
				if (data.net != null)
					this.propsCountLabel.text = (data.net.count + count).ToString ();
				else
					this.propsCountLabel.text = "[FF0000]0[-]/" + count;
			}

		}
	}
}