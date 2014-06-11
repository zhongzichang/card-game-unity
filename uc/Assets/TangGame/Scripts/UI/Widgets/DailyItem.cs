using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public UILabel btnLabel;

    public UILabel expLabel;
    public UILabel moenyLabel;
    public UILabel ingotLabel;
    public UILabel vitalityLabel;
    public SimplePropsItem props;
    private List<GameObject> propsList = new List<GameObject>();


    public override void Start ()
    {
      btn.onClick += BtnClickHandler;
      this.started = true;
      props.gameObject.SetActive(false);
      btnLabel.text = UIPanelLang.GOTO;
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
      if(daily.data.count == 0){
        this.countLabel.text = "";
      }else{
        this.countLabel.text = "0/" + daily.data.count;
      }

      btnGo.SetActive(daily.data.id != 1 && daily.data.id != 2);

      int x = -216;
      Vector3 temp = Vector3.zero;
      if(daily.data.exp > 0){
        expLabel.text = "X" + daily.data.exp;
        expLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(expLabel.transform.parent.gameObject, x);
        x += 98 + 7 + expLabel.width;
      }else{
        expLabel.transform.parent.gameObject.SetActive(false);
      }

      if(daily.data.moeny > 0){
        moenyLabel.text = "X" + daily.data.moeny;
        moenyLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(moenyLabel.transform.parent.gameObject, x);
        x += 45 + 7 + moenyLabel.width;
      }else{
        moenyLabel.transform.parent.gameObject.SetActive(false);
      }

      if(daily.data.ingot > 0){
        ingotLabel.text = "X" + daily.data.ingot;
        ingotLabel.transform.parent.gameObject.SetActive(true);
        UIUtils.SetX(ingotLabel.transform.parent.gameObject, x);
        x += 35 + 7 + ingotLabel.width;
      }else{
        ingotLabel.transform.parent.gameObject.SetActive(false);
      }

      if(daily.data.vitality > 0){
        vitalityLabel.text = "X" + daily.data.vitality;
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

      if(string.IsNullOrEmpty(daily.data.award)){
        /*
        tempGo = UIUtils.Duplicate(props.gameObject, props.transform.parent.gameObject);
        tempGo.transform.localPosition = props.transform.localPosition;
        UIUtils.SetX(tempGo, x);
        x += 94 + 7;
        */
      }

    }

    private void BtnClickHandler(GameObject go){
      if(this.onClick != null){
        this.onClick(this);
      }
    }
	}
}