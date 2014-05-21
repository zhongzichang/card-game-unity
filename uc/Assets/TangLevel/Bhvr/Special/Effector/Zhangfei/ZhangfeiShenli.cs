using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class ZhangfeiShenli : EffectorSpecialBhvr
  {
    public Vector3 offset = Vector3.zero;
    private Transform myTransform;
    private Transform targetTransform;
    private Vector3 backupPos;
    private BattleDirection direction;
    //
    // Use this for initialization
    void Awake ()
    {
      myTransform = transform;
    }
    // Update is called once per frame
    void Update ()
    {

      if (isPlay) {

        if (offset.y >= 0) {
          if (direction == BattleDirection.RIGHT) {
            targetTransform.localPosition = backupPos + offset;
          } else {
            targetTransform.localPosition = backupPos + new Vector3 (-offset.x, offset.y, offset.z);
          }
        } else {
          HeroBhvr targetHeroBhvr = w.target.GetComponent<HeroBhvr> ();
          if (targetHeroBhvr != null) {
            // 由僵直转化为打击
            targetHeroBhvr.BeBeat ();
          }
          myTransform.parent = null;
          Release ();
        }
      }
    }

    void OnEnable ()
    {
      if (!isPlay) {
        Resume ();
      }
    }

    public override void Play ()
    {

      isPlay = true;

      // 找最近的目标
      GameObject target = HeroSelector.FindClosestTarget (w.source.GetComponent<HeroBhvr> ());
      if (target != null) {

        w.target = target;
        targetTransform = target.transform;
        myTransform.parent = targetTransform;
        backupPos = targetTransform.localPosition;
        offset = Vector3.zero;
        direction = w.source.GetComponent<Directional> ().Direction;

        // 目标僵直
        HeroStatusBhvr targetStatusBhvr = target.GetComponent<HeroStatusBhvr> ();

        if (!targetStatusBhvr.IsBigMove) {
          HeroBhvr targetHeroBhvr = w.target.GetComponent<HeroBhvr> ();
          targetHeroBhvr.BeRigid ();
        }
      } else {
        Release ();
      }

    }
  }
}