using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class ResourceDuplItem : ViewItem {
    /// 点击处理
    public ViewItemDelegate onClick;
    public UISprite icon;
    public UISprite notOpenIcon;
    public UILabel label;

    public override void Start (){
      UIEventListener.Get(icon.gameObject).onClick += BtnClickHandler;
      UIEventListener.Get(notOpenIcon.gameObject).onClick += BtnClickHandler;
      icon.gameObject.SetActive(false);
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
        icon.gameObject.SetActive(true);
        notOpenIcon.gameObject.SetActive(false);
      }
    }
  }
}

