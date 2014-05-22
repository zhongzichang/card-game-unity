using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	public class PropsTipsPanel : MonoBehaviour {

		/// 效果文本和简介文本之间的间距
		private const int SPACE = 10;
		/// 基本高度，不包含效果和简介文本
		private const int BASE_HEIGHT = 102;

		public UISprite background;
		public UISprite frame;
		public UISprite icon;
		public UILabel nameLabel;
		public UILabel levelLabel;
		public UILabel effectLabel;
		public UILabel descLabel;
		public UILabel goldLabel;

		/// TIPS高度，包含基本高度和文本高度
		private int mHeight = 0;

		// Use this for initialization
		void Start () {
			CalculateHeight();
		}

		public int height{
			get{return this.mHeight;}
		}


		public void CalculateHeight(){
			mHeight = BASE_HEIGHT;
			int effectLabelHeight = 0;
			int space = 0;
			if(!string.IsNullOrEmpty(effectLabel.text)){
				effectLabelHeight = effectLabel.height;
				mHeight += effectLabelHeight;
				space += SPACE;
			}

			if(!string.IsNullOrEmpty(descLabel.text)){
				mHeight += descLabel.height + space;//20为2个文本之间的距离
				int y = (int)effectLabel.gameObject.transform.localPosition.y - effectLabelHeight - space;
				Vector3 temp = descLabel.gameObject.transform.localPosition;
				temp.y = y;
				descLabel.gameObject.transform.localPosition = temp;
			}
			background.height = mHeight;
			this.gameObject.transform.localPosition = new Vector3(-165, mHeight / 2, 0);
		}

		/// 设置道具
		public void SetProp(Props props){
			this.effectLabel.text = "";
			this.descLabel.text = "";
			this.goldLabel.text = "";
			this.frame.spriteName = Global.GetPropFrameName((PropsType)props.data.type, props.data.upgrade);
			this.icon.spriteName = props.data.icon;
			this.goldLabel.text = props.data.selling_price.ToString();
			this.nameLabel.text = props.data.name;
			if(props.data.level < 1){
				this.levelLabel.text = "";
			}else{
				this.levelLabel.text = string.Format(UIPanelLang.TIPS_LEVEL, props.data.level);
			}
			this.effectLabel.text = props.data.info;
			this.descLabel.text = props.data.description;

			this.CalculateHeight();
		}
	}
}