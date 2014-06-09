using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class SettingItem : ViewItem {

    public ViewItemDelegate onClick;

    public UILabel label;
 
    public GameObject mask;
    public UILabel btnLabel;
    public UIEventListener button;
    public UIEventListener maskButton;
    
    public override void Start (){
      if(button != null){
        button.onClick += ButtonClickHandler;
      }
      if(maskButton != null){
        maskButton.onClick += ButtonClickHandler;
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
      if(btnLabel != null){
        btnLabel.text = this.selected ? "开" : "关";
      }
    }
    
    private void ButtonClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
  }
}