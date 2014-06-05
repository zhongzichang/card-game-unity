﻿using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	/// 邮件
	public class MailItem : ViewItem {
    public ViewItemDelegate onClick;

    public UIEventListener btn;
    
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
    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
	}
}