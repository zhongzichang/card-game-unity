using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class RewardItemData{

    public string id;
    /// <summary>
    /// 2 物品名称      物品的名称。
    /// </summary>
    public string name;
    /// <summary>
    /// 3 类型      物品类型包括：装备，碎片，卷轴，灵魂石，药品，附魔材料。
    /// </summary>
    public int type;
    /// <summary>
    /// 4 品质      一个标记，用于物品外边框的显示，及一些规则的判断。白色，黄色，绿色，蓝色，紫色。                
    /// </summary>
    public int rank;
    /// <summary>
    /// 买入价格      金币价格
    /// </summary>
    public int goldCost;
    /// <summary>
    /// 需求等级    英雄要穿戴该装备需要的等级限制。    
    /// </summary>
    public int minLevel;
    /// <summary>
    /// 物品详细描述   物品的文字描述   
    /// </summary>
    public string detailDesc;
    /// <summary>
    /// 物品简短描述 
    /// </summary>
    public string briefDesc;
  }
}