using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI{
	/// 道具详情面板的获得途径块显示的Item
	public class PropsDetailsGetItem : ViewItem {

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
      LevelData levelsData = this.data as LevelData;
      icon.spriteName = levelsData.icon;
      label.text = levelsData.name;
    }
	}
}