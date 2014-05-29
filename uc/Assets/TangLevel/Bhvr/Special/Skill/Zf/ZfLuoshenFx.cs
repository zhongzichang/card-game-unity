using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class ZfLuoshenFx : SkillSpecialBhvr
  {
    public static Vector3 DEFAULT_OFFSET = new Vector3 (0, 2F, -3);
    public static Vector3 DEFAULT_SCALE = new Vector3 (1.3F, 1.3F, 1F);
    private Transform myTransform;
    public Vector3 offset = DEFAULT_OFFSET;
    public Vector3 scale = DEFAULT_SCALE;
    public float time;
    private float remainTime;
    private Vector3 reversalOffset = Vector3.zero;

    #region MonoMethods

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
      reversalOffset = new Vector3 (offset.x, offset.y, -offset.z);
    }

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

    void OnEnable ()
    {

      if (w != null && w.source != null) {

        // 定位到自己身上
        Transform st = w.source.transform;
        myTransform.parent = st;
        myTransform.localRotation = Quaternion.identity;
        myTransform.localScale = scale;
        myTransform.localPosition = st.localRotation == Quaternion.identity ? offset : reversalOffset;  
        remainTime = time;

      } else {
        StartRelease ();
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;
    }

    void OnDisable ()
    {

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;
    }

    #endregion
   

    #region PublicMethods

    public override void Play ()
    {
      isPlay = true;
      animator.speed = 1F;
      //StartPlayOnce
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

