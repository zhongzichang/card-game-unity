using System;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using TG = TangGame;

namespace TangLevel
{
  public abstract class EffectorSpecialBhvr : SpecialBhvr
  {

    public static readonly Vector3 HURT_TEXT_OFFSET = new Vector3(0, 160, 0);

    public EffectorWrapper w;

    /// <summary>
    /// 命中目标
    /// </summary>
    public void Hit(){

      HeroBhvr heroBhvr = w.target.GetComponent<HeroBhvr> ();

      // 只有英雄还没死，才会进行伤害计算
      if (heroBhvr.hero.hp > 0) {

        // 如果作用器减少HP
        // TODO 测试用，请使用正式的伤害计算公式
        bool crit = UnityEngine.Random.Range (0F, 1F) < 0.3F ? true : false;
        int hurt = crit ? UnityEngine.Random.Range (20, 40) : UnityEngine.Random.Range (1, 20);

        heroBhvr.hero.hp -= hurt;

        // 伤害冒字
        TG.BattleTxt battleTxt = new TG.BattleTxt ();
        battleTxt.type = TG.BattleTxtType.Hurt;
        battleTxt.value = hurt;
        battleTxt.crit = crit;
        if (heroBhvr.hero.battleDirection == BattleDirection.RIGHT)
          battleTxt.self = true;
        else
          battleTxt.self = false;
        battleTxt.position = Camera.main.WorldToScreenPoint (transform.localPosition) + HURT_TEXT_OFFSET;
        Facade.Instance.SendNotification (TG.BattleCommand.BattleTxt, battleTxt);

        // TODO 测试用，MP 增加
        HeroBhvr hb = w.source.GetComponent<HeroBhvr> ();
        if (hb != null) {
          hb.hero.mp += hurt;
        }

        // HP 小于等于0时，角色死亡
        if (heroBhvr.hero.hp == 0) {
          heroBhvr.Die ();
        } else {
          // 被击打
          heroBhvr.BeBeat ();
        }
      }
    }
  }
}

