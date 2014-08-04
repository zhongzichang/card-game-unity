using System;
using UnityEngine;
using TP = TangLevel.Playback;
using System.Collections.Generic;

namespace TangLevel
{
  public class PlaybackTest : MonoBehaviour
  {



    // test ............
    void OnGUI ()
    {

      if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 60, 100, 50), "Playback")) {

        int[] heroIds = new int[]{ 1, 2, 3, 4 };
        // LevelController.ChallengeLevel (1, Mocker.MockGroup (heroIds));

        if (Cache.recordTable.Count > 0) {

          TP.LevelRecord record = null;
          foreach (TP.LevelRecord r in Cache.recordTable.Values) {
            record = r;
            break;
          }

          TP.PlaybackController.Play (record);

        }

      }
    }
    // .................

  }
}

