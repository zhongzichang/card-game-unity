using UnityEngine;
using System.Collections;
using TangGame;

namespace TangGame.UI{
	/// <summary>
	/// 用于显示道具信息的Item，比如战斗结束获得的道具，副本调用的道具等
	/// </summary>
	public class PropsInfoItem : ViewItem {
		public UISprite frame;
		public UISprite icon;
		public UILabel numLabel;
		public UIEventListener frameBtn;

		public override void Start (){
			frameBtn.onPress += FrameBtnHandler;
			this.started = true;
			UpdateData();
		}

		public override void UpdateData(){
			if(!this.started){return;}
			if(this.data == null){return;}
			Props props = this.data as Props;
			if(props.count < 1){
				numLabel.text = "";
			}else{
        numLabel.text = props.count.ToString();
			}
			frame.spriteName = Global.GetPropFrameName((PropsType)props.data.type, props.data.upgrade);
			icon.spriteName = props.data.icon;
		}

		public override void OnDestroy (){
			base.OnDestroy ();
			frameBtn.onPress -= FrameBtnHandler;
		}

		private void FrameBtnHandler(GameObject go, bool state){
			if(this.data == null){return;}
			if(state){
				Props prop = this.data as Props;
				PropTips.Show(go.transform.position, 43, prop);
			}else{
				PropTips.Hiddle();
			}
		}

	}
}