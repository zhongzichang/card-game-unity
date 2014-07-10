using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 充值界面
  /// </summary>
  public class RechargePanel : ViewPanel {
    public UIEventListener btn;
    public UILabel btnLabel;

    //=================================
    public GameObject vipBuyGroup;
    public UIScrollView vipBuyScrollView;
    public GameObject vipBuyContainer;
    public VipBuyItem vipBuyItem;

    //=================================
    public GameObject vipDescGroup;
    public UILabel titleLabel;
    public UIEventListener viewBtn1;
    public UIEventListener viewBtn2;
    public UILabel viewVipLabel1;
    public UILabel viewVipLabel2;
    public UIScrollView vipDescScrollView;
    public GameObject vipDescContainer;
    public UILabel viewLabel;
    public GameObject viewGroup1;
    public GameObject viewGroup2;
    /// 用于定位介绍索引的
    private int index = 0;
    /// 移动标示
    private bool isMove;
    /// 移动目标坐标
    private Vector3 movePosition;
    /// 项总数
    private int total = 3;

    void Start(){
      movePosition = vipDescContainer.transform.localPosition;
      vipBuyItem.gameObject.SetActive(false);
      btn.onClick += BtnClickHandler;
      viewBtn1.onClick += ViewBtn1Handler;
      viewBtn2.onClick += ViewBtn2Handler;
      DisplayVipBuyItems();
      UpdateVipDescScroll();
    }

    void Update(){
      if(isMove){
        Vector3 temp = vipDescContainer.transform.localPosition;
        if( temp != movePosition){
          vipDescContainer.transform.localPosition = Vector3.MoveTowards(temp, movePosition, 35);
        }else{
          isMove = false;
        }
      }
    }


    private void BtnClickHandler(GameObject go){

    }

    /// 显示VIP的购买项
    private void DisplayVipBuyItems(){
      Vector3 tempPosition = vipBuyItem.transform.localPosition;
      Vector3 temp = Vector3.zero;
      for(int i = 0; i < 10; i++){
        GameObject go = UIUtils.Duplicate(vipBuyItem.gameObject, vipBuyContainer);
        temp.x = tempPosition.x + i % 2 * 410;
        temp.y = tempPosition.y - (int)(i / 2) * 143;
        go.transform.localPosition = temp;
        UIEventListener.Get(go).onClick += ItemClickHandler;
      }
    }

    /// VIP购买项点击处理
    private void ItemClickHandler(GameObject go){

    }

    //==================================================================
    private void ViewBtn1Handler(GameObject go){
      index--;
      UpdateVipDescScroll();
    }

    private void ViewBtn2Handler(GameObject go){
      index++;
      UpdateVipDescScroll();
    }

    /// 更新VIP介绍滚动
    private void UpdateVipDescScroll(){
      if(index >= 0){
        isMove = true;
      }else{
        index = 0;
      }

      if(index == 0){
        viewGroup1.SetActive(false);
      }else{
        viewGroup1.SetActive(true);
      }

      if(index < total){
        isMove = true;
      }else{
        index = total - 1;
      }

      if(index == total - 1){
        viewGroup2.SetActive(false);
      }else{
        viewGroup2.SetActive(true);
      }

      int cVip = index + 1;
      viewVipLabel1.text = "VIP" + (cVip - 1);
      viewVipLabel2.text = "VIP" + (cVip + 1);
      titleLabel.text = string.Format("VIP{0}等级特权", cVip);
      movePosition.x = index * -600;
    }
  }
}