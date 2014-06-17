using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using System;
using System.Text.RegularExpressions;

namespace TangGame.UI
{
	public class PropsDetailsInterfacePanel : MonoBehaviour
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
				if (propsDPbean != null && propsDPbean.props != null) {
					Flush (propsDPbean);
				}
			}
		}

		public GameObject SynthesisBtn;
		/// <summary>
		/// The synthesis button label.
		/// </summary>
		public GameObject SynthesisBtnLabel;
		bool started;
		// Use this for initialization
		void Start ()
		{
			this.SynthesisBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.SYNTHESIS_FORMULA;
			started = true;
			Flush (propsDPbean);
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
			if (!started)
				return;
			int propsId = bean.props.data.id;
			if (!Config.propsXmlTable.ContainsKey (propsId))
				return;
			Props props;
			if (!PropsCache.instance.propsTable.ContainsKey (propsId)) {
				props = new Props ();
				props.data = Config.propsXmlTable [propsId];
			} else {
				props = PropsCache.instance.propsTable[propsId];
			}
			if (this.gameObject.activeSelf == false) {
				this.gameObject.SetActive (true);
				this.GetComponent<TweenPosition> ().Play (true);
			}

			if (props.data.GetSyntheticPropsTable ().Count <= 0) {
				this.SynthesisBtnLabel.GetComponent<UILabel> ().text = UIPanelLang.GET_WAY;
			}

			this.UpPropsIcon (props.data.Icon);
			this.UpPropsFrames (props.data.rank);
			this.UpPropsInfo (props);
			this.UpPropsName (props.data.name);
			this.UpPropsLvLabel (props.data.level.ToString ());
			int propsCount = 0;
			if (props.net != null) {
				propsCount = props.net.count;
			} 
			this.UpPropsCount (propsCount);


			this.SynthesisBtn.SetActive (true);
			this.EquippedBtn.SetActive (false);
			if (bean.hero != null) {
				if (propsCount > 0) {
					this.SynthesisBtn.SetActive (false);
					this.EquippedBtn.SetActive (true);
					if (bean.props.data.level > bean.hero.Net.level) {
						EquippedBtn.GetComponent<UIButton> ().isEnabled = false;
					} else {
						EquippedBtn.GetComponent<UIButton> ().isEnabled = true;
					}
				}
			} 
			this.UpDescription (props.data.description);


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
		public void UpPropsFrames (int rank)
		{
			this.Frames.GetComponent<UISprite> ().spriteName = Global.GetPropFrameName (rank);
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
				if (data.data.strength == data.data.intellect && data.data.intellect == data.data.agile && data.data.strength > 0) {
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
				PropsRelationData propsRelationData = PropsCache.instance.GetPropsRelationData (data.data.id);
				if (propsRelationData != null && propsRelationData.synthetics.Count > 0) {
					infoStr += string.Format (UIPanelLang.RELL_INFO, propsRelationData.synthetics [0].name);
				}
			}
			//如果是灵魂石
			if (PropsType.SOULROCK == type) {
				TangGame.Xml.HeroData heroData = HeroCache.instance.GetSoulStoneRelation (data.data.id);
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