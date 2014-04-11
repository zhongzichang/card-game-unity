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

      if( LevelContext.IsWaitingForEnten && LevelContext.IsNextSubLevelResReady ){
        // enter sub level

      }
  
    }
  }

}