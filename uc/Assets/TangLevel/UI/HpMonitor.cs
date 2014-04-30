using UnityEngine;
using System.Collections;


public class HpMonitor : MonoBehaviour {

  private UISprite sprite;

  void Start(){
    sprite = GetComponent<UISprite> ();
  }
	
  public void OnChange(int val, int max){
    if (sprite != null) {
      sprite.fillAmount = ((float)val) / max;
    }
  }

}
