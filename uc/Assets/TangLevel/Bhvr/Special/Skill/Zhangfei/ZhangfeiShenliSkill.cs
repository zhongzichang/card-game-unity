using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class ZhangfeiShenliSkill : SkillSpecialBhvr
  {

    public Vector3 offset = new Vector3(0, 0, 1);

    public float effectTime = 1F;
    private float remainTime = 0;

    private Transform myTransform;

    void Awake(){
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update ()
    {
      if (isPlay) {
        if (remainTime > 0) {
          remainTime -= Time.deltaTime;
        } else {
          StartRelease ();
        }
      }
    }


    #region PublicMethods
    public override void Play(){

      isPlay = true;
      remainTime = effectTime;
      transform.parent = w.source.transform;
      transform.localPosition = offset;

    }


    public override void Pause ()
    {
      isPlay = false;
      animator.speed = 0F;
    }

    public override void Resume ()
    {
      isPlay = true;
      animator.speed = 1F;
    }

    #endregion

    #region ProtectedMethods
    protected override void OnRelease ()
    {
      if (myTransform != null) {
        myTransform.parent = null;
      }
    }
    #endregion
  }
}
