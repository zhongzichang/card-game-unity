using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

      DailyCache.instance.list.Add(new Daily());
      DailyCache.instance.list.Add(new Daily());
      DailyCache.instance.list.Add(new Daily());
      DailyCache.instance.list.Add(new Daily());
      DailyCache.instance.list.Add(new Daily());
      DailyCache.instance.list.Add(new Daily());
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
        position.y = position.y - 141;
      }
    }

    private void CloseBtnClickHandler(GameObject go){

    }

    private void ItemClickHandler(ViewItem viewItem){

    }
  }
}