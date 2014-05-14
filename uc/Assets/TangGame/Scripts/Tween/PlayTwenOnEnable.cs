using UnityEngine;
using System.Collections;

public class PlayTwenOnEnable : MonoBehaviour {


	void OnEnable(){
		this.StartPlayAnimation ();
	}
	/// <summary>
	/// Starts the play animation.
	/// 打开的动画
	/// </summary>
	public void StartPlayAnimation(){
		UITweener tweener = this.GetComponent<UITweener> ();
		if (tweener != null) {
			tweener.ResetToBeginning ();
			tweener.Play ();
		}
	}
}
