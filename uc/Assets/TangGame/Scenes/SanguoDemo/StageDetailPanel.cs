using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class StageDetailPanel : MonoBehaviour {

    public UILabel title;
    public UILabel desc;
    public UILabel vitCost;
    public UISprite start;
    public UIGrid enemies;
    public UIGrid rewards;

    private StageItemData stageData;
  	// Use this for initialization
  	void Start () {
      UIEventListener.Get (start.gameObject).onClick += OnButtonClicked;
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

    public void Refresh(StageItemData stage){
      stageData = stage;
      title.text = stage.name;
      desc.text = stage.desc;
      vitCost.text = stage.vitCost.ToString();

      enemies.Reposition ();
      rewards.Reposition ();
    }

    private void OnButtonClicked(GameObject obj){
      Debug.Log ("OnButtonClicked" + stageData.chapterId + stageData.id);
      // 进入关卡
    }

    void OnGUI(){
      if (GUILayout.Button ("Refresh")) {
        StageItemData  data = TestDataStore.Instance.RandomStage(0, 1);
        Refresh (data);
      }
    }
  }
}