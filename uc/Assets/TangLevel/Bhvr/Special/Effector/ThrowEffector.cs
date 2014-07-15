using System.Collections;
using UnityEngine;


namespace TangLevel
{
  /// <summary>
  /// 投射
  /// </summary>
  public class ThrowEffector : EffectorSpecialBhvr
  {

    public const float ANGLE_COMPUTE_PRECISE = 0.1F;

    //public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public Vector3 offset = Vector3.zero;

    private Transform myTransform;
    private Vector3 lastPosition = Vector3.zero; // 最进的位置

    void Awake ()
    {
      myTransform = transform;      
    }

    public override void Play ()
    {
      isPlay = true;
      StartCoroutine (SimulateProjectile ());
    }

    public override void Pause ()
    {
      isPlay = false;
    }

    public override void Resume ()
    {
      isPlay = true;
    }


    IEnumerator SimulateProjectile ()
    {
      // Short delay added before Projectile is thrown
      //yield return new WaitForSeconds (0.2f);

      // Move projectile to the position of throwing object + add some offset if needed.
      myTransform.position = w.source.transform.position + offset;

      // Calculate distance to target
      Vector3 srcpos = myTransform.position;
      Vector3 tgtpos = w.target.transform.position;

      // Calculate distance to target
      float target_Distance = Vector3.Distance (srcpos, tgtpos);

      // Calculate the velocity needed to throw the object to the target at specified angle.
      float projectile_Velocity = target_Distance / (Mathf.Sin (2 * firingAngle * Mathf.Deg2Rad) / gravity);

      // Extract the X  Y componenent of the velocity
      float Vx = Mathf.Sqrt (projectile_Velocity) * Mathf.Cos (firingAngle * Mathf.Deg2Rad);
      float Vy = Mathf.Sqrt (projectile_Velocity) * Mathf.Sin (firingAngle * Mathf.Deg2Rad);

      // Calculate flight time.
      float flightDuration = target_Distance / Vx;

      // Rotate projectile to face the target.
      //myTransform.rotation = Quaternion.LookRotation (tgtpos - srcpos);
      Quaternion quat = Quaternion.LookRotation (tgtpos - srcpos);
      TangUtils.TransformHelper.RotateWithoutChildren (myTransform, quat);
      lastPosition = srcpos;

      float elapse_time = 0;

      while (elapse_time < flightDuration) {
        if (isPlay) {

          myTransform.Translate (0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
          elapse_time += Time.deltaTime;

          Vector3 pos = myTransform.position;
          Vector3 diff = pos - lastPosition;
          diff = new Vector3 (diff.x, diff.y, 0);

          if (lastPosition != Vector3.zero && 
            (Mathf.Abs(diff.x) > ANGLE_COMPUTE_PRECISE ||
              Mathf.Abs(diff.y) > ANGLE_COMPUTE_PRECISE)) {

            quat = Quaternion.FromToRotation (Vector3.right, diff.normalized );
            TangUtils.TransformHelper.RotateChildren (myTransform, quat);

            lastPosition = pos;

          }

        }

        yield return null;
      }

      // 抛出子作用器 ---
      SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
      foreach (Effector e in w.effector.subEffectors) {
        EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, null);
        cw.param = myTransform.position; // 传递当前位置为爆炸位置
        sourceSkillBhvr.Cast (cw);
      }

      // 释放资源
      StartRelease ();
    }
  }
}

