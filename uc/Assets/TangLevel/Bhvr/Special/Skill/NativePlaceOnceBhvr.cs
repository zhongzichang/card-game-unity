using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NativePlaceOnceBhvr : SkillSpecialBhvr
  {
    private Transform myTransform;

    void Awake ()
    {
      myTransform = transform;
    }
    // Update is called once per frame
    void Update ()
    {
      if (isPlay) {
	
        if (!animation.isPlaying) {
          GobjManager.Release (gameObject);
        }

      }
    }

    void OnEnable ()
    {

      if (w != null && w.target != null) {

        // 绑定到目标身上
        myTransform.localPosition = w.target.transform.localPosition;

      } else {
        GobjManager.Release (gameObject);
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;
    }

    public override void Play ()
    {
      isPlay = true;
      animation.Play ();
    }

    public override void Pause ()
    {
      isPlay = false;
      foreach (AnimationState state in animation) {
        state.speed = 0;
      }
    }

    public override void Resume ()
    {
      isPlay = true;
      foreach (AnimationState state in animation) {
        state.speed = 1;
      }
    }
  }
}