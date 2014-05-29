using UnityEngine;
using System.Collections;
using System;

namespace TangLevel
{
  public class LevelTest : MonoBehaviour
  {


    // test ............
    void OnGUI ()
    {

      if (GUI.Button (new Rect (300, 10, 150, 50), "Load Level")) {

        int[] heroIds = new int[]{ 1, 2, 3, 4 };
        LevelController.ChallengeLevel (1, Mocker.MockGroup (heroIds));

      }
    }
    // .................
  }
}