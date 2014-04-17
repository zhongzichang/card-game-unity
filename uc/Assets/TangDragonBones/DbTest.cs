using System;
using UnityEngine;

namespace TangDragonBones
{
  public class DbTest : MonoBehaviour
  {

    void Start(){
      CharacterManager.LazyLoad ("bird");
    }

  }
}

