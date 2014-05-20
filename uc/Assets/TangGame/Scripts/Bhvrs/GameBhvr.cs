/**
 * Created by emacs
 * Date: 2013/10/16
 * Author: zzc
 */


using UnityEngine;
using System.Collections;


namespace TangGame
{
  
  public class GameBhvr : MonoBehaviour {

    public string nickName;

    // Use this for initialization
    void Start () {

      AutoOrientToLandScape ();
      DontDestroyOnLoad( gameObject );

    }

    void Update()
    {
      if( Input.GetKeyDown(KeyCode.Escape) )
    	{
    	  Application.Quit();
      }
  }

  public void AutoOrientToLandScape() {
    if(Vector3.Dot(Input.acceleration.normalized,new Vector3(1,0,0)) > 0.45){   
      Screen.orientation=ScreenOrientation.LandscapeRight;  
    }else if(Vector3.Dot(Input.acceleration.normalized,new Vector3(-1,0,0)) > 0.45){  
      Screen.orientation=ScreenOrientation.LandscapeLeft;   }   
    }
  }

}

