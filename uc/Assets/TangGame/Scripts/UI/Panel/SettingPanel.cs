using UnityEngine;
using System.Collections;


namespace TangGame.UI{
  /// <summary>
  /// 设置面板
  /// </summary>
  public class SettingPanel : ViewPanel {

    public const string NAME = "SettingPanel";

    public UILabel titleLabel;
    public UIEventListener closeBtn;
    public UIEventListener soundBtn;
    public UILabel soundLabel;
    public UIEventListener exchangeBtn;
    public UILabel exchangeLabel;
    public UILabel infoAlertLabel;
    public SettingItem[] items = new SettingItem[]{};

    //===============================
    public GameObject exchangeGroup;
    public UILabel exchangeTitleLabel;
    public UILabel exchangeInputLabel;
    public UILabel okLabel;
    public UILabel cancelLabel;
    public UIEventListener okBtn;
    public UIEventListener cancelBtn;
    public UIEventListener backBtn;
    public TweenScale tween;
    public UIInput input;


    private object mParam;

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
      soundBtn.onClick += SoundBtnClickHandler;
      exchangeBtn.onClick += ExchangeBtnClickHandler;

      okBtn.onClick += OkBtnClickHandler;
      cancelBtn.onClick += CancelBtnClickHandler;
      backBtn.onClick += BackBtnClickHandler;

      tween.eventReceiver = this.gameObject;
      tween.callWhenFinished = "OnFinishedHandler";
      this.exchangeGroup.SetActive(false);

      if (input != null) EventDelegate.Add(input.onChange, InputChangeHandler);

      titleLabel.text = UIPanelLang.SYSTEM_SETTING;
      okLabel.text = UIPanelLang.OK;
      cancelLabel.text = UIPanelLang.CANCEL;
      exchangeLabel.text = UIPanelLang.INPUT_EXCHANGE;
      exchangeTitleLabel.text = UIPanelLang.INPUT_EXCHANGE_TITLE;
      infoAlertLabel.text = UIPanelLang.INFO_ALERT;
      Init();
    }

    private void Init(){
      foreach(SettingItem item in items){
        item.onClick += ItemClickHandler;
      }
    }

    public object param{
      get{return mParam;}
      set{mParam = value; UpdateData();}
    }

    private void UpdateData(){
    }

    private void ItemClickHandler(ViewItem viewItem){

      SettingItem item = viewItem as SettingItem;
      string name = item.transform.name;
      name = name.Replace("Item", "");
      item.selected = !item.selected;
    }

    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }

    private void SoundBtnClickHandler(GameObject go){
      Setting.soundOn = !Setting.soundOn;
      if(Setting.soundOn){
        soundLabel.text = UIPanelLang.BATTLE_SOUND_OPEN;
      }else{
        soundLabel.text = UIPanelLang.BATTLE_SOUND_CLOSE;
      }
    }

    private void ExchangeBtnClickHandler(GameObject go){
      OpenExchangeGroup();
    }

    private void OkBtnClickHandler(GameObject go){
      
    }
    
    private void CancelBtnClickHandler(GameObject go){
      CloseExchangeGroup();
    }
    
    private void BackBtnClickHandler(GameObject go){
      CloseExchangeGroup();
    }

    /// 打开修改名称面板
    private void OpenExchangeGroup(){
      input.value = "";
      exchangeInputLabel.text = UIPanelLang.INPUT_EXCHANGE_TEXT;

      this.exchangeGroup.SetActive(true);
      tween.PlayForward();
    }
    
    /// 关闭修改名称面板
    private void CloseExchangeGroup(){
      tween.PlayReverse();
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        this.exchangeGroup.SetActive(false);
      }
    }

    private void InputChangeHandler(){
      Debug.Log(input.value);
    }
  }
}