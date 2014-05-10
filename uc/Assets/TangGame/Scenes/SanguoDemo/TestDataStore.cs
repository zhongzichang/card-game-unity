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

      HeroItemData data = new HeroItemData ();
      data.order = Random.Range(0, heroIds.Length);
      data.id = heroIds[data.order];
      data.rank = Random.Range(1, 10);
      data.level = Random.Range(1, 99);
      data.stars = Random.Range(1, 5);
      data.lineType = Random.Range(0, 3);
      data.hp = Random.Range(1, 100);
      data.hpMax = 100;
      data.mp = Random.Range(1, 100);
      data.mpMax = 100;
      return data;
    }

    public StageItemData RandomStage(){
      StageItemData data = new StageItemData ();
      data.desc = "这里有一个名叫New Blast的Boss。";
      data.name = "空手接白刃";
      data.id = "stage-1";
      data.maxCountById = 3;
      data.vitCost = 6;
      data.type = Random.Range(1,2);
      data.stars = Random.Range(0, 3);
      data.minLevel = Random.Range(40, 50);
      data.status = Random.Range(1,3);
      return data;
    }
  }
}
