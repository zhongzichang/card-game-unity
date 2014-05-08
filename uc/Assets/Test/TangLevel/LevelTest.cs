using UnityEngine;
using System.Collections;
using System;

namespace TangLevel
{
  public class LevelTest : MonoBehaviour
  {

    // Use this for initialization
    void Start ()
    {

      Mocker.Configure ();

    }


    // test ............
    void OnGUI ()
    {
      if (GUI.Button (new Rect (10, 10, 150, 50), "Load Level")) {

        LevelController.ChallengeLevel (1, Mocker.MockGroup ());

      }


      if (GUI.Button (new Rect (200, 10, 150, 50), " Next Sub Level")) {

        LevelController.ChallengeNextSubLevel ();

      }


      if (GUI.Button (new Rect (400, 10, 150, 50), " Pause")) {

        LevelController.Pause ();

      }


      if (GUI.Button (new Rect (600, 10, 150, 50), " Resume")) {

        LevelController.Resume ();

      }

      if (GUI.Button (new Rect (10, 210, 150, 50), " Left Level")) {

        LevelController.LeftLevel ();

      }
    }
    // .................
  }
}