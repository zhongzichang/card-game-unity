using System;
using UnityEngine;
namespace TangLevel
{
  public class TimeTargetEffect : EffectorSpecialBhvr
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

    void OnDisable(){
      myTransform.parent = null;
    }

    public override void Play(){
      isPlay = true;
      remainTime = effectTime;

    }
  }
}

