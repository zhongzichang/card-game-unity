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
			}
		}

		public GameObject UnitPriceStrLabel;
		public GameObject TotalPriceStrLabel;
		public GameObject PropsNumStrLabl;
		public GameObject MaxBtnLabel;
		public GameObject SellBtnLabel;
		// Use this for initialization
		void Start ()
		{
			UnitPriceStrLabel.GetComponent<UILabel> ().text = UIPanelLang.UNIT_PRICE;
			TotalPriceStrLabel.GetComponent<UILabel> ().text = UIPanelLang.TOTAL_PRICE;
			PropsNumStrLabl.GetComponent<UILabel> ().text = UIPanelLang.SELECT_NUMBER_OF_SELL;
			MaxBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.MAX;
			SellBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.CONFIRM_THE_SALE;
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		void Flush ()
		{

		}


	}
}