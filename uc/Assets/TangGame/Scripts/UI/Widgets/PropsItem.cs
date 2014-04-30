using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class PropsItem : MonoBehaviour
	{
		public UISprite frameSprite;
		public UILabel propsCountLabel;
		public UISprite propsIconSprite;
		public PropsBase data;
		private BagPanel bagPanel;
		// Use this for initialization
		void Start ()
		{
			if(bagPanel == null)
				bagPanel = NGUITools.FindInParents<BagPanel> (this.gameObject);
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		void Flush(PropsBase data){
			this.data = data;
			if(data != null){
				this.propsCountLabel.text = data.Count.ToString();
				this.propsIconSprite.spriteName = data.Xml.icon;
				this.frameSprite.spriteName = string.Format ("frame_{0}", data.Xml.upgrade);
			}

		}

		void OnClick ()
		{
			if (bagPanel != null && data != null) {
				bagPanel.UpBagPropsInfoSubPanel (data);
			}
		}
	}
}