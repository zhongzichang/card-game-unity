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
        subLevel.index = i - 1;
        if (i == 1) {
          subLevel.resName = "bbg_arena";
        } else if (i == 2) {
          subLevel.resName = "bbg_beach_building";
        } else if (i == 3) {
          subLevel.resName = "bbg_blood_elf_door";
        }

        level.subLeves [subLevel.index] = subLevel;
        subLevel.enemyGroup = MockGroup ();

      }
      if (!Config.levelTable.ContainsKey (level.id))
        Config.levelTable.Add (level.id, level);
    }

    static int tmpG = 0;

    public static Group MockGroup ()
    {

      Group group = new Group ();
      int maxHeros = 5;
      if (tmpG == 3) {
        maxHeros = 5;
      } else
        maxHeros = 3;
      tmpG++;
      group.heros = new Hero[ UnityEngine.Random.Range (2, maxHeros)];
      for (int j = 0; j < group.heros.Length; j++) {

        group.heros [j] = MockHero ();
      }
      return group;
    }

    static int tmp = 0;

    public static Hero MockHero ()
    {
      Hero hero = new Hero ();  
      hero.id = tmp;
      hero.resName = "hero_zf";
      hero.maxHp = 100;
      hero.hp = 100;
      hero.maxMp = 100;
      hero.mp = 100;
      hero.attackDistance = UnityEngine.Random.Range (3, 20);
      if (tmp % 2 == 0)
        hero.ai = new string[]{ "AutoFire" };
      else
        hero.ai = new string[]{ "AutoFire" };
      tmp++;

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockAttackSkill ());
      skills.Add (MockBingHuaSkill ());
      skills.Add (MockLuoshenSkill ());
      skills.Add (MockYuehuaSkill ());
      hero.skills = skills;
      hero.skillQueue = new int[]{ 0, 1, 2, 3 };
      return hero;
    }

    public static Skill MockAttackSkill ()
    {
      Skill skill = new Skill ();
      skill.effectors = new Effector[1];
      skill.effectors[0] = MockLineFlyEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "attack";
      skill.enable = true;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockBingHuaSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors[0] = MockLineFlyEffector ();
      skill.chargeClip = "binghua0";
      skill.releaseClip = "binghua1";
      skill.enable = true;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockLuoshenSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors[0] = MockLineFlyEffector ();
      //skill.chargeClip = "";
      skill.releaseClip = "luoshen";
      skill.chargeSpecials = new string[2];
      skill.chargeSpecials[0] = "Sprite_luoshen1";
      skill.chargeSpecials[1] = "Sprite_luoshen2";
      skill.enable = true;
      skill.cd = 2;

      return skill;
    }


    public static Skill MockYuehuaSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors[0] = MockLineFlyEffector ();
      //skill.chargeClip = "";
      skill.releaseClip = "yuehua";
      skill.enable = true;
      skill.cd = 2;

      return skill;
    }
    // 线性飞行
    public static Effector MockLineFlyEffector ()
    {

      Effector effector = new Effector ();
      effector.specialName = "Sprite_binghua";

      Effector[] subEffectors = new Effector[1];
      subEffectors [0] = MockHitEffector ();
      effector.subEffectors = subEffectors;

      return effector;
    }
    // 命中
    public static Effector MockHitEffector ()
    {
      Effector effector = new Effector ();
      effector.specialName = "Sprite_binghuajizhong";
      return effector;
    }
  }
}

