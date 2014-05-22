using System;
using Uni2DLab;
using UnityEngine;

namespace TangLevel
{
  public class ZfBinghuajizhong : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 1.5F, 0);

    private Transform myTransform;

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }

    void OnEnable ()
    {

      if (w != null && w.target != null) {

        // 绑定到目标身上
        myTransform.localPosition = w.target.transform.localPosition + OFFSET;
        Hit ();

      } else {
        Release ();
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;

      StartCoroutine (PlayOnce ("isPlay"));

    }

    void OnDisable ()
    {

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;

    }

    private void OnAnimationEnd ()
    {
      Release ();
    }

    public override void Play ()
    {
      isPlay = true;
    }

    public override void Pause ()
    {
      isPlay = false;
      animator.speed = 10;
    }

    public override void Resume ()
    {
      isPlay = true;
      animator.speed = 1;
    }
  }
}

