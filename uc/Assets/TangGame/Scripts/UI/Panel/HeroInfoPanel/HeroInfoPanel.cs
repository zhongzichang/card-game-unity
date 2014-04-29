using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class HeroInfoPanel : MonoBehaviour
	{
		public object param;
		public GameObject HeroInfoInterfaceSubPanel;
		public GameObject HeroInfoPictorialSubPanel;
		public GameObject HeroInfoPropertySubPanel;
		public GameObject HeroInfoSkillSubPanel;
		private HeroBase herobase;
		// Use this for initialization
		void Awake ()
		{
			DynamicBindUtil.BindScriptAndProperty (HeroInfoInterfaceSubPanel, HeroInfoInterfaceSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (HeroInfoPictorialSubPanel, HeroInfoPictorialSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (HeroInfoPropertySubPanel, HeroInfoPropertySubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (HeroInfoSkillSubPanel, HeroInfoSkillSubPanel.name);
		}

		void Start ()
		{

		}

		void OnEnable ()
		{
			if (herobase != param as HeroBase) {
				herobase = param as HeroBase;
				this.RefreshSubPanel (herobase);
			}
		}
		//TODO listener herobase change;
		void RefreshSubPanel (HeroBase hero)
		{
			HeroInfoInterfaceSubPanel.GetComponent<HeroInfoInterfaceSubPanel> ().RefreshPanel (hero);
			HeroInfoPictorialSubPanel.GetComponent<HeroInfoPictorialSubPanel> ().RefreshPanel (hero);
			HeroInfoPropertySubPanel.GetComponent<HeroInfoPropertySubPanel> ().Flush (hero);
			HeroInfoSkillSubPanel.GetComponent<HeroInfoSkillSubPanel> ().Flush (hero);

		}
	}
}