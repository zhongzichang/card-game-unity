using System;
using UnityEngine;

namespace TangUtils
{
  public class TransformHelper
  {

    /// <summary>
    /// 旋转游戏对象，但是不旋转它的子对象
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="quat">Quat.</param>
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

    /// <summary>
    /// 分别修改父对象和子对象的旋转
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="p_quat">P quat.</param>
    /// <param name="c_quat">C quat.</param>
    public static void RotateParentChildrenSeparately (Transform parent, 
                                                       Quaternion p_quat, Quaternion c_quat)
    {

      // 缓存
      Transform[] children = parent.GetComponentsInChildren<Transform> ();
      // 改变
      parent.rotation = p_quat;
      // 修改子对象的旋转
      if (children != null && children.Length > 0) {
        for (int i = 0; i < children.Length; i++) {
          if (children [i] != parent) {
            children [i].rotation = c_quat;
          }
        }
      }

    }

    /// <summary>
    /// 修改 Children 的旋转
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="quat">Quat.</param>
    public static void RotateChildren(Transform parent, 
      Quaternion quat){
      // 缓存
      Transform[] children = parent.GetComponentsInChildren<Transform> ();
      // 修改子对象的旋转
      if (children != null && children.Length > 0) {
        for (int i = 0; i < children.Length; i++) {
          if (children [i] != parent) {
            children [i].rotation = quat;
          }
        }
      }
    }
  }
}

