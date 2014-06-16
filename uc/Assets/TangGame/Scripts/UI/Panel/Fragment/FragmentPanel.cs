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
    public const string NAME = "FragmentPanel";

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

		private FragmentInterfaceSubPanel mInterPanel;
		private FragmentPropsInfoSubPanel mInfoPanel;
		private FragmentSynthesisSubPanel mSyntPanel;
		private FragmentSellSubPanel mSellPanel;

		void Awake(){
			DynamicBindUtil.BindScriptAndProperty (FragmentPropsInfoSubPanel,FragmentPropsInfoSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (FragmentInterfaceSubPanel, FragmentInterfaceSubPanel.name);
			mInterPanel = FragmentInterfaceSubPanel.GetComponent<FragmentInterfaceSubPanel> ();
			mInfoPanel = FragmentPropsInfoSubPanel.GetComponent<FragmentPropsInfoSubPanel> ();
			if (mInterPanel != null)
				mInterPanel.infoPanel = mInfoPanel;

		}

    void OnDisable(){
      mInfoPanel.gameObject.SetActive (false);
    }
	}
}