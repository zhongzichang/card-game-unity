using UnityEngine;
using System.Collections.Generic;
using System;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using TG = TangGame;

namespace TangLevel
{
  public class SkillBhvr : MonoBehaviour
  {
    // 当前技能
    public Skill skill;
    // 所有技能
    private Dictionary<int, Skill> skills;
    // 出手序列
    private int[] skillQueue;
    // 当前序列索引
    private int skillQueueIndex = 0;
    // 最近一个序列索引
    private int lastSkillQueueIndex = 0;
    private Dictionary<string, List<SkillWrapper>> wt = new Dictionary<string, List<SkillWrapper>> ();
    private HeroBhvr heroBhvr = null;
    private HeroStatusBhvr statusBhvr = null;

    #region Mono

    // Use this for initialization
    void Start ()
    {
      heroBhvr = GetComponent<HeroBhvr> ();
      statusBhvr = GetComponent<HeroStatusBhvr> ();
      skills = heroBhvr.hero.skills;
      skillQueue = heroBhvr.hero.skillQueue;
    }

    void Update ()
    {
      if (skill != null) {

        // 当技能配置文件中的前摇或后摇的时间 > 0 ; -------------
        // 前摇计时器
        if (skill.chargeTime > 0 && statusBhvr.Status == HeroStatus.charge) {
          skill.chargeTimer += Time.deltaTime;
          if (skill.chargeTimer > skill.chargeTime) { // 前摇时间结束
            statusBhvr.Status = HeroStatus.release;
          }
        }
        // 后摇计时器s
        if (skill.releaseTime > 0 && statusBhvr.Status == HeroStatus.charge) {
          skill.releaseTimer += Time.deltaTime;
          if (skill.releaseTimer > skill.releaseTime) { // 后摇时间结束
            statusBhvr.Status = HeroStatus.idle;
          }
        }
      }
    }

    void OnDisable ()
    {
      if (GobjManager.HasHandler (OnSpecialLoad)) {
        GobjManager.RaiseLoadEvent -= OnSpecialLoad;
      }
    }

    #endregion

    #region PublicMethods

    /// <summary>
    /// 获取下一个技能
    /// </summary>
    /// <value>The next skill.</value>
    public Skill NextSkill ()
    {
      if (skills != null && skills.Count > 0 && skillQueue != null && skillQueue.Length > 0) {
        // 确保 skillQueueIndex  在出手序列范围内
        if (skillQueueIndex < skillQueue.Length && skillQueueIndex >= 0) {
          int skillIndex = skillQueue [skillQueueIndex];

          // 确保技能索引在技能列表范围内
          if (skills.ContainsKey (skillIndex)) {
            Skill skill = skills [skillIndex];

            // 确保技能可用，技能不可以是大招
            if (skill.enable && !skill.bigMove && Time.time > skill.nextFire) {
              // 找到技能
              lastSkillQueueIndex = skillQueueIndex;
              skillQueueIndex++;
              return skill;

            } else {
              // 下一个技能
              skillQueueIndex++;
            }

          } else {
            // 下一个技能
            skillQueueIndex++;

          }

        } else {

          // 转到一个
          skillQueueIndex = 0;

        }

        if (lastSkillQueueIndex != skillQueueIndex) { // 还没有历遍
          // 继续下一个技能
          return NextSkill ();

        } else {

          Skill skill = skills [skillQueue [skillQueueIndex]];
          if (skill.enable && !skill.bigMove && Time.time > skill.nextFire) {
            return skill;
          }
        }
      }
      return null;
    }
    // --- 释放技能 ---
    /// <summary>
    /// 释放起手特效
    /// </summary>
    /// <param name="skill">Skill.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    public void CastChargeSpecial (Skill skill, GameObject source, GameObject target)
    {

      SkillWrapper w = SkillWrapper.W (skill, source, target);
      foreach (string specialName in skill.chargeSpecials) {
        CastSkillSpecial (specialName, w);
      }

    }

    /// <summary>
    /// 施放release特效
    /// </summary>
    /// <param name="skill">Skill.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    public void CastReleaseSpecial (Skill skill, GameObject source, GameObject target)
    {

      SkillWrapper w = SkillWrapper.W (skill, source, target);
      foreach (string specialName in skill.releaseSpecials) {
        CastSkillSpecial (specialName, w);
      }
    }

    /// <summary>
    ///  对目标抛出作用器
    /// </summary>
    /// <param name="effector">Effector.</param>
    /// <param name="skill">Skill.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    public void Cast (EffectorWrapper w)
    {
      string specialName = w.effector.specialName;
      if (specialName != null) {
        if (specialName.StartsWith (Config.DBFX_PREFIX)) {
          // 如果是 DragonBones 特效
          GameObject gobj = EffectorGobjManager.FetchUnused (w.effector);
          if (gobj != null) {
            ReleaseEffectorSpecial (gobj, w);
          }
        } else {
          // Unity 特效
          // 获取特效对象
          GameObject gobj = GobjManager.FetchUnused (specialName);
          if (gobj != null) {
            // 确保脚本正确挂载
            if (w.effector.scripts != null && w.effector.scripts.Length > 0) {
              foreach (string script in w.effector.scripts) {
                Component comp = gobj.GetComponent (script);
                if (comp == null) {
                  comp = gobj.AddComponent (script);
                }
              }
            }
            ReleaseEffectorSpecial (gobj, w);
          } else {
            // 添加到作用器列表
            Add (specialName, w);
            // 需要加载资源
            if (!GobjManager.HasHandler (OnSpecialLoad)) {
              GobjManager.RaiseLoadEvent += OnSpecialLoad;
            }
            GobjManager.LazyLoad (specialName);
          }
        }
      }
    }

    #endregion

    #region PrivateMethods

    private void CastSkillSpecial (string specialName, SkillWrapper w)
    {

      // 获取特效对象
      GameObject gobj = GobjManager.FetchUnused (specialName);
      if (gobj != null) {

        ReleaseSkillSpecial (gobj, w);

      } else {

        // 添加到作用器列表
        Add (specialName, w);
        // 需要加载资源
        if (!GobjManager.HasHandler (OnSpecialLoad)) {
          GobjManager.RaiseLoadEvent += OnSpecialLoad;
        }
        GobjManager.LazyLoad (specialName);
      }
    }

    private void OnSpecialLoad (object sender, LoadResultEventArgs args)
    {
      if (args.Success) {

        // 获取和播放作用器特效

        if (wt.ContainsKey (args.Name)) {
          foreach (SkillWrapper w in wt[args.Name]) {
            GameObject gobj = GobjManager.FetchUnused (args.Name);
            if (gobj != null) {
              if (w is EffectorWrapper) {
                ReleaseEffectorSpecial (gobj, w as EffectorWrapper);
              } else {
                ReleaseSkillSpecial (gobj, w);
              }
            }
          }
          Remove (args.Name);
        }
      }
    }

    /// <summary>
    /// 释放技能特效对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    /// <param name="w">The width.</param>
    private void ReleaseSkillSpecial (GameObject gobj, SkillWrapper w)
    {

      SkillSpecialBhvr bhvr = gobj.GetComponent<SkillSpecialBhvr> ();
      if (bhvr != null) {
        bhvr.w = w;
        gobj.SetActive (true);
        bhvr.Play ();
      }

    }

    /// <summary>
    /// 释放作用器特效对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    private void ReleaseEffectorSpecial (GameObject gobj, EffectorWrapper w)
    {

      EffectorSpecialBhvr bhvr = gobj.GetComponent<EffectorSpecialBhvr> ();
      if (bhvr != null) {
        bhvr.w = w;
        gobj.SetActive (true);
        bhvr.Play ();
      }
    }

    /// <summary>
    /// 增加一个对象到缓存
    /// </summary>
    private void Add (string special, SkillWrapper w)
    {
      if (wt.ContainsKey (special)) {
        wt [special].Add (w);
      } else {
        List<SkillWrapper> list = new List<SkillWrapper> ();
        list.Add (w);
        wt.Add (special, list);
      }
    }

    /// <summary>
    /// 删除持有某特效的所有技能和作用器
    /// </summary>
    /// <param name="special">Special.</param>
    private void Remove (string special)
    {
      if (wt.ContainsKey (special)) {
        wt [special].Clear ();
      }
    }

    #endregion
  }
}