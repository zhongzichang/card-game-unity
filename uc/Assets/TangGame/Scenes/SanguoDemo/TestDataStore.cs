﻿using UnityEngine;
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
    /// 生成测试用随机英雄数据
    /// </summary>
    /// <returns>The hero.</returns>
    public HeroItemData RandomHero(){
      HeroItemData hero = new HeroItemData ();
      hero.order = Random.Range(0, heroIds.Length);
      hero.id = heroIds[hero.order];
      hero.name = "不告诉你";
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

    public HeroItemData RandomEnemy(string enemyId){
      HeroItemData enemy = new HeroItemData ();
      enemy.id = enemyId;
      enemy.name = "可怕的狗娃";
      enemy.rank = Random.Range(1, 10);
      enemy.level = Random.Range(1, 99);
      enemy.stars = Random.Range(1, 5);
      return enemy;
    }

    public RewardItemData RandRewardItem(string rewardId){
      RewardItemData item = new RewardItemData ();
      item.id = rewardId;
      item.name = "东南枝";
      item.type = Random.Range (1, 3);
      item.rank = Random.Range (1, 5);
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
      stage.enemyIds = "Axe,DoT,DoT,CM";
      stage.bossId = "Zeus";
      stage.rewardIds = "371,372,373,374";
      return stage;
    }
  }
}
