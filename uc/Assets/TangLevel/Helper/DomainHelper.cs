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

    #region Monos

    void OnEnable ()
    {
      if (Config.levelTable.Count == 0) {
        BuildLevelTable ();
      }
    }

    #endregion

    #region Public

    public static Group GetInitGroup(int[] heroIds){

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
        hero.id = id;
        hero.resName = hb.Xml.model;
        hero.maxHp = Mathf.FloorToInt (hb.Net.HpMax);
        hero.hp = hero.maxHp;
        hero.maxMp = Config.MAX_HP;
        hero.mp = 0;
        if (TG.Config.heroSortTable.ContainsKey (id)) {
          hero.sort = TG.Config.heroSortTable [id];
        }
        hero.battleDirection = BattleDirection.RIGHT;

        // skills
        ArrayList skillIdList = JSON.JsonDecode (hb.Xml.skill_ids) as ArrayList;
        if (skillIdList != null) {
          int[] skillIds = TU.TypeUtil.ToArray<ArrayList, int> (skillIdList);
          if (skillIds != null) {
            Dictionary<int,Skill> skills = new Dictionary<int, Skill> ();
            for (int i = 0; i < skillIds.Length; i++) {
              Skill skill = BuildSkill (skillIds[i]);
              if (skill != null) {
                skill.grade = hb.Net.skillLevel[i];
                skills.Add (skillIds[i], skill);
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

    #region Config

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
        subLevels.Add (lvl);
      }

      if (!String.IsNullOrEmpty (data.lv2_bg) && !String.IsNullOrEmpty (data.lv2_monster_ids)) {
        SubLevel lvl = BuildSubLevel (data.lv2_bg, data.lv2_monster_ids);
        subLevels.Add (lvl);
      }

      if (!String.IsNullOrEmpty (data.lv3_bg) && !String.IsNullOrEmpty (data.lv3_monster_ids)) {
        SubLevel lvl = BuildSubLevel (data.lv3_bg, data.lv3_monster_ids);
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
          int hId = Convert.ToInt32 ((double)(realIds [i]));
          Hero h = BuildMonster (hId);
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
        int sort = TG.Config.heroSortTable [monsterId];

        Hero h = new Hero ();
        // ID
        h.id = data.id;
        // 名字
        h.name = data.name;
        // 资源名字
        h.resName = data.model;
        // 出场次序
        h.sort = sort;
        // 技能
        ArrayList skillIds = JSON.JsonDecode (data.skill) as ArrayList;
        if (skillIds != null) {
          h.skills = new Dictionary<int, Skill> ();
          for (int i = 0; i < skillIds.Count; i++) {
            int skillId = Convert.ToInt32 ((double)skillIds [i]);
            h.skills.Add (skillId, BuildSkill (skillId));
          }
        }
        if (h.skills == null || h.skills.Count == 0) {
          Debug.Log ("TangLevel: Failure to find skills for monster by id " + h.id);
        }
        // 技能出手序列
        if (!String.IsNullOrEmpty (data.shot_order)) {
          ArrayList list = JSON.JsonDecode (data.shot_order) as ArrayList;
          if (list != null) {
            ArrayList intList = TU.TypeUtil.DoubleToInt (list);
            if (intList != null) {
              h.skillQueue = TU.TypeUtil.ToArray<ArrayList, int> (intList);
            }
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
        ArrayList intList = TU.TypeUtil.DoubleToInt (list);
        if (intList != null) {
          int[] effectorIds = TU.TypeUtil.ToArray<ArrayList, int> (intList);
          if (effectorIds != null) {
            List<Effector> effectors = new List<Effector> ();
            foreach (int id in effectorIds) {
              list.Add (GetEffector (id));
            }
            return effectors.ToArray ();
          }
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
        if (!String.IsNullOrEmpty (data.effect_ids)) {
          // 效果编码
          ArrayList list = JSON.JsonDecode (data.effect_ids) as ArrayList;
          if (list != null) {
            effector.effectCodes = TU.TypeUtil.ToArray<ArrayList, int> (list);
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

