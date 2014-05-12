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
		/// 该面板当前的道具
		/// </summary>
		PropsBase mProps;
		/// <summary>
		/// 道具合成面板，父面板初始化时会被赋值
		/// </summary>
		FragmentSynthesisSubPanel mSyntPanel;

		void Flush (PropsBase props)
		{
			if (mProps != props) {
				mProps = props;
			}


		}

		void UpPropsItem (PropsBase props)
		{
			PropsItem item = this.PropsItem.GetComponent<PropsItem> ();
			if (item == null)
				return;
			item.Flush (props);
			item.propsCountLabel.text = "";
		}

		void UpPropsName (string name)
		{
			UILabel lab = PropsName.GetComponent<UILabel> ();
			if (lab != null)
				lab.text = name;
		}

		void UpPropsCount (int count)
		{
			UILabel lab = PropsCount.GetComponent<UILabel> ();
			if (lab != null)
				lab.text = string.Format (UIPanelLang.HAS_NUMBER_OF_PROPS, count);
		}
		void UpPropsInfo(string infoStr){
			UILabel lab = PropsInfoLabel.GetComponent<UILabel> ();
			if (lab != null) {
				lab.text = infoStr;
				Utils.TextBgAdaptiveSize (lab, PropsInfoBg.GetComponent<UISprite> ());
				PropInfoTable.GetComponent<UITable> ().repositionNow = true;
			}
		}
		void UpPropsDesc(string descStr){
			UILabel lab = PropsDescLabel.GetComponent<UILabel> ();
			if (lab != null) {
				lab.text = descStr;
			}
		}
		void UpPrice(int num){
			UILabel lab = PriceLabel.GetComponent<UILabel> ();
			if (lab != null)
				lab.text = num.ToString ();
		}

		#region hero the object onclick
		public void SynthesisBtnOnClick(GameObject obj){
			
		}
		public void SellBtnOnClick(){

		}
		#endregion

		#region here the objet's properties

		public FragmentSynthesisSubPanel syntPanel {
			get {
				return mSyntPanel;
			}
			set {
				mSyntPanel = value;
			}
		}

		public PropsBase props {
			get {
				return mProps;
			}
			set {
				mProps = value;
			}
		}

		#endregion

		#region here the gameobjets

		/// <summary>
		/// The properties item.
		/// 道具对象
		/// </summary>
		public GameObject PropsItem;
		public GameObject PropsName;
		public GameObject PropsCount;
		public GameObject PropsInfoLabel;
		public GameObject PropInfoTable;
		public GameObject PropsInfoBg;
		public GameObject PropsDescLabel;
		public GameObject PriceLabel;
		public GameObject SellBtn;
		public GameObject SynthesisBtn;

		#endregion
	}
}