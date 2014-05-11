using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class TestDataStore {

    static private TestDataStore _instance = null;
    private TestDataStore(){}

    static public TestDataStore Instance{
      get{
        if (_instance == null) {
            _instance = new TestDataStore ();
          }
          return _instance;
        }
    }

    /// <summary>
    /// 生成测试用随机英雄数据
    /// </summary>
    /// <returns>The hero.</returns>
    public HeroItemData RandomHero(){
      string[] heroIds = {"AV", "GoldenDragon", "NagaPriest", "SkeletonWarrior", 
        "BatRider", "Golem", "Necromancersr", "SpellBreaker",
        "Bone", "GraniteGolem", "OD", "TH",
        "CM", "Huskar", "OK", "SuicideGoblinjr",
        "Coco", "JUGG", "POM", "TK",
        "DOTsr", "KOTL", "Panda", "Tank",
        "DP", "LOA", "Pugna", "Tauren",
        "DR", "Lich", "QOP", "Tiny",
        "DoT", "Lina", "Razor", "Treant",
        "DragonBaby", "Lion", "SF", "Troll",
        "DragonTurtle", "Luna", "SG", "Ursa",
        "ES", "MeatWagon", "SNK", "VS",
        "Ench", "Med", "SP", "Viper",
        "Ghoul", "NEC", "Shaman", "WR",
        "GlaiveThrower", "Naga", "Sil", "Zeus",
        "Goblinjr", "NagaArcher", "SkeletonArcher", 
      };

      HeroItemData hero = new HeroItemData ();
      hero.order = Random.Range(0, heroIds.Length);
      hero.id = heroIds[hero.order];
      hero.rank = Random.Range(1, 10);
      hero.level = Random.Range(1, 99);
      hero.stars = Random.Range(1, 5);
      hero.lineType = Random.Range(0, 3);
      hero.hp = Random.Range(1, 100);
      hero.hpMax = 100;
      hero.mp = Random.Range(1, 100);
      hero.mpMax = 100;
      return hero;
    }

    public ChapterItemData RandomChapter(int chapterId){
      ChapterItemData chapter = new ChapterItemData ();
      chapter.id = chapterId;
      chapter.minLevel = Random.Range (40, 50);
      for (int stageId = 0; stageId < 2; stageId++) {
        StageItemData stage = RandomStage(chapterId, stageId);
        chapter.stages.Add(stage);
      }
      return chapter;
    }

    public StageItemData RandomStage(int chapterId, int stageId){
      StageItemData stage = new StageItemData ();
      stage.id = stageId;
      stage.chapterId = chapterId;
      stage.desc = "这里有一个名叫New Blast的Boss。";
      stage.name = "空手接白刃";
      stage.maxCountById = 3;
      stage.vitCost = 6;
      if (stageId == 1) {
        stage.type = 2;
        stage.stars = 2;
        stage.minLevel = 40;
        stage.status = 3;
      } else {
        stage.type = 1;
        stage.status = 2;
      }
      return stage;
    }
  }
}
