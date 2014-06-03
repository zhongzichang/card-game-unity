using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangUtils;

namespace TangGame.UI{

  /// 道具副本，即英雄试炼
  public class PropsDuplPanel : MonoBehaviour {

    public const string NAME = "PropsDuplPanel";

    /// 界面容器
    public GameObject container;
    public UIEventListener props1Btn;
    public UIEventListener props2Btn;
    public UIEventListener props3Btn;

    public GameObject tips;
    public UISprite title;
    public TweenScale tipsTween;
    public UIEventListener tipsBackBtn;
    public UILabel awardLabel;
    public UILabel tipsLabel;
    public UILabel proposeLabel;
    public SimplePropsItem[] propsItems = new SimplePropsItem[]{};

    private object mParam;
    private Dictionary<MapType, string> openList = new Dictionary<MapType, string>();

    void Start(){
      props1Btn.onClick += Props1BtnClickHandler;
      props2Btn.onClick += Props2BtnClickHandler;
      props3Btn.onClick += Props3BtnClickHandler;
      tipsBackBtn.onClick += TipsBackBtnClickHandler;
      tipsTween.eventReceiver = this.gameObject;
      tipsTween.callWhenFinished = "OnFinishedHandler";

      tips.SetActive(false);
      awardLabel.text = UIPanelLang.AWARD;
      openList.Add(MapType.Props1, "1,4,7");
      openList.Add(MapType.Props2, "2,5,7");
      openList.Add(MapType.Props3, "3,6,7");
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

    private void Props1BtnClickHandler(GameObject go){
      if(IsOpen(MapType.Props1)){
        UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Props1);
      }else{
        tips.SetActive(true);
        ShowProps1Tips();
        tipsTween.PlayForward();
      }
    }

    private void Props2BtnClickHandler(GameObject go){
      if(IsOpen(MapType.Props2)){
        UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Props2);
      }else{
        tips.SetActive(true);
        ShowProps2Tips();
        tipsTween.PlayForward();
      }
    }

    private void Props3BtnClickHandler(GameObject go){
      if(IsOpen(MapType.Props3)){
        UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Props3);
      }else{
        tips.SetActive(true);
        ShowProps3Tips();
        tipsTween.PlayForward();
      }
    }

    private void TipsBackBtnClickHandler(GameObject go){
      tipsTween.PlayReverse();
    }


    private void ShowProps1Tips(){
      title.spriteName = MapType.Props1 + "Title";
      title.MakePixelPerfect();
      tipsLabel.text = UIPanelLang.PROPS1_OPEN_TIPS;
      proposeLabel.text = UIPanelLang.PROPS1_PROPOSE;
      //public SimplePropsItem[] propsItems = new SimplePropsItem[]{};
    }

    private void ShowProps2Tips(){
      title.spriteName = MapType.Props2 + "Title";
      title.MakePixelPerfect();
      tipsLabel.text = UIPanelLang.PROPS2_OPEN_TIPS;
      proposeLabel.text = UIPanelLang.PROPS2_PROPOSE;
      //public SimplePropsItem[] propsItems = new SimplePropsItem[]{};
    }

    private void ShowProps3Tips(){
      title.spriteName = MapType.Props3 + "Title";
      title.MakePixelPerfect();
      tipsLabel.text = UIPanelLang.PROPS3_OPEN_TIPS;
      proposeLabel.text = UIPanelLang.PROPS3_PROPOSE;
      //public SimplePropsItem[] propsItems = new SimplePropsItem[]{};
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        tweener.gameObject.SetActive(false);
      }
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

