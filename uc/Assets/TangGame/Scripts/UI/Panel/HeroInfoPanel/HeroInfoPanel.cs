using UnityEngine;
using System.Collections;

public class HeroInfoPanel : MonoBehaviour
{

	public GameObject InterfaceSubPanel;
	public GameObject PictorialSubPanel;
	// Use this for initialization
	void Start ()
	{
		this.transform.localPosition += 100 * Vector3.left;
		DynamicBindUtil.BindScriptAndProperty (InterfaceSubPanel, InterfaceSubPanel.name);
		DynamicBindUtil.BindScriptAndProperty (PictorialSubPanel, PictorialSubPanel.name);
	}
}
