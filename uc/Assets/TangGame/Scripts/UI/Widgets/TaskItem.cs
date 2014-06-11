using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
	/// 任务项
	public class TaskItem : ViewItem {
    public ViewItemDelegate onClick;

    public UILabel conditionLabel;
    public UISprite icon;
    public UILabel titleLabel;


    public UILabel expLabel;
    public UILabel moenyLabel;
    public UILabel ingotLabel;
    public UILabel vitalityLabel;
    public SimplePropsItem props;
    private List<GameObject> propsList = new List<GameObject>();

    
    public override void Start ()
    {
      this.started = true;
      props.gameObject.SetActive(false);
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

      int x = -216;
      Vector3 temp = Vector3.zero;
      if(task.data.exp > 0){
        expLabel.text = "X" + task.data.exp;
        expLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(expLabel.transform.parent.gameObject, x);
        x += 98 + 7 + expLabel.width;
      }else{
        expLabel.transform.parent.gameObject.SetActive(false);
      }
      
      if(task.data.moeny > 0){
        moenyLabel.text = "X" + task.data.moeny;
        moenyLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(moenyLabel.transform.parent.gameObject, x);
        x += 45 + 7 + moenyLabel.width;
      }else{
        moenyLabel.transform.parent.gameObject.SetActive(false);
      }
      
      if(task.data.ingot > 0){
        ingotLabel.text = "X" + task.data.ingot;
        ingotLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(ingotLabel.transform.parent.gameObject, x);
        x += 35 + 7 + ingotLabel.width;
      }else{
        ingotLabel.transform.parent.gameObject.SetActive(false);
      }
      
      if(task.data.vitality > 0){
        vitalityLabel.text = "X" + task.data.vitality;
        vitalityLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(vitalityLabel.transform.parent.gameObject, x);
        x += 45 + 7 + ingotLabel.width;
      }else{
        vitalityLabel.transform.parent.gameObject.SetActive(false);
      }
      
      foreach(GameObject go in propsList){
        GameObject.Destroy(go);
      }
      
      GameObject tempGo = UIUtils.Duplicate(props.gameObject, props.transform.parent.gameObject);
      tempGo.transform.localPosition = props.transform.localPosition;
      UIUtils.SetX(tempGo, x);
      x += 94 + 7;

    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
	}
}