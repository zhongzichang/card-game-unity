using System;
using UnityEngine;

namespace TangLevel
{
  [RequireComponent (typeof(HeroStatusBhvr))]
  public class ZoffsetBhvr : MonoBehaviour
  {
    private Transform myTransform;
    private HeroStatusBhvr statusBhvr;
    private Vector3 offset = new Vector3 (0F, 0F, 1F);

    void Start ()
    {

      myTransform = transform;
      statusBhvr = GetComponent<HeroStatusBhvr> ();
      statusBhvr.statusChangedHandler += OnStatusChanged;

    }

    private void OnStatusChanged (HeroStatus status)
    {
      if (status == HeroStatus.charge) { // 进入起手

        myTransform.localPosition -= offset;

      } else {

        switch (statusBhvr.beforeStatus) {
        case HeroStatus.release:
          myTransform.localPosition += offset;
          break;
        }
      }
    }
  }
}

