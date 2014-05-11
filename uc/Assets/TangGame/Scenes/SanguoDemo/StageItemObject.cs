using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class StageItemObject : MonoBehaviour {

    public UISprite icon_small;
    public UISprite icon_big;
    public UIGrid starsGrid;

    private StageItemData stageData;

    public GameObject stage_small;
    public GameObject stage_big;

    public StageItemData StageData{
      get { return stageData; } 
      set { stageData=value; } 
    }

    public void Start(){
      UIEventListener.Get(icon_small.gameObject).onClick += OnItemClicked;
      UIEventListener.Get(icon_big.gameObject).onClick += OnItemClicked;
    }

    public void Refresh(StageItemData data){
      stageData = data;
      gameObject.name = "stage-" + data.id.ToString();
      UpdateStatus (data.status);
      UpdateStars(data.stars);
    }

    private void OnItemClicked(GameObject obj){
      StageItemObject stage = (StageItemObject)gameObject.GetComponent<StageItemObject> (); 
      Debug.Log ("OnItemClicked" + stage.StageData.chapterId.ToString() + stage.StageData.id);
      // 显示关卡详细信息
    }

    public void UpdateStatus(int status){
      stageData.status = status;
      UpdateIcon (stageData); 
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

    private void UpdateIcon(StageItemData data){
      // 2-一次性普通关卡,
      // 1-被锁, 2-当前, 3-可用
      if (data.IsOnceType ()) {
        if (data.IsLockedStatus ()) {
          icon_small.spriteName = "stagecircle_elite";
        } else if (data.IsCurrentStatus ()) {
          icon_small.spriteName = "stagecircle_current";
        } else {
          icon_small.spriteName = "stagecircle_skeleton1";
          BoxCollider collider = icon_small.GetComponent<BoxCollider> ();
          collider.enabled = false;
        }
        stage_small.SetActive (true);
        stage_big.SetActive (false);
      } else {
        if (data.IsLockedStatus ()) {
          icon_big.spriteName = "stage-" + data.id.ToString () + "-locked";
          BoxCollider collider = icon_big.GetComponent<BoxCollider> ();
          collider.enabled = false;
        } else {
          icon_big.spriteName = "stage-" + data.id.ToString ();
        }
        stage_small.SetActive (false);
        stage_big.SetActive (true);
      }
    }
  }
}
