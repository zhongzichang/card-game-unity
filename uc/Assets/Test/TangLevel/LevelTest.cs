using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class LevelTest : MonoBehaviour
  {

    // Use this for initialization
    void Start ()
    {
      Mocker.Configure ();
    }
    // Update is called once per frame
    void Update ()
    {

      //if (LevelContext.IsWaitingForEnten && LevelContext.IsNextSubLevelResReady) {
      // enter sub level

      // }

    }

    void OnGUI ()
    {
      if (GUI.Button (new Rect (10, 10, 150, 100), "Load Level")) {

        LevelController.ChallengeLevel (1, Mocker.MockGroup());

      }

      if (GUI.Button (new Rect (200, 10, 150, 100), "Enter Level")) {

        LevelController.EnterNextSubLevel ();

      }

      if (GUI.Button (new Rect (400, 10, 150, 100), " Left Level")) {

        LevelController.LeftLevel ();

      }

    }
  }
}