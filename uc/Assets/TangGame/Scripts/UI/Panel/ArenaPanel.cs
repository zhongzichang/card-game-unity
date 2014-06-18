using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场面板
  /// </summary>
  public class ArenaPanel : ViewPanel {
    public const string NAME = "ArenaPanel";

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

    /// 调整按钮
    public UIEventListener adjustBtn;
    /// 规则按钮
    public UIEventListener ruleBtn;
    /// 排行榜按钮
    public UIEventListener rankingBtn;
    /// 对战记录按钮
    public UIEventListener recordBtn;
    /// 兑换奖励按钮
    public UIEventListener exchangeBtn;
    /// 换一批按钮
    public UIEventListener changeBtn;   

    void Start(){
      adjustBtn.onClick += AdjustBtnClickHandler;
      ruleBtn.onClick += RuleBtnClickHandler;
      rankingBtn.onClick += RankingBtnClickHandler;
      recordBtn.onClick += RecordBtnnClickHandler;
      exchangeBtn.onClick += ExchangeBtnClickHandler;
      changeBtn.onClick += ChangeBtnClickHandler;
    }

    private void AdjustBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ArenaAdjustHeroPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.TEXTURE);
    }

    private void RuleBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ArenaRulePanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE);
    }

    private void RankingBtnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ArenaRankingPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE);
    }

    private void RecordBtnnClickHandler(GameObject go){
      UIContext.mgrCoC.LazyOpen(ArenaRecordPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE);
    }

    private void ExchangeBtnClickHandler(GameObject go){
      
    }

    private void ChangeBtnClickHandler(GameObject go){
      
    }

  }
}