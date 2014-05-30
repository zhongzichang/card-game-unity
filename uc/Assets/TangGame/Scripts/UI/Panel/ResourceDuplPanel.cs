using UnityEngine;
using System.Collections;


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

    private object mParam;

    void Start(){
      expBtn.onClick += ExpBtnClickHandler;
      moneyBtn.onClick += MoneyBtnClickHandler;
      tipsBackBtn.onClick += TipsBackBtnClickHandler;
      tipsTween.eventReceiver = this.gameObject;
      tipsTween.callWhenFinished = "OnFinishedHandler";
      tips.SetActive(false);
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
      UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Exp);
    }

    private void ExpTipsBackBtnClickHandler(GameObject go){
      tipsTween.PlayReverse();
    }

    private void MoneyBtnClickHandler(GameObject go){
      tips.SetActive(true);
      tipsTween.PlayForward();
    }

    private void TipsBackBtnClickHandler(GameObject go){
      tipsTween.PlayReverse();
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        tweener.gameObject.SetActive(false);
      }
    }

  }
}

