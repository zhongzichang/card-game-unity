using System;
using Uni2DLab;
using UnityEngine;

namespace TangLevel
{
  public class PlayOnceEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3(0, 3, 0);

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
      if (effector != null) {
        Skill skill = effector.skill;
        if (skill != null) {
          GameObject target = skill.target;
          if (target != null) {

            myTransform.localPosition = target.transform.localPosition + OFFSET;
            foundTarget = true;
          }
        }
      }

      if (!foundTarget) {
        GobjManager.Release (gameObject);
      }
    }

    private void OnAnimationEnd (Uni2DAnimationEvent animationEvent)
    {
      GobjManager.Release (gameObject);
    }

    public override void Play ()
    {
      animation.Play ();
    }
  }
}

