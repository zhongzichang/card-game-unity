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
    public GameObject selectedGroup;
    /// 英雄对象
    public ArenaAdjustHeroItem arenaAdjustHeroItem;
    /// 已经选择上阵的英雄对象
    public ArenaAdjustHeroSelectedItem arenaAdjustHeroSelectedItem;
    /// 菜单对象
    public SimpleMenuItem[] menus = new SimpleMenuItem[]{};

    private object mParam;
    /// 拥有的英雄
    private List<ArenaAdjustHeroItem> ownList = new List<ArenaAdjustHeroItem>();
    /// 当前面板显示的英雄
    private List<ArenaAdjustHeroItem> ownShowList = new List<ArenaAdjustHeroItem>();
    /// 上阵的对象
    private List<ArenaAdjustHeroSelectedItem> selectedList = new List<ArenaAdjustHeroSelectedItem>();
    /// 在取消队列的对象
    private Dictionary<int, ArenaAdjustHeroSelectedItem> cancelList = new Dictionary<int, ArenaAdjustHeroSelectedItem>();
    /// 选中的菜单按钮
    private SimpleMenuItem selectedMenu;


    void Start(){
      this.arenaAdjustHeroItem.gameObject.SetActive(false);
      this.arenaAdjustHeroSelectedItem.gameObject.SetActive(false);
      okBtn.onClick += OkBtnClickHandler;
      for(int i = 0; i < menus.Length; i++){
        menus[i].onClick += MenuClickHandler;
        menus[i].index = i;
      }
      this.selectedMenu = menus[0];
      this.selectedMenu.selected = true;
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

      List<ArenaHero> tempSelectedList = new List<ArenaHero>();
      /// 拥有的英雄列表
      List<ArenaHero> ownDataList = ArenaCache.instance.GetOwnList();
      foreach(ArenaHero arenaHero in ownDataList){
        GameObject go = UIUtils.Duplicate(arenaAdjustHeroItem.gameObject, ownGroup);
        ArenaAdjustHeroItem item = go.GetComponent<ArenaAdjustHeroItem>();
        item.data = arenaHero;
        UIEventListener.Get(go).onClick += OwnItemClickHandler;
        ownList.Add(item);

        if(arenaHero.isSelected){
          tempSelectedList.Add(arenaHero);
        }
      }

      // 创建选择的英雄
      foreach(ArenaHero arenaHero in tempSelectedList){
        GameObject go = UIUtils.Duplicate(arenaAdjustHeroSelectedItem.gameObject, selectedGroup);
        ArenaAdjustHeroSelectedItem item = go.GetComponent<ArenaAdjustHeroSelectedItem>();
        item.data = arenaHero;
        item.cancelCompleted += ItemCancelCompleted;
        UIEventListener.Get(go).onClick += SelectedItemClickHandler;
        selectedList.Add(item);
      }

      selectedList.Sort(SortOrder);
      for(int i = 0, length = selectedList.Count; i < length; i++){
        ArenaAdjustHeroSelectedItem tempItem = selectedList[i];
        Vector3 tempPosition = new Vector3(-130 * i, 0, 0);
        tempItem.transform.localPosition = tempPosition;
      }
      //==============================================================================================

      this.UpdateShowItem(selectedMenu.index);
    }

    private void OkBtnClickHandler(GameObject obj){

    }

    /// 菜单按钮点击处理
    private void MenuClickHandler(ViewItem viewItem){
      SimpleMenuItem item = viewItem as SimpleMenuItem;
      if(selectedMenu == item){
        return;
      }
      if(selectedMenu != null){
        selectedMenu.selected = false;
      }
      selectedMenu = item;
      selectedMenu.selected = true;
      UpdateShowItem(selectedMenu.index);
    }

    /// 更新显示显示项
    private void UpdateShowItem(int index){
      ownShowList.Clear();
      List<ArenaAdjustHeroItem> temp = new List<ArenaAdjustHeroItem>();
      foreach(ArenaAdjustHeroItem item in ownList){
        ArenaHero hero = item.data as ArenaHero;
        if(index == 0){
          item.gameObject.SetActive(true);
          if(hero.isSelected){
            ownShowList.Add(item);
          }else{
            temp.Add(item);
          }
        }else if(index == hero.GetHeroLocation()){
          item.gameObject.SetActive(true);
          if(hero.isSelected){
            ownShowList.Add(item);
          }else{
            temp.Add(item);
          }
        }else{
          item.gameObject.SetActive(false);
        }
      }
      ownShowList.AddRange(temp);

      int count = 0;
      foreach(ArenaAdjustHeroItem item in ownShowList){
        Vector3 tempPosition = new Vector3(-280 + (count % 5) * 140, 120 - (int)(count / 5) * 140, 0);
        item.transform.localPosition = tempPosition;
        count++;
      }
    }

    /// 拥有项点击处理
    private void OwnItemClickHandler(GameObject go){
      ArenaAdjustHeroItem item = go.GetComponent<ArenaAdjustHeroItem>();
      ArenaHero hero = item.data as ArenaHero;
      UpdateSelectedItem(hero.id);
    }


      /// 获取拥有的显示项
    private ArenaAdjustHeroItem GetOwnItem(int id){
      foreach(ArenaAdjustHeroItem item in ownList){
        ArenaHero hero = item.data as ArenaHero;
        if(hero.id == id){
          return item;
        }
      }
      return null;
    }


    /// 获取选中的显示项
    private ArenaAdjustHeroSelectedItem GetSelectedItem(int id){
      foreach(ArenaAdjustHeroSelectedItem item in selectedList){
        ArenaHero hero = item.data as ArenaHero;
        if(hero.id == id){
          return item;
        }
      }
      return null;
    }

    /// 对已经选择的英雄处理
    public void UpdateSelectedItem (int heroId){
      if(cancelList.ContainsKey(heroId)){//在取消队列的话，不处理
        return;
      }

      ArenaAdjustHeroSelectedItem selectedItem = GetSelectedItem(heroId);
      if(selectedItem != null){//包含就取消
        cancelList.Add(heroId, selectedItem);//添加进取消列表中
        Vector3 tempV3 = selectedItem.transform.localPosition;//保存当前的坐标

        ArenaAdjustHeroItem ownItem = GetOwnItem(heroId);

        if(!ownItem.gameObject.activeSelf){//未显示，表示不在当前的界面
          selectedItem.Cancel(tempV3, tempV3 + new Vector3(0, 300, 0));
        }else{
          selectedItem.transform.position = ownItem.transform.position;//设置世界坐标
          selectedItem.Cancel(tempV3, selectedItem.transform.localPosition);
        }
        selectedList.Remove(selectedItem);

        for(int i = 0, length = selectedList.Count; i < length; i++){
          ArenaAdjustHeroSelectedItem tempItem = selectedList[i];
          Vector3 tempPosition = new Vector3(-130 * i, 0, 0);
          tempItem.Move(tempPosition);
        }
      }else{//不包含就添加
        ArenaAdjustHeroItem ownItem = GetOwnItem(heroId);
        ArenaHero hero = ownItem.data as ArenaHero;
        hero.isSelected = true;
        ownItem.selected = true;

        GameObject go = UIUtils.Duplicate(arenaAdjustHeroSelectedItem.gameObject, selectedGroup);
        ArenaAdjustHeroSelectedItem item = go.GetComponent<ArenaAdjustHeroSelectedItem>();
        item.data = ownItem.data;
        item.cancelCompleted += ItemCancelCompleted;
        UIEventListener.Get(go).onClick += SelectedItemClickHandler;
        go.transform.position = ownItem.transform.position;

        selectedList.Add(item);
        selectedList.Sort(SortOrder);

        for(int i = 0, length = selectedList.Count; i < length; i++){
          ArenaAdjustHeroSelectedItem tempItem = selectedList[i];
          Vector3 tempPosition = new Vector3(-130 * i, 0, 0);
          if(item != tempItem){
            tempItem.Move(tempPosition);
          }else{
            tempItem.Arrive(tempPosition);
          }
        }
      }
    }

    /// ArenaAdjustHeroItem的排序
    private int SortOrder(ArenaAdjustHeroSelectedItem item1, ArenaAdjustHeroSelectedItem item2){
      return item1.index.CompareTo(item2.index);
    }

    /// 对已经选择的英雄处理，取消完成处理
    private void ItemCancelCompleted(ViewItem viewItem){
      ArenaAdjustHeroSelectedItem item = viewItem as ArenaAdjustHeroSelectedItem;
      ArenaHero hero = item.data as ArenaHero;

      if(cancelList.ContainsKey(hero.id)){
        cancelList.Remove(hero.id);
      }

      ArenaAdjustHeroItem ownItem = GetOwnItem(hero.id);
      hero.isSelected = false;
      ownItem.selected = false;

      Destroy(item.gameObject);
    }

    /// 拥有项点击处理
    private void SelectedItemClickHandler(GameObject go){
      ArenaAdjustHeroSelectedItem item = go.GetComponent<ArenaAdjustHeroSelectedItem>();
      ArenaHero hero = item.data as ArenaHero;
      UpdateSelectedItem(hero.id);
    }

  }
}