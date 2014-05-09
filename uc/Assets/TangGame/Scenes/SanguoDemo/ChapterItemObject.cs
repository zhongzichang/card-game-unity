using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class ChapterItemObject : MonoBehaviour {

    public BetterList<StageItemObject> stages;
    /// <summary>
    /// 标题
    /// </summary>
    public UISprite title;
    /// <summary>
    /// 路径
    /// </summary>
    public UISprite path;
    /// <summary>
    /// 背景
    /// </summary>
    public UISprite background;

    private ChapterItemData chapterData;
    public ChapterItemData ChapterData{
      get { return chapterData; } 
      set { chapterData=value; } 
    }
    public string ChapterId{
      get { return chapterData.id; } 
    }

    public void Refresh(ChapterItemData data){
      chapterData = data;
    }
  }
}
