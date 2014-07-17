using UnityEngine;
using System.Collections;
using Procurios.Public;

namespace TangGame.UI{
  /// <summary>
  /// 商店购买界面
  /// </summary>
  public class ShopBuyPanel : ViewPanel {
    public const string NAME = "ShopBuyPanel";

    public UISprite frame;
    public UISprite icon;
    public UILabel nameLabel;
    public UILabel numLabel;
    public UILabel descLabel;
    public UILabel buyNumLabel;
    public UILabel buyTotalLabel;
    public UISprite descBg;

    public UIEventListener closeBtn;
    public UIEventListener buyBtn;
    public UILabel buyBtnLabel;
    public UISprite moneyIcon;
    public UISprite ingotIcon;


    private object mParam;

    void Start(){
      moneyIcon.gameObject.SetActive(false);
      ingotIcon.gameObject.SetActive(false);
      closeBtn.onClick += CloseBtnClickHandler;
      buyBtn.onClick += BuyBtnClickHandler;

      this.started = true;
      UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;this.UpdateData();}
    }

    private void UpdateData(){
      if(!this.started){return;}
      ShopBuyPanelData shopBuyPanelData = this.mParam as ShopBuyPanelData;
      Goods goods = shopBuyPanelData.goods;

      this.nameLabel.text = goods.data.name + " X " + goods.num;
      this.numLabel.text = string.Format("拥有 {0} 件", "100");
      this.descLabel.text = goods.data.description;

      if(this.descLabel.height < 90){
        this.descBg.height = 110;
      }else{
        this.descBg.height = this.descLabel.height + 20;
      }

      this.buyNumLabel.text = string.Format("购买{0}件", goods.num);

      moneyIcon.gameObject.SetActive(false);
      ingotIcon.gameObject.SetActive(false);

      //金币，元宝，龙鳞，竞技场
      ArrayList arrayList = JSON.JsonDecode(goods.data.price_list) as ArrayList;
      int price = 0;
      if(arrayList != null){
        int temp = (int)arrayList[1];
        if(temp > 0){
          ingotIcon.gameObject.SetActive(true);
          price = temp;
        }else{
          moneyIcon.gameObject.SetActive(true);
          temp = (int)arrayList[0];
          price = temp;
        }
      }

      this.buyTotalLabel.text = (price * goods.num).ToString();
    }

    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }

    private void BuyBtnClickHandler(GameObject go){
      
    }

  }
}