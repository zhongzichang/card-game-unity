using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class StageItemData {
    public int id;
    public int chapterId;
    /// <summary>
    /// 关卡类型
    ///  1.普通关卡，无限制。
    ///  2.一次性普通关卡，终身只能完成一次，即只有星级为0时可以进行。
    ///  3.精英关卡
    ///  4.一次性精英关卡
    ///  5.时光之穴1
    ///  6.时光之穴2
    ///  7.英雄试炼1
    ///  8.英雄试炼2
    ///  9.英雄试炼3"     
    /// </summary>
    public int type;
    /// <summary>
    /// 6 体力消耗      完成该关卡需要消耗的体力值。 
    /// </summary>
    public int vitCost;
    /// <summary>
    /// 10  同id关卡次数限制     该关卡id每日只能完成的次数，用于精英关卡的次数限制。
    /// </summary>
    public int maxCountById;
    /// <summary>
    /// 11  同类型关卡次数限制     关卡类型相同的关卡，共用一个次数限制，比如时光之穴1，则里面的4个难度关卡，均共用一个次数限制。                
    /// </summary>
    public int maxCountByType;
    /// <summary>
    /// 12  开放等级限制      该关卡开放的主公等级限制。  
    /// </summary>
    public int minLevel;
    /// <summary>
    /// 13  关卡名     中文汉字表示关卡名称   
    /// </summary>
    public string name;
    /// <summary>
    /// 14  怪物一览      {怪物id1,怪物id2...}在关卡准备界面上，显示该关卡有哪些怪物，显示顺序同数组顺序。特别的，数组最后一个怪物为 boss。
    /// </summary>
    public string enemyIds;
    public string bossId;
    /// <summary>
    /// 15  掉落一览      {物品id1,物品id2...}在关卡准备界面上，显示该关卡可能掉落的物品，显示顺序同数组顺序。                
    /// </summary>
    public string rewardIds;
    /// <summary>
    /// 16  描述      关卡的文字描述
    /// </summary>
    public string desc;  
    /// <summary>
    /// 星数
    /// </summary>
    public int stars;
    /// <summary>
    /// 地图被锁定，1-被锁, 2-当前, 3-可用
    /// </summary>
    public int status;

    public bool IsOnceType(){
      return type == 1;
    }

    public bool IsCurrentStatus(){
      return status == 2;
    }

    public bool IsLockedStatus(){
      return status == 1;
    }

    public string[] GetEnemyIds(){
      return enemyIds.Split (',');
    }

    public string[] GetRewardIds(){
      return rewardIds.Split (',');
    }
  }
}

