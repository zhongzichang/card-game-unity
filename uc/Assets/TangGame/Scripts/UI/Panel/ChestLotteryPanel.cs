using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 宝箱抽奖界面
  /// </summary>
  public class ChestLotteryPanel : ViewPanel {
    public const string NAME = "ChestLotteryPanel";

    public GameObject bottomGroup;
    public GameObject ingotGroup;
    public GameObject moneyGroup;
    public UIEventListener buyBtn;
    public UIEventListener okBtn;
    public UILabel buyLabel;
    public UILabel okLabel;
    public UILabel ingotLabel;
    public UILabel moneyLabel;

    public TweenPosition tweenPosition;
    public TweenScale tweenScale;

    public ChestLotteryPropsItem chestLotteryPropsItem;

    private object mParam;
    private int createIndex = 0;
    private Vector2 itemStartPosition = new Vector2(-300, 20);
    private List<ChestLotteryPropsItem> items = new List<ChestLotteryPropsItem>();

    void Start () {
     
      ingotGroup.SetActive(false);
      bottomGroup.SetActive(false);
      buyBtn.onClick += BuyBtnClickHandler;
      okBtn.onClick += OkBtnClickHandler;
      okLabel.text = UIPanelLang.OK;
     
      tweenPosition.eventReceiver = this.gameObject;
      tweenPosition.callWhenFinished = "OnFinishedHandler";
      chestLotteryPropsItem.gameObject.SetActive(false);
      tweenPosition.gameObject.SetActive(false);

      this.started = true;
      UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;UpdateData();}
    }

    private void UpdateData(){
      if(!this.started){return;}
      
      foreach(ChestLotteryPropsItem item in items){
        GameObject.Destroy(item.gameObject);
      }
      items.Clear();

      ChestLotteryPanelData data = this.mParam as ChestLotteryPanelData;
      if(data.porps.Count < 1){
        return;
      }
      bottomGroup.SetActive(false);
      tweenPosition.gameObject.SetActive(true);
      tweenPosition.transform.localPosition = Vector3.zero;
      tweenPosition.transform.localScale = Vector3.one;

      Invoke("PlayChestAnimation", 1.2f);
    }


    /// 播放宝箱动画
    private void PlayChestAnimation(){

      tweenPosition.ResetToBeginning();
      tweenPosition.Play();
      tweenScale.ResetToBeginning();
      tweenScale.Play();
    }

    private void BuyBtnClickHandler(GameObject go){
      UpdateData();
    }

    private void OkBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }

    private void OnFinishedHandler(UITweener tweener){
      PlayPropsAnimation();
    }

    /// 播放道具显示动画
    private void PlayPropsAnimation(){
      ChestLotteryPanelData data = this.mParam as ChestLotteryPanelData;

      if(data.porps.Count == 1){
        GameObject go = UIUtils.Duplicate(chestLotteryPropsItem.gameObject, chestLotteryPropsItem.transform.parent.gameObject);
        ChestLotteryPropsItem item = go.GetComponent<ChestLotteryPropsItem>();
        item.startPosition = new Vector2(0, 220);
        item.targetPosition = new Vector2(0, -50);
        item.data = data.porps[0];
        items.Add(item);
        ShowBottomGroup();
      }else{
        createIndex = 0;
        StopCoroutine("CreateItem");
        StartCoroutine("CreateItem");
      }
    }

    IEnumerator CreateItem(){
      ChestLotteryPanelData data = this.mParam as ChestLotteryPanelData;
      GameObject go = UIUtils.Duplicate(chestLotteryPropsItem.gameObject, chestLotteryPropsItem.transform.parent.gameObject);
      ChestLotteryPropsItem item = go.GetComponent<ChestLotteryPropsItem>();
      item.startPosition = new Vector2(0, 220);
      float x = (createIndex % 5) * 150 + itemStartPosition.x;
      float y = itemStartPosition.y - (int)(createIndex / 5) * 155;
      item.targetPosition = new Vector2(x , y);
      item.data = data.porps[createIndex];
      items.Add(item);
      yield return new WaitForSeconds(0.2f);
      createIndex++;
      if(createIndex < data.porps.Count){
        StartCoroutine("CreateItem");
      }else{
        ShowBottomGroup();
      }
    }

    /// 显示下部信息
    private void ShowBottomGroup(){
      bottomGroup.SetActive(true);
      ChestLotteryPanelData data = this.mParam as ChestLotteryPanelData;
      if(data.type == 1){
        moneyGroup.SetActive(true);
        ingotGroup.SetActive(false);
        if(data.porps.Count == 1){
          moneyLabel.text = "10000";
          buyLabel.text = UIPanelLang.LOTTERY_ONE;
        }else{
          moneyLabel.text = "90000";
          buyLabel.text = UIPanelLang.LOTTERY_TEN;
        }
      }else if(data.type == 2){
        moneyGroup.SetActive(false);
        ingotGroup.SetActive(true);
        if(data.porps.Count == 1){
          ingotLabel.text = "288";
          buyLabel.text = UIPanelLang.LOTTERY_ONE;
        }else{
          ingotLabel.text = "2590";
          buyLabel.text = UIPanelLang.LOTTERY_TEN;
        }
      }else if(data.type == 3){
        moneyGroup.SetActive(false);
        ingotGroup.SetActive(true);
        ingotLabel.text = "400";
        buyLabel.text = UIPanelLang.LOTTERY_ONE;
      }
    }


  }
}