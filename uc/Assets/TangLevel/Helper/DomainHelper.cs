using System;
using System.Collections;
using System.Collections.Generic;
using TGX = TangGame.Xml;
using TG = TangGame;
using TU = TangUtils;
using UnityEngine;
using Procurios.Public;
using TGUI = TangGame.UI;

namespace TangLevel
{
  public class DomainHelper : MonoBehaviour
  {


    public const char SEP = ',';

    private static int autoIncrId = 10000;

    #region Properties

    private static int AutoIncrId {
      get {
        return autoIncrId++;
      }
    }

    #endregion

    #region Monos

    void OnEnable ()
    {
      if (Config.levelTable.Count == 0) {
        BuildLevelTable ();
      }
    }

    #endregion

    #region Public

    public static Group GetInitGroup (int[] heroIds)
    {

      Group g = new Group ();
      g.heros = new Hero[heroIds.Length];
      for (int i = 0; i < heroIds.Length; i++) {
        g.heros [i] = GetInitHero (heroIds [i]);
      }
      return g;

    }

    public static Hero GetInitHero (int id)
    {
      TGUI.HeroBase hb = TGUI.HeroCache.instance.GetHero (id);
      if (hb != null) {

        Hero hero = new Hero ();
        hero.configId = hb.Xml.id;
        hero.id = id;
        hero.resName = hb.Xml.model;
        hero.maxHp = Mathf.FloorToInt (hb.Net.HpMax);
        hero.hp = hero.maxHp;
        hero.maxMp = Config.MAX_HP;
        hero.mp = 0;
        hero.cd = Config.HERO_CD;
        if (TG.Config.heroSortTable.ContainsKey (hero.configId)) {
          hero.sort = TG.Config.heroSortTable [hero.configId];
          Debug.Log ("+++++++ " + hero.id + " - " + hero.configId + " - " + hero.sort);
        }
        hero.battleDirection = BattleDirection.RIGHT;

        hero.physicalAttack = hb.Net.attack_damage; // 物理攻击
        hero.magicPower = hb.Net.ability_power; // 魔法强度
        hero.physicalDefense = hb.Net.physical_defense; // 物理防御
        hero.magicDefense = hb.Net.magic_defense; // 魔法防御
        hero.physicalCrit = hb.Net.physical_crit; // 物理暴击
        hero.magicCrit = hb.Net.magic_crit; // 魔法暴击
        hero.hpRecovery = hb.Net.hp_recovery; // 生命恢复
        hero.mpRecovery = hb.Net.energy_recovery; // 能量恢复
        hero.routPhysicalDefense = hb.Net.physical_penetration; // 破甲
        hero.routMagicDefense = hb.Net.spell_penetration; // 无视魔抗
        hero.bloodSuckingGrade = hb.Net.bloodsucking_lv; // 吸血等级
        hero.eva = hb.Net.dodge; // 闪避
        hero.healEffect = hb.Net.addition_treatment; // 治疗效果
        hero.mp_consume_dec = hb.Net.energy_consume_dec; // 能量消耗降低

        // skills
        ArrayList skillIdList = JSON.JsonDecode (hb.Xml.skill_ids) as ArrayList;
        if (skillIdList != null) {
          int[] skillIds = TU.TypeUtil.ToArray<ArrayList, int> (skillIdList);
          if (skillIds != null) {
            Dictionary<int,Skill> skills = new Dictionary<int, Skill> ();
            int counter = 0;
            foreach (int skillId in skillIds) {
              Skill skill = BuildSkill (skillId);
              if (skill != null) {
                // WARN : hb.Net.skillLevel这个数组存在的问题时，需要确保技能顺序的正确
                skill.grade = hb.Net.skillLevel [counter];
                skill.enable = true;
                skills.Add (skillId, skill);
                counter++;
              }
            }
            hero.skills = skills;
          }
        }

        // skill queue
        if (hero.skills != null && hero.skills.Count > 0) {
          ArrayList skillQueueList = JSON.JsonDecode (hb.Xml.shot_order) as ArrayList;
          if (skillQueueList != null) {
            int[] skillQueue = TU.TypeUtil.ToArray<ArrayList, int> (skillQueueList);
            hero.skillQueue = skillQueue;
          }
        }

        return hero;
      }

      return null;
    }

    #endregion



    #region Private

    /// <summary>
    /// 从统一配置中 build 一个关卡配置
    /// </summary>
    /// <returns>The level table.</returns>
    private static void BuildLevelTable ()
    {
      if (TG.Config.levelsXmlTable != null && TG.Config.levelsXmlTable.Count > 0) {
        foreach (TGX.LevelData data in TG.Config.levelsXmlTable.Values) {
          //Debug.Log ("level - transform by id " + data.id);
          Level level = BuildLevel (data);
          Config.levelTable.Add (level.id, level);
        }
      }
    }

    /// <summary>
    /// build 一个关卡
    /// </summary>
    /// <returns>The level.</returns>
    /// <param name="data">Data.</param>
    private static Level BuildLevel (TGX.LevelData data)
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
    private static List<SubLevel> BuildSubLevels (TGX.LevelData data)
    {

      List<SubLevel> subLevels = new List<SubLevel> ();
      if (!String.IsNullOrEmpty (data.lv1_bg) && !String.IsNullOrEmpty (data.lv1_monster_ids)) {
        SubLevel lvl = BuildSubLevel (data.lv1_bg, data.lv1_monster_ids);
        lvl.index = 0;
        subLevels.Add (lvl);
      }

      if (!String.IsNullOrEmpty (data.lv2_bg) && !String.IsNullOrEmpty (data.lv2_monster_ids)) {
        SubLevel lvl = BuildSubLevel (data.lv2_bg, data.lv2_monster_ids);
        lvl.index = 1;
        subLevels.Add (lvl);
      }

      if (!String.IsNullOrEmpty (data.lv3_bg) && !String.IsNullOrEmpty (data.lv3_monster_ids)) {
        SubLevel lvl = BuildSubLevel (data.lv3_bg, data.lv3_monster_ids);
        lvl.index = 2;
        subLevels.Add (lvl);
      }
      return subLevels;

    }

    /// <summary>
    /// 建造一个关卡
    /// </summary>
    /// <returns>The level.</returns>
    /// <param name="background">Background.</param>
    /// <param name="monsterIds">Monster identifiers.</param>
    private static SubLevel BuildSubLevel (string background, string monsterIds)
    {
      SubLevel l = new SubLevel ();
      l.resName = background;
      Group g = new Group ();

      ArrayList realIds = JSON.JsonDecode (monsterIds) as ArrayList;
      if (realIds != null) {
        // group and heros
        Hero[] heros = new Hero[realIds.Count];
        for (int i = 0; i < realIds.Count; i++) {
          int hId = (int)(realIds [i]);
          Hero h = BuildMonster (hId);
          // 出场次序
          h.sort = i;
          if (h != null) {
            heros [i] = h;
          } else {
            Debug.Log ("TangLevel: Fail to transform monster data by monster id " + hId);
          }
        }

        g.heros = heros;
      }

      if (g.heros == null || g.heros.Length == 0) {
        Debug.LogError ("TangLevel: Failure to create group for level by monsterIds " + monsterIds);
      }

      l.enemyGroup = g;
      return l;
    }

    /// <summary>
    /// 建造一个怪物
    /// </summary>
    /// <returns>The monster.</returns>
    /// <param name="data">Data.</param>
    private static Hero BuildMonster (int monsterId)
    {

      if (TG.Config.monsterXmlTable.ContainsKey (monsterId)) {

        TGX.MonsterData data = TG.Config.monsterXmlTable [monsterId];

        Hero h = new Hero ();
        // ID
        h.configId = data.id; // 配置ID
        h.id = AutoIncrId; // 自增ID
        // 名字
        h.name = data.name;
        // 资源名字
        h.resName = data.model;
        // 技能
        ArrayList skillGrades = JSON.JsonDecode (data.skill) as ArrayList;
        foreach (object obj in skillGrades) {
          ArrayList skillGrade = obj as ArrayList;
          int skillId = (int)skillGrade [0];
          int grade = (int)skillGrade [1];
          Skill s = BuildSkill (skillId);
          s.enable = true;
          s.grade = grade;
          h.skills.Add (skillId, s);
        }
        if (h.skills == null || h.skills.Count == 0) {
          Debug.Log ("TangLevel: Failure to find skills for monster by id " + h.id);
        }
        // 技能出手序列
        if (!String.IsNullOrEmpty (data.shot_order)) {
          ArrayList list = JSON.JsonDecode (data.shot_order) as ArrayList;
          if (list != null) {
            h.skillQueue = TU.TypeUtil.ToArray<ArrayList, int> (list);
          }
        }
        if (h.skillQueue == null || h.skillQueue.Length == 0) {
          Debug.Log ("TangLevel: Failure to find skill queue for monster by id " + h.id);
        }

        // hp
        h.maxHp = data.hpMax;
        h.hp = data.hpMax;

        // mp
        h.maxMp = Config.MAX_HP;
        h.mp = 0;

        // cd
        h.cd = Config.HERO_CD;

        h.physicalAttack = data.attack_damage; // 物理攻击
        h.magicPower = data.ability_power; // 魔法攻击
        h.physicalDefense = data.physical_defense; // 物理防御
        h.magicDefense = data.magic_defense; // 魔法防御
        h.physicalCrit = data.physical_crit; // 物理暴击
        h.magicCrit = data.magic_crit; // 魔法暴击
        h.hpRecovery = data.hp_recovery; // 生命恢复
        h.mpRecovery = data.energy_recovery; // 能量恢复
        h.routPhysicalDefense = data.physical_penetration; // 破甲
        h.routMagicDefense = data.spell_penetration; // 无视魔抗
        h.bloodSuckingGrade = data.bloodsucking_lv; // 吸血等级
        h.eva = data.dodge; // 闪避
        h.healEffect = data.addition_treatment; // 治疗效果

        return h;

      } else {
        return null;
      }
    }

    /// <summary>
    /// 建造一项技能
    /// </summary>
    /// <returns>The skill.</returns>
    /// <param name="data">Data.</param>
    private static Skill BuildSkill (int skillId)
    {
      if (TG.Config.skillXmlTable.ContainsKey (skillId)) {

        TGX.SkillData data = TG.Config.skillXmlTable [skillId];

        Skill s = new Skill ();

        // ID
        s.id = data.id;

        // 循环次数, 0 为无限循环
        s.loopTimes = data.play_loop;

        // 冷却时间
        s.cd = ((float)data.cool_time) / Config.SECOND_TO_MIL;

        // 动作时间
        s.chargeTime = ((float)data.boot_time) / Config.SECOND_TO_MIL; // 配置文件以毫秒为单位
        s.releaseTime = ((float)data.after_time) / Config.SECOND_TO_MIL;

        // 动画剪辑
        s.chargeClip = data.boot_animation;
        s.releaseClip = data.after_animation;

        // 特效
        if (!String.IsNullOrEmpty (data.singing_effects)) {
          ArrayList list = JSON.JsonDecode (data.singing_effects) as ArrayList;
          if (list != null) {
            s.chargeSpecials = TU.TypeUtil.ToArray<ArrayList, string> (list);
          }
        }
        if (!String.IsNullOrEmpty (data.play_effects)) {
          ArrayList list = JSON.JsonDecode (data.play_effects) as ArrayList;
          if (list != null) {
            s.releaseSpecials = TU.TypeUtil.ToArray<ArrayList, string> (list);
          }
        }

        // 大招
        s.bigMove = data.isUltimate;
        // 等于 0 时是普通攻击
        s.normal = data.type == 0;
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
        // 基础系数
        s.coefficient = data.skill_coefficient;
        // 升级提升系数
        s.increment = data.up_add;

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

      } else {

        Debug.Log ("TangLevel: Fail to find skill by id " + skillId);

        return null;
      }
    }

    /// <summary>
    /// 生成作用器
    /// </summary>
    /// <returns>The effectors.</returns>
    /// <param name="data">Data.</param>
    private static Effector[] BuildEffectors (string textIds)
    {

      ArrayList list = JSON.JsonDecode (textIds) as ArrayList;
      if (list != null) {
        int[] effectorIds = TU.TypeUtil.ToArray<ArrayList, int> (list);
        if (effectorIds != null) {
          List<Effector> effectors = new List<Effector> ();
          foreach (int id in effectorIds) {
            effectors.Add (GetEffector (id));
          }
          return effectors.ToArray ();
        }
      }

      return null;

    }

    private static Effector GetEffector (int id)
    {

      if (TG.Config.effectorXmlTable.ContainsKey (id)) {
        TGX.EffectorData data = TG.Config.effectorXmlTable [id];
        Effector effector = new Effector ();
        effector.id = data.id;
        effector.specialName = data.special_effect; // 特效资源名称
        effector.probability = data.probability; // 概率
        effector.radius = data.radius; // 范围半径
        effector.times = data.times; // 次数
        effector.timeSpan = data.loop_time; // 间隔时间
        effector.type = data.type; // 类型

        // 效果
        if (data.effect_code > 0) {
          ArrayList list = null;
          if (!String.IsNullOrEmpty (data.effect_params)) {
            list = JSON.JsonDecode (data.effect_params) as ArrayList;
          }
          effector.effect = EffectEjector.Instance.NewEffect (data.effect_code, list);
        }

        // 子作用器Ids
        if (!String.IsNullOrEmpty (data.sub_ids)) {
          ArrayList list = JSON.JsonDecode (data.sub_ids) as ArrayList;
          effector.subEffectors = new Effector[list.Count];
          for (int i = 0; i < list.Count; i++) {
            effector.subEffectors [i] = GetEffector ((int)list [i]);
          }
        }
        return effector;
      } else {
        return null;
      }
    }


    #endregion
  }
}

