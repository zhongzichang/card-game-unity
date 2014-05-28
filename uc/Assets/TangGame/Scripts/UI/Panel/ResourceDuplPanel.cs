using UnityEngine;
using System.Collections;


namespace TangGame.UI{

  /// 资源副本，即时光洞穴之类的
  public class ResourceDuplPanel : MonoBehaviour {

    public const string NAME = "ResourceDuplPanel";

    /// 界面容器
    public GameObject container;
    public UIEventListener expBtn;
    public UIEventListener glodBtn;
    public GameObject expTips;
    public TweenScale expTipsTween;
    public UIEventListener expTipsBackBtn;

    public GameObject glodTips;
    public TweenScale glodTipsTween;
    public UIEventListener glodTipsBackBtn;

    private object mParam;

    void Start(){
      expBtn.onClick += ExpBtnClickHandler;
      glodBtn.onClick += GlodBtnClickHandler;
      expTipsBackBtn.onClick += ExpTipsBackBtnClickHandler;
      glodTipsBackBtn.onClick += GlodTipsBackBtnClickHandler;
      expTipsTween.eventReceiver = this.gameObject;
      expTipsTween.callWhenFinished = "OnFinishedHandler";
      glodTipsTween.eventReceiver = this.gameObject;
      glodTipsTween.callWhenFinished = "OnFinishedHandler";


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
      expTips.SetActive(true);
      //expTipsTween.ResetToBeginning();
      expTipsTween.PlayForward();
    }

    private void ExpTipsBackBtnClickHandler(GameObject go){
      expTipsTween.PlayReverse();
    }


    private void GlodBtnClickHandler(GameObject go){
      glodTips.SetActive(true);
      glodTipsTween.PlayForward();
    }

    private void GlodTipsBackBtnClickHandler(GameObject go){
      glodTipsTween.PlayReverse();
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        tweener.gameObject.SetActive(false);
      }
    }

  }
}

