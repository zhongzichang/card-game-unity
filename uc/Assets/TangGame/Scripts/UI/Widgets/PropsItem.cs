using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI
{
	public class PropsItem : MonoBehaviour
	{
		public UISprite frameSprite;
		public UILabel propsCountLabel;
		public UISprite propsIconSprite;
		public PropsBase data;
		private bool showCount = true;

		public bool ShowCount {
			get {
				return showCount;
			}
			set {
				showCount = value;
			}
		}

		// Use this for initialization
		void Start ()
		{

		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		/// <summary>
		/// Flush the specified data.
		/// 设置item
		/// </summary>
		/// <param name="data">Data.</param>
		public void Flush(PropsBase data){
			this.data = data;
			if(data != null){
				this.propsIconSprite.spriteName = data.Xml.icon;
				this.frameSprite.spriteName = string.Format ("equip_frame_{0}", HeroBase.GetRankColorStr((RankEnum)data.Xml.upgrade));
				if (propsCountLabel != null && showCount) {
					if(data.Net != null)
						this.propsCountLabel.text = data.Net.count.ToString();
				}
			}
		}


	}
}