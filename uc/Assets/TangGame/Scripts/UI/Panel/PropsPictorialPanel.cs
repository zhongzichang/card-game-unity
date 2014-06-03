using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI
{
	public class PropsPictorialPanel : MonoBehaviour
	{
		public GameObject ForwardBtn;
		public GameObject BackBtn;
		public GameObject PropsPictorialItemGrid;
		public GameObject PropsPictorialItem;
		public GameObject PageLabel;

		#region ===按钮===

		public GameObject AdBtn;
		public GameObject AgileBtn;
		public GameObject AllBtn;
		public GameObject HpBtn;
		public GameObject IntellectBtn;
		public GameObject StrengthBtn;
		public GameObject AdditionTreatmentBtn;
		public GameObject ApBtn;
		public GameObject CritBtn;
		public GameObject DefenseBtn;
		public GameObject HpReBtn;
		public GameObject MpReBtn;

		#endregion

		GameObject lastCheckedBtn;
		public int itemCount = 12;
		int pageIndex = 1;
		int pageIndexMax = 1;
		List<SimplePropsItem> propsItemList;
		List<Xml.PropsData> currentPropsList = new List<Xml.PropsData> ();

		string btnDefSpriteName = "";
		Color btnDefSpriteColor = Color.white;
		string btnCheckedSpriteName = "";
		Color btnCheckedColor = Color.white;
		void Start ()
		{
			lastCheckedBtn = AllBtn;
			btnDefSpriteName = MpReBtn.GetComponent<UISprite> ().spriteName;
			btnDefSpriteColor = MpReBtn.GetComponentInChildren<UILabel> ().color;
			btnCheckedSpriteName = AllBtn.GetComponent<UISprite> ().spriteName;
			btnCheckedColor = AllBtn.GetComponentInChildren<UILabel> ().color;
			InitPorpItemList ();
			SwitchToAll ();
			UIEventListener.Get (ForwardBtn).onClick += ForwardBtnOnClick;
			UIEventListener.Get (BackBtn).onClick += BackBtnOnClick;

			UIEventListener.Get (AdBtn).onClick += AdBtnOnClick;
			UIEventListener.Get (AgileBtn).onClick += AgileBtnOnClick;
			UIEventListener.Get (AllBtn).onClick += AllBtnOnClick;
			UIEventListener.Get (HpBtn).onClick += HpBtnOnClick;
			UIEventListener.Get (IntellectBtn).onClick += IntellectBtnOnClick;
			UIEventListener.Get (StrengthBtn).onClick += StrengthBtnOnClick;
			UIEventListener.Get (AdditionTreatmentBtn).onClick += AdditionTreatmentBtnOnClick;
			UIEventListener.Get (ApBtn).onClick += ApBtnOnClick;
			UIEventListener.Get (CritBtn).onClick += CritBtnOnClick;
			UIEventListener.Get (DefenseBtn).onClick += DefenseBtnOnClick;
			UIEventListener.Get (HpReBtn).onClick += HpReBtnOnClick;
			UIEventListener.Get (MpReBtn).onClick += MpReBtnOnClick;
		}
		void ToggleChecked(GameObject go){
			lastCheckedBtn.GetComponent<UISprite> ().spriteName = btnDefSpriteName;
			lastCheckedBtn.GetComponentInChildren<UILabel> ().color = btnDefSpriteColor;
			go.GetComponent<UISprite> ().spriteName = btnCheckedSpriteName;
			go.GetComponentInChildren<UILabel> ().color = btnCheckedColor;
			lastCheckedBtn = go;
		}

		void MpReBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToEnergyRecovery ();
		}

		void HpReBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToHpRecovery ();
		}

		void DefenseBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToDefense ();
		}

		void CritBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToCrit ();
		}

		void ApBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToAP ();
		}

		void AdditionTreatmentBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToAdditionTreatment ();
		}

		void StrengthBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToStrength ();
		}

		void AdBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToAD ();
		}
		void AgileBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToAgile ();
		}

		void AllBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToAll ();
		}

		void HpBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToHpRecovery ();
		}

		void IntellectBtnOnClick (GameObject go)
		{
			ToggleChecked (go);
			SwitchToIntellect ();
		}

		void ForwardBtnOnClick (GameObject go)
		{
			if (pageIndex < pageIndexMax) {
				ShowCurrentPageProps (++pageIndex);
			}
		}

		void BackBtnOnClick (GameObject go)
		{
			if (pageIndex > 1) {
				ShowCurrentPageProps (--pageIndex);
			}
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

		/// <summary>
		/// 获取当前页面的道具列表
		/// </summary>
		/// <returns>The current page properties list.</returns>
		/// <param name="index">Index.</param>
		List<Xml.PropsData> GetCurrentPagePropsList (int index)
		{
			pageIndex = index;
			int startIndex = (index - 1) * itemCount;
			int endIndex = index * itemCount;
			List<Xml.PropsData> list = new List<TangGame.Xml.PropsData> ();
			for (int i = startIndex; i < endIndex; i++) {
				if (currentPropsList.Count <= i)
					return list;
				list.Add (currentPropsList [i]);
			}
			return list;
		}

		/// <summary>
		/// 显示当前页面的道具
		/// </summary>
		/// <param name="index">Index.</param>
		void ShowCurrentPageProps (int index)
		{
			TweenAlpha alpha = PropsPictorialItemGrid.GetComponent<TweenAlpha> ();
			alpha.ResetToBeginning ();
			alpha.Play ();
			List<Xml.PropsData> list = GetCurrentPagePropsList (index);
			int indexMax = Mathf.CeilToInt ((float)currentPropsList.Count / (float)itemCount);
			pageIndexMax = indexMax;
			SetPageLabel (index, indexMax);
			HidePropsItems ();
			for (int i = 0; i < list.Count; i++) {
				Props props = new Props ();
				props.data = list [i];
				propsItemList [i].gameObject.SetActive (true);
				propsItemList [i].data = props;
				propsItemList [i].gameObject.GetComponentInChildren<UILabel> ().text = props.data.name;
				UIEventListener.Get (propsItemList [i].gameObject).onClick += PropsItemOnClick;
			}
		}

		/// <summary>
		/// 设置页面当前所在页面的标签
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="indexMax">Index max.</param>
		void SetPageLabel (int index, int indexMax)
		{
			PageLabel.GetComponent<UILabel> ().text = string.Format ("{0}/{1}", index, indexMax);
		}

		void PropsItemOnClick (GameObject go)
		{
			SimplePropsItem item = go.GetComponent<SimplePropsItem> ();
			PropsDetailsPanelBean bean = new PropsDetailsPanelBean ();
			bean.props = item.data as Props;
			UIContext.mgrCoC.LazyOpen (UIContext.EQUIP_DETAILS_PANEL_NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, bean, true);
		}

		/// <summary>
		/// 切换至全部
		/// </summary>
		void SwitchToAll ()
		{
			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if ((PropsType)data.type == PropsType.EQUIP) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// <summary>
		/// 力量
		/// </summary>
		void SwitchToStrength ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.strength != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// <summary>
		/// 切换到智力
		/// </summary>
		void SwitchToIntellect ()
		{
			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.intellect != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 敏捷
		void SwitchToAgile ()
		{
			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.agile != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);

		}

		/// 生命
		void SwitchTohpMax ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.hpMax != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 攻击
		void SwitchToAD ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.attack_damage != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		void SwitchToAP ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.ability_power != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 护甲
		void SwitchToDefense ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.magic_defense != 0 || data.physical_defense != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 暴击
		void SwitchToCrit ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.magic_crit != 0 || data.physical_crit != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 生命回复
		void SwitchToHpRecovery ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.hp_recovery != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 能量回复
		void SwitchToEnergyRecovery ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.energy_recovery != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// 治疗
		void SwitchToAdditionTreatment ()
		{

			CurrentPageReset ();
			foreach (Xml.PropsData data in Config.propsXmlTable.Values) {
				if (data.addition_treatment != 0) {
					currentPropsList.Add (data);
				}
			}
			ShowCurrentPageProps (1);
		}

		/// <summary>
		/// 重置当前页面
		/// </summary>
		void CurrentPageReset ()
		{
			currentPropsList.Clear ();
			HidePropsItems ();
		}

		void HidePropsItems ()
		{
			foreach (SimplePropsItem item in propsItemList) {
				if (item.gameObject.activeSelf)
					item.gameObject.SetActive (false);
			}
		}
	}
}