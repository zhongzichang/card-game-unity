using System;
using UnityEngine;

namespace TangUtils
{
  public class VectorUtil
  {
    public static bool EqualsIntXY (Vector3 v1, Vector3 v2)
    {

      if (((int)v1.x) == ((int)v2.x) && ((int)v1.y) == ((int)v2.y)) {
        return true;
      }

      return false;
    }
  }
}

