using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
using System;

namespace TangGame.UI
{
	public class EnchantsPanel : MonoBehaviour
	{
		/// <summary>
		/// The back.
		/// 返回按钮
		/// </summary>
		public GameObject BackBtn;
		/// <summary>
		/// The properties table.
		/// 道具table对象
		/// </summary>
		public GameObject PropsTable;
		/// <summary>
		/// The name of the hero.
		/// 英雄名字
		/// </summary>
		public GameObject HeroName;
		/// <summary>
		/// The select hero button.
		/// 选择英雄的按钮
		/// </summary>
		public GameObject SelectHeroBtn;

		/// <summary>
		/// The properties info label.
		/// 装备信息标签
		/// </summary>
		public GameObject EquipInfoLabel;
		// Use this for initialization
		void Start ()
		{

		}
		// Update is called once per frame
		void Update ()
		{

		}

		/// <summary>
		/// Ups the properties info label.
		/// 更新信息
		/// </summary>
		/// <param name="data">Data.</param>
		public void UpPropsInfo (EquipBase data)
		{

			PropsTypeEnum type = (PropsTypeEnum)data.Xml.type;
			string infoStr = "";
			float enchantingVariable = Utils.EnchantingVariable (data.Xml.upgrade,data.Net.enchantsLv);
			if (PropsTypeEnum.EQUIP == type) {
				if (data.Xml.strength == data.Xml.intellect && data.Xml.intellect == data.Xml.agile) {
					infoStr += UIPanelLang.STRENGTH + ",";
					infoStr += UIPanelLang.INTELLECT + ",";
					infoStr += UIPanelLang.AGILE + "+";
					infoStr += string.Format ("[c00ffff]{0}[-]",data.Xml.strength);
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.strength * enchantingVariable).ToString() + "[-]";
				} else {
					//		<!-- 属性加成 -->
					//		<!-- 力量 -->
					//		<strength>21</strength>
					if (data.Xml.strength > 0) {
						infoStr += UIPanelLang.STRENGTH + "+" + data.Xml.strength;
						infoStr +="+[33FF00]" +Mathf.Round(data.Xml.strength * enchantingVariable).ToString() + "[-]";
						infoStr += Environment.NewLine;
					}
					//		<!-- 智力 -->
					//		<intellect>42</intellect>
					if (data.Xml.intellect > 0) {
						infoStr += UIPanelLang.INTELLECT + "+" + data.Xml.intellect;
						infoStr +="+[33FF00]" +Mathf.Round(data.Xml.intellect * enchantingVariable).ToString() + "[-]";
						infoStr += Environment.NewLine;
					}
					//		<!-- 敏捷 -->
					//		<agile>2</agile>
					if (data.Xml.agile > 0) {
						infoStr += UIPanelLang.AGILE + "+" + data.Xml.agile;
						infoStr +="+[33FF00]" +Mathf.Round(data.Xml.agile * enchantingVariable).ToString() + "[-]";
						infoStr += Environment.NewLine;
					}
				}
				//		<!-- 生命最大 -->
				//		<hpMax>132</hpMax>
				if (data.Xml.hpMax > 0) {
					infoStr += UIPanelLang.HPMAX + "+" + data.Xml.hpMax;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.hpMax * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;

				}
				//		<!-- 攻击强度 -->
				//		<attack_damage>23</attack_damage>
				if (data.Xml.attack_damage > 0) {
					infoStr += UIPanelLang.ATTACK_DAMAGE + "+" + data.Xml.attack_damage;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.attack_damage * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术强度 -->
				//		<spell_power>123</spell_power>
				if (data.Xml.spell_power > 0) {
					infoStr += UIPanelLang.SPELL_POWER + "+" + data.Xml.spell_power;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.spell_power * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理防御 -->
				//		<physical_defense>321</physical_defense>
				if (data.Xml.physical_defense > 0) {
					infoStr += UIPanelLang.PHYSICAL_DEFENSE + "+" + data.Xml.physical_defense;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.physical_defense * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术防御 -->
				//		<spell_defense>123</spell_defense>
				if (data.Xml.spell_defense > 0) {
					infoStr += UIPanelLang.SPELL_DEFENSE + "+" + data.Xml.spell_defense;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.spell_defense * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理爆击 -->
				//		<physical_crit>12</physical_crit>
				if (data.Xml.physical_crit > 0) {
					infoStr += UIPanelLang.PHYSICAL_CRIT + "+" + data.Xml.physical_crit;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.physical_crit * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术爆击 -->
				//		<spell_crit>21</spell_crit>
				if (data.Xml.spell_cirt > 0) {
					infoStr += UIPanelLang.SPELL_CRIT + "+" + data.Xml.spell_cirt;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.spell_cirt * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 生命回复 -->
				//		<hp_re>12</hp_re>
				if (data.Xml.hp_re > 0) {
					infoStr += UIPanelLang.HP_RECOVERY + "+" + data.Xml.hp_re;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.hp_re * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 能量回复 -->
				//		<energy_re>21</energy_re>
				if (data.Xml.energy_re > 0) {
					infoStr += UIPanelLang.ENERGY_RECOVERY + "+" + data.Xml.energy_re;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.energy_re * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理穿透 -->
				//		<physical_penetrate>12</physical_penetrate>
				if (data.Xml.physical_penetrate > 0) {
					infoStr += UIPanelLang.PHYSICAL_PENETRATION + "+" + data.Xml.physical_penetrate;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.physical_penetrate * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术穿透 -->
				//		<spell_penetrate>21</spell_penetrate>
				if (data.Xml.spell_penetrate > 0) {
					infoStr += UIPanelLang.SPELL_PENETRATION + "+" + data.Xml.spell_penetrate;
					infoStr += "+[33FF00]" +Mathf.Round(data.Xml.spell_penetrate * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 吸血等级 -->
				//		<bloodsucking_lv>12</bloodsucking_lv>
				if (data.Xml.bloodsucking_lv > 0) {
					infoStr += UIPanelLang.BLOODSUCKING_LV + "+" + data.Xml.bloodsucking_lv;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.bloodsucking_lv * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 闪避 -->
				//		<dodge>21</dodge>
				if (data.Xml.dodge > 0) {
					infoStr += UIPanelLang.DODGE + "+" + data.Xml.dodge;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.dodge * enchantingVariable).ToString() + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 治疗效果 -->
				//		<addition_treatment>21</addition_treatment>
				if (data.Xml.addition_treatment > 0) {
					infoStr += UIPanelLang.ADDITION_TREATMENT + "+" + data.Xml.addition_treatment;
					infoStr +="+[33FF00]" +Mathf.Round(data.Xml.addition_treatment * enchantingVariable).ToString() + "[-]";
					infoStr +="%" + Environment.NewLine;
				}
			}

			UILabel label = EquipInfoLabel.GetComponent<UILabel> ();
			if (label != null) {
				label.text = infoStr;
			}
		}
	}
}