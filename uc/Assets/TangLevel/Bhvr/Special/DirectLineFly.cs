﻿using UnityEngine;
using System.Collections;

namespace TangLevel
{
  /// <summary>
  /// 直线飞行 - 从一个对象的位置飞行到另一个对象的位置。
  /// 到达指定的距离后发出作用器，然后消失。
  /// </summary>
  public class DirectLineFly : EffectorSpecialBhvr
  {

    public static Vector3 OFFSET = new Vector3(0F, 1.5F, 0F);

    public float speed = 20;
    public float disappearDistance = 2;
    private Transform myTransform;
    // 源位置
    private Vector3 spos;
    // 目标位置
    private Vector3 tpos;
    // 目标
    private GameObject target;
    private bool isPlay = false;

    void Awake ()
    {
      myTransform = transform;
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
            SkillBhvr targetSkillBhvr = effector.skill.target.GetComponent<SkillBhvr> ();
            if (targetSkillBhvr != null && effector.subEffectors != null && effector.subEffectors.Length > 0) {
              targetSkillBhvr.Receive (effector.subEffectors [0], effector.skill);
            }

            // 释放本特效资源
            GobjManager.Release (gameObject);
            isPlay = false;

          } else {

            myTransform.localPosition = Vector3.Lerp (myTransform.localPosition, 
              tpos, fraction);
          }

        } else {  // 目标已消失


          // 释放本特效资源
          GobjManager.Release (gameObject);
          isPlay = false;

        }
      }
	
    }

    void OnDisable ()
    {
      spos = Vector3.zero;
      tpos = Vector3.zero;
      myTransform.localRotation = Quaternion.identity;
      myTransform.localPosition = Vector3.zero;
      isPlay = false;
    }

    public override void Play ()
    {

      if (effector.skill != null && effector.skill.source != null && effector.skill.target != null) {

        target = effector.skill.target;
        spos = effector.skill.source.transform.localPosition + OFFSET;
        tpos = effector.skill.target.transform.localPosition + OFFSET;

        // 方向
        myTransform.localRotation = Quaternion.FromToRotation (Vector3.right, 
          (tpos - new Vector3 (spos.x, spos.y, tpos.z)).normalized);

        // 位置
        myTransform.localPosition = new Vector3 (spos.x, spos.y, spos.z);

        isPlay = true;

      } else {

        GobjManager.Release (gameObject);

      }
    }
  }
}