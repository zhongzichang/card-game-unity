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

    public UIPanel buyVitalityGroup;
    public TweenScale buyVitalityTween;
    public UILabel buyVitalityMsgLabel;
    public UIEventListener okBtn;
    public UIEventListener cancelBtn;
    public UILabel okBtnLabel;
    public UILabel cancelBtnLabel;

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

      //=====================
      buyVitalityGroup.gameObject.SetActive(false);
      okBtn.onClick += OkBtnClickHandler;
      cancelBtn.onClick += CancelBtnClickHandler;
      buyVitalityTween.eventReceiver = this.gameObject;
      buyVitalityTween.callWhenFinished = "BuyVitalityTweenFinishedHandler";
      UpdateDisplay();
    }

    private void MoneyBtnClickHandler(GameObject go){
      TangGame.UIContext.mgrCoC.LazyOpen(BuyMoneyPanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.NONE);
    }

    private void IngotBtnClickHandler(GameObject go){
      TangGame.UIContext.mgrCoC.LazyOpen(RechargePanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.NONE);
    }

    /// 体力的+按钮点击事件处理
    private void VitalityAddBtnClickHandler(GameObject go){
      buyVitalityGroup.gameObject.SetActive(true);
      buyVitalityTween.PlayForward();
    }

    /// 更新显示处理
    private void UpdateDisplay(){
      moneyLabel.text = GlobalUtils.MoneyFormat(Account.instance.money.ToString());
      ingotLabel.text = GlobalUtils.MoneyFormat(Account.instance.ingot.ToString());
      vitalityLabel.text = Account.instance.vitality + "/" + Account.instance.vitalityMax;
    }


    //=================================================================
    private void VitalityBtnClickHandler(GameObject go, bool state){
      if(state){
        vitalityGroup.gameObject.SetActive(true);
        SetVitalityInfo("当前时间：12:10:10\n已买体力次数：0/1\n体力已恢复满\n体力已恢复满\n体力已恢复满");
        tween.PlayForward();
      }else{
        tween.PlayReverse();
      }
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

    //=================================================================
    private void OkBtnClickHandler(GameObject go){
      
    }

    private void CancelBtnClickHandler(GameObject go){
      buyVitalityTween.PlayReverse();
    }

    private void BuyVitalityTweenFinishedHandler(UITweener tweener){
      if(buyVitalityGroup.transform.localScale.x < 0.01f){
        buyVitalityGroup.gameObject.SetActive(false);
      }
    }

  }

}