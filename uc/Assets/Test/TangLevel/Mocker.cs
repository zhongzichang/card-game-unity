using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  public class Mocker
  {
    public static void Configure ()
    {
      Level level = MockGrassLevel ();
      if (!Config.levelTable.ContainsKey (level.id))
        Config.levelTable.Add (level.id, level);
    }

    public static Level MockGrassLevel(){

      Level level = new Level ();
      level.id = 1;
      level.name = "Test Level 1";

      level.subLeves = new SubLevel[3];
      for (int i = 1; i < 4; i++) {
        SubLevel subLevel = new SubLevel ();
        subLevel.id = i;
        subLevel.index = i - 1;
        if (i == 1) {
          subLevel.resName = "lbg_grass1";
        } else if (i == 2) {
          subLevel.resName = "lbg_grass2";
        } else if (i == 3) {
          subLevel.resName = "lbg_grass3";
        }

        level.subLeves [subLevel.index] = subLevel;
        subLevel.enemyGroup = MockGroup ();

      }
      return level;
    }

    static int tmpG = 0;

    public static Group MockGroup(int count){

      Group group = new Group ();
      group.heros = new Hero[ count];
      tmpG++;
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

    public static Group MockGroup(int[] heroIds){
      Group group = new Group ();
      group.heros = new Hero[heroIds.Length];
      for (int i = 0; i < group.heros.Length; i++) {
        if (heroIds [i] == 1) {
          group.heros [i] = MockHeroZf ();
        } else if (heroIds [i] == 2) {
          group.heros [i] = MockHeroXc ();
        } else {
          group.heros [i] = MockHeroZhangfei ();
        }
      }
      return group;
    }

    public static Group MockGroup ()
    {

      Group group = new Group ();
      int maxHeros = 3;
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
      hero.id = 1;
      hero.resName = "hero_zf";
      hero.maxHp = 100;
      hero.hp = 100;
      hero.maxMp = 100;
      hero.mp = 0;
      hero.sort = 3;//UnityEngine.Random.Range (1, 20);
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
      skill.distance = 20;
      skill.cd = 3;

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
      skill.distance = 20;
      skill.cd = 3;

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
      skill.distance = 20;
      skill.cd = 3;

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
      skill.distance = 20;
      skill.cd = 3;

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
      hero.id = 2;
      hero.resName = "hero_xc";
      hero.maxHp = 120;
      hero.hp = 120;
      hero.maxMp = 100;
      hero.mp = 0;
      hero.sort = 1;//UnityEngine.Random.Range (1, 20);
      hero.ai = new string[]{ "AutoFire", "JumpRunning" };

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockXcAttackSkill ());
      skills.Add (MockQianjinzhuiSkill ());
      skills.Add (MockZhongjiSkill ());
      skills.Add (MockYemansicheSkill ());
      hero.skills = skills;
      hero.skillQueue = new int[]{ 0, 2, 3 };


      tmp++;
      return hero;
    }

    public static Skill MockXcAttackSkill ()
    {
      Skill skill = new Skill ();
      skill.effectors = new Effector[1];
      skill.effectors [0] = MockHitEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "attack";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }

    public static Skill MockQianjinzhuiSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors[0] = MockHitEffector ();
      skill.chargeClip = "qianjinzhui";
      skill.releaseClip = "qianjinzhui1";
      skill.enable = true;
      skill.bigMove = true;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }

    public static Skill MockZhongjiSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors[0] = MockHitEffector ();
      skill.chargeClip = "zhongji";
      skill.releaseClip = "zhongji1";
      //skill.releaseSpecials = new string[2];
      //skill.releaseSpecials[0] = "Sprite_luoshen1";
      //skill.releaseSpecials[1] = "Sprite_luoshen2";
      skill.enable = true;
      skill.bigMove = false;
      skill.chargeTime = 2F;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }

    public static Skill MockYemansicheSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockHitEffector ();
      skill.chargeClip = "yemansiche";
      skill.releaseClip = "yemansiche1";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }
    #endregion
    // mock hero zhangfei -----------------------------------------------------
    #region zhangfei
    public static Hero MockHeroZhangfei ()
    {
      Hero hero = new Hero ();  
      hero.id = 3;
      hero.resName = "hero_zhangfei";
      hero.maxHp = 120;
      hero.hp = 120;
      hero.maxMp = 100;
      hero.mp = 0;
      hero.sort = 2;//UnityEngine.Random.Range (1, 20);
      hero.ai = new string[]{ "AutoFire" };

      // skill
      List<Skill> skills = new List<Skill> ();
      skills.Add (MockZhangfeiAttackSkill ());
      skills.Add (MockShenliSkill ());
      skills.Add (MockBaqiSkill ());
      skills.Add (MockDaheSkill ());
      hero.skills = skills;
      hero.skillQueue = new int[]{ 0, 2, 3 };


      tmp++;
      return hero;
    }

    public static Skill MockZhangfeiAttackSkill ()
    {
      Skill skill = new Skill ();
      skill.effectors = new Effector[1];
      skill.effectors [0] = MockHitEffector ();
      //skill.chargeClip = "binghua0";
      skill.releaseClip = "attack";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }

    public static Skill MockShenliSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockShenliEffect ();
      //skill.chargeClip = "shenli";
      skill.releaseClip = "shenli";
      skill.enable = true;
      skill.bigMove = true;
      skill.distance = 3;
      skill.cd = 3;

      return skill;
    }

    public static Skill MockBaqiSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors[0] = MockHitEffector ();
      skill.chargeClip = "baqi";
      skill.releaseClip = "baqi1";
      //skill.releaseSpecials = new string[2];
      //skill.releaseSpecials[0] = "Sprite_luoshen1";
      //skill.releaseSpecials[1] = "Sprite_luoshen2";
      skill.enable = true;
      skill.bigMove = false;
      skill.chargeTime = 2F;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }

    public static Skill MockDaheSkill ()
    {
      Skill skill = new Skill ();

      skill.effectors = new Effector[1];
      skill.effectors [0] = MockDaheEffect ();
      skill.chargeClip = "dahe";
      skill.releaseClip = "dahe1";
      skill.enable = true;
      skill.bigMove = false;
      skill.distance = 10;
      skill.cd = 3;

      return skill;
    }

    public static Effector MockDaheEffect(){


      Effector effector = new Effector ();
      effector.specialName = "fx_dahe";
      effector.subEffectors = new Effector[1];
      effector.subEffectors[0] = MockXuanyunEffect ();

      return effector;
    }


    public static Effector MockShenliEffect(){


      Effector effector = new Effector ();
      effector.specialName = "fx_shenli";

      return effector;
    }
    #endregion

    #region commonfx
    public static Effector MockXuanyunEffect(){

      Effector effector = new Effector ();
      effector.specialName = "fx_xuanyun";

      return effector;
    }
    #endregion
  }
}

