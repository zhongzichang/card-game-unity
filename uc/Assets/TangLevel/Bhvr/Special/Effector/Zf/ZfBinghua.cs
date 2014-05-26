using UnityEngine;
using System.Collections;
using System;

namespace TangLevel
{
  /// <summary>
  /// 直线飞行 - 从一个对象的位置飞行到另一个对象的位置。
  /// 到达指定的距离后发出作用器，然后消失。
  /// </summary>
  public class ZfBinghua : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0F, 3F, 0F);
    public float speed = 30;
    public float disappearDistance = 2;
    private Transform myTransform;
    // 源位置
    private Vector3 spos;
    // 目标位置
    private Vector3 tpos;
    // 目标
    private GameObject target;

    #region MonoMethods

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }
    // Update is called once per frame
    void Update ()
    {

      if (isPlay) {


        if (target != null && target.activeSelf) { // 目标存在

          float distance = Vector3.Distance (myTransform.localPosition, tpos);

          float fraction = Time.deltaTime * speed / distance;

          if (distance < disappearDistance) {

            // 命中目标

            // 发出作用器
            SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
            if (sourceSkillBhvr != null && w.effector.subEffectors != null && w.effector.subEffectors.Length > 0) {
              EffectorWrapper cw = EffectorWrapper.W (w.effector.subEffectors [0], w.skill, w.source, w.target);
              sourceSkillBhvr.Cast (cw);
            }

            // 释放本特效资源
            Release ();
            isPlay = false;

          } else {

            myTransform.localPosition = Vector3.Lerp (myTransform.localPosition, 
              tpos, fraction);
          }

        } else {  // 目标已消失


          // 释放本特效资源
          Release ();
          isPlay = false;

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
      spos = Vector3.zero;
      tpos = Vector3.zero;
      myTransform.localRotation = Quaternion.identity;
      myTransform.localPosition = Vector3.zero;
      isPlay = false;

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;

      if (isPlay) {
        Pause ();
      }
    }

    #endregion

    #region PublicMethods

    public override void Play ()
    {

      if (w.skill != null && w.source != null && w.target != null) {

        target = w.target;
        spos = w.source.transform.localPosition + OFFSET;
        spos = new Vector3 (spos.x, spos.y, -(100 - spos.y));
        tpos = w.target.transform.localPosition + OFFSET;
        tpos = new Vector3 (tpos.x, tpos.y, -(100 - tpos.y));
        // 方向
        myTransform.localRotation = Quaternion.FromToRotation (Vector3.right, 
          (tpos - new Vector3 (spos.x, spos.y, tpos.z)).normalized);

        // 位置
        myTransform.localPosition = new Vector3 (spos.x, spos.y, spos.z);

        isPlay = true;


        StartPlayOnce ("isPlay");

      } else {

        Release ();

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

    #endregion

  }
}