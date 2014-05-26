using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI
{
  /// <summary>
  /// 地图章节面板
  /// </summary>
  public class BattleChaptersPanel : MonoBehaviour {

    public const string NAME = "BattleChaptersPanel";

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
    /// 章节对象
    public BattleChapterItem[] chapters = new BattleChapterItem[]{};

    /// 开启的章节
    private int openChapter = 3;
    /// 参数
    private object mParam;
    private bool started;

  	void Start () {
      normalUICenter.onFinished += OnNormalViewDragFinished;
      eliteUICenter.onFinished += OnEliteViewDragFinished;
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
      BattleChaptersPanelData data = this.mParam as BattleChaptersPanelData;


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