using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
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
		public GameObject PropsName;
		/// <summary>
		/// The properties lv label.
		/// 道具要求等级
		/// </summary>
		public GameObject PropsLvLabel;
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

		public GameObject SynthesisBtn;
		/// <summary>
		/// The synthesis button label.
		/// </summary>
		public GameObject SynthesisBtnLabel;
		// Use this for initialization
		void Start ()
		{
			if (propsDPbean != null && propsDPbean.props != null) {
				Flush (propsDPbean);
			}
			this.SynthesisBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.SYNTHESIS_FORMULA;
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// Flush the specified data.
		/// 刷新面板数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void Flush (PropsDetailsPanelBean bean)
		{
			if (!(bean.props is EquipBase))
				return;
			EquipBase data = bean.props as EquipBase;
			this.UpPropsIcon (data.Xml.icon);
			this.UpPropsFrames (data.Xml.upgrade);
			this.UpPropsInfo (data); //TODO 追加装备附魔信息
			this.UpPropsName (data.Xml.name);
			this.UpPropsLvLabel (data.Xml.level.ToString ());
			if (data.Net != null && data.Net.enchantsLv != 0) {
				this.UpEnchanting (true);
			} else {
				this.UpEnchanting (false);
			}				
			this.UpDescription (data.Xml.description);


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
			this.EquipIcon.GetComponent<UISprite> ().spriteName = icon;
		}

		/// <summary>
		/// Ups the properties frames.
		/// 更新阶级
		/// </summary>
		public void UpPropsFrames (short upgrade)
		{
			this.Frames.GetComponent<UISprite> ().spriteName = "equip_frame_" + HeroBase.GetRankColorStr ((RankEnum)upgrade);// TODO  需要根据图片名字修改
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
		public void UpPropsInfo (EquipBase data)
		{
			//如果网络数据未空，说明你打开了错误的页面
			if (data.Net == null)
				return;
			float enchantingVariable = Utils.EnchantingVariable (data.Xml.upgrade,data.Net.enchantsLv);
			PropsTypeEnum type = (PropsTypeEnum)data.Xml.type;
			string infoStr = "";
			if (PropsTypeEnum.EQUIP == type) {
				if (data.Xml.strength == data.Xml.intellect && data.Xml.intellect == data.Xml.agile) {
					infoStr += UIPanelLang.STRENGTH + ",";
					infoStr += UIPanelLang.INTELLECT + ",";
					infoStr += UIPanelLang.AGILE + "+";
					infoStr += data.Xml.strength;
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
			//如果是贵重物品
			if (PropsTypeEnum.VALUABLE == type) {
				infoStr += data.Xml.info;
			}
			//如果是卷轴
			if (PropsTypeEnum.SCROLLS == type) {
				infoStr += string.Format (UIPanelLang.RELL_INFO, Config.propsPropsRelationship [data.Xml.id] [0]);
			}
			//如果是灵魂石
			if (PropsTypeEnum.SOULROCK == type) {
				ArrayList heroids = Config.propsHeroesRelationship [data.Xml.id];
				if (heroids.Count > 0) {
					TangGame.Xml.HeroXml heroxml = Config.heroXmlTable [(int)heroids [0]];
					int count = Config.evolveXmlTable [heroxml.evolve].val;
					infoStr += string.Format (UIPanelLang.SOUL_STONE_INFO, Config.evolveXmlTable [count], heroxml.name);
				}

			}
			//如果是消耗品
			if (PropsTypeEnum.NONE == type)
				infoStr += data.Xml.info;

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
		/// Ups the properties lv label.
		/// 等下道具等级需求
		/// </summary>
		/// <param name="text">Text.</param>
		public void UpPropsLvLabel (string text)
		{
			UILabel lab = PropsLvLabel.GetComponent<UILabel> ();
			if (lab != null) {
				lab.text = string.Format (UIPanelLang.PROPS_LV_LABEL_TAG, text);
			}
		}
	}
}