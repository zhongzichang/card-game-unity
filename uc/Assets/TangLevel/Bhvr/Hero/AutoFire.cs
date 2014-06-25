using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  /// <summary>
  /// 找距离最近的目标开打
  /// </summary>
  [RequireComponent (typeof(HeroBhvr))]
  public class AutoFire : MonoBehaviour
  {
    public Skill nextSkill;
    // 当前技能

    public const float period = 3;
    private DirectedNavigable navigable;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private HeroBhvr heroBhvr;
    private SkillBhvr skillBhvr;
    private GroupBhvr groupBhvr;

    private float remainTime = period;

    void Start ()
    {
      // hero behaviour
      heroBhvr = GetComponent<HeroBhvr> ();
      skillBhvr = GetComponent<SkillBhvr> ();
      nextSkill = skillBhvr.NextSkill ();
      if (nextSkill == null) {
        this.enabled = false;
        Debug.Log ("Can not find active skill for hero .... please check your hero table and skill table !");
      } else {
        remainTime = 0;
      }
      // navigable
      navigable = GetComponent<DirectedNavigable> ();
      if (navigable == null) {
        navigable = gameObject.AddComponent<DirectedNavigable> ();
      }
      // status behaviour
      statusBhvr = GetComponent<HeroStatusBhvr> ();
      if (statusBhvr == null) {
        statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
      }
      // transform
      myTransform = transform;

      // group bhvr
      groupBhvr = GetComponent<GroupBhvr> ();
      if (groupBhvr == null) {
        groupBhvr = gameObject.AddComponent<GroupBhvr> ();
      }

    }

    void Update ()
    {
      // 确保没有被暂停，并且战队处于战斗状态
      if (!statusBhvr.IsPause && groupBhvr.Status == GroupStatus.battle) {

        if (nextSkill == null) {
          // 如果技能为空，获取一个技能
          nextSkill = skillBhvr.NextSkill ();
        }

        if (nextSkill != null) {
          TryAttack ();
        }

      }

      if (remainTime > 0) {
        // 还在冷却时间
        remainTime -= Time.deltaTime;
      }
    }

    private void TryAttack ()
    {

      switch (statusBhvr.Status) {

      case HeroStatus.idle:
        GameObject target = heroBhvr.FindClosestTarget ();
        if (target != null) {
          // 判断距离是否可攻击
          float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
          if (distance - nextSkill.distance > 0.2F) {
            // 移动到可攻击位置
            navigable.NavTo (target.transform.localPosition.x, nextSkill.distance);
          } else if (remainTime <= 0 || nextSkill.bigMove) {
            // 发起攻击
            heroBhvr.Attack (target, nextSkill);
            // 冷却开始
            remainTime = heroBhvr.hero.cd;
            // 获取下一个技能
            nextSkill = skillBhvr.NextSkill ();
          }
        }
        break;

      case HeroStatus.running:

        target = heroBhvr.FindClosestTarget ();
        if (target != null) {
          // 判断距离是否可攻击
          float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
          if (distance - nextSkill.distance > 0.1F) {
            // 移动到可攻击位置
            navigable.NavTo (target.transform.localPosition.x, nextSkill.distance);
          } else {
            // 转为空闲状态
            statusBhvr.Status = HeroStatus.idle;
          }
        }
        break;
      }
    }

  }
}

