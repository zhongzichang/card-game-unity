using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 任务奖励面板
  /// </summary>
  public class TaskAwardPanel : ViewPanel {
    public const string NAME = "TaskAwardPanel";

    public UISprite background;
    public UILabel titleLabel;
    public UIEventListener okBtn;
    public UISprite frame;

    public UILabel expLabel;
    public UILabel ingotLabel;
    public UILabel moneyLabel;
    public UILabel vitalityLabel;
    public SimplePropsItem simplePropsItem;

    private object mParam;

    void Start(){
      okBtn.onClick += OkBtnClickHandler;
      this.started = true;
      UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;UpdateData();}
    }

    private void UpdateData(){
      if(!this.started){return;}
      TaskAwardPanelData taskAwardPanelData = this.mParam as TaskAwardPanelData;

      int exp = 0;
      int moeny = 0;
      int ingot = 0;
      int vitality = 0; 

      if(taskAwardPanelData.task != null){
        titleLabel.text = string.Format(UIPanelLang.TASK_CPMPLETED, taskAwardPanelData.task.data.name);
        exp = taskAwardPanelData.task.data.exp;
        moeny = taskAwardPanelData.task.data.moeny;
        ingot = taskAwardPanelData.task.data.ingot;
        vitality = taskAwardPanelData.task.data.vitality;
      }else if(taskAwardPanelData.daily != null){
        titleLabel.text = string.Format(UIPanelLang.TASK_CPMPLETED, taskAwardPanelData.daily.data.name);
        exp = taskAwardPanelData.daily.data.exp;
        moeny = taskAwardPanelData.daily.data.moeny;
        ingot = taskAwardPanelData.daily.data.ingot;
        vitality = taskAwardPanelData.daily.data.vitality;
      }


      int y = -32;
      if(exp > 0){
        expLabel.text = "X" + exp;
        expLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetY(expLabel.transform.parent.gameObject, y);
        y -= 40;
      }else{
        expLabel.transform.parent.gameObject.SetActive(false);
      }
      
      if(moeny > 0){
        moneyLabel.text = "X" + moeny;
        moneyLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetY(moneyLabel.transform.parent.gameObject, y);
        y -= 40;
      }else{
        moneyLabel.transform.parent.gameObject.SetActive(false);
      }
      
      if(ingot > 0){
        ingotLabel.text = "X" + ingot;
        ingotLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetY(ingotLabel.transform.parent.gameObject, y);
        y -= 40;
      }else{
        ingotLabel.transform.parent.gameObject.SetActive(false);
      }
      
      if(vitality > 0){
        vitalityLabel.text = "X" + vitality;
        vitalityLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetY(vitalityLabel.transform.parent.gameObject, y);
        y -= 40;
      }else{
        vitalityLabel.transform.parent.gameObject.SetActive(false);
      }

      int height = Mathf.Abs(y);
      frame.height = height;

      Vector3 temp = okBtn.transform.localPosition;
      temp.y = -126 - height; 
      okBtn.transform.localPosition = temp;

      background.height = height + 126 + 50;
    }

    private void OkBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }

  }
}