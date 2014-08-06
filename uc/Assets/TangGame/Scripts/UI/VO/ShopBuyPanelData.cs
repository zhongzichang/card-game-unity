using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  public enum ShopType{
    /// 普通商店
    Shop,
    /// 竞技场商店
    Arena,
  }

  /// ShopBuyPanel商店购买面板传输数据
  public class ShopBuyPanelData {
    public ShopType type;
    public Goods goods;
  }
}

