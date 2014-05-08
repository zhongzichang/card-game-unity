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
    public const float period = 3;
    private DirectedNavigable navigable;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private HeroBhvr heroBhvr;
    private float remainTime = period;
    // 所有技能
    private List<Skill> skills;
    // 当前技能
    private Skill skill;
    // 出手序列
    private int[] skillQueue;
    // 当前序列索引
    private int skillQueueIndex = 0;
    // 最近一个序列索引
    private int lastSkillQueueIndex = 0;

    /// <summary>
    /// 获取下一个技能
    /// </summary>
    /// <value>The next skill.</value>
    private Skill NextSkill {

      get {

        if (skills != null && skills.Count > 0 && skillQueue != null && skillQueue.Length > 0) {
          // 确保 skillQueueIndex  在出手序列范围内
          if (skillQueueIndex < skillQueue.Length && skillQueueIndex >= 0) {
            int skillIndex = skillQueue [skillQueueIndex];

            // 确保技能索引在技能列表范围内
            if (skillIndex < skills.Count && skillIndex >= 0) {
              Skill skill = skills [skillIndex];

              // 确保技能可用
              if (skill.enable) {
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
            return NextSkill;

          }

        }
        return null;
      }
    }

    void Start ()
    {
      // hero behaviour
      heroBhvr = GetComponent<HeroBhvr> ();
      skills = heroBhvr.hero.skills;
      skillQueue = heroBhvr.hero.skillQueue;
      skill = NextSkill;
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

      if (remainTime > skill.cd) {

        // 空闲时找可攻击对象
        if (statusBhvr.Status == HeroStatus.idle || statusBhvr.Status == HeroStatus.running) {

          GameObject target = heroBhvr.FindClosestTarget ();
          if (target != null) {

            // Debug.Log ("find target ----" + target.name);

            // 判断距离是否可攻击
            float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
            if (distance - heroBhvr.hero.attackDistance > 0.1F) {

              // 移动到可攻击位置
              navigable.NavTo (target.transform.localPosition.x, heroBhvr.hero.attackDistance);

              // Debug.Log ("find target ---- distance " + distance + "hero.attackDistance " + hero.attackDistance);

            } else {

              // 发起攻击
              heroBhvr.Attack (target, skill);
              remainTime = 0;
              Skill s = NextSkill;
              if (s != null) {
                skill = s;
              }
            }
          }
        }
      }

      remainTime += Time.deltaTime;
    }

    void OnEnable(){
      if (skill != null) {
        remainTime = skill.cd;
      }
    }
  }
}

