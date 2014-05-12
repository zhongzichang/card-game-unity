using UnityEngine;
using System.Collections;

namespace TangGame.UI{
	public class UIUtils {
		
		
		/// 设置GameObject的localPosition的Y值
		public static void SetY(GameObject go, float y){
			Vector3 temp = go.transform.localPosition;
			temp.y = y;
			go.transform.localPosition = temp;
		}

		/// 设置GameObject的localPosition的X值
		public static void SetX(GameObject go, float x){
			Vector3 temp = go.transform.localPosition;
			temp.x = x;
			go.transform.localPosition = temp;
		}

		/// 设置GameObject的localPosition的X,Y值
		public static void SetXY(GameObject go, float x, float y){
			Vector3 temp = go.transform.localPosition;
			temp.x = x;
			temp.y = y;
			go.transform.localPosition = temp;
		}

		/// 复制GameObject，固定设置了localScale，父对象
		public static GameObject Duplicate(GameObject go, GameObject parent){
			GameObject result = GameObject.Instantiate(go) as GameObject;
			result.SetActive(true);
			result.transform.parent = parent.transform;
			result.transform.localScale = Vector3.one;
			return result;
		}
	}
}