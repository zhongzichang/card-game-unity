using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{
  /// <summary>
  /// 日常面板
  /// </summary>
  public class DailyPanel : ViewPanel {
    public const string NAME = "DailyPanel";

    public UILabel titleLabel;
    public DailyItem dailyItem;
    public UIScrollView scrollView;
    public UIEventListener closeBtn;

    private object mParam;
    private bool started;
    private List<DailyItem> items = new List<DailyItem>();

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
      titleLabel.text = UIPanelLang.DAILY;
      this.started = true;
      this.dailyItem.gameObject.SetActive(false);

      //=====测试=====
      foreach(DailyData tData in Config.dailyTable.Values){
        Daily daily = new Daily();
        daily.data = tData;
        DailyCache.instance.list.Add(daily);
      }
      //=====测试=====
      this.UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value; UpdateData();}
    }


    private void UpdateData(){
      if(!this.started){return;}
      foreach(DailyItem item in items){
        GameObject.Destroy(item);
      }
      items.Clear();

      GameObject go = null;
      Vector3 position = dailyItem.transform.localPosition;
      foreach(Daily daily in DailyCache.instance.list){
        go = UIUtils.Duplicate(dailyItem.gameObject, dailyItem.transform.parent.gameObject);
        go.transform.localPosition = position;
        DailyItem item = go.GetComponent<DailyItem>();
        items.Add(item);
        item.data = daily;
        item.onClick += ItemBtnClickHandler;
        UIEventListener.Get(go).onClick += ItemClickHandler;
        position.y = position.y - 141;
      }
    }

    private void CloseBtnClickHandler(GameObject go){
      this.GetComponentInChildren<Block>().Back();
    }

    private void ItemClickHandler(GameObject go){
      DailyItem item = go.GetComponent<DailyItem>();
      Daily daily = item.data as Daily;

      if(daily.data.id == 1 ){
        MsgTips.Show("任务尚未完成");
      }else if(daily.data.id == 2){
        TaskAwardPanelData taskAwardPanelData = new TaskAwardPanelData();
        taskAwardPanelData.daily = daily;
        UIContext.mgrCoC.LazyOpen(TaskAwardPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, taskAwardPanelData);
      }
      Debug.Log(daily.data.id);
    }

    private void ItemBtnClickHandler(ViewItem viewItem){
      DailyItem item = viewItem as DailyItem;
      Daily daily = viewItem.data as Daily;
      if(daily.data.id == 1 || daily.data.id == 2){

      }else{
        if(daily.data.openPanelType == 0){
          UIContext.mgrCoC.LazyOpen(BattleChaptersPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS, null);
        }else if(daily.data.openPanelType == 2){
          UIContext.mgrCoC.LazyOpen(BattleChaptersPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS, null);
        }else if(daily.data.openPanelType == 3){
          UIContext.mgrCoC.LazyOpen(ResourceDuplPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS, null);
        }else if(daily.data.openPanelType == 4){
          UIContext.mgrCoC.LazyOpen(PropsDuplPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS, null);
        }
      }
    }
  }
}