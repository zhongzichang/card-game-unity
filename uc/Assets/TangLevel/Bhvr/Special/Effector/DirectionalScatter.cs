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

      // source behaviours
      HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
      Directional sourceDirect = w.source.GetComponent<Directional> ();

      // 计算中轴线
      Vector3 middleDirection = Vector3.right;
      if (sourceDirect.Direction == BattleDirection.LEFT) {
        middleDirection = Vector3.left;
      }

      // 是否偶数
      bool isEven = false;
      if (particleCount % 2 == 0) {
        isEven = true;
      }

      // 扇形中轴线的 rotation
      Quaternion middleQuat = Quaternion.LookRotation (middleDirection);
      Quaternion[] quats = new Quaternion[particleCount];
      if (isEven) {
        // 粒子的数量为偶数
        // 中轴线上无发射物
        int half = particleCount / 2;
        for (int i = 0; i < half; i++) {
          quats [i*2] = middleQuat * Quaternion.AngleAxis (SEP_ANGLE * i + halfSepAngle, Vector3.right);
          quats [i*2+1] = middleQuat * Quaternion.AngleAxis (-((SEP_ANGLE * i) + halfSepAngle), Vector3.right);
        }
      } else {
        // 粒子的数量为奇数
        // 中轴线上有发射物
        quats [0] = middleQuat;
        int half = (particleCount - 1) / 2;
        for (int i = 0; i < half; i++) {
          quats [i*2+1] = middleQuat * Quaternion.AngleAxis (SEP_ANGLE * (i+1), Vector3.right);
          quats [i*2+2] = middleQuat * Quaternion.AngleAxis (-SEP_ANGLE * (i+1), Vector3.right);
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

