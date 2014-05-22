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
          myTransform.parent = null;
          StartRelease ();
        }

      }
    }

    void OnEnable ()
    {


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

      if (w != null && w.target != null && null != w.target.GetComponentInChildren<XuanyunEffector> ()) {

        isPlay = true;
        remainTime = effectTime;
        animator.speed = 1;

        // 绑定到目标身上
        myTransform.parent = w.target.transform;
        myTransform.localPosition = OFFSET;
        // 打晕
        HeroBhvr targetHeroBhvr = w.target.GetComponent<HeroBhvr> ();
        targetHeroBhvr.BeStun (effectTime);

        Hit ();
      } else {
        myTransform.parent = null;
        StartRelease ();

      }
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