using System;
using UnityEngine;

namespace TangLevel
{
  public class PlaybackTest : MonoBehaviour
  {


    void Start(){
    }


    // test ............
    void OnGUI ()
    {

      if (GUI.Button (new Rect (300, 10, 150, 50), "Load Level")) {

        int[] heroIds = new int[]{ 1, 2, 3, 4 };
        // LevelController.ChallengeLevel (1, Mocker.MockGroup (heroIds));

      }
    }
    // .................

  }
}

