/// <summary>
/// Fragment properties info sub panel.
/// xbhuang
/// 2014-5-10
/// </summary>
using UnityEngine;
namespace TangGame.UI
{
	public class FragmentPropsInfoSubPanel : MonoBehaviour
	{
		/// <summary>
		/// 道具合成面板，父面板初始化时会被赋值
		/// </summary>
		FragmentSynthesisSubPanel mSyntPanel;

		public FragmentSynthesisSubPanel syntPanel {
			get {
				return mSyntPanel;
			}
			set {
				mSyntPanel = value;
			}
		}
	}
}