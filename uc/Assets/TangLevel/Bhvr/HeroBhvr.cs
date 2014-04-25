using System;
using UnityEngine;
using TDB = TangDragonBones;

namespace TangLevel
{
  /// <summary>
  /// 英雄的基本行为
  /// </summary>
  [RequireComponent (typeof(DirectedNavigable), typeof(HeroStatusBhvr))]
  public class HeroBhvr : MonoBehaviour
  {
    public Hero hero;
    private DirectedNavigable navigable;
    private HeroStatusBhvr statusBhvr;
    private Transform myTransform;
    private TDB.ArmatureBhvr armatureBhvr;

    #region MonoBehaviours

    void Start ()
    {
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
      statusBhvr.statusStartHandler += OnStatusStart;
      // transform
      myTransform = transform;
      // dragonbones behaviour
      armatureBhvr = GetComponent<TDB.ArmatureBhvr> ();
      armatureBhvr.GotoAndPlay (statusBhvr.Status.ToString());
    }


    void OnEnable ()
    {
      // 重新打开，状态设置为空闲
      if( statusBhvr != null )
        statusBhvr.Status = HeroStatus.idle;
    }

    void OnDisable ()
    {

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 攻击指定目标
    /// </summary>
    /// <param name="target">Target.</param>
    public void Attack (GameObject target)
    {

      if (statusBhvr != null)
        statusBhvr.Status = HeroStatus.attack;

    }

    /// <summary>
    /// 找距离最近的目标
    /// </summary>
    /// <returns>The closest target.</returns>
    public GameObject FindClosestTarget(){
      return LevelController.FindClosestTarget (this);
    }

    #endregion

    #region Tang Callback
    /// <summary>
    /// 状态开始回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusStart(HeroStatus status){
      switch (status) {
      case HeroStatus.attack:
        armatureBhvr.GotoAndPlay (status.ToString ());
        break;
      default:
        armatureBhvr.GotoAndPlay (status.ToString ());
        break;
      }
    }
    #endregion
  }
}

