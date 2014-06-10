using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	/// 任务项
	public class TaskItem : ViewItem {
    public ViewItemDelegate onClick;

    public UILabel conditionLabel;
    public UISprite icon;
    public UILabel titleLabel;

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
      Task task = this.data as Task;
      if(task.data == null){
        Global.Log(">> TaskItem task.xml == null.");
        return;
      }
      this.conditionLabel.text = task.data.condition;
      this.titleLabel.text = task.data.name;
    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
	}
}