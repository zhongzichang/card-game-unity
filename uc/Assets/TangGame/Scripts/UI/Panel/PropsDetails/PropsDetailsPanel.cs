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
		private PropsDetailsPanelBean propsDPbean;
		// Use this for initialization
		void Start ()
		{

			DynamicBindUtil.BindScriptAndProperty (PropsDetailsInterfacePanel, PropsDetailsInterfacePanel.name);
			DynamicBindUtil.BindScriptAndProperty (PropsDetailsSubPanel, PropsDetailsSubPanel.name);

				PropsDetailsSubPanel.GetComponent<PropsDetailsSubPanel> ().PropsDPbean = propsDPbean;
				PropsDetailsInterfacePanel.GetComponent<PropsDetailsInterfacePanel> ().Flush (propsDPbean);
		}

		void OnEnable ()
		{
			if (param != null && param is PropsBase) {
				PropsDPbean = new PropsDetailsPanelBean ();
				PropsDPbean.props = param as PropsBase;
			}
			if (param != null && param is PropsDetailsPanelBean) {
				PropsDPbean = param as PropsDetailsPanelBean;
			}
			if (PropsDetailsSubPanel.GetComponent<PropsDetailsSubPanel> () != null)
				PropsDetailsSubPanel.GetComponent<PropsDetailsSubPanel> ().PropsDPbean = propsDPbean;

			if (PropsDetailsInterfacePanel.GetComponent<PropsDetailsInterfacePanel> () != null)
				PropsDetailsInterfacePanel.GetComponent<PropsDetailsInterfacePanel> ().PropsDPbean = propsDPbean;
		}

		public PropsDetailsPanelBean PropsDPbean {
			get {
				return propsDPbean;
			}
			set {
				propsDPbean = value;
			}
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
	}

	public class PropsDetailsPanelBean
	{
		public PropsBase props;
		public HeroBase hero;
	}
}