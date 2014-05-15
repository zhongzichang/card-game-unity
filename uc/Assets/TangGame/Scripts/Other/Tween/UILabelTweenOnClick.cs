using UnityEngine;
using System.Collections;

public class UILabelTweenOnClick : MonoBehaviour
{
	public Vector3 labVector = new Vector3 (0, -2, 0);
	bool mOnClicked;
	// Use this for initialization
	void Start ()
	{
	
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnPress (bool bl)
	{
		UILabel[] labs = GetComponentsInChildren<UILabel> ();
		if (bl) {
			foreach (UILabel lab in labs) {
				lab.gameObject.transform.localPosition += labVector;
			}
		} else {
			foreach (UILabel lab in labs) {
				lab.gameObject.transform.localPosition -= labVector;
			}
		}
	}
}
