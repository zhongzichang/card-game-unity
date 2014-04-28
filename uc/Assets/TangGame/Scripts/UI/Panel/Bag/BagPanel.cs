/// <summary>
/// Bag panel.
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
	public class BagPanel : MonoBehaviour
	{
		public GameObject BagInterfaceSubPanel;
		public GameObject BagPropsInfoSubPanel;
		// Use this for initialization
		void Start ()
		{
			DynamicBindUtil.BindScriptAndProperty (BagInterfaceSubPanel,BagInterfaceSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (BagPropsInfoSubPanel, BagPropsInfoSubPanel.name);
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}