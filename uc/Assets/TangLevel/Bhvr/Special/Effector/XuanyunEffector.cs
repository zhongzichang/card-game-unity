using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class XuanyunEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 10F, 0);
    //private Uni2DSprite sprite;
    //private Uni2DSpriteAnimation uni2DAnimation;
    private Transform myTransform;
    public float effectTime = 5F;
    private float remainTime = 0;

    void Awake ()
    {
      /*
      sprite = GetComponent<Uni2DSprite> ();
      if (sprite != null) {
        uni2DAnimation = sprite.spriteAnimation;
        uni2DAnimation.onAnimationEndEvent += OnAnimationEnd;
        if (uni2DAnimation.IsPlaying) {
          uni2DAnimation.Pause ();
        }
      }*/

      myTransform = transform;
    }
    // Update is called once per frame
    void Update ()
    {
      if (isPlay) {
        if (remainTime > 0) {
          remainTime -= Time.deltaTime;
        } else {
          Release ();
        }
      }
    }

    void OnEnable ()
    {

      if (w != null && w.target != null) {

        // 绑定到目标身上
        myTransform.localPosition = w.target.transform.localPosition + OFFSET;
        // 打晕
        HeroBhvr targetHeroBhvr = w.target.GetComponent<HeroBhvr> ();
        targetHeroBhvr.BeStun (effectTime);

        Hit ();

      } else {
        Release ();
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
      remainTime = effectTime;
    }

    public override void Pause ()
    {
      isPlay = false;
      //uni2DAnimation.Pause ();
    }

    public override void Resume ()
    {
      isPlay = true;
      //uni2DAnimation.Resume ();
    }
  }
}