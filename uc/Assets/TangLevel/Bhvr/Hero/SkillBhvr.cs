using UnityEngine;
using System.Collections.Generic;
using System;

namespace TangLevel
{
  public class SkillBhvr : MonoBehaviour
  {
    // 技能需要（当前释放的技能）
    private Skill skill;
    // 作用器需要（英雄接收到的作用器）
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
    // Update is called once per frame
    void Update ()
    {
	
    }

    void OnDisable ()
    {
      if (GobjManager.HasHandler (OnSpecialLoad)) {
        GobjManager.RaiseLoadEvent -= OnSpecialLoad;
      }
    }

    #endregion

    #region Hero

    private void WrapSkill(Skill skill, GameObject target) {
      skill.source = gameObject;
      skill.target = target;
    }

    private void WrapEffector(Effector effector, Skill skill){
      effector.skill = skill;
    }

    // --- 释放技能 ---


    // 对目标释放技能
    public void Cast (Skill skill, GameObject target)
    {
      this.skill = skill;

      // 包装技能
      WrapSkill (skill, target);

      string specialName = skill.specialName;

      if (specialName != null) {

        // 获取特效对象
        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {

          ReleaseSkillSpecial (gobj);

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
        if (skill != null && skill.specialName != null) {
          skillSpecialName = skill.specialName;
        }

        if (args.Name == skillSpecialName) {

          // 获取和播放技能特效

          GameObject gobj = GobjManager.FetchUnused (args.Name);
          if (gobj != null) {
            ReleaseSkillSpecial (gobj);
          }

        } else {

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
    }

    /// <summary>
    /// 释放技能特效对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    private void ReleaseSkillSpecial (GameObject gobj)
    {
      SkillSpecialBhvr[] bhvrs = gobj.GetComponents<SkillSpecialBhvr> ();
      if (bhvrs != null) {
        foreach (SkillSpecialBhvr bhvr in bhvrs) {
          bhvr.skill = this.skill;
          bhvr.Play ();
        }
      }
      gobj.SetActive (true);
      this.skill = null;
    }

    private void ReleaseEffectorSpecial ( GameObject gobj, Effector effector )
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

    // -- 接收作用器 --
    public void Receive (Effector effector, Skill skill)
    {
      // 包装作用器
      WrapEffector (effector, skill);


      // 如果作用器减少HP
      // 测试用
      heroBhvr.hero.hp -= 3;


      string specialName = effector.specialName;

      if (specialName != null) {

        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {

          ReleaseEffectorSpecial (gobj, effector);

        } else {

          // 添加到等待列表
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