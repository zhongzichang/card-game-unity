/// <summary>
/// Properties details sub panel.
/// xbhuang 
/// 2014-5-4
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using TangGame.Xml;

namespace TangGame.UI
{
	public class PropsDetailsSubPanel : MonoBehaviour
	{
		/// <summary>
		/// The SV properties item table.
		/// 道具列表
		/// </summary>
		public GameObject SVPropsItemTable;
		/// <summary>
		/// The SV properties item.
		/// 道具item
		/// </summary>
		public GameObject SVPropsItem;
		/// <summary>
		/// The properties item name label.
		/// 道具名称
		/// </summary>
		public GameObject PropsItemNameLabel;
		/// <summary>
		/// The main properties item.
		/// 主要道具item
		/// </summary>
		public GameObject MainPropsItem;
		/// <summary>
		/// The spending label.
		/// 合成花费
		/// </summary>
		public GameObject SpendingLabel;
		/// <summary>
		/// The sub item table.
		/// propsitem 子对象table
		/// </summary>
		public GameObject SubItemTable;
		/// <summary>
		/// The sub properties item.
		/// propsitem子对象
		/// </summary>
		public GameObject SubPropsItem;
		/// <summary>
		/// The parameter.
		/// </summary>
		private PropsDetailsPanelBean propsDPbean;
		/// <summary>
		/// The SV properties item array.
		/// </summary>
		private ArrayList SVPropsItemArray = new ArrayList ();
		/// <summary>
		/// The sub properties items.
		/// </summary>
		private ArrayList SubPropsItemArray = new ArrayList ();

		public PropsDetailsPanelBean PropsDPbean {
			get {
				return propsDPbean;
			}
			set {
				propsDPbean = value;
				if (propsDPbean != null && propsDPbean.props != null) {
					this.ClearSVPropsItemArray ();
					SVPropsItemArrayForward (propsDPbean.props.data);
				}
			}
		}
		bool mStarted = false;
		void Start(){
			mStarted = true;
      SVPropsItemArrayForward (propsDPbean.props.data);
		}
		/// <summary>
		/// SVs the properties item array forward.
		/// 点击到下一个道具
		/// </summary>
		void SVPropsItemArrayForward (PropsData propsXml)
		{
			if (!mStarted)
				return;

			if ((PropsType)propsXml.type == PropsType.DEBRIS)
				return;
			int count = SVPropsItemArray.Count;
			if (count != 0) {
				(SVPropsItemArray [count - 1] as SVPropsItem).IsChecked = false;
			}

			SVPropsItem svPropsItem;
			svPropsItem = NGUITools.AddChild (SVPropsItemTable.gameObject, SVPropsItem).GetComponent<SVPropsItem> ();
			svPropsItem.transform.localScale = SVPropsItem.transform.localScale;
			svPropsItem.gameObject.SetActive (true);
			svPropsItem.Flush (propsXml);
			SVPropsItemArray.Add (svPropsItem);
			SetCurrentPropsItem (svPropsItem);
			svPropsItem.name += SVPropsItemArray.IndexOf (svPropsItem);
			UIEventListener.Get (svPropsItem.gameObject).onClick += OnSVPropsItemOnClick;
			this.SVPropsItemTable.GetComponent<UIGrid> ().repositionNow = true;
			StartCoroutine (UpdateScrollView (new Vector3 (-SVPropsItemTable.GetComponent<UIGrid> ().cellWidth, 0, 0)));

		}

		/// <summary>
		/// Updates the scroll view.
		/// 以spring的方式位移scorll view ，
		/// </summary>
		/// <returns>The scroll view.</returns>
		/// <param name="constraint">如果constraint 是vector3.zero 则默认位移到初始状态</param>
		IEnumerator UpdateScrollView (Vector3 constraint)
		{
			yield return new WaitForSeconds (0.2F);
			UIScrollView sVPropsItemScrollView = NGUITools.FindInParents<UIScrollView> (SVPropsItemTable.gameObject);
			if (SVPropsItemArray.Count > 4) {
				sVPropsItemScrollView.contentPivot = UIWidget.Pivot.Right;

			} else {
				sVPropsItemScrollView.contentPivot = UIWidget.Pivot.TopLeft;
				constraint = Vector3.zero;

			}
			Transform svTrans = sVPropsItemScrollView.transform;
			UIPanel svPanel = sVPropsItemScrollView.panel;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds (svTrans, svTrans);
			if (constraint == Vector3.zero)
				constraint = svPanel.CalculateConstrainOffset (bounds.min, bounds.max);
			if (constraint.sqrMagnitude != 0f && sVPropsItemScrollView.dragEffect == UIScrollView.DragEffect.MomentumAndSpring) {
				// Spring back into place
				Vector3 pos = svTrans.localPosition + constraint;
				pos.x = Mathf.Round (pos.x);
				pos.y = Mathf.Round (pos.y);
				SpringPanel.Begin (svPanel.gameObject, pos, 13f);
			}
		}

		/// <summary>
		/// SVs the properties item array back.
		/// 返回上一个道具
		/// </summary>
		void SVPropsItemArrayBack ()
		{
			if (SVPropsItemArray.Count >= 2) {
				SVPropsItem item = SVPropsItemArray [SVPropsItemArray.Count - 2] as SVPropsItem;
				BackToSVPropsItem (item);
			}
		}

		/// <summary>
		/// Clears the sub properties item array.
		/// 清理sub array中的所有道具
		/// </summary>
		void ClearSubPropsItemArray ()
		{
			foreach (SubPropsItem item in SubPropsItemArray) {
				if (item.gameObject.activeSelf) {
					item.gameObject.SetActive (false);
					Destroy (item.gameObject);
				}
			}
			SubPropsItemArray.Clear ();
		}

		/// <summary>
		/// Clears the SV properties item array.
		/// 清空sv
		/// </summary>
		void ClearSVPropsItemArray ()
		{
			foreach (SVPropsItem item in SVPropsItemArray) {
				if (item.gameObject.activeSelf) {
					item.gameObject.SetActive (false);
					Destroy (item.gameObject);
				}
			}
			SVPropsItemArray.Clear ();
		}

		/// <summary>
		/// Backs the SV properties item array.
		/// </summary>
		/// <param name="sVPropsItem">S V properties item.</param>
		void BackToSVPropsItem (SVPropsItem sVPropsItem)
		{
			while (SVPropsItemArray.Count > 0) {
				SVPropsItem item = SVPropsItemArray [SVPropsItemArray.Count - 1] as SVPropsItem;
				if (sVPropsItem != item) {
					SVPropsItemArray.Remove (item);
					Destroy (item.gameObject);
				} else {
					sVPropsItem.IsChecked = true;
					break;
				}
			}
			StartCoroutine (UpdateScrollView (Vector3.zero));
			SetCurrentPropsItem (sVPropsItem);
			this.SVPropsItemTable.GetComponent<UIGrid> ().repositionNow = true;

		}

		/// <summary>
		/// Sets the current properties item.
		/// 设置当前道具
		/// </summary>
		/// <param name="svpItem">Svp item.</param>
		void SetCurrentPropsItem (SVPropsItem svpItem)
		{
			this.ClearSubPropsItemArray ();
			svpItem.IsChecked = true;
			if (SVPropsItemArray.LastIndexOf (svpItem) != 0) {
				svpItem.Arrow.SetActive (true);
			} 

			Props props = svpItem.data;
			PropsItem mainPropsItem = MainPropsItem.GetComponent<PropsItem> ();
			mainPropsItem.ShowCount = false;
			PropsItemNameLabel.GetComponent<UILabel> ().text = props.data.name;
			mainPropsItem.Flush (props);
			mainPropsItem.GetComponent<TweenAlpha> ().ResetToBeginning ();
			mainPropsItem.GetComponent<TweenAlpha> ().Play ();

			Dictionary<int,int> syntheticPropsTable = props.data.GetSyntheticPropsTable ();
			foreach (KeyValuePair<int,int> propsKeyVal in syntheticPropsTable) {
				SubPropsItem propsItem = NGUITools.AddChild (SubItemTable.gameObject, SubPropsItem.gameObject).GetComponent<SubPropsItem> ();
				propsItem.transform.localScale = SubPropsItem.transform.localScale;
				propsItem.gameObject.SetActive (true);
				SubPropsItemArray.Add (propsItem);
        if (PropsCache.instance.propsTable.ContainsKey (propsKeyVal.Key))
          propsItem.Flush (PropsCache.instance.propsTable [propsKeyVal.Key], propsKeyVal.Value);
				else if (Config.propsXmlTable.ContainsKey (propsKeyVal.Key))
					propsItem.Flush (Config.propsXmlTable [propsKeyVal.Key], propsKeyVal.Value);
				else
					Debug.LogWarning ("propsXmlTable can not fand id : " + propsKeyVal.Value);

				propsItem.name += SubPropsItemArray.IndexOf (propsItem);
				UIEventListener.Get (propsItem.gameObject).onClick += OnSubPropsItemOnClick;
				SubItemTable.GetComponent<UIGrid> ().repositionNow = true;
			}

			this.SpendingLabel.GetComponent<UILabel> ().text = string.Format (UIPanelLang.SYNTHESIS_SPEND, svpItem.data.data.synthetic_spend);
		}

		/// <summary>
		/// Subs the properties item on click.
		/// 
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnSubPropsItemOnClick (GameObject obj)
		{
			SubPropsItem item = obj.GetComponent<SubPropsItem> ();
			SVPropsItemArrayForward (item.data.data);
		}

		/// <summary>
		/// Raises the SV properties item on click event.
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnSVPropsItemOnClick (GameObject obj)
		{
			SVPropsItem item = obj.GetComponent<SVPropsItem> ();
			BackToSVPropsItem (item);
		}
	}
}