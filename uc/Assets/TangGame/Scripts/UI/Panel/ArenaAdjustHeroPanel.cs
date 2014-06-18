using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场调整界面
  /// </summary>
  public class ArenaAdjustHeroPanel : ViewPanel {

    public const string NAME = "ArenaAdjustHeroPanel";

    public UIEventListener okBtn;
    public GameObject ownGroup;
    /// 英雄对象
    public ArenaAdjustHeroItem arenaAdjustHeroItem;
    /// 已经选择上阵的英雄对象
    public ArenaAdjustHeroSelectedItem arenaAdjustHeroSelectedItem;
    /// 菜单对象
    public SimpleMenuItem[] menus = new SimpleMenuItem[]{};

    private object mParam;
    /// 拥有的英雄
    private List<ArenaAdjustHeroItem> ownList = new List<ArenaAdjustHeroItem>();
    /// 上阵的对象
    private List<ArenaAdjustHeroSelectedItem> selectedList = new List<ArenaAdjustHeroSelectedItem>();
    /// 在取消队列的对象
    private Dictionary<string, ArenaAdjustHeroSelectedItem> cancelList = new Dictionary<string, ArenaAdjustHeroSelectedItem>();

    void Start(){
      this.arenaAdjustHeroItem.gameObject.SetActive(false);
      this.arenaAdjustHeroSelectedItem.gameObject.SetActive(false);
      okBtn.onClick += OkBtnClickHandler;
      for(int i = 0; i < menus.Length; i++){
        menus[i].onClick += MenuClickHandler;
        menus[i].index = i;
      }
      this.MenuClickHandler(menus[0]);
      started = true;
      UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;UpdateData();}
    }

    private void UpdateData(){
      if(!this.started){return;}

      foreach(ArenaAdjustHeroItem tempItem in ownList){
        GameObject.Destroy(tempItem.gameObject);
      }
      ownList.Clear();

      //===============
      foreach(ArenaAdjustHeroSelectedItem tempItem in selectedList){
        GameObject.Destroy(tempItem.gameObject);
      }
      selectedList.Clear();

      
      foreach(ArenaAdjustHeroSelectedItem tempItem in cancelList.Values){
        GameObject.Destroy(tempItem.gameObject);
      }
      cancelList.Clear();

      /// 拥有的英雄列表
      List<ArenaHero> ownDataList = ArenaCache.instance.GetOwnList();
      foreach(ArenaHero arenaHero in ownDataList){
        GameObject go = UIUtils.Duplicate(arenaAdjustHeroItem.gameObject, ownGroup);
        ArenaAdjustHeroItem item = go.GetComponent<ArenaAdjustHeroItem>();
        item.data = arenaHero;
        ownList.Add(item);
      }
    }

    private void OkBtnClickHandler(GameObject obj){

    }

    /// 菜单按钮点击处理
    private void MenuClickHandler(ViewItem viewItem){
      SimpleMenuItem item = viewItem as SimpleMenuItem;
      for(int i = 0; i < menus.Length; i++){
        if(menus[i] != item){
          menus[i].selected = false;
        }
      }
      item.selected = true;
    }

  }
}