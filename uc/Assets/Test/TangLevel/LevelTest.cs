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
      LevelController.Init ();
      LevelContext.InLevel = false;
      LevelContext.subLevelStatus = LevelStatus.OUT;
      Mocker.Configure ();
      LevelController.ChallengeLevel (1, Mocker.MockGroup());
      LevelController.RaiseSubLevelLoadedEvent += OnSubLevelLoaded;
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

      if (GUI.Button (new Rect (10, 210, 150, 100), " Left Level")) {

        LevelController.OnDestory ();
        LevelController.LeftLevel ();
        //Application.LoadLevel ("Home");

      }

    }

    private void OnSubLevelLoaded(object sender, EventArgs args){

      LevelController.RaiseSubLevelLoadedEvent -= OnSubLevelLoaded;
      LevelController.EnterNextSubLevel ();

    }
  }
}