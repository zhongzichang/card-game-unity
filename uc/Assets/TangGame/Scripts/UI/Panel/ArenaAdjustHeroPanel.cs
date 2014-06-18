using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场调整界面
  /// </summary>
  public class ArenaAdjustHeroPanel : ViewPanel {

    public UIEventListener okBtn;
    /// 英雄对象
    public ArenaAdjustHeroItem arenaAdjustHeroItem;
    /// 已经选择上阵的英雄对象
    public ArenaAdjustHeroSelectedItem arenaAdjustHeroSelectedItem;
    /// 菜单对象
    public SimpleMenuItem[] menus = new SimpleMenuItem[]{};

    private object mParam;
    private bool started;
    /// 拥有的英雄
    private Dictionary<string, ArenaAdjustHeroItem> ownList = new Dictionary<string, ArenaAdjustHeroItem>();
    /// 上阵的对象
    private Dictionary<string, ArenaAdjustHeroSelectedItem> selectedList = new Dictionary<string, ArenaAdjustHeroSelectedItem>();
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