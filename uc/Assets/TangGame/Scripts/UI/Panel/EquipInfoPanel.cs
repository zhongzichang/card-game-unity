using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using System;
using System.Text.RegularExpressions;

namespace TangGame.UI
{
	public class EquipInfoPanel : MonoBehaviour
	{
		public object param;
		/// <summary>
		/// The properties info background.
		/// 物品信息文字的背景
		/// </summary>
		public GameObject EquipInfoBg;
		/// <summary>
		/// The properties info label.
		/// 物品的信息
		/// </summary>
		public GameObject EquipInfoLabel;
		/// <summary>
		/// The properties description.
		/// 物品的描述
		/// </summary>
		public GameObject EquipDescription;
		/// <summary>
		/// The property info table.
		/// 描述内容的table容器
		/// </summary>
		public GameObject EquipInfoTable;
		/// <summary>
		/// The frames.
		/// 物品边框对象
		/// </summary>
		public GameObject Frames;
		/// <summary>
		/// The properties icon.
		/// 物品图标
		/// </summary>
		public GameObject EquipIcon;
		/// <summary>
		/// The properties count.
		/// 物品是否附魔
		/// </summary>
		public	GameObject Enchanting;
		/// <summary>
		/// The name of the properties.
		/// 物品的名字
		/// </summary>
		public GameObject EquipName;
		/// <summary>
		/// The properties lv label.
		/// 道具要求等级
		/// </summary>
		public GameObject EquipLvLabel;
		/// <summary>
		/// The equipped button.
		/// 装备按钮
		/// </summary>
		public GameObject EquippedBtn;
		/// <summary>
		/// The data.
		/// 英雄数据
		/// </summary>
		private PropsDetailsPanelBean propsDPbean;

		public PropsDetailsPanelBean PropsDPbean {
			get {
				return propsDPbean;
			}
			set {
				propsDPbean = value;
			}
		}

		void OnEnable ()
		{
			if (param != null && param is PropsDetailsPanelBean) {
				PropsDPbean = param as PropsDetailsPanelBean;
			}
			if (propsDPbean != null && propsDPbean.props != null) {
				Flush (propsDPbean);
			}
		}

		void Start ()
		{
			if (propsDPbean != null && propsDPbean.props != null) {
				Flush (propsDPbean);
			}
			EquippedBtn.GetComponentInChildren<UILabel> ().text = UIPanelLang.OK;
			UIEventListener.Get (EquippedBtn.gameObject).onClick += EquippedBtnOnClick;
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		/// <summary>
		/// Equippeds the button on click.
		/// 关闭当前面板
		/// </summary>
		/// <param name="obj">Object.</param>
		void EquippedBtnOnClick(GameObject obj){
			UIContext.mgrCoC.Back ();
		}
		/// <summary>
		/// Flush the specified data.
		/// 刷新面板数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void Flush (PropsDetailsPanelBean bean)
		{
			if (!(bean.props is Equip))
				return;
			Equip data = bean.props as Equip;
			this.UpPropsIcon (data.data.icon);
			this.UpPropsFrames (data.data.upgrade);
			this.UpPropsInfo (data); //TODO 追加装备附魔信息
			this.UpPropsName (data.data.name);
			this.UpPropsLvLabel (data.data.level.ToString ());
			if (data.net != null && data.net.enchantsLv != 0) {
				this.UpEnchanting (true);
			} else {
				this.UpEnchanting (false);
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
			this.EquipName.GetComponent<UILabel> ().text = name;
		}

		/// <summary>
		/// Ups the properties icon.
		/// 更新图标
		/// </summary>
		/// <param name="name">Name.</param>
		public void UpPropsIcon (string icon)
		{
			this.EquipIcon.GetComponent<UISprite> ().spriteName = icon;
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
		public void UpEnchanting (bool bl)
		{
			UILabel label = this.Enchanting.GetComponent<UILabel> ();
			if (bl)
				label.text = UIPanelLang.UNENCHANTED;
			else
				label.text = "";
		}
		/// <summary>
		/// Ups the properties info label.
		/// 更新信息
		/// </summary>
		/// <param name="data">Data.</param>
		public void UpPropsInfo (Equip data)
		{
			//如果网络数据未空，说明你打开了错误的页面
			if (data.net == null)
				return;
			float enchantingVariable = Utils.EnchantingVariable (data.data.upgrade,data.net.enchantsLv);
			PropsType type = (PropsType)data.data.type;
			string infoStr = "";
			if (PropsType.EQUIP == type) {
				if (data.data.strength == data.data.intellect && data.data.intellect == data.data.agile) {
					infoStr += UIPanelLang.STRENGTH + ",";
					infoStr += UIPanelLang.INTELLECT + ",";
					infoStr += UIPanelLang.AGILE + "+";
					infoStr += data.data.strength;
					infoStr += "[33FF00] + " + Mathf.Round(data.data.strength * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				} else {
					//		<!-- 属性加成 -->
					//		<!-- 力量 -->
					//		<strength>21</strength>
					if (data.data.strength > 0) {
						infoStr += UIPanelLang.STRENGTH + "+" + data.data.strength;
						infoStr += "[33FF00] + " +Mathf.Round(data.data.strength * enchantingVariable).ToString() + "[-]";
						infoStr += Environment.NewLine;
					}
					//		<!-- 智力 -->
					//		<intellect>42</intellect>
					if (data.data.intellect > 0) {
						infoStr += UIPanelLang.INTELLECT + "+" + data.data.intellect;
						infoStr += "[33FF00] + " +Mathf.Round(data.data.intellect * enchantingVariable).ToString() + "[-]";
						infoStr += Environment.NewLine;
					}
					//		<!-- 敏捷 -->
					//		<agile>2</agile>
					if (data.data.agile > 0) {
						infoStr += UIPanelLang.AGILE + "+" + data.data.agile;
						infoStr += "[33FF00] + "+Mathf.Round(data.data.agile * enchantingVariable).ToString() + "[-]";
						infoStr += Environment.NewLine;
					}
				}
				//		<!-- 生命最大 -->
				//		<hpMax>132</hpMax>
				if (data.data.hpMax > 0) {
					infoStr += UIPanelLang.HPMAX + "+" + data.data.hpMax;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.hpMax * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;

				}
				//		<!-- 攻击强度 -->
				//		<attack_damage>23</attack_damage>
				if (data.data.attack_damage > 0) {
					infoStr += UIPanelLang.ATTACK_DAMAGE + "+" + data.data.attack_damage;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.attack_damage * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术强度 -->
				//		<spell_power>123</spell_power>
				if (data.data.ability_power > 0) {
					infoStr += UIPanelLang.SPELL_POWER + "+" + data.data.ability_power;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.ability_power * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理防御 -->
				//		<physical_defense>321</physical_defense>
				if (data.data.physical_defense > 0) {
					infoStr += UIPanelLang.PHYSICAL_DEFENSE + "+" + data.data.physical_defense;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.physical_defense * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术防御 -->
				//		<spell_defense>123</spell_defense>
				if (data.data.magic_defense > 0) {
					infoStr += UIPanelLang.SPELL_DEFENSE + "+" + data.data.magic_defense;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.magic_defense * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理爆击 -->
				//		<physical_crit>12</physical_crit>
				if (data.data.physical_crit > 0) {
					infoStr += UIPanelLang.PHYSICAL_CRIT + "+" + data.data.physical_crit;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.physical_crit * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术爆击 -->
				//		<spell_crit>21</spell_crit>
				if (data.data.magic_crit > 0) {
					infoStr += UIPanelLang.SPELL_CRIT + "+" + data.data.magic_crit;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.magic_crit * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 生命回复 -->
				//		<hp_re>12</hp_re>
				if (data.data.hp_recovery > 0) {
					infoStr += UIPanelLang.HP_RECOVERY + "+" + data.data.hp_recovery;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.hp_recovery * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 能量回复 -->
				//		<energy_re>21</energy_re>
				if (data.data.energy_recovery > 0) {
					infoStr += UIPanelLang.ENERGY_RECOVERY + "+" + data.data.energy_recovery;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.energy_recovery * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理穿透 -->
				//		<physical_penetrate>12</physical_penetrate>
				if (data.data.physical_penetration > 0) {
					infoStr += UIPanelLang.PHYSICAL_PENETRATION + "+" + data.data.physical_penetration;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.physical_penetration * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术穿透 -->
				//		<spell_penetrate>21</spell_penetrate>
				if (data.data.spell_penetration > 0) {
					infoStr += UIPanelLang.SPELL_PENETRATION + "+" + data.data.spell_penetration;
					infoStr +=  "[33FF00] + " +Mathf.Round(data.data.spell_penetration * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 吸血等级 -->
				//		<bloodsucking_lv>12</bloodsucking_lv>
				if (data.data.bloodsucking_lv > 0) {
					infoStr += UIPanelLang.BLOODSUCKING_LV + "+" + data.data.bloodsucking_lv;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.bloodsucking_lv * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 闪避 -->
				//		<dodge>21</dodge>
				if (data.data.dodge > 0) {
					infoStr += UIPanelLang.DODGE + "+" + data.data.dodge;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.dodge * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 治疗效果 -->
				//		<addition_treatment>21</addition_treatment>
				if (data.data.addition_treatment > 0) {
					infoStr += UIPanelLang.ADDITION_TREATMENT + "+" + data.data.addition_treatment;
					infoStr += "[33FF00] + " +Mathf.Round(data.data.addition_treatment * enchantingVariable).ToString() + "[-]";
					infoStr +="%" + Environment.NewLine;
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

			UILabel label = EquipInfoLabel.GetComponent<UILabel> ();
			if (label != null) {
				label.text = infoStr;
				Utils.TextBgAdaptiveSize (label, EquipInfoBg.GetComponent<UISprite> ());
				EquipInfoTable.GetComponent<UITable> ().repositionNow = true;
			}
		}

		/// <summary>
		/// Ups the description.
		/// 更新描述
		/// </summary>
		/// <param name="text">Text.</param>
		public void UpDescription (string text)
		{
			UILabel lab = EquipDescription.GetComponent<UILabel> ();
			if (lab != null) {
				lab.text = text;
			}
		}

		/// <summary>
		/// Ups the properties lv label.
		/// 等下道具等级需求
		/// </summary>
		/// <param name="text">Text.</param>
		public void UpPropsLvLabel (string text)
		{
			UILabel lab = EquipLvLabel.GetComponent<UILabel> ();
			if (lab != null) {
				lab.text = string.Format (UIPanelLang.PROPS_LV_LABEL_TAG, text);
			}
		}
	}
}