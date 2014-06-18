using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
	public class SellPanel : MonoBehaviour
	{
		object mParam;

		public object param {
			get {
				return mParam;
			}
			set {
				mParam = value;
				Flush ();
			}
		}

		Props mProps;
		bool isStarted;
		public GameObject UnitPriceStrLabel;
		public GameObject TotalPriceStrLabel;
		public GameObject PropsNumStrLabl;
		public GameObject MaxBtnLabel;
		public GameObject SellBtnLabel;

		public GameObject MinusBtn;
		public GameObject AddBtn;
		public GameObject MaxBtn;
		public GameObject SellBtn;
		// Use this for initialization
		void Start ()
		{
			UnitPriceStrLabel.GetComponent<UILabel> ().text = UIPanelLang.UNIT_PRICE + "：";
			TotalPriceStrLabel.GetComponent<UILabel> ().text = UIPanelLang.TOTAL_PRICE + "：";
			PropsNumStrLabl.GetComponent<UILabel> ().text = UIPanelLang.SELECT_NUMBER_OF_SELL + "：";
			MaxBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.MAX;
			SellBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.CONFIRM_THE_SALE;

			UIEventListener.Get (MinusBtn).onClick += OnMinusBtnClick;
			UIEventListener.Get (AddBtn).onClick += OnAddBtnClick;
			UIEventListener.Get (SellBtn).onClick += OnSellBtnClick;
			UIEventListener.Get (MaxBtn).onClick += OnMaxBtnClick;
			isStarted = true;
			Flush ();
		}

		void OnMaxBtnClick (GameObject go)
		{
			sellCount = mProps.net.count;
			Flush ();
		}

		void OnSellBtnClick (GameObject go)
		{
			//TODO
		}

		void OnAddBtnClick (GameObject go)
		{

			if (sellCount < mProps.net.count) {
				sellCount++;
			}
			Flush ();
		}

		void OnMinusBtnClick (GameObject go)
		{
			if (sellCount > 0) {
				sellCount--;
			}
			Flush ();
		}

		void Flush ()
		{
			if (!isStarted)
				return;
			if (mParam is Props) {
				this.mProps = mParam as Props;
				Flush (this.mProps);
			}
		}

		/// <summary>
		/// 出售数量
		/// </summary>
		int sellCount = 1;

		void OnDisable ()
		{
			sellCount = 1;
		}

		public void Flush (Props props)
		{
			this.mProps = props;
			UpPropsIcon (props.data.Icon);
			UpCount (props.net.count);
			UpName (props.data.name);
			UpUnitPrice (props.data.selling_price);
			int totalPrice = props.data.selling_price * sellCount; 
			UpTotalPrice (totalPrice);
			UpPropsSellNum (sellCount, props.net.count);
		}

		public GameObject PropsFrame;

		void UpPropsFrame (int rank)
		{
			string spriteName = Global.GetPropFrameName (rank);
			PropsFrame.GetComponent<UISprite> ().spriteName = spriteName;
		}

		public GameObject PropsIcon;

		void UpPropsIcon (string spriteName)
		{
			PropsIcon.GetComponent<UISprite> ().spriteName = spriteName;
		}

		public GameObject PropsCountLabel;

		void UpCount (int count)
		{
			PropsCountLabel.GetComponent<UILabel> ().text = string.Format (UIPanelLang.HAS_NUMBER_OF_PROPS, count);
		}

		public GameObject PropsNameLabel;

		void UpName (string name)
		{
			PropsNameLabel.GetComponent<UILabel> ().text = name;
		}

		public GameObject UnitPriceLabel;

		void UpUnitPrice (int price)
		{
			UnitPriceLabel.GetComponent<UILabel> ().text = price.ToString ();
		}

		public GameObject TotalPriceLabel;

		void UpTotalPrice (int pirce)
		{
			TotalPriceLabel.GetComponent<UILabel> ().text = pirce.ToString ();
		}

		public GameObject PropsSellNumLabl;

		void UpPropsSellNum (int count, int countMax)
		{
			PropsSellNumLabl.GetComponent<UILabel> ().text = string.Format ("{0}/{1}", count, countMax);
		}
	}
}