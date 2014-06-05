using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 邮件面板
  /// </summary>
  public class TaskPanel : ViewPanel {
    public const string NAME = "TaskPanel";

    public UILabel titleLabel;
    public TaskItem taskItem;
    public UIScrollView scrollView;
    public UIEventListener closeBtn;

    private object mParam;
    private bool started;
    private List<TaskItem> items = new List<TaskItem>();

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
      titleLabel.text = UIPanelLang.MAILBOX;
      this.started = true;
      this.taskItem.gameObject.SetActive(false);

      TaskCache.instance.list.Add(new Task());
      TaskCache.instance.list.Add(new Task());
      TaskCache.instance.list.Add(new Task());
      TaskCache.instance.list.Add(new Task());
      TaskCache.instance.list.Add(new Task());
      TaskCache.instance.list.Add(new Task());
      this.UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value; UpdateData();}
    }


    private void UpdateData(){
      if(!this.started){return;}
      foreach(TaskItem item in items){
        GameObject.Destroy(item);
      }
      items.Clear();

      GameObject go = null;
      Vector3 position = taskItem.transform.localPosition;
      foreach(Task task in TaskCache.instance.list){
        go = UIUtils.Duplicate(taskItem.gameObject, taskItem.transform.parent.gameObject);
        go.transform.localPosition = position;
        TaskItem item = go.GetComponent<TaskItem>();
        items.Add(item);
        item.data = task;
        position.y = position.y - 134;
      }
    }

    private void CloseBtnClickHandler(GameObject go){

    }

    private void ItemClickHandler(ViewItem viewItem){

    }
  }
}