using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	/// 邮件
	public class MailItem : ViewItem {
    public ViewItemDelegate onClick;

    public UIEventListener btn;
    public UILabel titleLabel;
    public UILabel sendLabel;
    public UILabel timeLabel;

    public override void Start ()
    {
      btn.onClick += BtnClickHandler;
      this.started = true;
      UpdateData ();
    }
    
    public override void UpdateData ()
    {
      if (!this.started) {
        return;
      }
      if(this.data == null){return;}
      Mail mail = this.data as Mail;
      titleLabel.text = mail.title;
      sendLabel.text = string.Format(UIPanelLang.MAIL_SENDER, mail.sender);
    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
	}
}