using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class ZhangfeiShenliSkill : SkillSpecialBhvr
  {
    public float effectTime = 1F;

    private float remainTime = 0;

    // Update is called once per frame
    void Update ()
    {
      if (isPlay) {
        if (remainTime > 0) {
          remainTime -= Time.deltaTime;
        } else {
          Release ();
        }
      }
    }

    public override void Play(){

      isPlay = true;
      remainTime = effectTime;
      transform.parent = w.source.transform;
      transform.localPosition = Vector3.zero;

    }
  }
}
