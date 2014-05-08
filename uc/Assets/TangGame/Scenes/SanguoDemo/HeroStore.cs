using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemUpdateHandler
  {
    public delegate void UpdateToggle(string heroId);
    public delegate void UpdateMp(string heroId, int mp);

    public UpdateToggle updateToggle;
    public UpdateMp updateMp;
  }

  public class HeroStore {

    private Hashtable heroes = new Hashtable();
    private Hashtable handlers = new Hashtable();

    static private HeroStore _instance = null;
    private HeroStore(){}

    static public HeroStore Instance{
      get{
        if (_instance == null) {
            _instance = new HeroStore ();
          }
          return _instance;
        }
    }

    public void AddHero(HeroItemData hero){
      this.heroes [hero.id] = hero;
    }

    public bool HasHero(string heroId){
      return this.heroes.ContainsKey (heroId);
    }

    public HeroItemUpdateHandler GetUpdateHandler(string heroId){

      if (!this.handlers.ContainsKey(heroId)) {
        this.handlers [heroId] = new HeroItemUpdateHandler ();
      }
      return (HeroItemUpdateHandler)this.handlers [heroId];
    }

    public void UpdateToggle(string heroId){
      if (!this.handlers.ContainsKey (heroId))
        return;
      HeroItemUpdateHandler handler = (HeroItemUpdateHandler)this.handlers [heroId];
      handler.updateToggle (heroId);
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

    private HeroItemData GetHeroById(string heroId){
      return (HeroItemData)this.heroes [heroId];
    }
  }
}
