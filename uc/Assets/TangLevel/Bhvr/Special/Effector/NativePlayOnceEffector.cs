using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NativePlaceOnceEffector : EffectorSpecialBhvr
  {


    /// <summary>
    /// 位置
    /// </summary>
    public enum Location {
      self, target
    }



    public Location location = Location.self;

    private Transform myTransform;

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }

    void OnEnable ()
    {

      if (w != null && w.target != null) {

        if (location == Location.self) {
          myTransform.position = w.source.transform.position;
        } else {
          myTransform.position = w.target.transform.position;
        }

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
