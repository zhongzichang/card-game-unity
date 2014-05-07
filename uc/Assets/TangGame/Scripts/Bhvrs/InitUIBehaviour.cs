using UnityEngine;
using System.Collections;

namespace TangGame{
	public class InitUIBehaviour : MonoBehaviour {
		
		public GameObject uiRoot;
		
		void Awake(){
			Global.uiRoot = uiRoot;
		}
	}
}

