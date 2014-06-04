using System;
using System.Collections.Generic;
using TGX = TangGame.Xml;
using TG = TangGame;
using TU = TangUtils;

namespace TangLevel
{
  public class DomainHelper
  {

    public const char SEP = ',';

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

      // ID
      s.id = data.id;

      // 冷却时间
      s.cd = data.cool_time;

      // 动作时间
      s.chargeTime = ((float)data.boot_time) / Config.SECOND_TO_MIL; // 配置文件以毫秒为单位
      s.releaseTime = ((float)data.after_time) / Config.SECOND_TO_MIL;

      // 动画剪辑
      s.chargeClip = data.boot_animation;
      s.releaseClip = data.after_animation;

      // 特效
      if (!String.IsNullOrEmpty (data.singing_effects)) {
        s.chargeSpecials = data.singing_effects.Split (new char[]{ SEP });
      }
      if (!String.IsNullOrEmpty (data.play_effects)) {
        s.releaseSpecials = data.play_effects.Split (new char[]{ SEP });
      }

      // 大招
      s.bigMove = data.isUltimate;
      // 攻击距离
      s.distance = data.cast_range;
      // 目标类型
      s.targetType = data.target_type;
      // 范围类型
      s.scopeType = data.range_type;
      // 是否可打断
      s.breakable = data.being_interrupted_by_injuries;
      // 是否永久增加属性
      s.attributePersistence = data.is_attribute_addition;
      // 替换技能ID
      s.replaceSkillId = data.code_coverage_skill_id;

      // 作用器
      string effectorIds = data.effector_ids;
      if (!String.IsNullOrEmpty (data.effector_ids)) {
        s.effectors = BuildEffectors (data.effector_ids);
      }
      // 前摇作用器
      if (!String.IsNullOrEmpty (data.boot_effector_ids)) {
        s.chargeEffectors = BuildEffectors (data.boot_effector_ids);
      }
      // 后摇作用器
      if (!string.IsNullOrEmpty (data.after_effector_ids)) {
        s.releaseEffectors = BuildEffectors (data.after_effector_ids);
      }

      return s;
    }

    /// <summary>
    /// 生成作用器
    /// </summary>
    /// <returns>The effectors.</returns>
    /// <param name="data">Data.</param>
    private static Effector[] BuildEffectors (string textIds)
    {

      int[] effectorIds = TU.TypeUtil.StringToIntArray (textIds, SEP);
      if (effectorIds != null) {
        List<Effector> list = new List<Effector> ();
        foreach (int id in effectorIds) {
          list.Add (GetEffector (id));
        }
        return list.ToArray();
      }
      return null;

    }

    private static Effector GetEffector (int id)
    {
      if (TG.Config.effectorXmlTable.ContainsKey (id)) {
        TGX.EffectorData data = TG.Config.effectorXmlTable [id];
        Effector effector = new Effector ();
        effector.specialName = data.special_effect; // 特效资源名称
        effector.probability = data.probability; // 概率
        effector.radius = data.radius; // 范围半径
        effector.times = data.times; // 次数
        effector.timeSpan = data.loop_time; // 间隔时间
        effector.type = data.type; // 类型
        if (!String.IsNullOrEmpty (data.effect_ids)) {
          // 效果编码
          effector.effectCodes = TU.TypeUtil.StringToIntArray (data.effect_ids, SEP);
        }
        return effector;
      } else {
        return null;
      }
    }
  }
}

