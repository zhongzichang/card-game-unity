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

      HeroBhvr tgtHeroBhvr = w.target.GetComponent<HeroBhvr> ();
      HeroBhvr srcHeroBhvr = w.source.GetComponent<HeroBhvr> ();
      Skill skill = w.skill;

      if( tgtHeroBhvr != null && srcHeroBhvr != null && tgtHeroBhvr.hero.hp > 0 ){

        Hero srcHero = srcHeroBhvr.hero;
        Hero tgtHero = tgtHeroBhvr.hero;

        // 只有英雄还没死，才会进行伤害计算

        int hurt = (int)(srcHero.physicalAttack * skill.coefficient + skill.increment * skill.grade);
        hurt = (int)(hurt * (1 - 0.01F * tgtHero.physicalDefense / (1 + 0.01F * tgtHero.physicalDefense)));

        if (hurt != 0) {

          // 如果作用器减少HP
          bool crit = UnityEngine.Random.Range (0F, 1F) < 0.3F ? true : false;
          if (crit) {
            hurt = hurt * 2;
          }

          // 目标减少血量
          tgtHero.hp -= hurt;

          bool self = false; // 是否主攻队伍
          if (tgtHeroBhvr.hero.battleDirection == BattleDirection.RIGHT) {
            self = true;
          }
          // 屏幕坐标
          Vector3 screenPos = Camera.main.WorldToScreenPoint (w.target.transform.localPosition) + HURT_TEXT_OFFSET;
          // 伤害冒字
          BattleTextController.Bubbling (TG.BattleTxtType.Hurt, hurt, screenPos, crit, self);

          // MP 增加
          int mpInc = 0;

          // HP 小于等于0时，角色死亡
          if (tgtHero.hp == 0) {
            // 受方死亡
            tgtHeroBhvr.Die ();
            // 攻方能量增加
            mpInc = 300;
          } else {
            // 被击打
            tgtHeroBhvr.BeBeat ();
            mpInc = 200;
          }
          srcHero.mp += mpInc;
          //Vector3 scrScreenPos = Camera.main.WorldToScreenPoint (w.source.transform.localPosition) + HURT_TEXT_OFFSET;
          //BattleTextController.Bubbling(TG.BattleTxtType.Energy, mpInc, scrScreenPos, crit, self );
        }
      }
    }
  }
}

