using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// 世界场景
  public class WorldScenePanel : MonoBehaviour {

    public GameObject fore;
    public GameObject mid;
    public GameObject sky;

    /// 战役按钮
    public UIEventListener zyBtn;
    /// 附魔按钮
    public UIEventListener fmBtn;

    private float x = 0;

    // Use this for initialization
    void Start () {
      zyBtn.onClick += ZYBtnClickHandler;
      fmBtn.onClick += FMBtnClickHandler;
    }
    
    void LateUpdate(){
      if(x != fore.transform.localPosition.x){
        x = fore.transform.localPosition.x;
        Vector3 temp = mid.transform.localPosition;
        temp.x = x / 2;
        mid.transform.localPosition = temp;
        temp = sky.transform.localPosition;
        temp.x = x / 4;
        sky.transform.localPosition = temp;
      }
    }

    private void ZYBtnClickHandler(GameObject go){
      BattleChaptersPanelData data = new BattleChaptersPanelData();
      data.stage = 1001;
      UIContext.mgrCoC.LazyOpen(BattleChaptersPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS, data);
    }

    private void FMBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(EnchantsPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.TEXTURE);
    }
  }
}