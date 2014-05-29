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

    private string[] heroIds = {"1", "2", "3",};

    /// <summary>
    /// 生成测试用随机战斗结果
    /// </summary>
    /// <returns>The hero.</returns>
    public static object RandomBattleResult (ArrayList heroIds, int resultType)
    {
      Props props = new Props();
      props.data = new TangGame.Xml.PropsData();
      props.data.name = "测试道具";
      props.data.type = 2;
			props.net.count = 2;
      props.data.icon = "104";
      props.data.level = 5;
      props.data.info = "使用后可以获得一个小萝莉";
      props.data.description = "这是测试道具，大家都懂得";

      BattleResultData data = new BattleResultData();
      data.type = (BattleResultType)resultType;
      data.level = 20;
      data.exp = 12;
      data.gold = 807;
      data.propsList.Add(props);

      foreach (int heroId in heroIds) {
        BattleResultHeroData battleResultHeroData = new BattleResultHeroData ();
        battleResultHeroData.id = heroId;
        battleResultHeroData.evolve = 1;
        battleResultHeroData.maxExp = battleResultHeroData.exp = 100;
        battleResultHeroData.level = 20;
        battleResultHeroData.levelUp = false;
        battleResultHeroData.rank = 2;
        data.herosList.Add (battleResultHeroData);
      }

      return data;
    }

    /// <summary>
    /// 生成测试用随机英雄数据
    /// </summary>
    /// <returns>The hero.</returns>
    public HeroItemData RandomHero (string heroId)
    {
      HeroItemData hero = new HeroItemData ();
      hero.id = heroId;
      if (heroId.Equals("1")) {
        hero.order = 3;
        hero.lineType = 2;
      }else if (heroId.Equals("2")) {
        hero.order = 1;
        hero.lineType = 0;
      }else if (heroId.Equals("3")) {
        hero.order = 2;
        hero.lineType = 1;
      }else if (heroId.Equals("4")) {
        hero.order = 4;
        hero.lineType = 3;
      }
      hero.name = "不告诉你";
      hero.rank_color = Random.Range (1, 10);
      hero.level = Random.Range (1, 99);
      hero.stars = Random.Range (1, 5);

      hero.hp = Random.Range (1, 100);
      hero.hpMax = 100;
      hero.mp = Random.Range (1, 100);
      hero.mpMax = 100;
      return hero;
    }

    public HeroItemData RandomEnemy(string enemyId){
      HeroItemData enemy = new HeroItemData ();
      enemy.id = enemyId;
      enemy.name = "可怕的狗娃";
      enemy.rank_color = Random.Range(1, 10);
      enemy.level = Random.Range(1, 99);
      enemy.stars = Random.Range(1, 5);
      return enemy;
    }

    public RewardItemData RandRewardItem(string rewardId){
      RewardItemData item = new RewardItemData ();
      item.id = rewardId;
      item.name = "东南枝";
      item.type = Random.Range (1, 3);
      item.rank_color = Random.Range (1, 5);
      item.minLevel = 20;
      item.goldCost = 4000;
      item.detailDesc = "生命回复-100";
      item.briefDesc = "狗娃的必杀技";
      return item;
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
      stage.desc = "涿县有英雄祭天地结拜桃圆，为桃园三结义。";
      stage.name = "黄巾谋逆－涿县郊外";
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
      stage.enemyIds = "1,2";
      stage.bossId = "3";
      stage.rewardIds = "371,372,373,374";
      return stage;
    }
  }
}
