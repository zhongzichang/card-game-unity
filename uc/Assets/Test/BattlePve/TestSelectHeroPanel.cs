using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  public class TestSelectHeroPanel : MonoBehaviour {

  	// Use this for initialization
  	void Start () {
      StartCoroutine (Setup ());
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	
  	}

    IEnumerator Setup(){
      yield return 0;
      SelectHeroPanel panel = GetComponent<SelectHeroPanel> ();

      panel.AddHero (TestDataStore.Instance.RandomHero("1"));
      panel.AddHero (TestDataStore.Instance.RandomHero("2"));
      panel.AddHero (TestDataStore.Instance.RandomHero("3"));
      panel.AddHero (TestDataStore.Instance.RandomHero("4"));
    }
  }
}