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
		private object mParam;
		public GameObject PropsDetailsInterfacePanel;
		public GameObject PropsDetailsSubPanel;
		private PropsDetailsPanelBean propsDPbean;
		void Awake(){
			DynamicBindUtil.BindScriptAndProperty (PropsDetailsInterfacePanel, PropsDetailsInterfacePanel.name);
			DynamicBindUtil.BindScriptAndProperty (PropsDetailsSubPanel, PropsDetailsSubPanel.name);
		}

		/// <summary>
		/// Ups the sub panels.
		/// 更新子面板的数据
		/// </summary>
		/// <param name="val">Value.</param>
		private void UpSubPanels (object val){
			if (val != null && val is PropsBase) {
				PropsDPbean = new PropsDetailsPanelBean ();
				PropsDPbean.props = val as PropsBase;
			}
			if (val != null && val is PropsDetailsPanelBean) {
				PropsDPbean = val as PropsDetailsPanelBean;
			}
			if (PropsDetailsSubPanel.GetComponent<PropsDetailsSubPanel> () != null)
				PropsDetailsSubPanel.GetComponent<PropsDetailsSubPanel> ().PropsDPbean = propsDPbean;
			if (PropsDetailsInterfacePanel.GetComponent<PropsDetailsInterfacePanel> () != null)
				PropsDetailsInterfacePanel.GetComponent<PropsDetailsInterfacePanel> ().PropsDPbean = propsDPbean;
		}

		//=====================对象属性相关／property=================
		public object param {
			get {
				return mParam;
			}
			set {
				mParam = value;
				UpSubPanels (mParam);
			}
		}
		public PropsDetailsPanelBean PropsDPbean {
			get {
				return propsDPbean;
			}
			set {
				propsDPbean = value;
			}
		}
	}
	/// <summary>
	/// Properties details panel bean.
	/// 装备数据
	/// </summary>
	public class PropsDetailsPanelBean
	{
		public PropsBase props;
		public HeroBase hero;
	}
}