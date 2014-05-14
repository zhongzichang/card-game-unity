using System;
using UnityEngine;

namespace TangLevel
{
  public class BigMoveBlock : MonoBehaviour
  {
    void Awake(){

      LevelController.BigMoveStart += OnBigMoveStart;
      LevelController.BigMoveEnd += OnBigMoveEnd;

      gameObject.SetActive (false);

    }

    private void OnBigMoveStart(object sender, EventArgs args){
      gameObject.SetActive (true);
    }

    private void OnBigMoveEnd(object sender, EventArgs args){
      gameObject.SetActive (false);
    }
  }
}

