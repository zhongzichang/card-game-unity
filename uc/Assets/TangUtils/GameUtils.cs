using UnityEngine;
using System.Collections;

public class GameUtils
{
		/// <summary>
		/// 获取第二个点相对于第一个点的方向
		/// </summary>
		/// <param name="position1">第一个点</param>
		/// <param name="position2">第二个点</param>
		/// <returns></returns>
		public static Vector3 GetDirection (Vector3 position1, Vector3 position2)
		{
				Vector3 heading = position2 - position1;
				float distance = heading.magnitude;
				return heading / distance;
		}
}
