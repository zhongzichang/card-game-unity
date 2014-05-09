using UnityEngine;
using System.Collections;

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
    public string ChapterId{
      get { return stageData.id; } 
    }

    public void Refresh(StageItemData data){
      stageData = data;
      gameObject.name = data.id;
      UpdateStatus (data.status);
      UpdateStars(data.stars);
    }

    public void UpdateStatus(int status){
      stageData.status = status;
      icon.spriteName = GetIconName (stageData); 
    }

    public void UpdateStars(int stars){
      stageData.stars = stars;
      GameObject parent = starsGrid.transform.parent.gameObject;
      if (stars == 0) {
        parent.SetActive (false);
      } else {
        starsGrid.maxPerLine = stars;
        starsGrid.Reposition ();
        parent.SetActive (true);
      }
    }

    private string GetIconName(StageItemData data){
      // 2-一次性普通关卡,
      // 1-被锁, 2-当前, 3-可用
      if (data.IsOnceType()) {
        if (data.IsLockedStatus()) {
          return "stagecircle_elite";
        } else if (data.IsCurrentStatus ()) {
          return "stagecircle_current";
        } else {
          return "stagecircle_skeleton1";
        }
      }
      if (data.IsLockedStatus()) {
        return data.id + "-locked";
      }
      return data.id;
    }

    void OnGUI(){
      if (GUILayout.Button ("Test Stage")) {
        Refresh (TestDataStore.Instance.RandomStage ());
      }
      if (GUILayout.Button ("Toggle Stars")) {
        UpdateStars(Random.Range(0,3));
      }
      if (GUILayout.Button ("Toggle Status")) {
        UpdateStatus(Random.Range(0,2));
      }
    }

  }
}
