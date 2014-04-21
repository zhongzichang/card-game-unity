using System;
using UnityEngine;

namespace TangLevel
{
  [RequireComponent (typeof(DirectedNavigable), typeof(HeroStatusBhvr))]
  public class HeroBhvr : MonoBehaviour
  {
    public Hero hero;
    private DirectedNavigable navigable;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;

    #region MonoBehaviours

    void Start ()
    {
      navigable = GetComponent<DirectedNavigable> ();
      if (navigable == null) {
        navigable = gameObject.AddComponent<DirectedNavigable> ();
      }
      statusBhvr = GetComponent<HeroStatusBhvr> ();
      if (statusBhvr == null) {
        statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
      }
      myTransform = transform;
    }

    void Update ()
    {

      // 空闲时找可攻击对象
      if (statusBhvr.Status == HeroStatus.idle || statusBhvr.Status == HeroStatus.running) {

        GameObject target = LevelController.FindClosestTarget (this);
        if (target != null) {
          //Debug.Log ("find target ----" + target.name);

          // 判断距离是否可攻击
          float distance = Mathf.Abs (target.transform.localPosition.x - myTransform.localPosition.x);
          if (distance - hero.attackDistance > 0.1F) {

            // 移动到可攻击位置
            navigable.NavTo (target.transform.localPosition.x, hero.attackDistance);
            //Debug.Log ("find target ---- distance " + distance + "hero.attackDistance " + hero.attackDistance);
          } else {

            if (statusBhvr.Status != HeroStatus.attack) {

              // 发起攻击
              Attack (target);

            }
          }
        }
      }


    }

    void OnEnable ()
    {
      // 重新打开，状态设置为空闲
      if (statusBhvr != null)
        statusBhvr.Status = HeroStatus.idle;
    }

    void OnDisable ()
    {

    }

    #endregion

    #region Private Methods

    private void Attack (GameObject target)
    {

      if (statusBhvr != null)
        statusBhvr.Status = HeroStatus.attack;

    }

    #endregion
  }
}

