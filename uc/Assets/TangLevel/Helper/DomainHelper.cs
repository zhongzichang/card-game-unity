using System;
using System.Collections.Generic;
using TGX = TangGame.Xml;
using TG = TangGame;

namespace TangLevel
{
  public class DomainHelper
  {

    /// <summary>
    /// 从统一配置中 build 一个关卡配置
    /// </summary>
    /// <returns>The level table.</returns>
    public static Dictionary<int, Level> BuildLevelTable ()
    {
      Dictionary<int, Level> table = new Dictionary<int, Level> ();
      foreach (TGX.LevelData data in TG.Config.levelsXmlTable.Values) {

        Level level = BuildLevel (data);
        table.Add (level.id, level);

      }
      return table;
    }

    /// <summary>
    /// build 一个关卡
    /// </summary>
    /// <returns>The level.</returns>
    /// <param name="data">Data.</param>
    public static Level BuildLevel (TGX.LevelData data)
    {
      Level level = new Level ();
      level.id = data.id;
      // 子关卡
      List<SubLevel> subLevels = BuildSubLevels (data);

      level.subLevels = subLevels.ToArray ();
      return level;
    }

    /// <summary>
    /// 从关卡配置表中提取出子关卡s
    /// </summary>
    /// <returns>The conf.</returns>
    /// <param name="data">Data.</param>
    public static List<SubLevel> BuildSubLevels (TGX.LevelData data)
    {

      List<SubLevel> subLevels = new List<SubLevel> ();
      SubLevel l1 = new SubLevel ();
      if (!String.IsNullOrEmpty (data.lv1_bg) && !String.IsNullOrEmpty (data.lv1_monster_ids)) {
        l1 = BuildLevel (data.lv1_bg, data.lv1_monster_ids);
        subLevels.Add (l1);
      }
      return subLevels;

    }

    /// <summary>
    /// 建造一个关卡
    /// </summary>
    /// <returns>The level.</returns>
    /// <param name="background">Background.</param>
    /// <param name="monsterIds">Monster identifiers.</param>
    private static SubLevel BuildLevel (string background, string monsterIds)
    {
      SubLevel l = new SubLevel ();
      l.resName = background;

      string[] tmpIds = monsterIds.Split (new Char[]{ ',' });
      int[] realIds = new int[tmpIds.Length];
      for (int i = 0; i < realIds.Length; i++) {
        realIds [i] = int.Parse (tmpIds [i]);
      }

      l.enemyGroup = new Group ();
      return l;
    }


    /// <summary>
    /// 建造一个怪物
    /// </summary>
    /// <returns>The monster.</returns>
    /// <param name="data">Data.</param>
    private static Hero BuildMonster (TGX.MonsterData data)
    {
      /*
       *    public string name;
    public string resName;
    /// <summary>
    /// 出场顺序值
    /// </summary>
    public int sort;
    /// <summary>
    /// 英雄技能
    /// </summary>
    public List<Skill> skills;
    /// <summary>
    /// 出手序列
    /// </summary>
    public int[] skillQueue;
    /// <summary>
    /// 英雄的 AI，可以有多个
    /// </summary>
    public string[] ai;
    public int maxHp;
    private int m_hp;
    public int maxMp;
    private int m_mp;*/
      Hero h = new Hero ();
      h.name = data.name;
      h.resName = data.model;

      return h;

    }

    /// <summary>
    /// 建造一项技能
    /// </summary>
    /// <returns>The skill.</returns>
    /// <param name="data">Data.</param>
    private static Skill BuildSkill (TGX.SkillData data)
    {

      Skill s = new Skill ();
      s.id = data.id;
      s.cd = data.cool_time;

      s.chargeTime = ((float)data.boot_time) / 1000;
      s.releaseTime = ((float)data.after_time) / 1000;
      s.chargeClip = data.boot_animation;
      s.releaseClip = data.after_animation;
      if (!String.IsNullOrEmpty (data.singing_effect)) {
        s.chargeSpecials = data.singing_effect.Split (new char[]{ ',' });
      }
      if (!String.IsNullOrEmpty (data.play_effect)) {
        s.releaseSpecials = data.play_effect.Split (new char[]{ ',' });
      }

      s.bigMove = data.isUltimate;
      s.distance = data.cast_range;
      s.targetType = data.target_type;
      s.scopeType = data.range_type;
      s.breakable = data.being_interrupted_by_injuries;
      s.attributePersistence = data.is_attribute_addition;
      s.replaceSkillId = data.code_coverage_skill_id;

      // 作用器
      //s.chargeEffectors;
      //s.releaseEffectors;
      //s.effectors;

      return s;


      /*
       *
       *
    /// <summary>
      /// 编号
      /// </summary>
      public int id;

      /// <summary>
      /// 冷却时间
      /// </summary>
      public float cd;

      /// <summary>
      /// 起手动作时间
      /// </summary>
      public float chargeTime;

      /// <summary>
      /// 释放动作时间
      /// </summary>
      public float releaseTime;

      /// <summary>
      /// 是否大招
      /// </summary>
      public bool bigMove;

      /// <summary>
      /// 攻击距离
      /// </summary>
      public float distance;

      // 技能系统基本参数
      // 升级提升参数

      /// 目标类型
      public int targetType;

      /// <summary>
      /// 范围类型
      /// </summary>
      public int scopeType;

      /// <summary>
      ///   指定区域 - 当 targetType = TARGET_RANGE 时有用
      /// </summary>
      public Rect region;

      /// <summary>
      /// 作用器
      /// </summary>
      public Effector[] effectors;


      /// <summary>
      /// 前摇作用器
      /// </summary>
      public Effector[] chargeEffectors;

      /// <summary>
      /// 后摇作用器
      /// </summary>
      public Effector[] releaseEffectors;

      /// <summary>
      /// 是否可打断
      /// </summary>
      public bool breakable;

      /// <summary>
      /// 永久增加属性
      /// </summary>
      public bool attributePersistence;

      /// <summary>
      /// 被替换释放的另一个技能ID
      /// </summary>
      public int replaceSkillId;

      /// <summary>
      /// 蓄力动画剪辑
      /// </summary>
      public string chargeClip;

      /// <summary>
      /// 释放动画剪辑
      /// </summary>
      public string releaseClip;

      /// <summary>
      /// 蓄力特效
      /// </summary>
      public string[] chargeSpecials;

      /// <summary>
      /// 释放特效
      /// </summary>
      public string[] releaseSpecials;
       */
      return s;
    }

  }
}

