using System;
using UnityEngine;

namespace TangLevel
{

  /// <summary>
  /// 方向性散射
  /// </summary>
  public class DirectionalScatter : EffectorSpecialBhvr
  {

    public const float SEP_ANGLE = 10F;

    private Transform myTransform;
    private int particleCount = 3;
    private float halfSepAngle = SEP_ANGLE / 2;


    void Awake ()
    {
      myTransform = transform;
    }

    public override void Play ()
    {

      // 计算方向
      HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
      Directional sourceDirect = w.source.GetComponent<Directional> ();

      Vector3 middleDirection = Vector3.zero;
      if (sourceDirect.Direction == BattleDirection.RIGHT) {
        middleDirection = Vector3.right;
      } else {
        middleDirection = Vector3.left;
      }


      bool isEven = false;
      if (particleCount % 2 == 0) {
        isEven = true;
      }
      Quaternion middleQuat = Quaternion.LookRotation (middleDirection);
      Quaternion[] quats = new Quaternion[particleCount];
      if (isEven) {
        // 粒子的数量为偶数
        for (int i = 0; i < particleCount; i += 2) {
          quats [i] = middleQuat * Quaternion.AngleAxis (SEP_ANGLE * i + halfSepAngle, Vector3.forward);
          quats [i + 1] = middleQuat * Quaternion.AngleAxis (-SEP_ANGLE * i + halfSepAngle, Vector3.forward);
        }
      } else {
        // 粒子的数量为奇数
        quats [0] = middleQuat;
        for (int i = 1; i < particleCount; i++) {
          if (i % 2 == 0) {
            quats [i] = middleQuat * Quaternion.AngleAxis (SEP_ANGLE * i, Vector3.forward);
          } else {
            quats [i] = middleQuat * Quaternion.AngleAxis (-SEP_ANGLE * i, Vector3.forward);
          }
        }
      }

      if (quats.Length > 0) {
        // 抛出子作用器 ---
        foreach (Quaternion quat in quats) {
          SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
          foreach (Effector e in w.effector.subEffectors) {
            EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, w.target);
            cw.param = quat; // 传递当前旋转 quaternion
            sourceSkillBhvr.Cast (cw);
          }
        }
      }

      StartRelease ();

    }

  }
}

