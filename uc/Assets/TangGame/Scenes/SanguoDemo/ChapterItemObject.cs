using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class ChapterItemObject : MonoBehaviour {

    /// <summary>
    /// 路径
    /// </summary>
    public UITexture path;
    /// <summary>
    /// 背景
    /// </summary>
    public UITexture background;

    public GameObject stages;
    public BetterList<StageItemObject> stageObjs = new BetterList<StageItemObject>();

    private ChapterItemData chapterData;
    public ChapterItemData ChapterData{
      get { return chapterData; } 
      set { chapterData=value; } 
    }

    public void Refresh(ChapterItemData data){
      chapterData = data;
      gameObject.name = "chapter-" + data.id.ToString();
      foreach (StageItemData stage in data.stages) {
        Debug.Log (stage.id);
        StageItemObject obj = CreateStageItemObject (stages, stage);
        stageObjs.Add (obj);
      }
    }

    private StageItemObject CreateStageItemObject(GameObject parent, StageItemData data){
      GameObject obj = NGUITools.AddChild (parent, (GameObject)Resources.Load("Prefabs/PvE/StageItemObj"));

      StageItemObject stage = (StageItemObject)obj.GetComponent<StageItemObject> ();
      stage.Refresh (data);
      /// TODO: 测试代码
      obj.transform.localPosition = new Vector3 (data.id * 160, data.id * 90, 0);
      return stage;
    }

  }
}
