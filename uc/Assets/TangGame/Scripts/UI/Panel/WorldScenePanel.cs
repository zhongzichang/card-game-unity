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

    /// 时光按钮
    public UIEventListener sgBtn;
    /// 邮件按钮
    public UIEventListener mailBtn;
    /// 商店按钮
    public UIEventListener shopBtn;
    /// 试炼按钮
    public UIEventListener slBtn;
    /// 宝箱按钮
    public UIEventListener chestBtn;
    /// 竞技场按钮
    public UIEventListener arenaBtn;

    private float x = 0;

    // Use this for initialization
    void Start () {
      zyBtn.onClick += ZYBtnClickHandler;
      fmBtn.onClick += FMBtnClickHandler;
      sgBtn.onClick += SGBtnClickHandler;
      mailBtn.onClick += MailBtnClickHandler;
      shopBtn.onClick += ShopBtnClickHandler;
      slBtn.onClick += SLBtnClickHandler;
      chestBtn.onClick += ChestBtnClickHandler;
      arenaBtn.onClick += ArenaBtnClickHandler;
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

    private void SGBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ResourceDuplPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS);
    }

    private void MailBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(MailPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE);
    }

    private void ShopBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ShopPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS);
    }

    private void SLBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(PropsDuplPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS);
    }

    private void ChestBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ChestPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS);
    }

    private void ArenaBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ArenaPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS);
    }


  }
}