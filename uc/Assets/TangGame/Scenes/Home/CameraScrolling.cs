using UnityEngine;
using System.Collections;

namespace TangGame
{
  public class CameraScrolling : MonoBehaviour
  {
    public const float PIXEL_SCALE = 0.01F;
    public Vector2 bound = new Vector2 (1024F, 615F);
    public float springiness = 4.0F;

    private Vector3 goalPosition;
    private bool needMove = false;
    private Transform myTransform;
    private Vector3 leftBound;
    private Vector3 rightBound;

    void Start ()
    {
      myTransform = transform;
      leftBound = new Vector3 (Screen.width * PIXEL_SCALE / 2, Screen.height * PIXEL_SCALE / 2, myTransform.localPosition.z);
      rightBound = new Vector3 (bound.x * PIXEL_SCALE - leftBound.x, leftBound.y, leftBound.z);
      goalPosition = leftBound;      
      myTransform.localPosition = leftBound;
    }

    void LateUpdate ()
    {

      if (needMove) {

        if (Mathf.Abs (goalPosition.x - myTransform.localPosition.x) > PIXEL_SCALE
            || Mathf.Abs (goalPosition.y - myTransform.localPosition.y) > PIXEL_SCALE) {
          myTransform.localPosition = Vector3.Lerp (myTransform.localPosition, goalPosition, Time.deltaTime * springiness);

        } else {
          needMove = false;
        }
      }
    }

    public void Moeve (Vector2 move)
    {
      goalPosition = GetGoalPosition (myTransform.localPosition - new Vector3 (move.x * PIXEL_SCALE, 0, 0));
      needMove = true;
    }

    public void Reset ()
    {
      goalPosition = myTransform.localPosition;
    }

    Vector3 GetGoalPosition (Vector3 position)
    {
      Vector3 goalPosition = position;
      if (goalPosition.x >= rightBound.x)
        goalPosition = rightBound;
      else if(goalPosition.x <= leftBound.x)
        goalPosition = leftBound;

      return goalPosition;
    }
  }
}