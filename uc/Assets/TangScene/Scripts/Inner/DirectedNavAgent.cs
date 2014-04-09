using UnityEngine;
using System.Collections;


namespace TangScene {

  public class DirectedNavAgent : MonoBehaviour {

    // speed
    public float speed;
    // stopping distance
    public float stoppingDistance;
    // destination
    public Vector3 destination{
      private set;
      get;
    }

    public bool hasPath {
      private set;
      get;
    }



  	// Use this for initialization
  	void Start () {
  	  
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	  
  	}


    public void ResetPath (){
      hasPath = false;
    }

    public void SetDestination (Vector3 destination) {
      this.destination = destination;
      hasPath = true;
    }
    
  }

}