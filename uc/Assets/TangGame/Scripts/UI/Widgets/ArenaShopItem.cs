using UnityEngine;
using System.Collections;
using Procurios.Public;

namespace TangGame.UI{
  public class ArenaShopItem : ViewItem {

    public ViewItemDelegate onClick;
    public UILabel nameLabel;
    public UILabel priceLabel;
    public GameObject mask;
    public SimplePropsItem propsItem;
   
    public override void Start (){
      mask.SetActive(false);
      this.started = true;
      UpdateData();
    }

    public override void UpdateData (){
      if(!this.started){return;}
      if(this.data == null){return;}
      Goods goods = this.data as Goods;
      this.nameLabel.text = goods.data.name;
      if(goods.isSell){
        mask.SetActive(true);
      }
      Props props = new Props();
      props.data = goods.data;
      propsItem.data = props;

      //金币，元宝，龙鳞，竞技场
      ArrayList arrayList = JSON.JsonDecode(goods.data.price_list) as ArrayList;
      if(arrayList != null){
        int temp = (int)arrayList[1];
        if(temp > 0){
          priceLabel.text = temp.ToString();
        }else{
          temp = (int)arrayList[0];
          priceLabel.text = temp.ToString();
        }
      }

    }
  }
}