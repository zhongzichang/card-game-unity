using System;
using System.Collections;
using UnityEngine;

namespace TangLevel
{
  /* 参数为百分比，同时提高／降低对象当前的施法前摇，后摇，技能的冷却时间。负数表示减速。*/
  // zzc: 只改CD
  [EffectAttribute(CODE)]
  public class AttackSpeedEffect : Effect
  {
    public const int CODE = 13;

    public AttackSpeedEffect() : base(CODE) {

    }

    public static void Arise (Effect effect, EffectorWrapper w)
    {

      ArrayList paramList = effect.paramList;
      float scale = (float)paramList [0];

      GameObject target = w.target;

      Hero targetHero = target.GetComponent<HeroBhvr> ().hero;

      targetHero.cd *= scale;
    }
  }
}

