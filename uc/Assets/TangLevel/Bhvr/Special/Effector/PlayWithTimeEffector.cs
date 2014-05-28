using UnityEngine;
using System.Collections;

namespace TangLevel
{
  /// <summary>
  /// 播放指定的时间后退出
  /// </summary>
  public class PlayWithTimeEffector : EffectorSpecialBhvr
  {

    public float effectTime = 3F;

    private float remainTime = 0;

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

    public override void Play(){
      isPlay = true;
      remainTime = effectTime;
    }
  }
}
