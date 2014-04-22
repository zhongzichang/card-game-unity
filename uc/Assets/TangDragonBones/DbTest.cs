using System;
using UnityEngine;

namespace TangDragonBones
{
  public class DbTest : MonoBehaviour
  {

    void Start(){
      DbgoManager.LazyLoad ("bird");
    }

  }
}

