using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class TestStageDetailPanel : MonoBehaviour {

  	// Use this for initialization
  	void Start () {
      BattleStageDetailPanel panel = GetComponent<BattleStageDetailPanel> ();
      StageItemData  data = TestDataStore.Instance.RandomStage(0, 1);
      panel.param = data;	
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}
  }
}
