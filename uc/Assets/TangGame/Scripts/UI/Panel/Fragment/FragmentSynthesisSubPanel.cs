
/// <summary>
/// Fragment synthesis sub panel.
/// xbhuang
/// 2014-5-10
/// </summary>
using UnityEngine;
using System.Collections.Generic;

namespace TangGame.UI
{
	public class FragmentSynthesisSubPanel : MonoBehaviour
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

		public const string NAME = "FragmentSynthesisSubPanel";
		void Start ()
		{
			isStarted = true;
			Flush ();
		}

		Props mFragment;
		bool isStarted = false;

		void Flush ()
		{
			if (!isStarted)
				return;
			if (mParam is Props) {
				this.mFragment = mParam as Props;
				Flush (this.mFragment);
			}
		}

		public void Flush (Props fragment)
		{
			NGUITools.SetActive (this.gameObject, true);
			mFragment = fragment;
			UpFragmentItem (mFragment);
			UpPropsItem (PropsCache.instance.GetPropsRelationData (mFragment.data.id).synthetics [0]);
		}

		public GameObject FragmentFrames;
		public GameObject FragmentIcon;
		public GameObject FragmentCount;
		public GameObject InfoTag;

		public void UpFragmentItem (Props props)
		{
			FragmentIcon.GetComponent<UISprite> ().spriteName = props.data.Icon;
			FragmentFrames.GetComponent<UISprite> ().spriteName = Global.GetPropFrameName (props.data.rank);
			Dictionary<int, int> syntheticsTable = PropsCache.instance.GetPropsRelationData (props.data.id).synthetics [0].GetSyntheticPropsTable ();
			int maxCount = 0;
			if (syntheticsTable.ContainsKey (props.data.id))
				maxCount = syntheticsTable [props.data.id];

			if (props.net.count < maxCount) {
				InfoTag.SetActive (true);
			} else {
				InfoTag.SetActive (false);
			}
			FragmentCount.GetComponent<UILabel> ().text = props.net.count + "/" + maxCount; 
		}

		public GameObject PropsFrames;
		public GameObject PropsIcon;
		public GameObject PropsName;
		public GameObject PriceLabel;

		public void UpPropsItem (Xml.PropsData data)
		{
			PropsIcon.GetComponent<UISprite> ().spriteName = data.Icon;
			PropsFrames.GetComponent<UISprite> ().spriteName = Global.GetPropFrameName (data.rank);
			PropsName.GetComponent<UILabel> ().text = data.name;
			PriceLabel.GetComponent<UILabel> ().text = data.synthetic_spend.ToString ();
		}

	}
}