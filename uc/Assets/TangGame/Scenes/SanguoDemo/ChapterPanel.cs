using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class ChapterPanel : MonoBehaviour {

    /// <summary>
    /// 标题
    /// </summary>
    public UITexture title;

    /// <summary>
    /// 普通副本
    /// </summary>
    public UIScrollView normalView;
    /// <summary>
    /// 精英副本
    /// </summary>
    public UIScrollView eliteView;

    /// <summary>
    /// The chapter objects.
    /// </summary>
    public UIGrid pageIndicators;

    private BetterList<ChapterItemObject> chapterObjs = new BetterList<ChapterItemObject>();

  	// Use this for initialization
  	void Start () {
      normalView.onDragFinished += OnNormalViewDragFinished;
      eliteView.onDragFinished += OnEliteViewDragFinished;
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

//    void OnGUI(){
//      if (GUILayout.Button ("AddCapter")) {
//        AddChapter (TestDataStore.Instance.RandomChapter (0));
//      }
//    }

    private void OnNormalViewDragFinished(){
      int val = GetIndexByScrollBar (normalView.horizontalScrollBar.value);
      title.mainTexture = GetMapTitle (val);
      UpdatePageIndicators (val);
    }

    private void OnEliteViewDragFinished(){
      int val = GetIndexByScrollBar (eliteView.horizontalScrollBar.value);
      title.mainTexture = GetMapTitle (val);
      UpdatePageIndicators (val);
    }

    private int GetIndexByScrollBar(float val){
      if (val < 0.5f) {
        return 1;
      }
      return 2;
    }

    private Texture GetMapTitle(int val){
      string maptitlePath = "Textures/BattleChapters/Title/maptitle-chapter" + val.ToString();
      Debug.Log (maptitlePath);
      return Resources.Load (maptitlePath) as Texture;
    }

    private void UpdatePageIndicators(int val){
      Transform t = pageIndicators.GetChild(val-1);
      Debug.Log (t.name);
      UIToggle toggle = t.GetComponent<UIToggle>();
      toggle.value = true;
    }

    private void UpdateStatus (int chapterId, int stageId, int status){
      ChapterItemObject chapter = (ChapterItemObject)chapterObjs [chapterId];
      if (chapter == null)
        return;

      StageItemObject stage = (StageItemObject) chapter.stageObjs [stageId];
      if (stage == null)
        return;

      stage.UpdateStatus (status);
    }
  }
}