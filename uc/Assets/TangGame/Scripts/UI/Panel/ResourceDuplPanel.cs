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
    public GameObject expTips;
    public TweenScale expTipsTween;
    public UIEventListener expTipsBackBtn;

    public GameObject moneyTips;
    public TweenScale moneyTipsTween;
    public UIEventListener moneyTipsBackBtn;

    private object mParam;

    void Start(){
      expBtn.onClick += ExpBtnClickHandler;
      moneyBtn.onClick += MoneyBtnClickHandler;
      expTipsBackBtn.onClick += ExpTipsBackBtnClickHandler;
      moneyTipsBackBtn.onClick += MoneyTipsBackBtnClickHandler;
      expTipsTween.eventReceiver = this.gameObject;
      expTipsTween.callWhenFinished = "OnFinishedHandler";
      moneyTipsTween.eventReceiver = this.gameObject;
      moneyTipsTween.callWhenFinished = "OnFinishedHandler";


      expTips.SetActive(false);
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
      //expTips.SetActive(true);
      //expTipsTween.PlayForward();
      UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Exp);
    }

    private void ExpTipsBackBtnClickHandler(GameObject go){
      expTipsTween.PlayReverse();
    }


    private void MoneyBtnClickHandler(GameObject go){
      moneyTips.SetActive(true);
      moneyTipsTween.PlayForward();
    }

    private void MoneyTipsBackBtnClickHandler(GameObject go){
      moneyTipsTween.PlayReverse();
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        tweener.gameObject.SetActive(false);
      }
    }

  }
}

