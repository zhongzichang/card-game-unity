using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using System;
using TangGame.UI;

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
		/// 装备table对象
		/// </summary>
		public GameObject EquipTable;
		/// <summary>
		/// The properties table.
		/// 道具table 对象
		/// </summary>
		public GameObject PropsGridObj;
		/// <summary>
		/// The properties item.
		/// 道具对象
		/// </summary>
		public GameObject PropsItemObj;
		/// <summary>
		/// The hero table.
		/// 可选择的英雄界面
		/// </summary>
		public GameObject HeroTable;
		/// <summary>
		/// The name of the hero.
		/// 英雄名字
		/// </summary>
		public GameObject HeroName;
		/// <summary>
		/// The hero item.
		/// 英雄item对象
		/// </summary>
		public GameObject HeroItem;
		/// <summary>
		/// The select hero button.
		/// 选择英雄的按钮
		/// </summary>
		public GameObject SelectHeroBtn;
		/// <summary>
		/// The unenchanted.
		///  附魔状态标示
		/// </summary>
		public GameObject Unenchanted;
		/// <summary>
		/// The properties info label.
		/// 装备信息标签
		/// </summary>
		public GameObject EquipInfoLabel;
		/// <summary>
		/// The content of the equip.
		/// 装备相关内容
		/// </summary>
		public GameObject EquipContent;
		/// <summary>
		/// The enchanting exp.
		/// 附魔经验
		/// </summary>
		public GameObject EnchantingExp;
		/// <summary>
		/// The content of the hero.
		/// 英雄相关内容
		/// </summary>
		public GameObject HeroContent;
		/// <summary>
		/// The sub hero avatar item.
		/// 可选择英雄英雄对象
		/// </summary>
		public GameObject SubHeroAvatarItem;
		/// <summary>
		/// The select hero panel block.
		/// 英雄选择面板的遮罩
		/// </summary>
		public GameObject SelectHeroPanelBlock;
		/// <summary>
		/// The enchants spend gold.
		/// 附魔需要多少金币label
		/// </summary>
		public GameObject EnchantsSpendGold;
		/// <summary>
		/// The enchants spend gem.
		/// 一件附魔需要多少宝石label
		/// </summary>
		public GameObject EnchantsSpendGem;
		/// <summary>
		/// The select hero panel.
		/// </summary>
		public GameObject SelectHeroPanel;
		/// <summary>
		/// The exp progress full.
		/// </summary>
		public GameObject ExpProgressFull;
		/// <summary>
		/// 主界面英雄头像
		/// </summary>
		private HeroAvatarItem heroAvatarItem;
		/// <summary>
		/// The hero base.
		/// 选中的英雄
		/// </summary>
		private HeroBase heroBase = null;
		/// <summary>
		/// The equip base.
		/// 选中的道具
		/// </summary>
		private Equip mEquipBase = null;
		/// <summary>
		/// The en C xml.
		/// 当前装备的附魔表
		/// </summary>
		TangGame.Xml.EnchantsConsumedData enCXml;
		/// <summary>
		/// 当前进度条显示的经验
		/// </summary>
		int mEnExpCurrent = 0;
		/// <summary>
		/// The properties exp sum.
		/// 装备附魔经验总和
		/// </summary>
		int mEnExpSum = 0;
		/// <summary>
		/// The m en lv up.
		/// 装备附魔提
		/// </summary>
		int mEnLvUp = 0;
		/// <summary>
		/// The m gem spend lab.
		/// 显示一键附魔需要多少宝石的标签
		/// </summary>
		UILabel mGemSpendLab;
		/// <summary>
		/// The m gold spend lab.
		/// 显示附魔需要多少金币的标签
		/// </summary>
		UILabel mGoldSpendLab;
		/// <summary>
		/// The m exp sprite.
		/// 显示当前附魔经验的进度条
		/// </summary>
		UISprite mExpSprite;
		/// <summary>
		/// The m exp lab.
		/// 显示经验的标签
		/// </summary>
		UILabel mExpLab;
		/// <summary>
		/// The hero avatar item table.
		/// 英雄对象列表
		/// </summary>
		Dictionary<int,HeroAvatarItem> heroAvatarItemTable = new Dictionary<int, HeroAvatarItem> ();
		/// <summary>
		/// The properties base item table.
		/// 背包道具列表
		/// </summary>
		Dictionary<int,PropsItem> propsBaseItemTable = new Dictionary<int,PropsItem> ();
		/// <summary>
		/// The properties checked count.
		/// 被选中装备的数量
		/// </summary>
		Dictionary<PropsItem,int> propsCheckedCountTable = new Dictionary<PropsItem, int> ();

		void Awake ()
		{
			mGemSpendLab = this.EnchantsSpendGem.GetComponentInChildren<UILabel> ();
			mGoldSpendLab = this.EnchantsSpendGold.GetComponentInChildren<UILabel> ();
			mExpSprite = this.EnchantingExp.GetComponent<UISprite> ();
			mExpLab = this.EnchantingExp.GetComponentInChildren<UILabel> ();
		}
		// Use this for initialization
		void Start ()
		{
			if (heroBase == null) {
				HeroContent.SetActive (false);
			}
			if (mEquipBase == null) {
				EquipContent.SetActive (false);
			}
			SetEquipsEvent ();
			UIEventListener.Get (SelectHeroBtn.gameObject).onClick += SelectHeroBtnOnClick;
			UIEventListener.Get (SelectHeroPanelBlock.gameObject).onClick += SelectHeroPanelBlockOnClcik;
			UIEventListener.Get (HeroItem.gameObject).onClick += SelectHeroBtnOnClick;
		}

		/// <summary>
		/// 更新英雄对象
		/// </summary>
		/// <param name="heroBase">Hero base.</param>
		void UpHeroContent (HeroBase heroBase)
		{
			this.heroBase = heroBase;
			HeroContent.SetActive (true);
			EquipContent.SetActive (false);
			heroAvatarItem = HeroItem.GetComponent<HeroAvatarItem> ();
			heroAvatarItem.Flush (heroBase);
			UILabel lab = HeroName.GetComponent<UILabel> ();
			if (lab != null)
				lab.text = heroBase.Xml.name;
			int upgradeRem = Global.GetHeroUpgradeRem (heroBase.Net.upgrade);

			if (upgradeRem > 0) {
				lab.text += String.Format ("[000000] + {0}[-]", upgradeRem);
			}
			SetEquipList (heroBase);
		}

		/// <summary>
		/// Ups the content of the equip.
		/// 更新装备相关数据
		/// </summary>
		/// <param name="equipNet">Equip net.</param>
		void UpEquipContent (Equip equipBase)
		{
			this.mEquipBase = equipBase;
			//重置附魔点数
			mEnExpSum = 0;
			mEnLvUp = 0;
			mEnExpCurrent = equipBase.net.enchantsExp;
			propsCheckedCountTable.Clear ();
			enCXml = Config.enchantsConsumedXmlTable [equipBase.data.upgrade];
			int enLv = equipBase.net.enchantsLv;
			int enNextLv = enLv + 1;
			int enExp = equipBase.net.enchantsExp;
			if (enCXml.GetLvMax () == 0) {
				//此装备不能附魔
				this.EquipContent.SetActive (false);
			} else if (equipBase.net.enchantsLv >= enCXml.GetLvMax ()) {
				UpUnenchanted (enLv);
				UpEquipsInfo (equipBase);
				mGoldSpendLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
				mGemSpendLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
				mExpSprite.fillAmount = 1f;
				mExpLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
			} else {
				UpUnenchanted (enLv);
				UpEquipsInfo (equipBase);
				PropsTableLoad ();
				mGoldSpendLab.text = UIPanelLang.ENCHANTING_IS_NOT_SELECTED;
				int expToMax = enCXml.GetToMaxExp (enLv, enExp);
				mGemSpendLab.text = (expToMax * enCXml.gem_spend).ToString ();
				mExpSprite.fillAmount = (float)enExp / (float)enCXml.GetExpSpend (enNextLv);
				mExpLab.text = enExp + "/" + enCXml.GetExpSpend (enNextLv);
			}

		}

		/// <summary>
		/// Selects the hero panel block on clcik.
		/// 当选择英雄面板的遮罩被点击的时候
		/// </summary>
		/// <param name="obj">Object.</param>
		void SelectHeroPanelBlockOnClcik (GameObject obj)
		{
			SelectHeroPanel.SetActive (false);
		}

		/// <summary>
		/// Selects the hero button on click.
		/// 当切换英雄的按钮被点击的时候
		/// </summary>
		/// <param name="obj">Object.</param>
		void SelectHeroBtnOnClick (GameObject obj)
		{
			this.SelectHero ();
		}

		/// <summary>
		/// Clears the hero list view.
		/// 清空列表中的所有的英雄对象
		/// </summary>
		void ClearHeroListView ()
		{
			foreach (HeroAvatarItem item in heroAvatarItemTable.Values) {
				item.gameObject.SetActive (false);
				Destroy (item.gameObject);
			}
			heroAvatarItemTable.Clear ();
		}

		/// <summary>
		/// Selects the hero.
		/// 选择英雄
		/// </summary>
		void SelectHero ()
		{
			this.SelectHeroPanel.SetActive (true);
			ClearHeroListView ();
			foreach (HeroBase hero in HeroCache.instance.heroBeseTable.Values) {
				if (hero.Islock)
					continue;
				GameObject obj = NGUITools.AddChild (HeroTable.gameObject, SubHeroAvatarItem.gameObject);
				obj.SetActive (true);
				HeroAvatarItem heroAvatarItem = obj.GetComponent<HeroAvatarItem> ();
				heroAvatarItem.Flush (hero);
				heroAvatarItemTable.Add (hero.Xml.id, heroAvatarItem);
				UIEventListener.Get (obj).onClick -= SubHeroAvatarItemOnClick;
				UIEventListener.Get (obj).onClick += SubHeroAvatarItemOnClick;
				
			}
			this.HeroTable.GetComponent<UITable> ().repositionNow = true;

		}

		/// <summary>
		/// Propertieses the table load.
		/// </summary>
		void PropsTableLoad ()
		{

			foreach (Props props in PropsCache.instance.propsTable.Values) {
				//如果附魔经验为零则标示该装备不能成为附魔的消耗品
				if (props.data.enchant_points != 0) {
					AddPorpsItemToPropsTable (props);
				}
			}
		}

		/// <summary>
		/// Adds the porps item to properties table.
		/// 添加一个道具对象到道具列表中去
		/// </summary>
		/// <param name="propsBase">Properties base.</param>
		PropsItem AddPorpsItemToPropsTable (Props props)
		{
			PropsItem item;
			if (propsBaseItemTable.ContainsKey (props.data.id)) {
				item = propsBaseItemTable [props.data.id];
			} else {
				item = NGUITools.AddChild (PropsGridObj, PropsItemObj).GetComponent<PropsItem> ();//添加一个对象
				propsBaseItemTable.Add (props.data.id, item);
				UIEventListener.Get (item.gameObject).onClick += PropsItemOnClick;
				UIEventListener.Get (item.MinusBtn.gameObject).onClick += MinusBtnOnClick;
			}
			item.Flush (props);
			if (!item.gameObject.activeSelf) {
				item.gameObject.SetActive (true);
			}
			this.PropsGridObj.GetComponent<UIGrid> ().repositionNow = true;
			return item;
		}

		/// <summary>
		/// Minuses the button on click.
		/// 道具减号被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void MinusBtnOnClick (GameObject obj)
		{
			PropsItem itemTmp = obj.transform.parent.gameObject.GetComponent<PropsItem> ();
			if (itemTmp == null)
				return;
			mEnExpSum -= itemTmp.data.data.enchant_points;
			mEnExpCurrent -= itemTmp.data.data.enchant_points;
			int expMax = enCXml.GetExpSpend (mEquipBase.net.enchantsLv + mEnLvUp + 1);
			while (mEnExpCurrent <= 0) {
				if (--mEnLvUp >= 0) {
					expMax = enCXml.GetExpSpend (mEquipBase.net.enchantsLv + mEnLvUp + 1);
					mEnExpCurrent = expMax + mEnExpCurrent;
				} else {
					mEnExpCurrent = 0;
				}
			}
			propsCheckedCountTable [itemTmp] -= 1;
			if (propsCheckedCountTable [itemTmp] == 0) {
				obj.SetActive (false);
				itemTmp.propsCountLabel.text = itemTmp.data.net.count.ToString ();
			} else {
				itemTmp.propsCountLabel.text = propsCheckedCountTable [itemTmp] + "/" + itemTmp.data.net.count;
			}
			SetEnchantValue (expMax);
			
		}

		/// <summary>
		/// Propsitems the on click.
		/// 道具被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void PropsItemOnClick (GameObject obj)
		{
			PropsItem itemTmp = obj.GetComponent<PropsItem> ();
			if (itemTmp == null)
				return;
			int expMax = enCXml.GetExpSpend (mEquipBase.net.enchantsLv + mEnLvUp + 1);
			//如果当前装备附魔经验超出最大附魔经验则
			if (expMax == 0)
				return;

			if (propsCheckedCountTable.ContainsKey (itemTmp)) {
				//这是已经标示的物品大于当前物品的数量
				if (propsCheckedCountTable [itemTmp] >= itemTmp.data.net.count) {
					return;
				}
				propsCheckedCountTable [itemTmp] += 1;
			} else {
				propsCheckedCountTable.Add (itemTmp, 1);
			}

			itemTmp.MinusBtn.gameObject.SetActive (true);
			mEnExpSum += itemTmp.data.data.enchant_points;
			mEnExpCurrent += itemTmp.data.data.enchant_points;



			while (mEnExpCurrent + 1 > expMax && expMax != 0) {
				mEnLvUp++;
				mEnExpCurrent = mEnExpCurrent - expMax;
				expMax = enCXml.GetExpSpend (mEquipBase.net.enchantsLv + mEnLvUp + 1);
			}
			itemTmp.propsCountLabel.text = propsCheckedCountTable [itemTmp] + "/" + itemTmp.data.net.count;
			SetEnchantValue (expMax);
		}

		/// <summary>
		/// Sets the enchant value.
		/// 设置附魔点数的值
		/// </summary>
		/// <param name="expMax">Exp max.</param>
		void SetEnchantValue (int expMax)
		{
			//如果当前装备附魔经验超出最大附魔经验则
			if (expMax == 0) {
				NGUITools.SetActive (ExpProgressFull, true);
				mExpSprite.fillAmount = 1f;
				mExpLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
				mGoldSpendLab.text = (enCXml.GetToMaxExp (mEquipBase.net.enchantsLv, mEquipBase.net.enchantsExp) * enCXml.gold_spend).ToString ();
			} else {
				NGUITools.SetActive (ExpProgressFull, false);
				mExpSprite.fillAmount = (float)mEnExpCurrent / (float)expMax;
				mExpLab.text = mEnExpCurrent + "/" + expMax; 
				mGoldSpendLab.text = (mEnExpSum * enCXml.gold_spend).ToString ();
			}
		}

		/// <summary>
		/// Subs the hero avatar item on click.
		/// </summary>
		/// <param name="obj">Object.</param>
		void SubHeroAvatarItemOnClick (GameObject obj)
		{
			HeroAvatarItem heroAvatarItem = obj.GetComponent<HeroAvatarItem> ();
			this.UpHeroContent (heroAvatarItem.heroBase);
			SelectHeroPanel.SetActive (false);
		}

		/// <summary>
		/// 更新是否附魔
		/// </summary>
		/// <param name="enchantsLv">Enchants lv.</param>
		void UpUnenchanted (int enchantsLv)
		{
			if (enchantsLv > 0) {
				Unenchanted.GetComponent<UILabel> ().text = "";
			} else {
				Unenchanted.GetComponent<UILabel> ().text = UIPanelLang.UNENCHANTED;
			}
		}

		/// <summary>
		/// Sets the properties list.
		/// 设置穿戴的道具
		/// </summary>
		void SetEquipList (HeroBase heroBase)
		{
			EquipItemForEnchants[] items = EquipTable.GetComponentsInChildren<EquipItemForEnchants> ();
			for (int i = 0; i < items.Length; i++) {
				if (items [i] == null)
					continue;
				items [i].Flush (heroBase);
			}
		}

		/// <summary>
		/// Sets the equip list event.
		/// 设置按钮的点击事件，只能在start中使用一次
		/// </summary>
		void SetEquipsEvent ()
		{
			EquipItemForEnchants[] items = EquipTable.GetComponentsInChildren<EquipItemForEnchants> (true);
			foreach (EquipItemForEnchants equipItem in items) {
				UIEventListener.Get (equipItem.gameObject).onClick += OnEquipItemFEClick;
			}
		}

		/// <summary>
		/// Equips the item for enchants onclick.
		/// 装备被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnEquipItemFEClick (GameObject obj)
		{
			EquipItemForEnchants equipItem = obj.GetComponent<EquipItemForEnchants> ();
			if (equipItem != null && equipItem.EquipBase != null && equipItem.EquipBase.net != null) {
				this.EquipContent.SetActive (true);
				UpEquipContent (equipItem.EquipBase);
			} else {
				this.EquipContent.SetActive (false);
			}
		}

		/// <summary>
		/// Ups the properties info label.
		/// 更新装备的文本信息显示内容
		/// </summary>
		/// <param name="data">Data.</param>
		void UpEquipsInfo (Equip data)
		{

			PropsType type = (PropsType)data.data.type;
			string infoStr = "[000000]";
			float enchantingVariable = Utils.EnchantingVariable (data.data.upgrade, data.net.enchantsLv);
			if (PropsType.EQUIP == type) {
				if (data.data.strength == data.data.intellect && data.data.intellect == data.data.agile && data.data.strength > 0) {
					infoStr += UIPanelLang.STRENGTH + ",";
					infoStr += UIPanelLang.INTELLECT + ",";
					infoStr += UIPanelLang.AGILE + ":[CC0033] ";
					infoStr += data.data.strength + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.strength * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				} else {
					//		<!-- 属性加成 -->
					//		<!-- 力量 -->
					//		<strength>21</strength>
					if (data.data.strength > 0) {
						infoStr += UIPanelLang.STRENGTH + ":[CC0033] " + data.data.strength + "[-]";
						if (enchantingVariable != 0)
							infoStr += "[33FF00] + " + Mathf.Ceil (data.data.strength * enchantingVariable).ToString () + "[-]";
						infoStr += Environment.NewLine;
					}
					//		<!-- 智力 -->
					//		<intellect>42</intellect>
					if (data.data.intellect > 0) {
						infoStr += UIPanelLang.INTELLECT + ":[CC0033] " + data.data.intellect + "[-]";
						if (enchantingVariable != 0)
							infoStr += "[33FF00] + " + Mathf.Ceil (data.data.intellect * enchantingVariable).ToString () + "[-]";
						infoStr += Environment.NewLine;
					}
					//		<!-- 敏捷 -->
					//		<agile>2</agile>
					if (data.data.agile > 0) {
						infoStr += UIPanelLang.AGILE + ":[CC0033] " + data.data.agile + "[-]";
						if (enchantingVariable != 0)
							infoStr += "[33FF00] + " + Mathf.Floor (data.data.agile * enchantingVariable).ToString () + "[-]";
						infoStr += Environment.NewLine;
					}
				}
				//		<!-- 生命最大 -->
				//		<hpMax>132</hpMax>
				if (data.data.hpMax > 0) {
					infoStr += UIPanelLang.HPMAX + ":[CC0033] " + data.data.hpMax + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.hpMax * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;

				}
				//		<!-- 攻击强度 -->
				//		<attack_damage>23</attack_damage>
				if (data.data.attack_damage > 0) {
					infoStr += UIPanelLang.ATTACK_DAMAGE + ":[CC0033] " + data.data.attack_damage + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.attack_damage * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术强度 -->
				//		<spell_power>123</spell_power>
				if (data.data.ability_power > 0) {
					infoStr += UIPanelLang.SPELL_POWER + ":[CC0033] " + data.data.ability_power + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.ability_power * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理防御 -->
				//		<physical_defense>321</physical_defense>
				if (data.data.physical_defense > 0) {
					infoStr += UIPanelLang.PHYSICAL_DEFENSE + ":[CC0033] " + data.data.physical_defense + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.physical_defense * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术防御 -->
				//		<spell_defense>123</spell_defense>
				if (data.data.magic_defense > 0) {
					infoStr += UIPanelLang.SPELL_DEFENSE + ":[CC0033] " + data.data.magic_defense + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.magic_defense * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理爆击 -->
				//		<physical_crit>12</physical_crit>
				if (data.data.physical_crit > 0) {
					infoStr += UIPanelLang.PHYSICAL_CRIT + ":[CC0033] " + data.data.physical_crit + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.physical_crit * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术爆击 -->
				//		<spell_crit>21</spell_crit>
				if (data.data.magic_crit > 0) {
					infoStr += UIPanelLang.SPELL_CRIT + ":[CC0033] " + data.data.magic_crit + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.magic_crit * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 生命回复 -->
				//		<hp_re>12</hp_re>
				if (data.data.hp_recovery > 0) {
					infoStr += UIPanelLang.HP_RECOVERY + ":[CC0033] " + data.data.hp_recovery + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.hp_recovery * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 能量回复 -->
				//		<energy_re>21</energy_re>
				if (data.data.energy_recovery > 0) {
					infoStr += UIPanelLang.ENERGY_RECOVERY + ":[CC0033] " + data.data.energy_recovery + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.energy_recovery * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理穿透 -->
				//		<physical_penetrate>12</physical_penetrate>
				if (data.data.physical_penetration > 0) {
					infoStr += UIPanelLang.PHYSICAL_PENETRATION + ":[CC0033] " + data.data.physical_penetration + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.physical_penetration * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术穿透 -->
				//		<spell_penetrate>21</spell_penetrate>
				if (data.data.spell_penetration > 0) {
					infoStr += UIPanelLang.SPELL_PENETRATION + ":[CC0033] " + data.data.spell_penetration + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.spell_penetration * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 吸血等级 -->
				//		<bloodsucking_lv>12</bloodsucking_lv>
				if (data.data.bloodsucking_lv > 0) {
					infoStr += UIPanelLang.BLOODSUCKING_LV + ":[CC0033] " + data.data.bloodsucking_lv + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.bloodsucking_lv * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 闪避 -->
				//		<dodge>21</dodge>
				if (data.data.dodge > 0) {
					infoStr += UIPanelLang.DODGE + ":[CC0033] " + data.data.dodge + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.dodge * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 治疗效果 -->
				//		<addition_treatment>21</addition_treatment>
				if (data.data.addition_treatment > 0) {
					infoStr += UIPanelLang.ADDITION_TREATMENT + ":[CC0033] " + data.data.addition_treatment + "[-]";
					if (enchantingVariable != 0)
						infoStr += "[33FF00] + " + Mathf.Ceil (data.data.addition_treatment * enchantingVariable).ToString () + "[-]";
					infoStr += "%" + Environment.NewLine;
				}
			}
			UILabel label = EquipInfoLabel.GetComponent<UILabel> ();
			if (label != null) {
				label.text = infoStr;
			}
		}

		/// <summary>
		/// Adds the propertie.
		/// 添加一条属性
		/// </summary>
		/// <param name="infoStr">Info string.</param>
		/// <param name="title">Title.</param>
		/// <param name="val">Value.</param>
		/// <param name="enchantingVariable">Enchanting variable.</param>
		void AddPropertie (ref string infoStr, string title, float val, float enchantingVariable)
		{
			infoStr += string.Format ("{0}[ff0000] + {1}[-]", title, val);
			if (enchantingVariable != 0 && Mathf.Round (val * enchantingVariable) != 0) {
				infoStr += "[33FF00] +" + Mathf.Round (val * enchantingVariable).ToString () + "[-]";
			}
			infoStr += Environment.NewLine;
		}
	}
}