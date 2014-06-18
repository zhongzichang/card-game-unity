using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场规则面板
  /// </summary>
  public class ArenaRulePanel : ViewPanel {
    public const string NAME = "ArenaRulePanel";


    public UIEventListener closeBtn;

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
    }

    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }
  }
}
