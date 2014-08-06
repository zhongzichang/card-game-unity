using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 显示道具简单信息，并显示道具名称
  /// </summary>
  public class SimplePropsNameItem : ViewItem {
    public UISprite frame;
    public UISprite icon;
    public UILabel nameLabel;
    
    public override void Start (){
      this.started = true;
      UpdateData();
    }
    
    public override void UpdateData(){
      if(!this.started){return;}
      if(this.data == null){return;}
      Props props = this.data as Props;
      if(nameLabel != null){
        nameLabel.text = props.data.name + " X" + props.net.count.ToString();
      }
      
      frame.spriteName = Global.GetPropFrameName((PropsType)props.data.type, props.data.rank);
      icon.spriteName = props.data.Icon;
    }
    
    public override void OnDestroy (){
      base.OnDestroy ();
    }
  }
}
