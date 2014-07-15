using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NativePlaceOnceEffector : EffectorSpecialBhvr
  {

    private Transform myTransform;

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }

    void OnEnable ()
    {

      if (w != null && w.target != null) {

        // 定位到目标身上
        //myTransform.localPosition = w.target.transform.localPosition;
        // 定位到自己身上
        //myTransform.localPosition = w.source.transform.localPosition;
        Hit ();

      } else {
        StartRelease();
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;


    }

    void OnDisable ()
    {

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;

    }

    private void OnAnimationEnd ()
    {
      StartRelease ();
    }

    public override void Play ()
    {
      isPlay = true;
      StartPlayOnce ("isPlay");
    }

    public override void Pause ()
    {
      isPlay = false;
      animator.speed = 0;
    }

    public override void Resume ()
    {
      isPlay = true;
      animator.speed = 1;
    }
  }
}
