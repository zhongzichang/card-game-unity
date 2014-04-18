using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class DirectedNavAgent : MonoBehaviour
  {
    private Transform myTransform;
    // speed
    public float speed;
    // stopping distance
    public float stoppingDistance;
    // destination
    public Vector3 destination {
      private set;
      get;
    }

    public bool hasPath {
      private set;
      get;
    }
    // Use this for initialization
    void Start ()
    {
      myTransform = transform;
    }
    // Update is called once per frame
    void Update ()
    {

      if (hasPath) {

        float distance = Vector3.Distance (myTransform.localPosition, destination);
        float fraction = Time.deltaTime * speed / distance;

        if (distance <= stoppingDistance - 0.001F) {
          ResetPath ();
        } else {
          myTransform.localPosition = Vector3.Lerp (myTransform.localPosition, 
            destination, fraction);
        }
      }
    }

    public void ResetPath ()
    {
      hasPath = false;
    }

    public void SetDestination (Vector3 destination)
    {
      this.destination = destination;
      hasPath = true;
    }
  }
}