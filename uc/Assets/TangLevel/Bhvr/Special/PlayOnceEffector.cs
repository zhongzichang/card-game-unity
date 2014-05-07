using System;
using Uni2DLab;
using UnityEngine;

namespace TangLevel
{
  public class PlayOnceEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 1.5F, 0);
    private Uni2DSprite sprite;
    private Uni2DSpriteAnimation animation;
    private Transform myTransform;

    void Awake ()
    {

      sprite = GetComponent<Uni2DSprite> ();
      if (sprite != null) {
        animation = sprite.spriteAnimation;
        animation.onAnimationEndEvent += OnAnimationEnd;
        if (animation.IsPlaying) {
          animation.Pause ();
        }
      }

      myTransform = transform;
    }

    void OnEnable ()
    {

      bool foundTarget = false;
      if (w != null) {
        Skill skill = w.skill;
        if (skill != null) {
          GameObject target = w.target;
          if (target != null) {

            // 绑定到目标身上
            myTransform.localPosition = target.transform.localPosition + OFFSET;
            foundTarget = true;
          }
        }
      }

      if (!foundTarget) {
        GobjManager.Release (gameObject);
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;

      if (!isPlay) {
        Resume ();
      }

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

    private void OnAnimationEnd (Uni2DAnimationEvent animationEvent)
    {
      GobjManager.Release (gameObject);
    }

    public override void Play ()
    {
      isPlay = true;
      animation.Play ();
    }

    public override void Pause ()
    {
      isPlay = false;
      animation.Pause ();
    }

    public override void Resume ()
    {
      isPlay = true;
      animation.Resume ();
    }
  }
}

