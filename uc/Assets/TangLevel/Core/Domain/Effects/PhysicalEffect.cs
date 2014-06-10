using System;
using System.Collections;
using TG = TangGame;
using UnityEngine;
using PureMVC.Core;
using PureMVC.Patterns;
namespace TangLevel
{
  /*
    * 无参数。物理伤害公式：
    * 物理伤害＝物理攻击＊（1-0.01*护甲／（1+0.01*护甲））
  */
  [EffectAttribute(CODE)]
  public class PhysicalEffect : Effect
  {
    public const int CODE = 1;
    public PhysicalEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {

      HeroBhvr heroBhvr = w.target.GetComponent<HeroBhvr> ();

      // 只有英雄还没死，才会进行伤害计算
      if (heroBhvr.hero.hp > 0) {

        // 如果作用器减少HP
        // TODO 测试用，请使用正式的伤害计算公式
        bool crit = UnityEngine.Random.Range (0F, 1F) < 0.3F ? true : false;
        int hurt = crit ? UnityEngine.Random.Range (50, 100) : UnityEngine.Random.Range (1, 50);

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
        battleTxt.position = Camera.main.WorldToScreenPoint (w.target.transform.localPosition) + HURT_TEXT_OFFSET;
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

