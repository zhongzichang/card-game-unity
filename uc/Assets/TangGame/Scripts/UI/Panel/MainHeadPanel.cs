using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 邮件面板
  /// </summary>
  public class MainHeadPanel : ViewPanel {
    public const string NAME = "MainHeadPanel";

    public UIEventListener headBtn;

    private object mParam;

    void Start(){
      headBtn.onClick += HeadBtnClickHandler;
      this.started = true;
      this.UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value; UpdateData();}
    }


    private void UpdateData(){
      if(!this.started){return;}
    }

    private void HeadBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(RolePanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, null);
    }
  }
}