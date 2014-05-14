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
    public GameObject normalGrid;
    /// <summary>
    /// 精英副本
    /// </summary>
    public GameObject eliteGrid;

    private BetterList<ChapterItemObject> chapterObjs = new BetterList<ChapterItemObject>();

  	// Use this for initialization
  	void Start () {
  	
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

//    void OnGUI(){
//      if (GUILayout.Button ("AddCapter")) {
//        AddChapter (TestDataStore.Instance.RandomChapter (0));
//      }
//    }

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