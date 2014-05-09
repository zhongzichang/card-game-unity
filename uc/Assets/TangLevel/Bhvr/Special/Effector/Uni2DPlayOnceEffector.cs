using System;
using Uni2DLab;
using UnityEngine;

namespace TangLevel
{
  public class Uni2DPlayOnceEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 1.5F, 0);
    private Uni2DSprite sprite;
    private Uni2DSpriteAnimation uni2DAnimation;
    private Transform myTransform;

    void Awake ()
    {

      sprite = GetComponent<Uni2DSprite> ();
      if (sprite != null) {
        uni2DAnimation = sprite.spriteAnimation;
        uni2DAnimation.onAnimationEndEvent += OnAnimationEnd;
        if (uni2DAnimation.IsPlaying) {
          uni2DAnimation.Pause ();
        }
      }

      myTransform = transform;
    }

    void OnEnable ()
    {

      if (w != null && w.target != null) {

        // 绑定到目标身上
        myTransform.localPosition = w.target.transform.localPosition + OFFSET;

      } else {
        GobjManager.Release (gameObject);
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

      if (isPlay) {
        Pause ();
      }
    }

    private void OnAnimationEnd (Uni2DAnimationEvent uni2DAnimationEvent)
    {
      GobjManager.Release (gameObject);
    }

    public override void Play ()
    {
      isPlay = true;
      uni2DAnimation.Play ();
    }

    public override void Pause ()
    {
      isPlay = false;
      uni2DAnimation.Pause ();
    }

    public override void Resume ()
    {
      isPlay = true;
      uni2DAnimation.Resume ();
    }
  }
}

