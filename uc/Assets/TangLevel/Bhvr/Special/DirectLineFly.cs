using UnityEngine;
using System.Collections;

namespace TangLevel
{
  /// <summary>
  /// 直线飞行 - 从一个对象的位置飞行到另一个对象的位置
  /// </summary>
  public class DirectLineFly : SpecialBhvr
  {
    public float speed = 2;
    public float disappearDistance = 2;
    private Transform myTransform;
    private Vector3 spos;
    // 源位置
    private Vector3 tpos;
    // 目标位置
    private bool isPlay = false;

    void Awake ()
    {
      myTransform = transform;
    }
    // Update is called once per frame
    void Update ()
    {

      if (isPlay) {

        float distance = Vector3.Distance (myTransform.localPosition, tpos);

        float fraction = Time.deltaTime * speed / distance;

        if (distance < disappearDistance) {
          // 命中目标
          //Cache.notificationQueue.Enqueue (new Notification (NtftNames.HIT, 
          //  new HitBean (actorId, targetId, tokenCode)));

          Debug.Log (target+"===");
          // 发出作用器
          SkillBhvr targetSkillBhvr = target.GetComponent<SkillBhvr> ();
          if (targetSkillBhvr != null) {
            targetSkillBhvr.Receive (skill.effector, skill, source);
          }

          //Destroy (gameObject);
          GobjManager.Release (gameObject);
          isPlay = false;

        } else {

          myTransform.localPosition = Vector3.Lerp (myTransform.localPosition, 
            tpos, fraction);
        }
      }
	
    }

    void OnDisable ()
    {
      source = null;
      target = null;
      spos = Vector3.zero;
      tpos = Vector3.zero;
      myTransform.localRotation = Quaternion.identity;
    }

    public override void Play ()
    {

      if (source != null && target != null) {

        isPlay = true;

        spos = source.transform.localPosition;
        tpos = target.transform.localPosition;

        // 位置
        myTransform.localPosition = new Vector3 (spos.x, spos.y, spos.z);

        // 方向
        myTransform.localRotation = Quaternion.FromToRotation (Vector3.right, (tpos - myTransform.localPosition).normalized);

      } else {

        isPlay = false;
        GobjManager.Release (gameObject);

      }
    }
  }
}