using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	/// 道具详情面板的英雄可装备块显示的Item
	public class PropsDetailsHeroItem : ViewItem {
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
      PropsHeroEquipData propsHeroEquipData = this.data as PropsHeroEquipData;
      frame.spriteName = Global.GetHeroIconFrame(propsHeroEquipData.rank);
      icon.spriteName = propsHeroEquipData.data.avatar;
      int index = Global.GetHerorankRem(propsHeroEquipData.rank);
      string addStr = "";
      if(index > 0){
        addStr = "[" + Global.GetHerorankHexColor(propsHeroEquipData.rank) + "]+" + Global.GetHerorankRem(propsHeroEquipData.rank) + "[-]";
      }
      label.text = "[FEDEBB]" + propsHeroEquipData.data.name + "[-]" + addStr;
    }
	}
}