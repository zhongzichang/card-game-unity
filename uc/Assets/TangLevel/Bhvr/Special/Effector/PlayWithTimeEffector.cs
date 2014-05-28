using UnityEngine;
using System.Collections;

namespace TangLevel
{
  /// <summary>
  /// 播放指定的时间后退出
  /// </summary>
  public class PlayWithTimeEffector : EffectorSpecialBhvr
  {
    public static Vector3 OFFSET = new Vector3 (0, 0, 3);

    public float effectTime = 3F;

    private float remainTime = 0;

    private Transform myTransform;

    void Awake(){
      myTransform = transform;
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

    void OnEnable(){

      // 绑定到目标身上
      myTransform.parent = w.target.transform;

      myTransform.localPosition = OFFSET;
      myTransform.localRotation = Quaternion.identity;
    }

    public override void Play(){
      isPlay = true;
      remainTime = effectTime;

    }
  }
}
