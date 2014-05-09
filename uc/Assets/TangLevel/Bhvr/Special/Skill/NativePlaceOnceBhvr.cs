using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NativePlaceOnceBhvr : SkillSpecialBhvr
  {

    public static Vector3 OFFSET = new Vector3 (0,0,1);

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

      if (w != null && w.source != null) {

        // 绑定到目标身上
        myTransform.localPosition = w.source.transform.localPosition + OFFSET;
        //myTransform.localPosition = Vector3.zero;

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