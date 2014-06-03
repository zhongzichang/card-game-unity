using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class ResourceDuplItem : ViewItem {
    /// 点击处理
    public ViewItemDelegate onClick;
    public UISprite icon;
    public UISprite background;
    public UILabel label;

    /// 用于存储名称
    private string dName;

    public override void Start (){
      dName = icon.spriteName;
      UIEventListener.Get(background.gameObject).onClick += BtnClickHandler;
      this.started = true;
      UpdateData();
    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        onClick(this);
      }
    }

    public override void UpdateData (){
      if(!this.started){return;}
      if(this.data == null){return;}
      Level level = this.data as Level;
      if(level.data.team_lv <= Account.instance.level){
        icon.spriteName = dName;
        icon.MakePixelPerfect();
        background.color = new Color32(255, 255, 255, 255);
      }else{
        icon.spriteName = "notOpen";
        icon.MakePixelPerfect();
        background.color = new Color32(0, 255, 255, 255);
      }
    }
  }
}

