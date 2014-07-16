using UnityEngine;
using System.Collections;


namespace TangLevel
{
  
  public class ShotEffector : EffectorSpecialBhvr
  {

    public float speed = 30;
    public float disappearDistance = 2;
    public Vector3 offset = Vector3.zero;
    public float flightDuration = 3F;
    
    private Transform myTransform;
    private Transform targetTransform;

    void Awake ()
    {
      myTransform = transform;
    }


    public override void Play ()
    {
      isPlay = true;
      StartCoroutine (SimulateFly ());
    }

    private IEnumerator SimulateFly ()
    {
      myTransform.position = w.source.transform.position + offset;
      Quaternion quat = (Quaternion)w.param;
      if (quat == null) {
        quat = Quaternion.identity;
      }
      TangUtils.TransformHelper.RotateWithoutChildren (myTransform, quat);

      float elapse_time = 0;
      while (elapse_time < flightDuration) {
        if (isPlay) {
          myTransform.Translate (speed * Time.deltaTime, 0, 0);

          float distance = Vector3.Distance (myTransform.position, targetTransform.position);

          if (distance < disappearDistance) {
// 命中目标
            SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
            if (sourceSkillBhvr != null && w.effector.subEffectors != null && w.effector.subEffectors.Length > 0) {
              EffectorWrapper cw = EffectorWrapper.W (w.effector.subEffectors [0], w.skill, w.source, w.target);
              sourceSkillBhvr.Cast (cw);
            }

            StartRelease ();
            isPlay = false;

          }

        }
        yield return null;
      }
    }
  }
}