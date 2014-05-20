using UnityEngine;
using System.Collections;
using TangGame.UI;

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
			//给相关面板动态绑定脚本
			DynamicBindUtil.BindScriptAndProperty (HeroInfoInterfaceSubPanel, HeroInfoInterfaceSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (HeroInfoPictorialSubPanel, HeroInfoPictorialSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (HeroInfoPropertySubPanel, HeroInfoPropertySubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (HeroInfoSkillSubPanel, HeroInfoSkillSubPanel.name);
		}

		IEnumerator Start ()
		{
			float waitTime = 0.5f;
			TweenAlpha al = GetComponent<TweenAlpha> ();
			if (al != null && al.duration >= 0.1f) {
				waitTime = al.duration - 0.1f;
			}
			yield return new WaitForSeconds (0.5f);
			HeroInfoInterfaceSubPanel.SetActive (true);
			HeroInfoPictorialSubPanel.SetActive (true);
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