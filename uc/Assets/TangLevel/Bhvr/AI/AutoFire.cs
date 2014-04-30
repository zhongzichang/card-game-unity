using System;
using UnityEngine;

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

    void Start ()
    {
      // hero behaviour
      heroBhvr = GetComponent<HeroBhvr> ();
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

      if (remainTime > period) {

        // 空闲时找可攻击对象
        if (statusBhvr.Status == HeroStatus.idle || statusBhvr.Status == HeroStatus.running) {

          GameObject target = heroBhvr.FindClosestTarget ();
          if (target != null) {

            //Debug.Log ("find target ----" + target.name);

            // 判断距离是否可攻击
            float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
            if (distance - heroBhvr.hero.attackDistance > 0.1F) {

              // 移动到可攻击位置
              navigable.NavTo (target.transform.localPosition.x, heroBhvr.hero.attackDistance);

              //Debug.Log ("find target ---- distance " + distance + "hero.attackDistance " + hero.attackDistance);

            } else {

              if (statusBhvr.Status == HeroStatus.idle) {

                // 发起攻击
                if (heroBhvr.hero.skills != null && heroBhvr.hero.skills.Count > 0) {
                  heroBhvr.Attack (target, heroBhvr.hero.skills [0]);
                  remainTime = 0;
                }

              }
            }
          }
        }
      }

      remainTime += Time.deltaTime;
    }
  }
}

