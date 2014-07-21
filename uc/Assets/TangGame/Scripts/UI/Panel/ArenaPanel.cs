using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private object mParam;
    private List<GameObject> heros = new List<GameObject>();

    void Start(){
      adjustBtn.onClick += AdjustBtnClickHandler;
      ruleBtn.onClick += RuleBtnClickHandler;
      rankingBtn.onClick += RankingBtnClickHandler;
      recordBtn.onClick += RecordBtnnClickHandler;
      exchangeBtn.onClick += ExchangeBtnClickHandler;
      changeBtn.onClick += ChangeBtnClickHandler;
      simpleHeroItem.gameObject.SetActive(false);
      this.started = true;
      UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;UpdateData();}
    }

    protected override void UpdateData (){
      if(!this.started){return;}

      UpdateSelectedHero();
    }

    /// 更新选中的英雄
    private void UpdateSelectedHero(){
      foreach(GameObject go in heros){
        GameObject.Destroy(go);
      }
      heros.Clear();
      
      /// 拥有的英雄列表
      List<ArenaHero> ownDataList = ArenaCache.instance.GetOwnList();
      List<ArenaHero> tempList = new List<ArenaHero>();
      foreach(ArenaHero arenaHero in ownDataList){
        if(arenaHero.isSelected){
          tempList.Add(arenaHero);
        }
      }
      tempList.Sort(ArenaCache.instance.SortOrderByDescending);
      Vector3 tempPosition = simpleHeroItem.transform.localPosition;
      foreach(ArenaHero arenaHero in tempList){
        GameObject go = UIUtils.Duplicate(simpleHeroItem.gameObject, simpleHeroItem.transform.parent);
        SimpleHeroItem item = go.GetComponent<SimpleHeroItem>();
        item.data = arenaHero.heroBase;
        go.transform.localPosition = tempPosition;
        tempPosition.x += 130;
        heros.Add(go);
      }
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
      UIContext.mgrCoC.LazyOpen(ArenaShopPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.TEXTURE);
    }

    private void ChangeBtnClickHandler(GameObject go){
      
    }

  }
}