/// <summary>
/// Fragment interface sub panel.
/// xbhuang 
/// 2014-5-10
/// </summary>
using UnityEngine;

namespace TangGame.UI
{
	public class FragmentInterfaceSubPanel : MonoBehaviour
	{
		/// <summary>
		/// 碎片信息面板，父面板初始化时会被赋值
		/// </summary>
		FragmentPropsInfoSubPanel mInfoPanel;

		public GameObject ToggleAll;

		public FragmentPropsInfoSubPanel infoPanel {
			get {
				return mInfoPanel;
			}
			set {
				mInfoPanel = value;
			}
		}
	}
}