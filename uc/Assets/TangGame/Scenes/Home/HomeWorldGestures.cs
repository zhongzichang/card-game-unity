using UnityEngine;
using System.Collections;
using TangPlace;
namespace TangGame
{
  public class HomeWorldGestures : MonoBehaviour
  {
    public CameraScrolling cameraScrolling;

    void OnTap (TapGesture gesture)
    { 
      if (gesture.Selection) {

        // 测试代码
        if ("Sprite_main_pve_title_press".Equals (gesture.Selection.name)) {
          PlaceController.Place = Place.level;
        }
      }
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