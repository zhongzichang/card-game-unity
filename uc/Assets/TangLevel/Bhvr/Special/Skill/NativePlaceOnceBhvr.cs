using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NativePlaceOnceBhvr : SkillSpecialBhvr
  {

    public static Vector3 OFFSET = new Vector3 (0,0,1);

    private Transform myTransform;


    #region MonoMethods
    void Awake ()
    {
      myTransform = transform;
    }


    void OnEnable ()
    {

      if (w != null && w.source != null) {

        // 绑定到目标身上
        myTransform.localPosition = w.source.transform.localPosition + OFFSET;
        myTransform.localPosition = Vector3.zero;

      } else {
        StartRelease ();
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;
    }

    void OnDisable(){

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;
    }
    #endregion

    #region AnimationEvents
    public void OnAnimationEnd(){
      StartRelease ();
    }
    #endregion

    #region PublicMethods
    public override void Play ()
    {
      isPlay = true;
      animator.speed = 1F;
    }

    public override void Pause ()
    {
      isPlay = false;
      animator.speed = 0F;
    }

    public override void Resume ()
    {
      isPlay = true;
      animator.speed = 1F;
    }
    #endregion
  }
}