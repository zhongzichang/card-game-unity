using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI
{
  /// <summary>
  /// 地图章节面板
  /// </summary>
  public class BattleChaptersPanel : MonoBehaviour {

    public const string NAME = "BattleChaptersPanel";
    /// 星级
    public static BattleChapterStarItem StarItem;

    /// <summary>
    /// 标题
    /// </summary>
    public UISprite title;

    /// <summary>
    /// 普通副本
    /// </summary>
    public UIScrollView normalView;
    public UICenterOnChild normalUICenter;

    /// <summary>
    /// 精英副本
    /// </summary>
    public UIScrollView eliteView;
    public UICenterOnChild eliteUICenter;

    /// <summary>
    /// The chapter objects.
    /// </summary>
    public UIGrid pageIndicators;
    /// 目标动画对象
    public GameObject target;
    /// 星级对象
    public BattleChapterStarItem battleChapterStarItem;
    /// 点对象
    public UIToggle point;

    public UISprite normalButton;
    public UISprite eliteButton;

    /// 普通关卡章节对象
    public BattleChapterItem[] normalChapters = new BattleChapterItem[]{};
    /// 普通关卡章节对象
    public BattleChapterItem[] eliteChapters = new BattleChapterItem[]{};

    /// 开启的章节
    private int openChapter = 3;
    /// 开启的章节ID
    private int chapterId = 0;
    /// 参数
    private object mParam;
    private bool started;
    private bool isInit;
    private List<int> normalIds = new List<int>();
    private List<int> eliteIds = new List<int>();

  	void Start () {
      StarItem = battleChapterStarItem;
      normalUICenter.onFinished += OnNormalViewDragFinished;
      eliteUICenter.onFinished += OnEliteViewDragFinished;
      UIEventListener.Get(normalButton.gameObject).onClick += NormalButtonClickHandler;
      UIEventListener.Get(eliteButton.gameObject).onClick += EliteButtonClickHandler;
      for(int i = 0; i < normalChapters.Length; i++){
        BattleChapterItem battleChapterItem = normalChapters[i];
        for(int j = 0; j < battleChapterItem.list.Length; j++){
          UIEventListener.Get(battleChapterItem.list[j].gameObject).onClick += BattleStageItemClickHandler;
        }
      }
      target.SetActive(false);
      battleChapterStarItem.gameObject.SetActive(false);
      point.gameObject.SetActive(false);
      UpdatePoints();
      started = true;
      UpdateData();
  	}

    public object param{
      get{return this.mParam;}
      set{
        this.mParam = value;
        UpdateData();
      }
    }

    private void UpdateData(){
      if(!started){return;}
      if(mParam == null){return;}
      Init();
      BattleChaptersPanelData data = this.mParam as BattleChaptersPanelData;
      Level level = LevelCache.instance.GetLevel(data.stage);
      List<Level> list = LevelCache.instance.GetMapLevels(level.data.map_id);
      MapData mapData = LevelCache.instance.GetMapData(level.data.map_id);
      chapterId = level.data.map_id;
    }

    private void Init(){
      if(isInit){return;}
      isInit = true;
      List<MapData> normalList = new List<MapData>();//分别获取到地图数据
      List<MapData> eliteList = new List<MapData>();
      foreach(MapData data in Config.mapXmlTable.Values){
        MapType type = (MapType)data.type;
        if(type == MapType.Normal){
          normalList.Add(data);
          normalIds.Add(data.id);
        }else if(type == MapType.Elite){
          eliteList.Add(data);
          eliteIds.Add(data.id);
        }
      }
      normalIds.Sort();//地图根据ID排序
      eliteIds.Sort();
      int count = 0;
      foreach(int id in normalIds){
        if(normalChapters.Length > count){
          BattleChapterItem battleChapterItem = normalChapters[count];
          MapData mapData = LevelCache.instance.GetMapData(id);
          List<Level> list = LevelCache.instance.GetMapLevels(id);
          int c = 0;
          foreach(Level level in list){
            if(battleChapterItem.list.Length > c){
              battleChapterItem.list[c].data = level;
            }
            c++;
          }
        }
        count++;
      }
    }
  	
    /// 普通按钮点击处理
    private void NormalButtonClickHandler(GameObject go){

    }

    /// 精英按钮点击处理
    private void EliteButtonClickHandler(GameObject go){
      
    }

    /// 显示对象点击处理
    private void BattleStageItemClickHandler(GameObject go){

    }


    private void OnNormalViewDragFinished(){
      int index = GetIndexByScrollBar(normalView.horizontalScrollBar.value);
      UpdatePageIndicators(index);
    }


    private void OnEliteViewDragFinished(){
      int index = GetIndexByScrollBar(eliteView.horizontalScrollBar.value);
      UpdatePageIndicators(index);
    }

    /// 获取滑动的索引
    private int GetIndexByScrollBar(float val){
      float num = openChapter > 1 ? (openChapter - 1) : 1;
      float step = 1f / num;

      int index = (int)(val / step);

      float temp = val - index * step;
      if(temp > step / 2){//判读是否过半
        index += 1;
      }

      return index;
    }

    /// 获取地图标题图片名称
    private string GetMapTitleName(int id){
      return "maptitle-chapter" + id;
    }

    /// 更新页数指示
    private void UpdatePageIndicators(int index){
      Transform t = pageIndicators.GetChild(index);
      UIToggle toggle = t.GetComponent<UIToggle>();
      toggle.value = true;
    }

    /// 更新点的显示
    private void UpdatePoints(){
      BetterList<Transform> list = pageIndicators.GetChildList();
      if(list.size < openChapter){//小于就创建
        for(int i = 0, length = openChapter - list.size; i < length; i++){
          UIUtils.Duplicate(point.gameObject, pageIndicators.gameObject);
        }
      }else{//删除多余的pageIndicators
        int length = list.size - openChapter;
        for(int i = 0; i < length; i++){
          Destroy(list[i].gameObject);
        }
      }
      pageIndicators.repositionNow = true;
    }

  }
}