using UnityEngine;
using System.Collections;

namespace TangGame
{
	public class HeroInfoPanel : MonoBehaviour
	{
		public object param;
		public GameObject InterfaceSubPanel;
		public GameObject PictorialSubPanel;
		public GameObject PropertyInfoSubPanel;
		public GameObject SkillInfoSubPanel;
		private HeroBase herobase;
		// Use this for initialization
		void Start ()
		{
			this.transform.localPosition += 100 * Vector3.left;
			DynamicBindUtil.BindScriptAndProperty (InterfaceSubPanel, InterfaceSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (PictorialSubPanel, PictorialSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (PropertyInfoSubPanel, PropertyInfoSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (SkillInfoSubPanel, SkillInfoSubPanel.name);
			herobase = param as HeroBase;
			this.RefreshSubPanel (herobase);
		}

		void OnEnable ()
		{
			this.transform.localPosition += 100 * Vector3.left;
			if (herobase != param as HeroBase) {
				herobase = param as HeroBase;
				this.RefreshSubPanel (herobase);
			}
		}
		//TODO listener herobase change;

		void RefreshSubPanel (HeroBase hero)
		{
			InterfaceSubPanel.GetComponent<InterfaceSubPanel> ().RefreshPanel (hero);
			PictorialSubPanel.GetComponent<PictorialSubPanel> ().RefreshPanel (hero);
			PropertyInfoSubPanel.GetComponent<PropertyInfoSubPanel> ().Flush (hero);
			SkillInfoSubPanel.GetComponent<SkillInfoSubPanel> ().Flush (hero);

		}

	}
}