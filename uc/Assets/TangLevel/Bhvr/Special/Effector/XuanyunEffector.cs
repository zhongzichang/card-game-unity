using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
  public class XuanyunEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 2.5F, 0);
    public static HashSet<GameObject> vertigos = new HashSet<GameObject> ();
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

      if (w != null && w.target != null && !vertigos.Contains (w.target)) {

        // 绑定到目标身上
        myTransform.parent = w.target.transform;

        myTransform.localPosition = OFFSET;
        myTransform.localRotation = Quaternion.identity;
        Vector3 parentLocalScale = myTransform.parent.transform.localScale;
        myTransform.localScale = new Vector3(1/parentLocalScale.x, 1/parentLocalScale.y, 1);


        // 打晕
        HeroBhvr targetHeroBhvr = w.target.GetComponent<HeroBhvr> ();
        targetHeroBhvr.BeStun (effectTime);

        Hit ();

        vertigos.Add (w.target);

      } else {

        StartRelease ();

      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;

    }

    void OnDisable ()
    {
      if (w != null) {

        if (vertigos.Contains (w.target)) {
          vertigos.Remove (w.target);
        }

        // 关卡控制
        LevelController.RaisePause -= OnPause;
        LevelController.RaiseResume -= OnResume;

      }

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