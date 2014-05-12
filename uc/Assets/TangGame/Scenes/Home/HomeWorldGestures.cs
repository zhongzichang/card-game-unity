using UnityEngine;
using System.Collections;

namespace TangGame
{
  public class HomeWorldGestures : MonoBehaviour
  {

    public CameraScrolling cameraScrolling;

    void OnTap (TapGesture gesture)
    { 
      if (gesture.Selection)
        Debug.Log ("Tapped object: " + gesture.Selection.name);
      else
        Debug.Log ("No object was tapped at " + gesture.Position);
    }

    void OnSwipe (SwipeGesture gesture)
    {
      // Total swipe vector (from start to end position)
      Vector2 move = gesture.Move;

      // Instant gesture velocity in screen units per second
      float velocity = gesture.Velocity;

      // Approximate swipe direction
      FingerGestures.SwipeDirection direction = gesture.Direction;

      if (cameraScrolling != null) {
        cameraScrolling.Moeve (move);
      }

    }
  }
}