using UnityEngine;
using System.Collections;


namespace TangGame.UI{
  /// <summary>
  /// 设置面板
  /// </summary>
  public class SettingPanel : ViewPanel {

    public const string NAME = "SettingPanel";

    public UIEventListener closeBtn;
    public UIEventListener soundBtn;
    public UILabel soundLabel;
    public UIEventListener exchangeBtn;
    public UILabel exchangeLabel;
    public SettingItem[] items = new SettingItem[]{};

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
      soundBtn.onClick += SoundBtnClickHandler;
      exchangeBtn.onClick += ExchangeBtnClickHandler;
      Init();
    }

    private void Init(){
      foreach(SettingItem item in items){
        item.onClick += ItemClickHandler;
      }
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
      
    }
  }
}