/// <summary>
/// Properties details panel.
/// xbhuang 
/// 2014-5-3
/// </summary>
using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
namespace TangGame.UI
{
	public class PropsDetailsPanel : MonoBehaviour
	{
		public object param;
		public GameObject PropsDetailsInterfacePanel;
		public GameObject PropsDetailsSubPanel;
		private PropsBase data;

		// Use this for initialization
		void Start ()
		{
			if(param != null && param is PropsBase)
				data = param as PropsBase;
			DynamicBindUtil.BindScriptAndProperty (PropsDetailsInterfacePanel, PropsDetailsInterfacePanel.name);
			DynamicBindUtil.BindScriptAndProperty (PropsDetailsSubPanel, PropsDetailsSubPanel.name);

			PropsDetailsSubPanel.GetComponent<PropsDetailsSubPanel> ().Data = data;
			PropsDetailsInterfacePanel.GetComponent<PropsDetailsInterfacePanel> ().Data = data;
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public PropsBase Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}
}