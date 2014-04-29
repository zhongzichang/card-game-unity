using UnityEngine;
using System.Collections.Generic;
using System;

namespace TangLevel
{
  public class SkillBhvr : MonoBehaviour
  {
    // 技能需要
    private Skill skill;
    private GameObject target;

    // 作用器需要
    private List<Effector> effectors = new List<Effector> ();
    private List<GameObject> sources = new List<GameObject> ();
    private List<Skill> skills = new List<Skill> ();
    private List<Effector> removes = new List<Effector> ();

    #region Mono

    // Use this for initialization
    void Start ()
    {
	
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

    // --- 释放技能 ---
    public void Cast (Skill skill, GameObject target)
    {
      this.skill = skill;
      this.target = target;

      string specialName = skill.specialName;

      if (specialName != null) {

        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {
          ReleaseSpecial (gobj);
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
      if (skill != null) {

        // skill
        string specialName = skill.specialName;
        if (args.Success && args.Name == specialName) {
          GameObject gobj = GobjManager.FetchUnused (specialName);
          if (gobj != null) {
            ReleaseSpecial (gobj);
          }
        }
      } else {

        // effector
        foreach (Effector effector in effectors) {
          if (args.Success && effector.specialName == args.Name) {
            GameObject gobj = GobjManager.FetchUnused (args.Name);
            if (gobj != null) {
              Skill s = null;
              int index = effectors.IndexOf (effector);
              if (index != -1) {
                s = skills [index];
              }
              ReleaseSpecial (gobj, s, effector);
            }
            removes.Add (effector);
          }
        }
        if (removes.Count > 0) {
          foreach (Effector e in removes) {
            int index = effectors.IndexOf (e);
            if (index != -1) {
              effectors.RemoveAt (index);
              sources.RemoveAt (index);
              skills.RemoveAt (index);
            }
          }
        }
      }
    }

    /// <summary>
    /// 释放特效对象
    /// </summary>
    /// <param name="gobj">Gobj.</param>
    private void ReleaseSpecial (GameObject gobj)
    {
      SpecialBhvr[] bhvrs = gobj.GetComponents<SpecialBhvr> ();
      if (bhvrs != null) {
        foreach (SpecialBhvr bhvr in bhvrs) {
          bhvr.source = gameObject;
          bhvr.target = target;
          bhvr.skill = skill;
          bhvr.Play ();
        }
      }
      gobj.SetActive (true);

      ResetCast ();
    }

    private void ReleaseSpecial (GameObject gobj, Skill skill, Effector effector)
    {
      EffectorBhvr[] bhvrs = gobj.GetComponents<EffectorBhvr> ();
      if (bhvrs != null) {
        foreach (EffectorBhvr bhvr in bhvrs) {
          bhvr.source = gameObject;
          bhvr.target = target;
          bhvr.skill = skill;
          bhvr.effector = effector;
          bhvr.Play ();
        }
      }
      gobj.SetActive (true);

      ResetCast ();
    }

    private void ResetCast ()
    {
      this.skill = null;
      this.target = null;
    }

    // -- 接收作用器 --
    public void Receive (Effector effector, Skill skill, GameObject source)
    {

      string specialName = effector.specialName;

      if (specialName != null) {

        GameObject gobj = GobjManager.FetchUnused (specialName);
        if (gobj != null) {
          ReleaseSpecial (gobj, skill, effector);
        } else {
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