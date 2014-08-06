using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 购买金钱界面
  /// </summary>
  public class BuyMoneyPanel : ViewPanel {
    public const string NAME = "BuyMoneyPanel";

    public UILabel titleLabel;
    public UILabel timesLabel;
    public UILabel ExplainLabel;
    public UILabel IngotLabel;
    public UILabel MoneyLabel;
    public UILabel useBtnLabel;
    public UIEventListener useBtn;

    void Start(){
      useBtn.onClick += UseBtnClickHandler;
    }

    private void UseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }


  }
}

