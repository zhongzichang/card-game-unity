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
    public Skill skill; // 当前技能

    public const float period = 3;
    private DirectedNavigable navigable;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private HeroBhvr heroBhvr;
    private SkillBhvr skillBhvr;
    private float remainTime = period;

    void Start ()
    {
      // hero behaviour
      heroBhvr = GetComponent<HeroBhvr> ();
      skillBhvr = GetComponent<SkillBhvr> ();
      skill = skillBhvr.NextSkill();
      if (skill == null) {
        this.enabled = false;
        Debug.Log ("Can not find active skill for hero .... please check your hero table and skill table !");
      } else {
        remainTime = skill.cd;
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

    }

    void Update ()
    {

      if (skill != null && !statusBhvr.IsPause) {

        switch (statusBhvr.Status) {

        case HeroStatus.idle:
          GameObject target = heroBhvr.FindClosestTarget ();
          if (target != null) {
            // 判断距离是否可攻击
            float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
            if (distance - skill.distance > 0.2F) {
              // 移动到可攻击位置
              navigable.NavTo (target.transform.localPosition.x, skill.distance);
            } else if(remainTime > skill.cd) {
              // 发起攻击
              heroBhvr.Attack (target, skill);
              remainTime = 0;
              skill = skillBhvr.NextSkill();
            }
          }
          break;
        case HeroStatus.running:
          target = heroBhvr.FindClosestTarget ();
          if (target != null) {
            // 判断距离是否可攻击
            float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
            if (distance - skill.distance > 0.1F) {
              // 移动到可攻击位置
              navigable.NavTo (target.transform.localPosition.x, skill.distance);
            } else {
              statusBhvr.Status = HeroStatus.idle;
            }
          }
          break;
        }
      }
      remainTime += Time.deltaTime;
    }
  }
}

