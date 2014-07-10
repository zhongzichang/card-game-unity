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

    void Start(){
      vipBuyItem.gameObject.SetActive(false);
      btn.onClick += BtnClickHandler;
      DisplayVipBuyItems();
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
  }
}