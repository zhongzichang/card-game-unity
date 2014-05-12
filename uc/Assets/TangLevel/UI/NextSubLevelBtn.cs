using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NextSubLevelBtn : MonoBehaviour
  {

    // Use this for initialization
    void Start ()
    {
      UIEventListener.Get (gameObject).onClick += OnClickMe;
    }

    private void OnClickMe(GameObject g){

      // 游戏暂停，打开暂停面板
      LevelController.ChallengeNextSubLevel ();

    }
  }
}
