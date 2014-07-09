using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame.UI{
  public class MainStatusPanel : ViewPanel {

    public UILabel moneyLabel;
    public UILabel ingotLabel;
    public UILabel vitalityLabel;

    public UIEventListener moneyBtn;
    public UIEventListener ingotBtn;
    public UIEventListener vitalityBtn;
    public UIEventListener vitalityAddBtn;


    public UIPanel vitalityGroup;
    public TweenScale tween;
    public UILabel vitalityInfoLabel;
    public UISprite background;

    private int renderQueueIndex = 10000;

    void Start(){
      renderQueueIndex = Global.GetTipsPanelRenderQueueIndex();
      vitalityGroup.renderQueue = UIPanel.RenderQueue.StartAt;
      vitalityGroup.startingRenderQueue = renderQueueIndex;
      vitalityGroup.gameObject.SetActive(false);
      moneyBtn.onClick += MoneyBtnClickHandler;
      ingotBtn.onClick += IngotBtnClickHandler;
      vitalityBtn.onPress += VitalityBtnClickHandler;
      vitalityAddBtn.onClick += VitalityAddBtnClickHandler;
      tween.eventReceiver = this.gameObject;
      tween.callWhenFinished = "TweenFinishedHandler";
    
    }

    private void MoneyBtnClickHandler(GameObject go){
      TangGame.UIContext.mgrCoC.LazyOpen(BuyMoneyPanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.NONE);
    }

    private void IngotBtnClickHandler(GameObject go){
      
    }

    private void VitalityBtnClickHandler(GameObject go, bool state){
      if(state){
        vitalityGroup.gameObject.SetActive(true);
        SetVitalityInfo("当前时间：12:10:10\n已买体力次数：0/1\n体力已恢复满\n体力已恢复满\n体力已恢复满");
        tween.PlayForward();
      }else{
        tween.PlayReverse();
      }
    }

    private void VitalityAddBtnClickHandler(GameObject go){
      
    }

    private void TweenFinishedHandler(UITweener tweener){
      if(vitalityGroup.transform.localScale.x < 0.01f){
        vitalityGroup.gameObject.SetActive(false);
      }
    }

    public void SetVitalityInfo(string info){
      vitalityInfoLabel.text = info;
      int height = vitalityInfoLabel.height + 30;      
      background.height = height;
      UIUtils.SetY(vitalityGroup.gameObject, -(100 + height / 2));
    }


  }

}