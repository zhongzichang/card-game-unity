using UnityEngine;
using System.Collections;

public class FixedAspectCamera : MonoBehaviour {

  void Awake(){

    if (camera != null) {

      Debug.Log("dddffff  "+camera.aspect);
      camera.aspect = 1024 / 615f;
      Debug.Log("dddffff dddfffff "+camera.aspect+"   "+Screen.width+"   "+Screen.height);
    }
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
