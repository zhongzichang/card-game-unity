using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangUtils;

namespace TangGame.UI{

  /// 资源副本，即时光洞穴之类的
  public class ResourceDuplPanel : MonoBehaviour {

    public const string NAME = "ResourceDuplPanel";

    /// 界面容器
    public GameObject container;
    public UIEventListener expBtn;
    public UIEventListener moneyBtn;
 
    public GameObject tips;
    public UISprite title;
    public TweenScale tipsTween;
    public UIEventListener tipsBackBtn;
    public UILabel awardLabel;
    public UILabel tipsLabel;
    public SimplePropsItem[] propsItems = new SimplePropsItem[]{};
    private Dictionary<MapType, string> openList = new Dictionary<MapType, string>();

    private object mParam;

    void Start(){
      expBtn.onClick += ExpBtnClickHandler;
      moneyBtn.onClick += MoneyBtnClickHandler;
      tipsBackBtn.onClick += TipsBackBtnClickHandler;
      tipsTween.eventReceiver = this.gameObject;
      tipsTween.callWhenFinished = "OnFinishedHandler";
      tips.SetActive(false);
      awardLabel.text = UIPanelLang.AWARD;
      openList.Add(MapType.Exp, "1,3,5,7");
      openList.Add(MapType.Money, "2,4,6,7");
      Global.Log(DateUtil.GetDayOfWeek());
    }

    public object param{
      get{return this.mParam;}
      set{
        this.mParam = value;
        UpdateData();
      }
    }

    private void UpdateData(){

    }

    private void ExpBtnClickHandler(GameObject go){
      if(IsOpen(MapType.Exp)){
        UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Exp);
      }else{
        tips.SetActive(true);
        ShowExpTips();
        tipsTween.PlayForward();
      }
    }

    private void MoneyBtnClickHandler(GameObject go){
      if(IsOpen(MapType.Money)){
        UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Money);
      }else{
        tips.SetActive(true);
        ShowMoneyTips();
        tipsTween.PlayForward();
      }
    }

    private void TipsBackBtnClickHandler(GameObject go){
      tipsTween.PlayReverse();
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        tweener.gameObject.SetActive(false);
      }
    }

    private void ShowExpTips(){
      title.spriteName = MapType.Exp + "Title";
      title.MakePixelPerfect();
      tipsLabel.text = UIPanelLang.EXP_OPEN_TIPS;
      //public SimplePropsItem[] propsItems = new SimplePropsItem[]{};
    }

    private void ShowMoneyTips(){
      title.spriteName = MapType.Money + "Title";
      title.MakePixelPerfect();
      tipsLabel.text = UIPanelLang.MONEY_OPEN_TIPS;
      //public SimplePropsItem[] propsItems = new SimplePropsItem[]{};
    }

    /// 判断是否开启
    private bool IsOpen(MapType type){
      if(openList.ContainsKey(type)){
        string str = openList[type];
        string week = DateUtil.GetDayOfWeek().ToString();
        if(str.IndexOf(week) != -1){
          return true;
        }
      }
      return false;
    }
  }
}

