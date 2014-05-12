using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class ChapterPanel : MonoBehaviour {

    public GameObject chaptersGrid;
    private BetterList<ChapterItemObject> chapterObjs = new BetterList<ChapterItemObject>();

  	// Use this for initialization
  	void Start () {
  	
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}


    private void AddChapter(ChapterItemData data){
      ChapterItemObject obj = CreateChapterItemObject (chaptersGrid, data);
    }

    private ChapterItemObject CreateChapterItemObject(GameObject parent, ChapterItemData data){
      GameObject obj = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/ChapterItemObj"));

      ChapterItemObject chapter = (ChapterItemObject)obj.GetComponent<ChapterItemObject> ();
      chapter.Refresh (data);

      return chapter;
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