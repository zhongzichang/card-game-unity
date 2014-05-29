using UnityEngine;
using System.Collections;


namespace TangGame.UI{

  /// 资源副本，即时光洞穴之类的
  public class PropsDuplPanel : MonoBehaviour {

    public const string NAME = "PropsDuplPanel";

    /// 界面容器
    public GameObject container;
    public UIEventListener props1Btn;
    public UIEventListener props2Btn;
    public UIEventListener props3Btn;
    public GameObject tips;
    public TweenScale tipsTween;
    public UIEventListener tipsBackBtn;

    public UILabel awardLabel;
    public UILabel tipsLabel;
    public UILabel openLabel;

    public SimplePropsItem[] propsItems = new SimplePropsItem[]{};

    private object mParam;

    void Start(){
      props1Btn.onClick += Props1BtnClickHandler;
      props2Btn.onClick += Props2BtnClickHandler;
      props3Btn.onClick += Props3BtnClickHandler;
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

    private void Props1BtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ResourceDuplSelectPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, MapType.Props1);
    }

    private void Props2BtnClickHandler(GameObject go){
    }


    private void Props3BtnClickHandler(GameObject go){
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

