using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class TestStageDetailPanel : MonoBehaviour {

  	// Use this for initialization
  	void Start () {
      StageDetailPanel panel = GetComponent<StageDetailPanel> ();
      StageItemData  data = TestDataStore.Instance.RandomStage(0, 1);
      panel.Refresh (data);	
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}
  }
}
