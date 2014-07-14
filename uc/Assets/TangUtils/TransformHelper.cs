using System;
using UnityEngine;

namespace TangUtils
{
  public class TransformHelper
  {
    public static void RotateWithoutChildren (Transform transform, Quaternion quat)
    {
      // 缓存
      Transform[] children = transform.GetComponentsInChildren<Transform> ();
      Quaternion[] childrenQuat = null;
      if (children != null && children.Length > 0) {
        childrenQuat = new Quaternion[children.Length];
        for (int i = 0; i < children.Length; i++) {
          childrenQuat [i] = children [i].rotation;
        }
      }
      // 改变
      transform.rotation = quat;
      // 还原
      if (children != null && children.Length > 0) {
        for (int i = 0; i < children.Length; i++) {
          if (children [i] != transform) {
            children [i].rotation = childrenQuat [i];
          }
        }
      }
    }
  }
}

