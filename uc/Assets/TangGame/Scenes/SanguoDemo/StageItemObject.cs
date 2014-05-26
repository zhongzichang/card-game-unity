using UnityEngine;
using System.Collections;
using TangUI;

namespace TangGame.UI
{
  public class StageItemObject : MonoBehaviour {

    public UISprite icon;
    public UIGrid starsGrid;

    private StageItemData stageData;
    public StageItemData StageData{
      get { return stageData; } 
      set { stageData=value; } 
    }

    public void Start(){
      UIEventListener.Get(icon.gameObject).onClick += OnItemClicked;
    }

    public void Refresh(StageItemData data){
      stageData = data;
      gameObject.name = "stage-" + data.id.ToString();
      UpdateStatus (data.status);
      UpdateStars(data.stars);
    }

    private void OnItemClicked(GameObject obj){
			StageItemObject stage = (StageItemObject)gameObject.GetComponent<StageItemObject> ();  
			// 显示关卡详细信息
      UIContext.mgrCoC.LazyOpen (UIContext.BATTLE_STAGE_DETAIL_PANEL_NAME, UIPanelNode.OpenMode.OVERRIDE, UIPanelNode.BlockMode.TEXTURE, stage);
    }

    public void UpdateStatus(int status){
      stageData.status = status;
//      icon.spriteName = ""; 
    }

    public void UpdateStars(int stars){
      stageData.stars = stars;
      if (stageData.IsOnceType ())
        return;

      GameObject parent = starsGrid.transform.parent.gameObject;
      if (stars == 0) {
        parent.SetActive (false);
      } else {
        starsGrid.maxPerLine = stars;
        starsGrid.Reposition ();
        parent.SetActive (true);
      }
    }
  }
}
