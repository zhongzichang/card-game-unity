using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class XuanyunEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 10F, 0);
    private Transform myTransform;
    public float effectTime = 5F;
    private float remainTime = 0;

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update ()
    {
      if (isPlay) {

        if (remainTime > 0) {
          remainTime -= Time.deltaTime;
        } else {
          StartRelease ();
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

        StartRelease ();

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

    public override void Play ()
    {
      isPlay = true;
      remainTime = effectTime;
      animator.speed = 1;
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