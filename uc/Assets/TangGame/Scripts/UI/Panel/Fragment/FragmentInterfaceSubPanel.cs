/// <summary>
/// Fragment interface sub panel.
/// xbhuang 
/// 2014-5-10
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI
{
	public class FragmentInterfaceSubPanel : MonoBehaviour
	{
		/// <summary>
		/// 碎片信息面板，父面板初始化时会被赋值
		/// </summary>
		FragmentPropsInfoSubPanel mInfoPanel;
		/// <summary>
		/// The toggle all.
		/// 显示所有物品碎片的按钮对象
		/// </summary>
		public GameObject ToggleAll;
		/// <summary>
		/// The toggle equip.
		/// 显示所有装备碎片的按钮对象
		/// </summary>
		public GameObject ToggleEquip;
		/// <summary>
		/// The toggle scroll.
		/// 显示所有卷轴碎片的按钮对象
		/// </summary>
		public GameObject ToggleScroll;
		/// <summary>
		/// The properties table.
		/// 道具列表对象
		/// </summary>
		public GameObject PropsTable;
		Dictionary<int,PropsItem> scrollItems = new Dictionary<int, PropsItem> ();

		void Start ()
		{
			this.LoadAllScrollItem ();
			UIEventListener.Get (ToggleAll).onClick += ToggleAllOnClick;
			UIEventListener.Get (ToggleEquip).onClick += ToggleEquipOnClick;
			UIEventListener.Get (ToggleScroll).onClick += ToggleScrollOnClick;
		}

		/// <summary>
		/// Loads all scroll item.
		/// 加载所有的碎片对象
		/// </summary>
		void LoadAllScrollItem ()
		{
			StartCoroutine (LoadAllScrollItemAtMount());
		}

		IEnumerator LoadAllScrollItemAtMount(){
			PropsItem itemObj = Resources.Load<PropsItem> (UIContext.getWidgetsPath (UIContext.PROPS_ITEM_NAME));
			PropsItem item = null;
			foreach (Props props in PropsCache.instance.propsTable.Values) {
				if (PropsType.DEBRIS != (PropsType)props.data.type)
					continue;
				item = NGUITools.AddChild (PropsTable.gameObject, itemObj.gameObject).GetComponent<PropsItem> ();
				item.Flush (props);
				scrollItems.Add (item.data.data.id, item);
				UIEventListener.Get (item.gameObject).onClick += ScrollItemOnClick;
				yield return new WaitForFixedUpdate ();
				this.SortSorollTableView ();
			}
		}

		/// <summary>
		/// Shows the type of the scroll item by.
		/// </summary>
		/// <param name="type">Type.</param>
		void ShowScrollItemByType (PropsType type)
		{
			foreach (PropsItem item in scrollItems.Values) {
				bool itemStatus = item.gameObject.activeSelf;
				PropsType mType;

        PropsRelationData propsRelationData = PropsCache.instance.GetPropsRelationData(item.data.data.id);
        if(propsRelationData == null || propsRelationData.synthetics.Count == 0){
          item.gameObject.SetActive (false);
          continue;
        }

        mType = (PropsType)propsRelationData.synthetics[0].type;
				if (type == PropsType.NONE) {
					item.gameObject.SetActive (true);
					continue;
				}
				if (type == mType) {
					item.gameObject.SetActive (true);
				} else {
					item.gameObject.SetActive (false);
				}
			}
			this.SortSorollTableView ();
		}

		void SortSorollTableView(){
			PropsTable.GetComponent<UITable> ().repositionNow = true;
		}

		#region here the onclick methods

		/// <summary>
		/// Toggles the scroll on click.
		/// </summary>
		/// <param name="go">Go.</param>
		void ToggleScrollOnClick (GameObject go)
		{
			ShowScrollItemByType (PropsType.SCROLLS);
		}

		/// <summary>
		/// Toggles the equip on click.
		/// </summary>
		/// <param name="go">Go.</param>
		void ToggleEquipOnClick (GameObject go)
		{
			ShowScrollItemByType (PropsType.EQUIP);
		}

		/// <summary>
		/// Toggles all on click.
		/// </summary>
		/// <param name="go">Go.</param>
		void ToggleAllOnClick (GameObject go)
		{
			ShowScrollItemByType (PropsType.NONE);
		}

		/// <summary>
		/// Scrolls the item on click.
		/// </summary>
		/// <param name="go">Go.</param>
		void ScrollItemOnClick (GameObject go)
		{
			PropsItem item = go.GetComponent<PropsItem> ();
			if (item != null && item.data != null)
				mInfoPanel.props = item.data;
		}

		#endregion

		#region here the object's properties

		/// <summary>
		/// Gets or sets the info panel.
		/// </summary>
		/// <value>The info panel.</value>
		public FragmentPropsInfoSubPanel infoPanel {
			get {
				return mInfoPanel;
			}
			set {
				mInfoPanel = value;
			}
		}

		#endregion
	}
}