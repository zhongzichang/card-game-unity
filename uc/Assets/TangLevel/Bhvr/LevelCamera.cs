using System;
using UnityEngine;

namespace TangLevel
{
  public class LevelCamera : MonoBehaviour
  {
    void Start(){

      camera.aspect = 1024F / 615F;
    }
  }
}

