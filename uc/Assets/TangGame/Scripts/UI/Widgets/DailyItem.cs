using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	/// 日常项
	public class DailyItem : ViewItem {
    public ViewItemDelegate onClick;

    public UILabel conditionLabel;
    public UISprite icon;
    public UILabel titleLabel;
    public UIEventListener btn;
    public GameObject btnGo;
    public UILabel countLabel;
    
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
      Daily daily = this.data as Daily;
      if(daily.data == null){
        Global.Log(">> DailyItem daily.xml == null.");
        return;
      }
      this.conditionLabel.text = daily.data.condition;
      this.titleLabel.text = daily.data.name;
    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
	}
}