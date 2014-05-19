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
		public UISprite MinusBtn;
		public Props data;
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
		void OnEnable(){
			MinusBtn.gameObject.SetActive (false);
		}

		/// <summary>
		/// Flush the specified data.
		/// 设置item
		/// </summary>
		/// <param name="data">Data.</param>
		public void Flush (Props data)
		{
			this.data = data;
			if (data != null) {
				this.propsIconSprite.spriteName = data.data.icon;
				this.frameSprite.spriteName = Global.GetPropFrameName(data.data.upgrade);
				if (propsCountLabel != null && showCount) {
					if (data.net != null)
						this.propsCountLabel.text = data.net.count.ToString ();
				}
			}
		}
	}
}