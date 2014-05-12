/// <summary>
/// FragmentPanel panel.
/// xbhuang 2014-5-10
/// </summary>
/// 
using UnityEngine;

namespace TangGame.UI
{
	public class FragmentPanel : MonoBehaviour
	{
		/// <summary>
		/// The fragment interface sub panel.
		/// 碎片列表主面板
		/// </summary>
		public GameObject FragmentInterfaceSubPanel;
		/// <summary>
		/// The fragment properties info sub panel.
		/// 单个碎片的信息面板
		/// </summary>
		public GameObject FragmentPropsInfoSubPanel;
		/// <summary>
		/// The fragment synthesis sub panel.
		/// 碎片合成面板
		/// </summary>
		public GameObject FragmentSynthesisSubPanel;

		private FragmentInterfaceSubPanel mInterPanel;
		private FragmentPropsInfoSubPanel mInfoPanel;
		private FragmentSynthesisSubPanel mSyntPanel;
		private FragmentSellSubPanel mSellPanel;

		void Awake(){
			DynamicBindUtil.BindScriptAndProperty (FragmentPropsInfoSubPanel,FragmentPropsInfoSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (FragmentSynthesisSubPanel, FragmentSynthesisSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (FragmentInterfaceSubPanel, FragmentInterfaceSubPanel.name);
			mInterPanel = FragmentInterfaceSubPanel.GetComponent<FragmentInterfaceSubPanel> ();
			mInfoPanel = FragmentPropsInfoSubPanel.GetComponent<FragmentPropsInfoSubPanel> ();
			mSyntPanel = FragmentSynthesisSubPanel.GetComponent < FragmentSynthesisSubPanel> ();
			if (mInterPanel != null)
				mInterPanel.infoPanel = mInfoPanel;
			if (mInfoPanel != null)
				mInfoPanel.syntPanel = mSyntPanel;

		}
	}
}