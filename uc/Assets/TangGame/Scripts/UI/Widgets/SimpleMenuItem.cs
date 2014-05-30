using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  /// <summary>
  /// 简单的菜单按钮项
  /// </summary>
  public class SimpleMenuItem : ViewItem {

    public ViewItemDelegate onClick;

    public GameObject mask;
    public UILabel label;
    public UIEventListener button;

    public override void Start (){
      if(button != null){
        button.onClick += ButtonClickHandler;
      }
      if(mask != null){
        mask.SetActive(false);
      }
      this.started = true;
      UpdateData();
    }

    public override void UpdateData (){

    }

    public override void UpdateSelected (){
      if(this.mask != null){
        this.mask.SetActive(this.selected);
      }
    }

    private void ButtonClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }

  }
}