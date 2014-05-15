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
		UITweener[] tweeners = GetComponents<UITweener> ();
		foreach(UITweener tweener in tweeners){
			tweener.ResetToBeginning ();
			tweener.Play ();
		}
	}
}
