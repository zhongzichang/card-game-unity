using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class ChapterPanel : MonoBehaviour {

    public GameObject tag;
    public ArrayList chapters;

    private Hashtable chapterObjs = new Hashtable();

  	// Use this for initialization
  	void Start () {
  	
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

//    private void AddHeroToList(HeroItemData data){
//      HeroStore.Instance.AddHero (data);
//      HeroItemUpdateHandler handler = HeroStore.Instance.GetUpdateHandler(data.id);
//
//      {
//        HeroItemObject obj = CreateHeroItemObj (selectedGrid.gameObject, data);
//        heroObjs [obj.HeroId] = obj;
//        handler.updateMp += UpdateMp;
//      }
//    }

    private void UpdateStatus (string stageId, int status){
      StageItemObject stage = (StageItemObject) chapterObjs [stageId];
      if (stage == null)
        return;
      stage.UpdateStatus (status);
    }

    private void OnItemClicked(GameObject obj){
      StageItemObject hero = (StageItemObject)obj.GetComponent<StageItemObject> (); 
      Debug.Log ("OnItemClicked");
    }
  }
}