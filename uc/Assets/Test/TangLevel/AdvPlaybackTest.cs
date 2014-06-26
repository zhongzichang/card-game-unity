using System;
using UnityEngine;


namespace TangLevel
{
  public class AdvPlaybackTest : MonoBehaviour
  {

    void Awake(){
      Level l = Mocker.MockGrassLevel ();
      Config.levelTable.Add (l.id, l);
    }


    // test ............
    void OnGUI ()
    {

      if (GUI.Button (new Rect (300, 10, 150, 50), "Load Level")) {

        int[] heroIds = new int[]{ 1, 2, 3, 4 };
        AdventureController.ChallengeLevel (1, Mocker.MockGroup (heroIds));

      }
    }
    // .................


  }
}

