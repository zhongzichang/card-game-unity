using UnityEngine;
using System.Collections;

public class OnClickLableDown : MonoBehaviour
{
	/// <summary>
	/// The displacement pixels. 位移多少像素
	/// </summary>
	public Vector3 displacementPixels = Vector3.zero;
	private UILabel[] labels;

	void OnEnable(){
		labels = GetComponentsInChildren<UILabel> ();
	}
	void OnPress (bool bl)
	{
		if (bl) {
			foreach (UILabel label in labels) {
				label.transform.localPosition += displacementPixels;
			}
		} else {
			foreach (UILabel label in labels) {
				label.transform.localPosition -= displacementPixels;
			}
		}
			
	}
}
