using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 邮件详情面板
  /// </summary>
  public class MailDetailsPanel : ViewPanel {
    public const string NAME = "MailDetailsPanel";

    public UILabel titleLabel;
    public UILabel contentLabel;
    public UILabel sendLabel;
    public UILabel btnLabel;
    public UILabel attachmentLabel;
    public UIEventListener btn;


    private object mParam;
    private bool started;

    void Start(){
      btn.onClick += BtnClickHandler;
      btnLabel.text = UIPanelLang.MAIL_RECEIVE;
      attachmentLabel.text = UIPanelLang.MAIL_ATTACHMENT;
      this.started = true;
      this.UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value; UpdateData();}
    }


    private void UpdateData(){
      if(!this.started){return;}
      if(this.mParam == null){return;}
      Mail mail = this.mParam as Mail;


    }

    private void BtnClickHandler(GameObject go){

    }

  }
}