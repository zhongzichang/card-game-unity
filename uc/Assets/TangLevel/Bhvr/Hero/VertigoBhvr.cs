using System;
using UnityEngine;
namespace TangLevel
{
  public class VertigoBhvr : MonoBehaviour
  {

    public float effectTime = 3F;
    private float remainTime = 0;
    private HeroStatusBhvr statusBhvr;


    void Awake(){
      statusBhvr = GetComponent<HeroStatusBhvr> ();
    }

    void Update(){
      if (remainTime > 0) {
        remainTime -= Time.deltaTime;
      } else {
        statusBhvr.Status = HeroStatus.idle;
        this.enabled = false;
      }
    }

    void OnEnable(){
      remainTime = effectTime;
    }


  }
}

