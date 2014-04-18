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

      Config.levelTable.Add (level.id, level);
    }

    public static Group MockGroup ()
    {

      Group group = new Group ();
      group.heros = new Hero[5];
      for (int j = 0; j < 5; j++) {
        Hero hero = new Hero ();      
        hero.resName = "hero_zf";
        hero.hp = 100;
        hero.attackDistance = j;
        group.heros [j] = hero;
      }
      return group;
    }
  }
}

