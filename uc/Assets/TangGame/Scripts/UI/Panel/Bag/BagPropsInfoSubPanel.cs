using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using System;
using System.Text.RegularExpressions;
using TangUI;

namespace TangGame.UI
{
	public class BagPropsInfoSubPanel : MonoBehaviour
	{
		/// <summary>
		/// The properties info background.
		/// 物品信息文字的背景
		/// </summary>
		public GameObject PropsInfoBg;
		/// <summary>
		/// The properties info label.
		/// 物品的信息
		/// </summary>
		public GameObject PropsInfoLabel;
		/// <summary>
		/// The properties description.
		/// 物品的描述
		/// </summary>
		public GameObject PropsDescription;
		/// <summary>
		/// The property info table.
		/// 描述内容的table容器
		/// </summary>
		public GameObject PropInfoTable;
		/// <summary>
		/// The frames.
		/// 物品边框对象
		/// </summary>
		public GameObject Frames;
		/// <summary>
		/// The properties icon.
		/// 物品图标
		/// </summary>
		public GameObject PropsIcon;
		/// <summary>
		/// The properties count.
		/// 物品的数量
		/// </summary>
		public	GameObject PropsCount;
		/// <summary>
		/// The price label.
		/// 物品的价格
		/// </summary>
		public GameObject PriceLabel;
		/// <summary>
		/// The name of the properties.
		/// 物品的名字
		/// </summary>
		public GameObject PropsName;
		public GameObject DetailButton;
		Props mProps;

		void Start(){
			UIEventListener.Get (DetailButton).onClick += OnDetailButtonClick;
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		void OnDetailButtonClick(GameObject obj){
			TangGame.UIContext.mgrCoC.LazyOpen (PropsDetailsPanel.name, UIPanelNode.OpenMode.ADDITIVE,UIPanelNode.BlockMode.SPRITE,mProps.data);
		}

		/// <summary>
		/// Flush the specified data.
		/// 刷新面板数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void Flush (Props data)
		{
			this.mProps = data;
			if (this.gameObject.activeSelf == false) {
				this.gameObject.SetActive (true);
				this.GetComponent<TweenPosition> ().ResetToBeginning ();
				this.GetComponent<TweenPosition> ().Play (true);
			}
      this.UpPropsIcon (data.data.icon);
      this.UpPropsFrames (data.data.upgrade);
			this.UpPropsInfo (data);
      this.UpPropsName (data.data.name);
      this.UpPropsPrice (data.data.selling_price);
			if (data.net != null) {
        this.UpPropsCount (data.net.count);
			} else {
				this.UpPropsCount (0);
			}
      this.UpDescription (data.data.description);

		}

		/// <summary>
		/// Ups the name of the properties.
		/// 更新物品名字
		/// </summary>
		/// <param name="name">Name.</param>
		public void UpPropsName (string name)
		{
			this.PropsName.GetComponent<UILabel> ().text = name;
		}

		/// <summary>
		/// Ups the properties icon.
		/// 更新图标
		/// </summary>
		/// <param name="name">Name.</param>
		public void UpPropsIcon (string icon)
		{
			this.PropsIcon.GetComponent<UISprite> ().spriteName = icon;
		}

		/// <summary>
		/// Ups the properties frames.
		/// 更新阶级
		/// </summary>
		public void UpPropsFrames (int upgrade)
		{
			this.Frames.GetComponent<UISprite> ().spriteName = Global.GetPropFrameName (upgrade);
		}

		/// <summary>
		/// Ups the properties count.
		/// 更新数量
		/// </summary>
		/// <param name="num">Number.</param>
		public void UpPropsCount (int num)
		{
			UILabel label = this.PropsCount.GetComponent<UILabel> ();
			label.text = string.Format (UIPanelLang.HAS_NUMBER_OF_PROPS, num);
		}

		/// <summary>
		/// Ups the properties info label.
		/// 更新信息
		/// </summary>
		/// <param name="data">Data.</param>
		public void UpPropsInfo (Props data)
		{

			PropsType type = (PropsType)data.data.type;
			string infoStr = "";
			if (PropsType.EQUIP == type) {
				if (data.data.strength == data.data.intellect && data.data.intellect == data.data.agile && data.data.strength >0) {
					infoStr += UIPanelLang.STRENGTH + ",";
					infoStr += UIPanelLang.INTELLECT + ",";
					infoStr += UIPanelLang.AGILE + "+";
					infoStr += data.data.strength;
					infoStr += Environment.NewLine;
				} else {
					//		<!-- 属性加成 -->
					//		<!-- 力量 -->
					//		<strength>21</strength>
					if (data.data.strength > 0) {
						infoStr += UIPanelLang.STRENGTH + "+" + data.data.strength + Environment.NewLine;
					}
					//		<!-- 智力 -->
					//		<intellect>42</intellect>
					if (data.data.intellect > 0) {
						infoStr += UIPanelLang.INTELLECT + "+" + data.data.intellect + Environment.NewLine;
					}
					//		<!-- 敏捷 -->
					//		<agile>2</agile>
					if (data.data.agile > 0) {
						infoStr += UIPanelLang.AGILE + "+" + data.data.agile + Environment.NewLine;
					}
				}
				//		<!-- 生命最大 -->
				//		<hpMax>132</hpMax>
				if (data.data.hpMax > 0) {
					infoStr += UIPanelLang.HPMAX + "+" + data.data.hpMax + Environment.NewLine;

				}
				//		<!-- 攻击强度 -->
				//		<attack_damage>23</attack_damage>
				if (data.data.attack_damage > 0) {
					infoStr += UIPanelLang.ATTACK_DAMAGE + "+" + data.data.attack_damage + Environment.NewLine;
				}
				//		<!-- 法术强度 -->
				//		<spell_power>123</spell_power>
				if (data.data.ability_power > 0) {
					infoStr += UIPanelLang.SPELL_POWER + "+" + data.data.ability_power + Environment.NewLine;
				}
				//		<!-- 物理防御 -->
				//		<physical_defense>321</physical_defense>
				if (data.data.physical_defense > 0) {
					infoStr += UIPanelLang.PHYSICAL_DEFENSE + "+" + data.data.physical_defense + Environment.NewLine;
				}
				//		<!-- 法术防御 -->
				//		<spell_defense>123</spell_defense>
				if (data.data.magic_defense > 0) {
					infoStr += UIPanelLang.SPELL_DEFENSE + "+" + data.data.magic_defense + Environment.NewLine;
				}
				//		<!-- 物理爆击 -->
				//		<physical_crit>12</physical_crit>
				if (data.data.physical_crit > 0) {
					infoStr += UIPanelLang.PHYSICAL_CRIT + "+" + data.data.physical_crit + Environment.NewLine;
				}
				//		<!-- 法术爆击 -->
				//		<spell_crit>21</spell_crit>
				if (data.data.magic_crit > 0) {
					infoStr += UIPanelLang.SPELL_CRIT + "+" + data.data.magic_crit + Environment.NewLine;
				}
				//		<!-- 生命回复 -->
				//		<hp_re>12</hp_re>
				if (data.data.hp_recovery > 0) {
					infoStr += UIPanelLang.HP_RECOVERY + "+" + data.data.hp_recovery + Environment.NewLine;
				}
				//		<!-- 能量回复 -->
				//		<energy_re>21</energy_re>
				if (data.data.energy_recovery > 0) {
					infoStr += UIPanelLang.ENERGY_RECOVERY + "+" + data.data.energy_recovery + Environment.NewLine;
				}
				//		<!-- 物理穿透 -->
				//		<physical_penetrate>12</physical_penetrate>
				if (data.data.physical_penetration > 0) {
					infoStr += UIPanelLang.PHYSICAL_PENETRATION + "+" + data.data.physical_penetration + Environment.NewLine;
				}
				//		<!-- 法术穿透 -->
				//		<spell_penetrate>21</spell_penetrate>
				if (data.data.spell_penetration > 0) {
					infoStr += UIPanelLang.SPELL_PENETRATION + "+" + data.data.spell_penetration + Environment.NewLine;
				}
				//		<!-- 吸血等级 -->
				//		<bloodsucking_lv>12</bloodsucking_lv>
				if (data.data.bloodsucking_lv > 0) {
					infoStr += UIPanelLang.BLOODSUCKING_LV + "+" + data.data.bloodsucking_lv + Environment.NewLine;
				}
				//		<!-- 闪避 -->
				//		<dodge>21</dodge>
				if (data.data.dodge > 0) {
					infoStr += UIPanelLang.DODGE + "+" + data.data.dodge + Environment.NewLine;
				}
				//		<!-- 治疗效果 -->
				//		<addition_treatment>21</addition_treatment>
				if (data.data.addition_treatment > 0) {
					infoStr += UIPanelLang.ADDITION_TREATMENT + "+" + data.data.addition_treatment + "%" + Environment.NewLine;
				}
			}
			//如果是贵重物品
			if (PropsType.VALUABLE == type) {
				infoStr += data.data.info;
			}
			//如果是卷轴
			if (PropsType.SCROLLS == type) {
        PropsRelationData propsRelationData = PropsCache.instance.GetPropsRelationData(data.data.id);
        if(propsRelationData != null && propsRelationData.synthetics.Count > 0){
					infoStr += string.Format (UIPanelLang.RELL_INFO, propsRelationData.synthetics[0].name);
        }
			}
			//如果是灵魂石
			if (PropsType.SOULROCK == type) {
        TangGame.Xml.HeroData heroData = HeroCache.instance.GetSoulStoneRelation(data.data.id);
        if (heroData != null) {
          int count = Config.evolveXmlTable [heroData.evolve].val;
					infoStr += string.Format (UIPanelLang.SOUL_STONE_INFO, Config.evolveXmlTable [count], heroData.name);
				}

			}
			//如果是消耗品
			if (PropsType.NONE == type)
				infoStr += data.data.info;

			UILabel label = PropsInfoLabel.GetComponent<UILabel> ();
			if (label != null) {
				label.text = infoStr;
				Utils.TextBgAdaptiveSize (label, PropsInfoBg.GetComponent<UISprite> ());
				PropInfoTable.GetComponent<UITable> ().repositionNow = true;
			}
		}

		/// <summary>
		/// Ups the description.
		/// 更新描述
		/// </summary>
		/// <param name="text">Text.</param>
		public void UpDescription (string text)
		{
			UILabel lab = PropsDescription.GetComponent<UILabel> ();
			if (lab != null) {
				lab.text = text;
			}
		}

		/// <summary>
		/// Ups the properties info.
		///  更新出售单价
		/// </summary>
		/// <param name="num">Number.</param>
		public void UpPropsPrice (int num)
		{
			UILabel lab = PriceLabel.GetComponent<UILabel> ();
			if (lab != null) {
				string pattern = "\\d+";
				Regex rgx = new Regex (pattern);
				string result = rgx.Replace (lab.text, num.ToString ());
				lab.text = result;
			}
		}
	}
}