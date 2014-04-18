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
    }

    void Update ()
    {

      // 空闲时找可攻击对象
      if (statusBhvr.Status == HeroStatus.idle) {

        GameObject target = LevelController.FindClosestTarget (this);
        if (target != null) {
          navigable.NavTo (target.transform.localScale.x);
        }
      }


    }

    void OnEnable ()
    {

    }

    void OnDisable ()
    {

    }
  }
}

