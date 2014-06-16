using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class ChestPanel : ViewPanel {

    public const string NAME = "ChestPanel";

    public UIEventListener viewBtn1;
    public UIEventListener arrowBtn1;
    public UIEventListener buyOneBtn1;
    public UIEventListener buyTenBtn1;
    public TweenPosition tween1;
    public UILabel freeLabel1;
    public GameObject moneyGroup;

    public UIEventListener viewBtn2;
    public UIEventListener arrowBtn2;
    public UIEventListener buyOneBtn2;
    public UIEventListener buyTenBtn2;
    public TweenPosition tween2;
    public UILabel freeLabel2;
    public GameObject ingotGroup;

    public UIEventListener viewBtn3;
    public UIEventListener arrowBtn3;
    public TweenPosition tween3;
    public UIEventListener buyOneBtn3;


    void Start () {
      viewBtn1.onClick += ViewBtn1ClickHandler;
      arrowBtn1.onClick += ArrowBtn1ClickHandler;
      buyOneBtn1.onClick += BuyOneBtn1ClickHandler;
      buyTenBtn1.onClick += BuyTenBtn1ClickHandler;

      viewBtn2.onClick += ViewBtn2ClickHandler;
      arrowBtn2.onClick += ArrowBtn2ClickHandler;
      buyOneBtn2.onClick += BuyOneBtn2ClickHandler;
      buyTenBtn2.onClick += BuyTenBtn2ClickHandler;

      viewBtn3.onClick += ViewBtn3ClickHandler;
      arrowBtn3.onClick += ArrowBtn3ClickHandler;
      buyOneBtn3.onClick += BuyOneBtn3ClickHandler;

      freeLabel1.gameObject.SetActive(false);
      freeLabel2.gameObject.SetActive(false);
    }

    private void ViewBtn1ClickHandler(GameObject go){
      tween1.PlayForward();
    }

    private void ArrowBtn1ClickHandler(GameObject go){
      tween1.PlayReverse();
    }

    private void BuyOneBtn1ClickHandler(GameObject go){
      ChestLotteryPanelData data = new ChestLotteryPanelData();
      data.type = 1;
      ChestLotteryPropsData t = new ChestLotteryPropsData();
      t.id = 1001;
      t.num = 3;
      data.porps.Add(t);
      UIContext.mgrCoC.LazyOpen(ChestLotteryPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, data);
    }

    private void BuyTenBtn1ClickHandler(GameObject go){
      ChestLotteryPanelData data = new ChestLotteryPanelData();
      data.type = 1;
      for(int i = 0; i < 10; i++){
        ChestLotteryPropsData t = new ChestLotteryPropsData();
        t.id = 1001 + i;
        t.num = 3;
        data.porps.Add(t);
      }
      UIContext.mgrCoC.LazyOpen(ChestLotteryPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, data);
    }

    private void ViewBtn2ClickHandler(GameObject go){
      tween2.PlayForward();
    }
    
    private void ArrowBtn2ClickHandler(GameObject go){
      tween2.PlayReverse();
    }

    private void BuyOneBtn2ClickHandler(GameObject go){
      ChestLotteryPanelData data = new ChestLotteryPanelData();
      data.type = 2;
      ChestLotteryPropsData t = new ChestLotteryPropsData();
      t.id = 1002;
      t.num = 3;
      data.porps.Add(t);
      UIContext.mgrCoC.LazyOpen(ChestLotteryPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, data);
    }
    
    private void BuyTenBtn2ClickHandler(GameObject go){
      ChestLotteryPanelData data = new ChestLotteryPanelData();
      data.type = 2;
      for(int i = 0; i < 10; i++){
        ChestLotteryPropsData t = new ChestLotteryPropsData();
        t.id = 1002 + i;
        t.num = 3;
        data.porps.Add(t);
      }
      UIContext.mgrCoC.LazyOpen(ChestLotteryPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, data);
    }

    private void ViewBtn3ClickHandler(GameObject go){
      tween3.PlayForward();
    }
    
    private void ArrowBtn3ClickHandler(GameObject go){
      tween3.PlayReverse();
    }

    private void BuyOneBtn3ClickHandler(GameObject go){
      ChestLotteryPanelData data = new ChestLotteryPanelData();
      data.type = 3;
      ChestLotteryPropsData t = new ChestLotteryPropsData();
      t.id = 1003;
      t.num = 3;
      data.porps.Add(t);
      UIContext.mgrCoC.LazyOpen(ChestLotteryPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, data);
    }

  }
}