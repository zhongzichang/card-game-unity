using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场面板
  /// </summary>
  public class ArenaPanel : ViewPanel {

    public SimpleHeroItem simpleHeroItem;
    public ArenaItem arenaItem;
    public UILabel label;
    public UILabel battleLabel;
    public UILabel battleValueLabel;
    /// 调整按钮文本
    public UILabel adjustBtnLabel;
    /// 我的排名文本
    public UILabel rankingLabel;
    /// 规则按钮文本
    public UILabel ruleBtnLabel;
    /// 排行榜按钮文本
    public UILabel rankingBtnLabel;
    /// 对战记录按钮文本
    public UILabel recordBtnLabel;
    /// 兑换奖励按钮文本
    public UILabel exchangeBtnLabel;
    /// 换一批按钮文本
    public UILabel changeBtnLabel;
    /// 今日剩余次数文本
    public UILabel surplusLabel;
    /// 今日剩余次数值得文本
    public UILabel surplusValueLabel; 
        
        
  }
}