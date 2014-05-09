using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame{
	/// <summary>
	/// 用于显示道具信息的Item，比如战斗结束获得的道具，副本调用的道具等
	/// </summary>
	public class PropInfoItem : ViewItem {
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
			PropsBase prop = this.data as PropsBase;
			if(prop.Count < 1){
				numLabel.text = "";
			}else{
				numLabel.text = prop.Count.ToString();
			}
			frame.spriteName = Global.GetPropFrameName((PropsType)prop.Xml.type, prop.Xml.upgrade);
			icon.spriteName = prop.Xml.icon;
		}

		public override void OnDestroy (){
			base.OnDestroy ();
			frameBtn.onPress -= FrameBtnHandler;
		}

		private void FrameBtnHandler(GameObject go, bool state){
			if(this.data == null){return;}
			if(state){
				PropsBase prop = this.data as PropsBase;
				PropTips.Show(go.transform.position, 43, prop);
			}else{
				PropTips.Hiddle();
			}
		}

	}
}