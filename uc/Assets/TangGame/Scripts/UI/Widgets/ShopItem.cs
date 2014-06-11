using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class ShopItem : ViewItem {

    public ViewItemDelegate onClick;
    public UILabel nameLabel;
    public UILabel priceLabel;
    public GameObject ingotIcon;
    public GameObject moenyIcon;
    public GameObject mask;
    public SimplePropsItem props;
   
  }
}