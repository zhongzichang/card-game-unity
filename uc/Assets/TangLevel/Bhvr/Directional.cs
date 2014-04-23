using System;
using UnityEngine;
using TD = TangDragonBones;

namespace TangLevel
{
  [ ExecuteInEditMode]
  [RequireComponent(typeof(HeroBhvr))]
  public class Directional : MonoBehaviour
  {
    public delegate void DirectionChange (BattleDirection direction);

    public DirectionChange directionChangeHandler;
    public HeroBhvr heroBhvr;

    public BattleDirection m_direction;
    public BattleDirection Direction {
      get {
        return m_direction;
      }
      set {
        m_direction = value;
        // 是否需要翻转方向
        if (m_direction == BattleDirection.LEFT) {
          transform.localRotation = Quaternion.FromToRotation (Vector3.left, Vector3.right);
        }
        else
          transform.localRotation = Quaternion.identity;
        if (directionChangeHandler != null) {
          // 调用委派
          directionChangeHandler (m_direction);
        }
      }
    }


    void Start(){

      heroBhvr = GetComponent<HeroBhvr> ();
      if (heroBhvr == null) {
        heroBhvr = gameObject.AddComponent<HeroBhvr> ();
      }
    }


  }
}

