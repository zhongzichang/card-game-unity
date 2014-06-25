using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场排行榜
  /// </summary>
  public class ArenaRankingPanel : ViewPanel {
    public const string NAME = "ArenaRankingPanel";

    public UIEventListener closeBtn;
    
    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
    }
    
    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }
  }
}