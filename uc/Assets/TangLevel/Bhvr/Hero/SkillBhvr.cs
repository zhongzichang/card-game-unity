﻿using UnityEngine;
using System.Collections.Generic;
using System;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using TG = TangGame;

namespace TangLevel
{
  public class SkillBhvr : MonoBehaviour
  {

    private Dictionary<string, List<SkillWrapper>> wt = new Dictionary<string, List<SkillWrapper>> ();
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

    #region PublicMethods

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
    public void CastReleaseSpecial(Skill skill, GameObject source, GameObject target){

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

        // 获取特效对象
        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {

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

    #endregion

    #region PrivateMethods

    private void CastSkillSpecial(string specialName, SkillWrapper w){

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

      SkillSpecialBhvr[] bhvrs = gobj.GetComponents<SkillSpecialBhvr> ();
      if (bhvrs != null) {
        foreach (SkillSpecialBhvr bhvr in bhvrs) {
          bhvr.w = w;
          bhvr.Play ();
        }
      }
      gobj.SetActive (true);

    }

    /// <summary>
    /// 释放作用器特效对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    private void ReleaseEffectorSpecial (GameObject gobj, EffectorWrapper w)
    {
      EffectorSpecialBhvr[] bhvrs = gobj.GetComponents<EffectorSpecialBhvr> ();
      if (bhvrs != null) {
        foreach (EffectorSpecialBhvr bhvr in bhvrs) {
          bhvr.w = w;
          bhvr.Play ();
        }
      }
      gobj.SetActive (true);
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
        wt[special].Clear();
      }
    }

    #endregion
  }
}