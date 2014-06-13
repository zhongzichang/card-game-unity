using System;
using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class HealEffector : EffectorSpecialBhvr
  {

    void Update(){

      WaitRelease (1F);
    }

    void OnEnable ()
    {
      if (w != null && w.target != null) {
        Hit ();
      }
    }


    private IEnumerator WaitRelease (float seconds)
    {
      yield return new WaitForSeconds (seconds);
      Release();
    }

    public override void Play ()
    {
      isPlay = true;
    }

    public override void Pause ()
    {
      isPlay = false;
    }

    public override void Resume ()
    {
      isPlay = true;
    }
  }
}

