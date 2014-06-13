using UnityEngine;
using System.Collections;
using TangGame;

namespace TangGame.UI{
	/// <summary>
	/// 用于显示简单道具的Item，主要显示图标，边框
	/// </summary>
	public class SimplePropsItem : ViewItem {
		public UISprite frame;
		public UISprite icon;
		public UILabel numLabel;

		public override void Start (){
			this.started = true;
			UpdateData();
		}

		public override void UpdateData(){
			if(!this.started){return;}
			if(this.data == null){return;}
			Props props = this.data as Props;
			if(numLabel != null){
				if(props.net.count < 2){
					numLabel.text = "";
				}else{
					numLabel.text = props.net.count.ToString();
				}
			}
			
			frame.spriteName = Global.GetPropFrameName((PropsType)props.data.type, props.data.rank);
			icon.spriteName = props.data.Icon;
		}

		public override void OnDestroy (){
			base.OnDestroy ();
		}

	}
}