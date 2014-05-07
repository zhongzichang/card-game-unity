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
    // 作用器列表(需要)
    private List<EffectorWrapper> ws = new List<EffectorWrapper> ();
    // 作用完毕，需要被删除的作用器
    private List<EffectorWrapper> rws = new List<EffectorWrapper> ();
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


    // --- 释放技能 ---
    // 对目标释放技能
    public void Cast (Effector effector, Skill skill, GameObject source, GameObject target)
    {
      EffectorWrapper w = EffectorWrapper.W (effector, skill, gameObject, target);

      string specialName = effector.specialName;

      if (specialName != null) {

        // 获取特效对象
        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {

          ReleaseEffectorSpecial ( gobj, w);

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

        // 获取和播放作用器特效
        foreach (EffectorWrapper w in rws) {

          if (w.effector.specialName == args.Name) {
            GameObject gobj = GobjManager.FetchUnused (args.Name);
            if (gobj != null) {
              ReleaseEffectorSpecial (gobj, w);
            }
            rws.Add (w);
          }
        }

        // 删除已经被释放的作用器
        if (rws.Count > 0) {
          foreach (EffectorWrapper e in rws) {
            ws.Remove (e);
          }
        }
      }
    }

    /// <summary>
    /// 释放技能特效对象
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
    /// 接收作用器
    /// </summary>
    /// <param name="effector">Effector.</param>
    /// <param name="skill">Skill.</param>
    public void Receive (EffectorWrapper w)
    {

      // 只有英雄还没死，才会进行伤害计算
      if (heroBhvr.hero.hp > 0) {

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
        } else {
          // 被击打
          heroBhvr.Beat ();
        }
      }


      // 作用器特效
      string specialName = w.effector.specialName;
      if (specialName != null) {
        GameObject gobj = GobjManager.FetchUnused (specialName);

        if (gobj != null) {

          ReleaseEffectorSpecial (gobj, w);

        } else {

          // 添加到受作用器列表
          ws.Add (w);
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