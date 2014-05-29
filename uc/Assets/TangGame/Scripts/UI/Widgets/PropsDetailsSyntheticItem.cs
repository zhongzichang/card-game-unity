using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI{
	/// 道具详情面板的可合成块显示的Item
	public class PropsDetailsSyntheticItem : ViewItem {

    public UISprite frame;
    public UISprite icon;
    public UILabel label;

    public override void Start ()
    {
      this.started = true;
      UpdateData ();
    }
    
    public override void UpdateData ()
    {
      if (!this.started) {
        return;
      }
      if(this.data == null){return;}
      PropsData propsData = this.data as PropsData;
      frame.spriteName = Global.GetPropFrameName((PropsType)propsData.type, propsData.rank);
      icon.spriteName = propsData.icon;
      label.text = propsData.name;
    }
	}
}