using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class ZhangfeiDahe : EffectorSpecialBhvr
  {
    public static readonly Vector3 OFFSET = new Vector3 (0, 5, 0);
    public static readonly Vector3 TARGET_OFFSET = new Vector3 (100, 5, 0);
    public const float DISAPPEAR_DISTANCE = 2;
    public const float SPEED = 60;
    private Transform myTransform;
    private Vector3 tpos = Vector3.zero;
    // Use this for initialization
    void Awake ()
    {
      myTransform = transform;
      //animator = GetComponent<Animator> ();
    }
    // Update is called once per frame
    void Update ()
    {

      if (isPlay) {

        myTransform.localScale += Vector3.one * Time.deltaTime * 2;

        float distance = Vector3.Distance (myTransform.localPosition, tpos);
        float fraction = Time.deltaTime * SPEED / distance;

        if (distance < DISAPPEAR_DISTANCE) {

          // 释放本特效资源
          Release ();
          isPlay = false;

        } else {

          Vector3 pos = Vector3.Lerp (myTransform.localPosition, tpos, fraction);
          myTransform.localPosition = pos;

          HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
          SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
          // 对方全部活着的英雄
          List<GameObject> gl = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
            LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
          // 在范围内的英雄
          gl = HeroSelector.FindTargetsWithWidth (gl, pos, 3F);
          foreach (GameObject g in gl) {

            // 判断对手状态，没在放大招或者处于眩晕状态
            HeroStatusBhvr targetStatusBhvr = g.GetComponent<HeroStatusBhvr> ();
            if (targetStatusBhvr.Status != HeroStatus.vertigo && !targetStatusBhvr.IsBigMove) {
              // 抛出作用器
              foreach (Effector e in w.effector.subEffectors) {
                EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, g);
                sourceSkillBhvr.Cast (cw);
              }
            }
          }
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

      isPlay = true;

      if (w != null && w.target != null) {

        // 初始位置在张飞的嘴巴上
        myTransform.localPosition = w.source.transform.localPosition + OFFSET;

        // 计算目标位置
        Directional directional = w.source.GetComponent<Directional> ();
        if (directional.Direction == BattleDirection.RIGHT) {
          tpos = myTransform.localPosition + TARGET_OFFSET;
        } else {
          tpos = myTransform.localPosition - TARGET_OFFSET;
        }


        myTransform.localScale = Vector3.zero;

      } else {
        Release ();
      }
    }

    public override void Pause ()
    {
      isPlay = false;
      //animator.speed = 0;
    }

    public override void Resume ()
    {
      isPlay = true;
      //animator.speed = 1;
    }
  }
}

