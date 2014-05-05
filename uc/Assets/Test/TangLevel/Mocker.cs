using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  public class Mocker
  {
    public static void Configure ()
    {

      Level level = new Level ();
      level.id = 1;
      level.name = "Test Level 1";

      level.subLeves = new SubLevel[3];
      for (int i = 1; i < 4; i++) {
        SubLevel subLevel = new SubLevel ();
        subLevel.id = i;
        subLevel.resName = "bbg_arena";
        level.subLeves [i - 1] = subLevel;
        subLevel.enemyGroup = MockGroup ();

      }
      if (!Config.levelTable.ContainsKey (level.id))
        Config.levelTable.Add (level.id, level);
    }

    public static Group MockGroup ()
    {

      Group group = new Group ();
      group.heros = new Hero[ UnityEngine.Random.Range(2,5)];
      for (int j = 0; j < group.heros.Length; j++) {

        group.heros [j] = MockHero ();
      }
      return group;
    }

    static int tmp = 0;

    public static Hero MockHero ()
    {
      Hero hero = new Hero ();      
      hero.resName = "hero_zf";
      hero.maxHp = 100;
      hero.hp = 100;
      hero.maxMp = 100;
      hero.mp = 100;
      hero.attackDistance = UnityEngine.Random.Range(3, 20);
      if (tmp % 2 == 0)
        hero.ai = new string[]{ "AutoFire" };
      else
        hero.ai = new string[]{ "AutoFire" };
      tmp++;

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockSkill ());
      hero.skills = skills;
      return hero;
    }

    public static Skill MockSkill ()
    {
      Skill skill = new Skill ();

      skill.effector = MockLineFlyEffector ();

      return skill;
    }

    // 线性飞行
    public static Effector MockLineFlyEffector(){

      Effector effector = new Effector ();
      effector.specialName = "Sprite_binghua";

      Effector[] subEffectors = new Effector[1];
      subEffectors [0] = MockHitEffector ();
      effector.subEffectors = subEffectors;

      return effector;
    }

    // 命中
    public static Effector MockHitEffector(){
      Effector effector = new Effector ();
      effector.specialName = "Sprite_binghuajizhong";
      return effector;
    }
  }
}

