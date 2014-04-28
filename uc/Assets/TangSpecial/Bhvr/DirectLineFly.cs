using UnityEngine;
using System.Collections;

namespace TangSpecial
{
  /// <summary>
  /// 直线飞行 - 从一个对象的位置飞行到另一个对象的位置
  /// </summary>
  public class DirectLineFly : SpecialBhvr
  {
  
    public float speed;
    public float disappearDistance;


    private Transform myTransform;
    private Vector3 spos; // 源位置
    private Vector3 tpos; // 目标位置

    void Awake ()
    {
      myTransform = transform;
    }

    void Start(){
      Init ();
    }

    // Update is called once per frame
    void Update ()
    {

      float distance = Vector3.Distance (myTransform.localPosition, tpos);

      float fraction = Time.deltaTime * speed / distance;

      if (distance < disappearDistance) {
        // 命中目标
        //Cache.notificationQueue.Enqueue (new Notification (NtftNames.HIT, 
        //  new HitBean (actorId, targetId, tokenCode)));

        //Destroy (gameObject);
        GobjManager.Release (gameObject);

      } else {

        myTransform.localPosition = Vector3.Lerp (myTransform.localPosition, 
          tpos, fraction);
      }
	
    }

    void OnEanble ()
    {
      Init ();
    }

    void OnDisable(){
      source = null;
      target = null;
      spos = Vector3.zero;
      tpos = Vector3.zero;
    }

    private void Init(){

      if (source != null && target != null) {

        spos = source.transform.localPosition;
        tpos = target.transform.localPosition;

        // 位置
        myTransform.localPosition = new Vector3 (spos.x, spos.y, spos.z);

        // 方向
        myTransform.localRotation = Quaternion.FromToRotation (Vector3.right, (tpos - myTransform.localPosition).normalized);

      } else {

        gameObject.SetActive (false);

      }
    }
  }
}