/// <summary>
/// Fragment properties info sub panel.
/// xbhuang
/// 2014-5-10
/// </summary>
using UnityEngine;
using TangUI;
namespace TangGame.UI
{
	public class FragmentPropsInfoSubPanel : MonoBehaviour
	{
		/// <summary>
		/// 该面板当前的道具
		/// </summary>
		Props mProps;

		void Start(){
			UIEventListener.Get (SellBtn).onClick += SellBtnOnClick;
			UIEventListener.Get (SynthesisBtn).onClick += SynthesisBtnOnClick;
		}
		void UpPropsItem (Props props)
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
			TangGame.UIContext.mgrCoC.LazyOpen (FragmentSynthesisSubPanel.NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE, mProps);
		}
		public void SellBtnOnClick(GameObject obj){
			TangGame.UIContext.mgrCoC.LazyOpen (UIContext.SELL_PANEL_NAME, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE, mProps);
		}
		#endregion

		#region here the objet's properties


		public Props props {
			get {
				return mProps;
			}
			set {
				mProps = value;
        UpdateData();
			}
		}
		private void UpdateData(){
			if(!this.gameObject.activeSelf){
				this.gameObject.SetActive(true);
				TweenPosition tween = this.gameObject.GetComponent<TweenPosition>();
				tween.ResetToBeginning();
				tween.Play();
			}
			UpPropsItem (mProps);
			UpPropsName (mProps.data.name);
			UpPropsCount (mProps.net.count);
			UpPropsInfo (mProps.data.info);
			UpPropsDesc (mProps.data.description);
			UpPrice (mProps.data.selling_price);
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