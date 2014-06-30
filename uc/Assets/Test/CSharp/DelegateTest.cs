using System;
using UnityEngine;

namespace test
{
  public class DelegateTest : MonoBehaviour
  {
    /// <summary>
    /// 成功进入关卡
    /// </summary>
    public static event EventHandler handler;

    void Start(){

      handler += OnHandler;
      handler += OnHandler;

      handler (this, EventArgs.Empty);

    }

    private void OnHandler(object sender, EventArgs args){
      Debug.Log ("being called.");
    }

  }
}

