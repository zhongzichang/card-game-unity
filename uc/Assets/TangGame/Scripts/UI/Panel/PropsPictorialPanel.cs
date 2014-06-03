using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI
{
	public class PropsPictorialPanel : MonoBehaviour
	{
		public GameObject LeftButtons;
		public GameObject RightButtons;
		public GameObject PropsPictorialItemGrid;
		public GameObject PropsPictorialItem;
		public int itemCount = 12;
		int pageIndex = 1;
		List<SimplePropsItem> propsItemList;
		List<Xml.PropsData> currentPropsList = new List<Xml.PropsData> ();

		void Start ()
		{
			InitPorpItemList ();
			SwitchToAll ();
		}

		void InitPorpItemList ()
		{
			propsItemList = new List<SimplePropsItem> ();
			for (int i = 0; i < itemCount; i++) {
				GameObject obj = NGUITools.AddChild (PropsPictorialItemGrid, PropsPictorialItem);
				obj.name = i.ToString ();
				SimplePropsItem item = obj.GetComponent<SimplePropsItem> ();
				if (item == null) {
					item = obj.AddComponent <SimplePropsItem> ();
				}
				propsItemList.Add (item);
			}
			PropsPictorialItemGrid.GetComponent<UIGrid> ().repositionNow = true;
		}

		List<Xml.PropsData> GetCurrentPagePropsList (int index)
		{
			pageIndex = index;
			int startIndex = (index - 1) * itemCount;
			int endIndex = index * itemCount;
			List<Xml.PropsData> list = new List<TangGame.Xml.PropsData> ();
			for (int i = startIndex; i < endIndex; i++) {
				if (currentPropsList.Count < i)
					return list;
				list.Add (currentPropsList [i]);
			}
			return list;
		}

		void ShowCurrentPageProps(int index){
			List<Xml.PropsData> list = GetCurrentPagePropsList (index);
			for (int i = 0; i < list.Count; i++) {
				Props props = new Props ();
				props.data = list [i];
				propsItemList [i].gameObject.SetActive (true);
				propsItemList [i].data = props;
				propsItemList [i].gameObject.GetComponentInChildren<UILabel> ().text = props.data.name;
			}
		}

		/// <summary>
		/// 切换至全部
		/// </summary>
		void SwitchToAll ()
		{
			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				currentPropsList.Add (data);
			}
			ShowCurrentPageProps (1);
		}

		/// <summary>
		/// 力量
		/// </summary>
		void SwitchToStrength ()
		{

		}

		void SwitchToIntellect ()
		{

		}

		/// 敏捷
		void SwitchToAgile ()
		{

		}

		/// 生命值
		void SwitchTohpMax ()
		{

		}

		/// 物理攻击
		void SwitchToAttack ()
		{
		
		}

		/// 物理护甲
		void SwitchToDefense ()
		{

		}

		/// 物理暴击
		void SwitchToCrit ()
		{

		}

		/// 生命回复
		void SwitchToHpRecovery ()
		{

		}

		/// 能量回复
		void SwitchToEnergyRecovery ()
		{

		}

		/// 治疗
		void SwitchToAdditionTreatment ()
		{

		}

		/// <summary>
		/// 重置当前页面
		/// </summary>
		void CurrentPageReset ()
		{
			currentPropsList.Clear ();
			foreach (SimplePropsItem item in propsItemList) {
				if (item.gameObject.activeSelf)
					item.gameObject.SetActive (false);
			}
		}
	}
}