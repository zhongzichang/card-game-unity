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
    // 作用器列表(需要)
    private List<Effector> effectors = new List<Effector> ();
    // 作用完毕，需要被删除的作用器
    private List<Effector> removes = new List<Effector> ();
    private HeroBhvr heroBhvr = null;

    #region Mono

    // Use this for initialization
    void Start ()
    {
      heroBhvr = GetComponent<HeroBhvr> ();
    }

    void OnDisable ()
    {
      if (GobjManager.HasHandler (OnSpecialLoad)) {
        GobjManager.RaiseLoadEvent -= OnSpecialLoad;
      }
    }

    #endregion

    #region Hero

    private void WrapSkill (Skill skill, GameObject target)
    {
      skill.source = gameObject;
      skill.target = target;
    }

    private void WrapEffector (Effector effector, Skill skill)
    {
      effector.skill = skill;
    }
    // --- 释放技能 ---
    // 对目标释放技能
    public void Cast (Effector effector, Skill skill, GameObject target)
    {

      skill.source = gameObject;
      skill.target = target;

      // 包装作用器
      WrapEffector (effector, skill);

      string specialName = effector.specialName;

      if (specialName != null) {

        // 获取特效对象
        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {

          ReleaseEffectorSpecial ( gobj, effector);

        } else {

          // 需要加载资源
          if (!GobjManager.HasHandler (OnSpecialLoad)) {
            GobjManager.RaiseLoadEvent += OnSpecialLoad;
          }
          GobjManager.LazyLoad (specialName);
        }
      }

    }

    private void OnSpecialLoad (object sender, LoadResultEventArgs args)
    {
      if (args.Success) {

        // 获取技能特效名称
        string skillSpecialName = null;

        // 获取和播放作用器特效

        removes.Clear ();
        foreach (Effector effector in effectors) {

          if (effector.specialName == args.Name) {
            GameObject gobj = GobjManager.FetchUnused (args.Name);
            if (gobj != null) {
              ReleaseEffectorSpecial (gobj, effector);
            }
            removes.Add (effector);
          }
        }

        // 删除已经被释放的作用器
        if (removes.Count > 0) {
          foreach (Effector e in removes) {
            effectors.Remove (e);
          }
        }
      }
    }

    /// <summary>
    /// 释放技能特效对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    private void ReleaseEffectorSpecial (GameObject gobj, Effector effector)
    {
      EffectorSpecialBhvr[] bhvrs = gobj.GetComponents<EffectorSpecialBhvr> ();
      if (bhvrs != null) {
        foreach (EffectorSpecialBhvr bhvr in bhvrs) {
          bhvr.effector = effector;
          bhvr.Play ();
        }
      }
      gobj.SetActive (true);
    }

    /// <summary>
    /// 接收作用器
    /// </summary>
    /// <param name="effector">Effector.</param>
    /// <param name="skill">Skill.</param>
    public void Receive (Effector effector, Skill skill)
    {
      // 包装作用器
      WrapEffector (effector, skill);


      // 如果作用器减少HP
      // TODO 测试用，请使用正式的伤害计算公式
      int hurt = UnityEngine.Random.Range (1, 20);
      heroBhvr.hero.hp -= hurt;

      // 冒字
      TG.BattleTxt battleTxt = new TG.BattleTxt ();
      battleTxt.type = TG.BattleTxtType.Hurt;
      battleTxt.value = hurt;
      if (heroBhvr.hero.battleDirection == BattleDirection.RIGHT)
        battleTxt.self = true;
      else
        battleTxt.self = false;
      battleTxt.position = Camera.main.WorldToScreenPoint (transform.localPosition);
      Facade.Instance.SendNotification (TG.BattleCommand.BattleTxt, battleTxt);
      // HP 小于等于0时，角色死亡
      if (heroBhvr.hero.hp <= 0) {
        heroBhvr.Die ();
      }

      // 作用器特效
      string specialName = effector.specialName;
      if (specialName != null) {
        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {
          ReleaseEffectorSpecial (gobj, effector);
        } else {
          // 添加到受作用器列表
          effectors.Add (effector);
          // 需要加载资源
          if (!GobjManager.HasHandler (OnSpecialLoad)) {
            GobjManager.RaiseLoadEvent += OnSpecialLoad;
          }
          GobjManager.LazyLoad (specialName);
        }
      }
    }

    #endregion
  }
}