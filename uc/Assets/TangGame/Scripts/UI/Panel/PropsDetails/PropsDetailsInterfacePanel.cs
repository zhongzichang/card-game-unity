using UnityEngine;
using System.Collections;
using TangGame.UI.Base;
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
			}
		}

		void OnEnable ()
		{
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
			PropsBase data = bean.props;
			if (this.gameObject.activeSelf == false) {
				this.gameObject.SetActive (true);
				this.GetComponent<TweenPosition> ().Play (true);
			}
			this.UpPropsIcon (data.Xml.icon);
			this.UpPropsFrames (data.Xml.upgrade);
			this.UpPropsInfo (data);
			this.UpPropsName (data.Xml.name);
			this.UpPropsLvLabel (data.Xml.level.ToString ());
			int propsCount = 0;
			if (data.Net != null) {
				propsCount = data.Net.count;
			} else {
				this.UpPropsCount (propsCount);
			}


			this.SynthesisBtn.SetActive (true);
			this.EquippedBtn.SetActive (false);
			if (bean.hero != null) {
				if (propsCount > 0) {
					this.SynthesisBtn.SetActive (false);
					this.EquippedBtn.SetActive (true);
					if (bean.props.Xml.level > bean.hero.Net.level) {
						EquippedBtn.GetComponent<UIButton> ().isEnabled = false;
					} else {
						EquippedBtn.GetComponent<UIButton> ().isEnabled = true;
					}
				}
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
			this.PropsIcon.GetComponent<UISprite> ().spriteName = icon;
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
		public void UpPropsInfo (PropsBase data)
		{

			PropsType type = (PropsType)data.Xml.type;
			string infoStr = "";
			if (PropsType.EQUIP == type) {
				if (data.Xml.strength == data.Xml.intellect && data.Xml.intellect == data.Xml.agile) {
					infoStr += UIPanelLang.STRENGTH + ",";
					infoStr += UIPanelLang.INTELLECT + ",";
					infoStr += UIPanelLang.AGILE + "+";
					infoStr += data.Xml.strength;
				} else {
					//		<!-- 属性加成 -->
					//		<!-- 力量 -->
					//		<strength>21</strength>
					if (data.Xml.strength > 0) {
						infoStr += UIPanelLang.STRENGTH + "+" + data.Xml.strength + Environment.NewLine;
					}
					//		<!-- 智力 -->
					//		<intellect>42</intellect>
					if (data.Xml.intellect > 0) {
						infoStr += UIPanelLang.INTELLECT + "+" + data.Xml.intellect + Environment.NewLine;
					}
					//		<!-- 敏捷 -->
					//		<agile>2</agile>
					if (data.Xml.agile > 0) {
						infoStr += UIPanelLang.AGILE + "+" + data.Xml.agile + Environment.NewLine;
					}
				}
				//		<!-- 生命最大 -->
				//		<hpMax>132</hpMax>
				if (data.Xml.hpMax > 0) {
					infoStr += UIPanelLang.HPMAX + "+" + data.Xml.hpMax + Environment.NewLine;

				}
				//		<!-- 攻击强度 -->
				//		<attack_damage>23</attack_damage>
				if (data.Xml.attack_damage > 0) {
					infoStr += UIPanelLang.ATTACK_DAMAGE + "+" + data.Xml.attack_damage + Environment.NewLine;
				}
				//		<!-- 法术强度 -->
				//		<spell_power>123</spell_power>
				if (data.Xml.spell_power > 0) {
					infoStr += UIPanelLang.SPELL_POWER + "+" + data.Xml.spell_power + Environment.NewLine;
				}
				//		<!-- 物理防御 -->
				//		<physical_defense>321</physical_defense>
				if (data.Xml.physical_defense > 0) {
					infoStr += UIPanelLang.PHYSICAL_DEFENSE + "+" + data.Xml.physical_defense + Environment.NewLine;
				}
				//		<!-- 法术防御 -->
				//		<spell_defense>123</spell_defense>
				if (data.Xml.spell_defense > 0) {
					infoStr += UIPanelLang.SPELL_DEFENSE + "+" + data.Xml.spell_defense + Environment.NewLine;
				}
				//		<!-- 物理爆击 -->
				//		<physical_crit>12</physical_crit>
				if (data.Xml.physical_crit > 0) {
					infoStr += UIPanelLang.PHYSICAL_CRIT + "+" + data.Xml.physical_crit + Environment.NewLine;
				}
				//		<!-- 法术爆击 -->
				//		<spell_crit>21</spell_crit>
				if (data.Xml.spell_cirt > 0) {
					infoStr += UIPanelLang.SPELL_CRIT + "+" + data.Xml.spell_cirt + Environment.NewLine;
				}
				//		<!-- 生命回复 -->
				//		<hp_re>12</hp_re>
				if (data.Xml.hp_re > 0) {
					infoStr += UIPanelLang.HP_RECOVERY + "+" + data.Xml.hp_re + Environment.NewLine;
				}
				//		<!-- 能量回复 -->
				//		<energy_re>21</energy_re>
				if (data.Xml.energy_re > 0) {
					infoStr += UIPanelLang.ENERGY_RECOVERY + "+" + data.Xml.energy_re + Environment.NewLine;
				}
				//		<!-- 物理穿透 -->
				//		<physical_penetrate>12</physical_penetrate>
				if (data.Xml.physical_penetrate > 0) {
					infoStr += UIPanelLang.PHYSICAL_PENETRATION + "+" + data.Xml.physical_penetrate + Environment.NewLine;
				}
				//		<!-- 法术穿透 -->
				//		<spell_penetrate>21</spell_penetrate>
				if (data.Xml.spell_penetrate > 0) {
					infoStr += UIPanelLang.SPELL_PENETRATION + "+" + data.Xml.spell_penetrate + Environment.NewLine;
				}
				//		<!-- 吸血等级 -->
				//		<bloodsucking_lv>12</bloodsucking_lv>
				if (data.Xml.bloodsucking_lv > 0) {
					infoStr += UIPanelLang.BLOODSUCKING_LV + "+" + data.Xml.bloodsucking_lv + Environment.NewLine;
				}
				//		<!-- 闪避 -->
				//		<dodge>21</dodge>
				if (data.Xml.dodge > 0) {
					infoStr += UIPanelLang.DODGE + "+" + data.Xml.dodge + Environment.NewLine;
				}
				//		<!-- 治疗效果 -->
				//		<addition_treatment>21</addition_treatment>
				if (data.Xml.addition_treatment > 0) {
					infoStr += UIPanelLang.ADDITION_TREATMENT + "+" + data.Xml.addition_treatment + "%" + Environment.NewLine;
				}
			}
			//如果是贵重物品
			if (PropsType.VALUABLE == type) {
				infoStr += data.Xml.info;
			}
			//如果是卷轴
			if (PropsType.SCROLLS == type) {
				infoStr += string.Format (UIPanelLang.RELL_INFO, Config.propsPropsRelationship [data.Xml.id] [0]);
			}
			//如果是灵魂石
			if (PropsType.SOULROCK == type) {
				ArrayList heroids = Config.propsHeroesRelationship [data.Xml.id];
				if (heroids.Count > 0) {
					TangGame.Xml.HeroXml heroxml = Config.heroXmlTable [(int)heroids [0]];
					int count = Config.evolveXmlTable [heroxml.evolve].val;
					infoStr += string.Format (UIPanelLang.SOUL_STONE_INFO, Config.evolveXmlTable [count], heroxml.name);
				}

			}
			//如果是消耗品
			if (PropsType.NONE == type)
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