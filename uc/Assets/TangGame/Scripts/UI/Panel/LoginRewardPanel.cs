using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 登陆奖励，即每日签到功能
  /// </summary>
  public class LoginRewardPanel : ViewPanel {
    public const string NAME = "LoginRewardPanel";

    public UIEventListener closeBtn;
    public UIEventListener explainBtn;
    public UIEventListener explainMask;
    public TweenScale tweenScale;
    public UILabel explainDescLabel;
    public UILabel explainBtnLabel;
    public UILabel titleLabel;
    public UILabel timesLabel;




    void Start(){
      tweenScale.transform.localScale = Vector3.zero;
      explainBtn.onClick += ExplainBtnClickHandler;
      explainMask.onClick += ExplainMaskClickHandler;
      closeBtn.onClick += CloseBtnClickHandler;
    }

    private void ExplainBtnClickHandler(GameObject go){
      tweenScale.PlayForward();
    }

    private void ExplainMaskClickHandler(GameObject go){
      tweenScale.PlayReverse();
    }

    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }
  }
}

