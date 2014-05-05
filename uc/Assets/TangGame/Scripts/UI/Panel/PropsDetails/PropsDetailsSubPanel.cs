/// <summary>
/// Properties details sub panel.
/// xbhuang 
/// 2014-5-4
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI.Base;
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
		private PropsBase data;
		/// <summary>
		/// The SV properties item array.
		/// </summary>
		private ArrayList SVPropsItemArray = new ArrayList ();

		public PropsBase Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
		// Use this for initialization
		void Start ()
		{

		}
		// Update is called once per frame
		void Update ()
		{

		}

		void OnEnable ()
		{
			if (data != null) {
				SVPropsItem svpItem = this.SVPropsItem.GetComponent<SVPropsItem> ();
				svpItem.Flush (data);
				SVPropsItemArray.Add (svpItem);
				SetCurrentPropsItem (svpItem);
			}
		}

		/// <summary>
		/// SVs the properties item array forward.
		/// 点击到下一个道具
		/// </summary>
		void SVPropsItemArrayForward (PropsXml propsXml)
		{
			//TODO 点击到下一个道具
			int count = SVPropsItemArray.Count;
			SVPropsItem svPropsItem = SVPropsItemArray [count - 1] as SVPropsItem;
			svPropsItem.IsChecked = false;
			svPropsItem = NGUITools.AddChild (SVPropsItemTable.gameObject, SVPropsItem).GetComponent<SVPropsItem> ();
			svPropsItem.Flush (propsXml);
			this.SVPropsItemTable.GetComponent<UITable> ().repositionNow = true;
		}

		/// <summary>
		/// SVs the properties item array back.
		/// 返回上一个道具
		/// </summary>
		void SVPropsItemArrayBack ()
		{
			if (SVPropsItemArray.Count > 1) {
				SVPropsItemArray.RemoveAt (SVPropsItemArray.Count - 1);
				SetCurrentPropsItem (SVPropsItemArray[SVPropsItemArray.Count - 1] as SVPropsItem);
			}
			this.SVPropsItemTable.GetComponent<UITable> ().repositionNow = true;
		}

		/// <summary>
		/// Sets the current properties item.
		/// 设置当前道具
		/// </summary>
		/// <param name="svpItem">Svp item.</param>
		void SetCurrentPropsItem (SVPropsItem svpItem)
		{
			svpItem.IsChecked = true;
			if (SVPropsItemArray.LastIndexOf (svpItem) != 0) {
				svpItem.Arrow.SetActive (true);
			} 
			PropsBase propsBase = svpItem.data;
			PropsItem mainPropsItem = MainPropsItem.GetComponent<PropsItem> ();
			mainPropsItem.ShowCount = false;
			mainPropsItem.Flush (propsBase);

			Dictionary<int,int> syntheticPropsTable = propsBase.Xml.GetSyntheticPropsTable ();
			ArrayList subPropsItems = new ArrayList ();
			foreach (KeyValuePair<int,int> propsKeyVal in syntheticPropsTable) {
				SubPropsItem propsItem = NGUITools.AddChild (SubItemTable.gameObject, SubPropsItem.gameObject).GetComponent<SubPropsItem> ();
				subPropsItems.Add (propsItem);
				if (BaseCache.propsBaseTable.ContainsKey (propsKeyVal.Key))
					propsItem.Flush (BaseCache.propsBaseTable [propsKeyVal.Key], propsKeyVal.Value);
				else
					propsItem.Flush (Config.propsXmlTable [propsKeyVal.Key], propsKeyVal.Value);

				SVPropsItemTable.GetComponent<UITable> ().repositionNow = true;
			}

			this.SpendingLabel.GetComponent<UILabel> ().text = string.Format (UIPanelLang.SYNTHESIS_SPEND,svpItem.data.Xml.synthetic_spend);
		}
	}
}