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
        if (j % 3 == 0)
          group.heros [j] = MockHeroZf ();
        else if (j % 3 == 1)
          group.heros [j] = MockHeroXc ();
        else if (j % 3  == 2) {
          group.heros [j] = MockHeroZhangfei ();
        }
      }
      return group;
    }

    static int tmp = 0;
    // mock hero zf -----------------------------------------------------
    #region zf
    public static Hero MockHeroZf ()
    {
      Hero hero = new Hero ();  
      hero.id = tmp;
      hero.resName = "hero_zf";
      hero.maxHp = 100;
      hero.hp = 100;
      hero.maxMp = 100;
      hero.mp = 0;
      hero.sort = 1;//UnityEngine.Random.Range (1, 20);
      if (tmp % 2 == 0)
        hero.ai = new string[]{ "AutoFire" };
      else
        hero.ai = new string[]{ "AutoFire" };

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockZfAttackSkill ());
      skills.Add (MockBingHuaSkill ());
      skills.Add (MockYuehuaSkill ());
      //if( hero.id == 0)
      skills.Add (MockLuoshenSkill ());
      hero.skills = skills;
      hero.skillQueue = new int[]{ 0, 1, 2 };


      tmp++;
      return hero;
    }

    public static Skill MockZfAttackSkill ()
    {
      Skill skill = new Skill ();
      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "attack";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockBingHuaSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      skill.chargeClip = "binghua0";
      skill.releaseClip = "binghua1";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockYuehuaSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      //skill.chargeClip = "";
      skill.releaseClip = "yuehua";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockLuoshenSkill ()
    {
      Skill skill = new Skill ();

      //skill.effectors = new Effector[1];
      //skill.effectors[0] = MockLineFlyEffector ();
      skill.chargeClip = "luoshen";
      //skill.releaseClip = "luoshen";
      //skill.releaseSpecials = new string[2];
      //skill.releaseSpecials[0] = "Sprite_luoshen1";
      //skill.releaseSpecials[1] = "Sprite_luoshen2";
      skill.enable = true;
      skill.bigMove = true;
      skill.chargeTime = 2F;
      skill.distance = 10;
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
    #endregion
    // mock hero xc -----------------------------------------------------
    #region xc
    public static Hero MockHeroXc ()
    {
      Hero hero = new Hero ();  
      hero.id = tmp;
      hero.resName = "hero_xc";
      hero.maxHp = 100;
      hero.hp = 100;
      hero.maxMp = 100;
      hero.mp = 0;
      hero.sort = 1;//UnityEngine.Random.Range (1, 20);
      hero.ai = new string[]{ "AutoFire" };

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockXcAttackSkill ());
      skills.Add (MockQianjinzhuiSkill ());
      skills.Add (MockZhongjiSkill ());
      skills.Add (MockYemansicheSkill ());
      hero.skills = skills;
      hero.skillQueue = new int[]{ 0, 1, 2 };


      tmp++;
      return hero;
    }

    public static Skill MockXcAttackSkill ()
    {
      Skill skill = new Skill ();
      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "attack";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockQianjinzhuiSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "qianjinzhui";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockZhongjiSkill ()
    {
      Skill skill = new Skill ();

      //skill.effectors = new Effector[1];
      //skill.effectors[0] = MockLineFlyEffector ();
      skill.chargeClip = "zhongji";
      //skill.releaseClip = "luoshen";
      //skill.releaseSpecials = new string[2];
      //skill.releaseSpecials[0] = "Sprite_luoshen1";
      //skill.releaseSpecials[1] = "Sprite_luoshen2";
      skill.enable = true;
      skill.bigMove = false;
      skill.chargeTime = 2F;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockYemansicheSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      //skill.chargeClip = "";
      skill.releaseClip = "yemansiche";
      skill.enable = true;
      skill.bigMove = true;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }
    #endregion
    // mock hero zhangfei -----------------------------------------------------
    #region zhangfei
    public static Hero MockHeroZhangfei ()
    {
      Hero hero = new Hero ();  
      hero.id = tmp;
      hero.resName = "hero_zhangfei";
      hero.maxHp = 100;
      hero.hp = 100;
      hero.maxMp = 100;
      hero.mp = 0;
      hero.sort = 1;//UnityEngine.Random.Range (1, 20);
      hero.ai = new string[]{ "AutoFire" };

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockZhangfeiAttackSkill ());
      skills.Add (MockShenliSkill ());
      skills.Add (MockBaqiSkill ());
      skills.Add (MockDaheSkill ());
      hero.skills = skills;
      hero.skillQueue = new int[]{ 0, 1, 2 };


      tmp++;
      return hero;
    }

    public static Skill MockZhangfeiAttackSkill ()
    {
      Skill skill = new Skill ();
      //skill.effectors = new Effector[1];
      //skill.effectors [0] = MockLineFlyEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "attack";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockShenliSkill ()
    {
      Skill skill = new Skill ();

      //skill.effectors = new Effector[1];
      //skill.effectors [0] = MockLineFlyEffector ();
      skill.chargeClip = "shenli";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockBaqiSkill ()
    {
      Skill skill = new Skill ();

      //skill.effectors = new Effector[1];
      //skill.effectors[0] = MockLineFlyEffector ();
      //skill.chargeClip = "baqi";
      skill.releaseClip = "baqi";
      //skill.releaseSpecials = new string[2];
      //skill.releaseSpecials[0] = "Sprite_luoshen1";
      //skill.releaseSpecials[1] = "Sprite_luoshen2";
      skill.enable = true;
      skill.bigMove = false;
      skill.chargeTime = 2F;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }

    public static Skill MockDaheSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockLineFlyEffector ();
      skill.chargeClip = "Dahe";
      //skill.releaseClip = "Dahe";
      skill.enable = true;
      skill.bigMove = true;
      skill.distance = 4;
      skill.cd = 2;

      return skill;
    }
    #endregion
  }
}

