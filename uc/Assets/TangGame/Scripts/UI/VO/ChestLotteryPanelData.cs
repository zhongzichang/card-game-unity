using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 宝箱抽奖界面数据
  /// </summary>
  public class ChestLotteryPanelData {

    /// 抽奖类型，比如1金币抽，2元宝抽，3抽灵魂石
    public int type;
    /// 抽奖的道具数量
    public List<ChestLotteryPropsData> porps = new List<ChestLotteryPropsData>();

  }
}