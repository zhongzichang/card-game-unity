using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI{
  /// <summary>
  /// 资源副本选择面板
  /// </summary>
  public class ResourceDuplSelectPanel : ViewPanel {
    public const string NAME = "ResourceDuplSelectPanel";

    public UIEventListener closeBtn;
    public UILabel titleLabel;
    public UILabel surplusLabel;
    public TweenScale tween;
    public ResourceDuplItem[] list = new ResourceDuplItem[]{};

    private object mParam;
    private MapData mapData;

    void Start(){
      this.started = true;
      closeBtn.onClick += CloseBtnClickHandler;
      Init();
      UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;this.UpdateData();}
    }

    private void Init(){
      foreach(ResourceDuplItem item in list){
        item.onClick += ItemClickHandler;
      }
    }

    private void UpdateData(){
      tween.ResetToBeginning();
      tween.Play();
      MapType mapType = MapType.Exp;
      if(this.mParam != null){
        mapType = (MapType)this.mParam;
      }
      foreach(MapData data in Config.mapXmlTable.Values){
        MapType type = (MapType)data.type;
        if(type == mapType){
          mapData = data;
          break; 
        }
      }
      if(this.mapData == null){
        Global.LogError(">> ResourceDuplSelectPanel mapData is null, type = " + mapType);
        return;
      }
      List<Level> levelList = LevelCache.instance.GetMapLevels(mapData.id);
      levelList.Sort(SortLevel);

      int count = 0;
      foreach(ResourceDuplItem item in list){
        if(levelList.Count > count){
          item.data = levelList[count];
        }
        count++;
      }
    }

    /// List排序使用
    private int SortLevel(Level a1, Level a2){
      return a1.data.id.CompareTo(a2.data.id);
    }

    private void ItemClickHandler(ViewItem vi){
      ResourceDuplItem item = vi as ResourceDuplItem;
    }

    private void CloseBtnClickHandler(GameObject go){

    }
  }
}