﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI
{
  public class SelectHeroPanel : MonoBehaviour {

    public UIScrollView scrollView;
    public HeroListGrid allGrid;
    public HeroListGrid frontGrid;
    public HeroListGrid middleGrid;
    public HeroListGrid backGrid;

    public GameObject selectedGroup;
    public UIButton battleButton;
    public UILabel messageLabel;

    /// 英雄对象
    public HeroItemObject heroItemObject;
    /// 已经选择上阵的英雄对象
    public HeroSelectedItem heroSelectedItem;
    /// 菜单对象
    public SimpleMenuItem[] menus = new SimpleMenuItem[]{};
    /// 面板对象
    public GameObject[] grids = new GameObject[]{};

    /// 上阵的对象
    private Hashtable selectedHeroes = new Hashtable();
    /// 在取消队列的对象
    private Dictionary<string, HeroSelectedItem> cancelList = new Dictionary<string, HeroSelectedItem>();

    void Start(){
      this.heroItemObject.gameObject.SetActive(false);
      this.heroSelectedItem.gameObject.SetActive(false);
      UIEventListener.Get (battleButton.gameObject).onClick += OnBattleButtonClicked;
      for(int i = 0; i < menus.Length; i++){
        menus[i].onClick += MenuClickHandler;
        menus[i].index = i;
      }
      this.MenuClickHandler(menus[0]);
    }

    /// 菜单按钮点击处理
    private void MenuClickHandler(ViewItem viewItem){
      SimpleMenuItem item = viewItem as SimpleMenuItem;
      for(int i = 0; i < menus.Length; i++){
        if(menus[i] != item){
          menus[i].selected = false;
          grids[i].SetActive(false);
        }
      }
      item.selected = true;
      grids[item.index].SetActive(true);
    }

    public void AddHero(HeroItemData data){
      if(HeroStore.Instance.HasHero(data.id)){
        return;
      }
      AddHeroToList (data);
    }

    private void OnBattleButtonClicked(GameObject obj){
      int levelId = -1;
      Debug.Log ("===== ChallengeLevel ====");
      if (selectedHeroes.Count == 0) {
        StartCoroutine (ShowToast ("请选择上阵英雄"));
      }else{
        ArrayList list = new ArrayList();
        foreach(string key in selectedHeroes.Keys){
          list.Add(int.Parse(key));
        }
        // 开始战斗
        WaitingForEnterLevel ();

        int[] heroes = (int[])list.ToArray( typeof( int ));
        TangLevel.LevelController.ChallengeLevel (levelId, heroes);
      }
    }
    /// <summary>
    /// 临时补丁，等待场景加载，修改Place值，不隐藏当前主界面
    /// </summary>
    private void WaitingForEnterLevel(){
      TangPlace.PlaceController mainUI = NGUITools.GetRoot (gameObject).GetComponent<TangPlace.PlaceController> ();
      mainUI.myPlace = TangPlace.Place.level;
     
      TangLevel.LevelController.RaiseEnterLevelSuccess += OnEnterLevelSuccess;
    }

    /// <summary>
    /// 场景加载完毕后回调，隐藏当前主界面，改回Place值
    /// </summary>
    private void OnEnterLevelSuccess(object sender, System.EventArgs args){
      TangPlace.PlaceController mainUI = NGUITools.GetRoot (gameObject).GetComponent<TangPlace.PlaceController> ();
      mainUI.myPlace = TangPlace.Place.home;
      mainUI.gameObject.SetActive (false);
    }


    private IEnumerator ShowToast(string message){
      messageLabel.text = message;
      messageLabel.gameObject.SetActive (true);
      yield return new WaitForSeconds (1);
      messageLabel.gameObject.SetActive (false);
    }

    private void AddHeroToList(HeroItemData data){
      HeroStore.Instance.AddHero (data);
      HeroItemUpdateHandler handler = HeroStore.Instance.GetUpdateHandler(data.id);

      HeroListGrid heroGrid = GetHeroGrid (data);
      if (heroGrid) {
        HeroItemObject obj = CreateHeroItemObj(heroGrid.gameObject, data);
        heroGrid.AddHeroItemObject (obj);
        handler.updateToggle += heroGrid.UpdateToggle;
      }

      {
        HeroItemObject obj = CreateHeroItemObj (allGrid.gameObject, data);
        allGrid.AddHeroItemObject (obj);
        handler.updateToggle += allGrid.UpdateToggle;
      }

      handler.updateToggle += UpdateSelectedGridToggle;
    }

    /// 获取当前的激活了的Grid
    private HeroListGrid GetActiveGrid(){
      if(allGrid.gameObject.activeSelf){return allGrid;}
      if(frontGrid.gameObject.activeSelf){return frontGrid;}
      if(middleGrid.gameObject.activeSelf){return middleGrid;}
      if(backGrid.gameObject.activeSelf){return backGrid;}
      return null;
    }

    /// 对已经选择的英雄处理
    public void UpdateSelectedGridToggle (string heroId){
      if(cancelList.ContainsKey(heroId)){//在取消队列的话，不处理
        return;
      }

      HeroItemData itemData = HeroStore.Instance.GetHero(heroId);

      HeroListGrid grid = GetActiveGrid();
      if(grid == null || itemData == null){return;}

      HeroItemObject item = grid.FindHeroItemObject(heroId);

      if(selectedHeroes.ContainsKey(heroId)){//包含就取消

        HeroSelectedItem obj = selectedHeroes[heroId] as HeroSelectedItem;
        cancelList.Add(heroId, obj);//添加进取消列表中
        Vector3 tempV3 = obj.transform.localPosition;//保存当前的坐标
        if(item == null){
          obj.Cancel(tempV3, tempV3 + new Vector3(0, 300, 0));
        }else{
          obj.transform.position = item.transform.position;//设置世界坐标
          obj.Cancel(tempV3, obj.transform.localPosition);
        }
        selectedHeroes.Remove(heroId);

        List<string> list = Sort();
        for(int i = 0, length = list.Count; i < length; i++){
          string id = list[i];
          HeroSelectedItem tempItem = selectedHeroes[id] as HeroSelectedItem;
          Vector3 tempPosition = new Vector3(-130 * i, 0, 0);
          tempItem.Move(tempPosition);
        }
      }else{//不包含就添加
        HeroSelectedItem obj = this.CreateHeroSelectedItem(selectedGroup, itemData);
        obj.transform.position = item.transform.position;
        selectedHeroes[heroId] = obj;
        List<string> list = Sort();
        for(int i = 0, length = list.Count; i < length; i++){
          string id = list[i];
          HeroSelectedItem tempItem = selectedHeroes[id] as HeroSelectedItem;
          Vector3 tempPosition = new Vector3(-130 * i, 0, 0);
          if(id != heroId){
            tempItem.Move(tempPosition);
          }else{
            tempItem.Arrive(tempPosition);
          }
        }
      }
    }

    /// 排序
    private List<string> Sort(){
      Dictionary<int, string> temp = new Dictionary<int, string>();
      List<int> orders = new List<int>();
      foreach(HeroSelectedItem item in selectedHeroes.Values){
        orders.Add(item.data.order);
        if(temp.ContainsKey(item.data.order)){
          Global.LogError(">> SelectedHeroGrid Hero order is repeat. heroId = " + item.data.id);
        }
        temp[item.data.order] = item.data.id;
      }
      orders.Sort();
      List<string> result = new List<string>();
      for(int i = 0, length = orders.Count; i < length; i++){
        result.Add(temp[orders[i]]);
      }
      return result;
    }

    /// 对已经选择的英雄处理，取消完成处理
    private void ItemCancelCompleted(ViewItem item){
      HeroSelectedItem obj = item as HeroSelectedItem;
      if(cancelList.ContainsKey(obj.data.id)){
        cancelList.Remove(obj.data.id);
      }
      Destroy(obj.gameObject);
    }

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = obj.GetComponent<HeroItemObject> (); 
      HeroStore.Instance.UpdateToggle (hero.HeroId);
    }

    private void OnSelectedItemClicked(GameObject obj){
      HeroSelectedItem hero = obj.GetComponent<HeroSelectedItem> (); 
      HeroStore.Instance.UpdateToggle (hero.HeroId);
    }

    /// 创建英雄对象
    private HeroItemObject CreateHeroItemObj(GameObject parent, HeroItemData data){
      GameObject hero = NGUITools.AddChild (parent, this.heroItemObject.gameObject);
      hero.SetActive(true);
      HeroItemObject obj = (HeroItemObject)hero.GetComponent<HeroItemObject> ();
      obj.Refresh (data);

      UIEventListener.Get (obj.gameObject).onClick += OnItemClicked;

      // 添加DragScrollView
      UIDragScrollView dragScrollView = hero.AddComponent<UIDragScrollView> ();
      dragScrollView.scrollView = scrollView;

      // 刷新
      UIGrid grid = parent.GetComponent<UIGrid> ();
      grid.Reposition ();

      return obj;
    }

    /// 创建选择的英雄对象
    private HeroSelectedItem CreateHeroSelectedItem(GameObject parent, HeroItemData data){
      GameObject hero = NGUITools.AddChild (parent, this.heroSelectedItem.gameObject);
      hero.SetActive(true);
      HeroSelectedItem obj = hero.GetComponent<HeroSelectedItem>();
      obj.cancelCompleted += ItemCancelCompleted;
      obj.data = data;
      
      UIEventListener.Get (obj.gameObject).onClick += OnSelectedItemClicked;
      return obj;
    }

    /// 获取英雄该添加到的对象
    private HeroListGrid GetHeroGrid(HeroItemData data){
      if (data.IsFront()) return frontGrid;
      if (data.IsMiddle()) return middleGrid;
      if (data.IsBack()) return backGrid;
      return null;
    }
  }
}