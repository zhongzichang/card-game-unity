using UnityEngine;
using System.Collections;


namespace TangLevel
{
  
  public class ShotEffector : EffectorSpecialBhvr
  {
    /// <summary>
    /// 飞行速度
    /// </summary>
    public float speed = 30;
    /// <summary>
    /// 距离目标多远的时候消失（距离目标多远可以认为是命中）
    /// </summary>
    public float disappearDistance = 2;
    /// <summary>
    /// 偏移
    /// </summary>
    public Vector3 offset = Vector3.zero;
    // 飞行时间，如果超过这个时间还没有命中目标，则释放自己所占资源
    public float flightDuration = 3F;
    
    private Transform myTransform;
    private Transform sourceTransform;
    private Transform targetTransform;

    void Awake ()
    {
      myTransform = transform;
    }

    void OnDisable(){
      isPlay = false;
    }

    public override void Play ()
    {
      isPlay = true;

      sourceTransform = w.source.transform;
      targetTransform = w.target.transform;

      StartCoroutine (SimulateFly ());
    }

    private IEnumerator SimulateFly ()
    {
      // 子弹的当前位置
      myTransform.position = sourceTransform.position + offset;
      // 子弹的射击方向
      Vector3 direction = targetTransform.position - sourceTransform.position;
      Vector3 direction2D = new Vector3 (direction.x, direction.y, 0);

      // ratation
      Quaternion quat;
      if (w.param != null) {
        quat = (Quaternion)w.param;
      } else {
        // 计算 Quaternion
        quat = Quaternion.LookRotation (direction);
      }
      TangUtils.TransformHelper.RotateWithoutChildren (myTransform, quat);

      // children rotation
      Quaternion childQuat = Quaternion.FromToRotation (Vector3.right,  direction2D);
      TangUtils.TransformHelper.RotateChildren (myTransform, childQuat);

      float elapse_time = 0;
      while (elapse_time < flightDuration) {

        if (isPlay) {

          myTransform.Translate (0, 0, speed * Time.deltaTime);
          elapse_time += Time.deltaTime;

          float distance = Vector3.Distance (myTransform.position, targetTransform.position + offset);

          if (distance < disappearDistance) {

            // 命中目标
            SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
            if (sourceSkillBhvr != null
                  && w.effector.subEffectors != null
                  && w.effector.subEffectors.Length > 0) {
              foreach (Effector e in w.effector.subEffectors) {
                EffectorWrapper cw = EffectorWrapper.W (e, 
                                         w.skill, w.source, w.target);
                sourceSkillBhvr.Cast (cw);
              }
            }

            isPlay = false;
            break;
          } // 已命中


        } // isPlay

        yield return null;
      } // while


      StartRelease ();

    }
    // method
  }
}