﻿using System;
using System.Collections;
using TG = TangGame;
using UnityEngine;

namespace TangLevel
{
  /*同魔法伤害，{技能系数，升级提升参数}，通常由技能表para 传入。治疗效果不会超出最大生命值。*/
  [EffectAttribute(CODE)]
  public class HealEffect : Effect
  {
    public const int CODE = 4;
    public HealEffect() : base(CODE) {

    }
    public static void Arise (Effect effect, EffectorWrapper w)
    {

      HeroBhvr tgtHeroBhvr = w.target.GetComponent<HeroBhvr> ();
      HeroBhvr srcHeroBhvr = w.source.GetComponent<HeroBhvr> ();

      if( tgtHeroBhvr != null && srcHeroBhvr != null && tgtHeroBhvr.hero.hp > 0 ){

        Hero srcHero = srcHeroBhvr.hero;
        Hero tgtHero = tgtHeroBhvr.hero;
        Skill skill = w.skill;

        // 只有英雄还没死，才会治疗
        int heal = (int)(srcHero.magicPower * skill.coefficient + skill.increment * skill.grade);
        heal = (int)(heal * (1 - 0.01F * tgtHero.magicDefense / (1 + 0.01F * tgtHero.magicDefense)));

        if (heal != 0) {

          // 目标增加血量
          tgtHero.hp += heal;

          bool attack = false; // 是否主攻队伍
          if (tgtHeroBhvr.hero.battleDirection == BattleDirection.RIGHT) {
            attack = true;
          }
          // 屏幕坐标
          Vector3 screenPos = Camera.main.WorldToScreenPoint (w.target.transform.localPosition) + HURT_TEXT_OFFSET;
          // 伤害冒字，伤害为负就是加血
          BattleTextController.Bubbling (TG.BattleTxtType.Hurt, -heal, screenPos, false, attack);

          // TODO 测试用，MP 增加
          if (srcHeroBhvr != null) {
            srcHero.mp += 100;
          }

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
          //BattleTextController.Bubbling(TG.BattleTxtType.Energy, mpInc, scrScreenPos, crit, attack );
        }
      }


    }
  }
}

