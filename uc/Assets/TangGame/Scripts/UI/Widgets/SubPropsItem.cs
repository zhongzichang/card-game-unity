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
        this.propsIconSprite.spriteName = data.data.Icon;
				this.frameSprite.spriteName = Global.GetPropFrameName(data.data.rank);
				if (data.net != null) {
					string str;
					if (data.net.count < count) {
						str = "[FF0000]" + data.net.count + "[-]/" + count;
					} else {
						str = data.net.count + "/" + count;
					}
					this.propsCountLabel.text = str;

				}
				else
					this.propsCountLabel.text = "[FF0000]0[-]/" + count;
			}

		}
	}
}