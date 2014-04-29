using System;

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
      group.heros = new Hero[1];
      for (int j = 0; j < group.heros.Length; j++) {

        group.heros [j] = MockHero();
      }
      return group;
    }


    public static Hero MockHero(){
      Hero hero = new Hero ();      
      hero.resName = "hero_zf";
      hero.hp = 100;
      hero.attackDistance = 5;
      hero.ai = new string[]{ "AutoFire" };
      return hero;
    }

    public static Skill MockSkill(){
      Skill skill = new Skill ();
      skill.specialName = "binghua";
      return skill;
    }
  }
}

