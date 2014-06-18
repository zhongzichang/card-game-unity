using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场战斗记录面板
  /// </summary>
  public class ArenaRecordPanel : ViewPanel {
    public const string NAME = "ArenaRecordPanel";
    
    public UIEventListener closeBtn;
    
    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
    }
    
    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }
  }
}