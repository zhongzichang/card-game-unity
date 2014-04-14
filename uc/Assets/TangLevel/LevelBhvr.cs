using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class LevelBhvr : MonoBehaviour
  {

    // Use this for initialization
    void Start ()
    {
    }
    // Update is called once per frame
    void Update ()
    {

      if (LevelContext.IsWaitingForEnten && LevelContext.IsNextSubLevelResReady) {
        // enter sub level

      }
  
    }

    void OnGUI ()
    {
      if (GUI.Button (new Rect (10, 10, 150, 100), "Load Level")) {



      }

      if (GUI.Button (new Rect (200, 10, 150, 100), "Enter Level")) {


      }

      if (GUI.Button (new Rect (400, 10, 150, 100), " Left Level")) {


      }

    }
  }
}