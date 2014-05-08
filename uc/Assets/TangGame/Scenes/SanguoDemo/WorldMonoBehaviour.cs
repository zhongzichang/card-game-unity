using UnityEngine;
using System.Collections;

public class WorldMonoBehaviour : MonoBehaviour {

  public GameObject pve;
	// Use this for initialization
	void Start () {
    pve.animation.wrapMode = WrapMode.PingPong;
    pve.animation.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
