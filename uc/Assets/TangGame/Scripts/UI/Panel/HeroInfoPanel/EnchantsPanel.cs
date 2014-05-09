using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI.Base;
using System;
using TangGame.UI.Base;

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
		public GameObject PropsTableObj;
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
		private EquipBase mEquipBase = null;
		TangGame.Xml.EnchantsConsumedXml enCXml;

		UILabel mGemSpendLab;
		UILabel mGoldSpendLab;
		UISprite mExpSprite;
		UILabel mExpLab;

		void Awake(){
		}
		// Use this for initialization
		void Start ()
		{
			mGemSpendLab = this.EnchantsSpendGem.GetComponentInChildren<UILabel> ();
			mGoldSpendLab = this.EnchantsSpendGold.GetComponentInChildren<UILabel> ();
			mExpSprite = this.EnchantingExp.GetComponent<UISprite> ();
			mExpLab = this.EnchantingExp.GetComponentInChildren<UILabel> ();
			if (heroBase == null) {
				HeroContent.SetActive (false);
			}
			if (mEquipBase == null) {
				EquipContent.SetActive (false);
			}

			SetEquipListEvent ();

			UIEventListener.Get (SelectHeroBtn.gameObject).onClick += SelectHeroBtnOnClick;
			UIEventListener.Get (SelectHeroPanelBlock.gameObject).onClick += SelectHeroPanelBlockOnClcik;
			UIEventListener.Get (HeroItem.gameObject).onClick += SelectHeroBtnOnClick;
		}

		/// <summary>
		/// 更新英雄对象
		/// </summary>
		/// <param name="heroBase">Hero base.</param>
		public void UpHeroContent (HeroBase heroBase)
		{
			this.heroBase = heroBase;
			HeroContent.SetActive (true);
			EquipContent.SetActive (false);
			heroAvatarItem = HeroItem.GetComponent<HeroAvatarItem> ();
			heroAvatarItem.Flush (heroBase);
			UILabel lab = HeroName.GetComponent<UILabel> ();
			if (lab != null)
				lab.text = heroBase.Xml.name;
			int upgradeRem = Global.GetUpgradeRem (heroBase.Net.upgrade);

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
		public void UpEquipContent (EquipBase equipBase)
		{
			this.mEquipBase = equipBase;
			//重置附魔点数
			mEnExpSum = 0;
			mEnLvUp = 0;
			mEnExpCurrent = equipBase.Net.enchantsExp;
			propsCheckedCountTable.Clear ();
			enCXml = Config.enchantsConsumedXmlTable [equipBase.Xml.upgrade];
			int enLv = equipBase.Net.enchantsLv;
			int enNextLv = enLv+1;
			int enExp = equipBase.Net.enchantsExp;
			if (enCXml.GetLvMax () == 0) {
				//此装备不能附魔
				this.EquipContent.SetActive (false);
			} else if (equipBase.Net.enchantsLv >= enCXml.GetLvMax ()) {
				mGoldSpendLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
				mGemSpendLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
				mExpSprite.fillAmount = 1f;
				mExpLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
			} else {
				UpUnenchanted (enLv);
				UpPropsInfo (equipBase);
				PropsTableLoad ();
				mGoldSpendLab.text = UIPanelLang.ENCHANTING_IS_NOT_SELECTED;
				int expToMax = enCXml.GetToMaxExp (enLv,enExp);
				mGemSpendLab.text = (expToMax * enCXml.gem_spend).ToString();
				mExpSprite.fillAmount = (float)enExp/(float)enCXml.GetExpSpend (enNextLv);
				mExpLab.text = enExp + "/" + enCXml.GetExpSpend (enNextLv);
			}

		}

		/// <summary>
		/// Selects the hero panel block on clcik.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void SelectHeroPanelBlockOnClcik (GameObject obj)
		{
			SelectHeroPanel.SetActive (false);
		}

		/// <summary>
		/// Selects the hero button on click.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void SelectHeroBtnOnClick (GameObject obj)
		{
			this.SelectHero ();
		}

		private Dictionary<int,HeroAvatarItem> heroAvatarItemTable = new Dictionary<int, HeroAvatarItem> ();

		/// <summary>
		/// Clears the hero list view.
		/// 清空列表中的所有的英雄对象
		/// </summary>
		public void ClearHeroListView ()
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
		public void SelectHero ()
		{
			this.SelectHeroPanel.SetActive (true);
			ClearHeroListView ();
			foreach (HeroBase hero in BaseCache.heroBeseTable.Values) {
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
		/// The properties base item table.
		/// 背包道具列表
		/// </summary>
		public Dictionary<int,PropsItem> propsBaseItemTable = new Dictionary<int,PropsItem> ();
		/// <summary>
		/// The properties checked count.
		/// 被选中装备的数量
		/// </summary>
		private Dictionary<PropsItem,int> propsCheckedCountTable = new Dictionary<PropsItem, int> ();

		/// <summary>
		/// Propertieses the table load.
		/// </summary>
		public void PropsTableLoad ()
		{

			foreach (PropsBase propsBase in BaseCache.propsBaseTable.Values) {
				//如果附魔经验为零则标示该装备不能成为附魔的消耗品
				if (propsBase.Xml.enchant_points != 0) {
					AddPorpsItemToPropsTable (propsBase);
				}
			}
		}

		/// <summary>
		/// Adds the porps item to properties table.
		/// 添加一个道具对象到道具列表中去
		/// </summary>
		/// <param name="propsBase">Properties base.</param>
		public PropsItem AddPorpsItemToPropsTable (PropsBase propsBase)
		{
			PropsItem item;
			if (propsBaseItemTable.ContainsKey (propsBase.Xml.id)) {
				item = propsBaseItemTable [propsBase.Xml.id];
			} else {
				item = NGUITools.AddChild (PropsTableObj, PropsItemObj).GetComponent<PropsItem> ();//添加一个对象
				propsBaseItemTable.Add (propsBase.Xml.id, item);
			}
			item.Flush (propsBase);
			if (!item.gameObject.activeSelf) {
				item.gameObject.SetActive (true);
			}
			UIEventListener.Get (item.gameObject).onClick -= PropsItemOnClick;
			UIEventListener.Get (item.gameObject).onClick += PropsItemOnClick;
			this.PropsTableObj.GetComponent<UITable> ().repositionNow = true;
			return item;
		}

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
		/// Propsitems the on click.
		/// 道具被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void PropsItemOnClick (GameObject obj)
		{
			PropsItem itemTmp = obj.GetComponent<PropsItem> ();
			if (itemTmp == null)
				return;

			mEnExpSum += itemTmp.data.Xml.enchant_points;
			mEnExpCurrent += itemTmp.data.Xml.enchant_points;
			int expMax = enCXml.GetExpSpend(mEquipBase.Net.enchantsLv + mEnLvUp + 1);
//			int expTmp = expMax - mEquipBase.Net.enchantsExp;
			//如果当前装备附魔经验超出最大附魔经验则
			if (expMax == 0)
				return;



			if (propsCheckedCountTable.ContainsKey (itemTmp)) {
				//这是已经标示的物品大于当前物品的数量
				if (propsCheckedCountTable [itemTmp] >= itemTmp.data.Net.count) {
					return;
				}
				propsCheckedCountTable [itemTmp] += 1;
			} else {
				propsCheckedCountTable.Add (itemTmp, 1);
			}
			while(mEnExpCurrent + 1 > expMax && expMax != 0){
				mEnLvUp++;
				mEnExpCurrent = mEnExpCurrent - expMax;
//				expTmp = enCXml.GetExpSpend (mEquipBase.Net.enchantsLv + mEnLvUp);
				expMax = enCXml.GetExpSpend(mEquipBase.Net.enchantsLv + mEnLvUp + 1);
			}
			//如果当前装备附魔经验超出最大附魔经验则
			if (expMax == 0) {
				mExpSprite.fillAmount = 1f;
				mExpLab.text = UIPanelLang.ENCHANTING_HAS_TO_TOP;
				mGoldSpendLab.text = (enCXml.GetToMaxExp (mEquipBase.Net.enchantsLv, mEquipBase.Net.enchantsExp) * enCXml.gold_spend).ToString ();
			} else {
				mExpSprite.fillAmount = (float)mEnExpCurrent / (float)expMax;
				mExpLab.text = mEnExpCurrent + "/" + expMax; 
				mGoldSpendLab.text = (mEnExpSum * enCXml.gold_spend).ToString ();
			}
			itemTmp.propsCountLabel.text = propsCheckedCountTable [itemTmp] + "/" + itemTmp.data.Net.count;
		}

		/// <summary>
		/// Subs the hero avatar item on click.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void SubHeroAvatarItemOnClick (GameObject obj)
		{
			HeroAvatarItem heroAvatarItem = obj.GetComponent<HeroAvatarItem> ();
			this.UpHeroContent (heroAvatarItem.heroBase);
			SelectHeroPanel.SetActive (false);
		}

		/// <summary>
		/// 更新是否附魔
		/// </summary>
		/// <param name="enchantsLv">Enchants lv.</param>
		public void UpUnenchanted (int enchantsLv)
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
		void SetEquipListEvent ()
		{
			EquipItemForEnchants[] items = EquipTable.GetComponentsInChildren<EquipItemForEnchants> (true);
			foreach (EquipItemForEnchants equipItem in items) {
				UIEventListener.Get (equipItem.gameObject).onClick += EquipItemForEnchantsOnClick;
			}
		}

		/// <summary>
		/// Equips the item for enchants onclick.
		/// 装备被点击
		/// </summary>
		/// <param name="obj">Object.</param>
		void EquipItemForEnchantsOnClick (GameObject obj)
		{
			EquipItemForEnchants equipItem = obj.GetComponent<EquipItemForEnchants> ();
			if (equipItem != null && equipItem.EquipBase != null && equipItem.EquipBase.Net != null) {
				this.EquipContent.SetActive (true);
				UpEquipContent (equipItem.EquipBase);
			} else {
				this.EquipContent.SetActive (false);
			}
		}

		/// <summary>
		/// Ups the properties info label.
		/// 更新信息
		/// </summary>
		/// <param name="data">Data.</param>
		public void UpPropsInfo (EquipBase data)
		{

			PropsType type = (PropsType)data.Xml.type;
			string infoStr = "[000000]";
			float enchantingVariable = Utils.EnchantingVariable (data.Xml.upgrade, data.Net.enchantsLv);
			if (PropsType.EQUIP == type) {
				if (data.Xml.strength == data.Xml.intellect && data.Xml.intellect == data.Xml.agile) {
					string title = UIPanelLang.STRENGTH + "," + UIPanelLang.INTELLECT + "," + UIPanelLang.AGILE;
					AddPropertie (ref infoStr, title, data.Xml.strength, enchantingVariable);
				} else {
					//		<!-- 属性加成 -->
					//		<!-- 力量 -->
					//		<strength>21</strength>
					if (data.Xml.strength > 0) {
						AddPropertie (ref infoStr, UIPanelLang.STRENGTH, data.Xml.strength, enchantingVariable);
					}
					//		<!-- 智力 -->
					//		<intellect>42</intellect>
					if (data.Xml.intellect > 0) {
						AddPropertie (ref infoStr, UIPanelLang.INTELLECT, data.Xml.intellect, enchantingVariable);
					}
					//		<!-- 敏捷 -->
					//		<agile>2</agile>
					if (data.Xml.agile > 0) {
						AddPropertie (ref infoStr, UIPanelLang.AGILE, data.Xml.agile, enchantingVariable);
					}
				}
				//		<!-- 生命最大 -->
				//		<hpMax>132</hpMax>
				if (data.Xml.hpMax > 0) {
					AddPropertie (ref infoStr, UIPanelLang.HPMAX, data.Xml.hpMax, enchantingVariable);

				}
				//		<!-- 攻击强度 -->
				//		<attack_damage>23</attack_damage>
				if (data.Xml.attack_damage > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.ATTACK_DAMAGE, data.Xml.attack_damage);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.attack_damage * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术强度 -->
				//		<spell_power>123</spell_power>
				if (data.Xml.spell_power > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.SPELL_POWER, data.Xml.spell_power);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.spell_power * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理防御 -->
				//		<physical_defense>321</physical_defense>
				if (data.Xml.physical_defense > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.PHYSICAL_DEFENSE, data.Xml.physical_defense);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.physical_defense * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术防御 -->
				//		<spell_defense>123</spell_defense>
				if (data.Xml.spell_defense > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.SPELL_DEFENSE, data.Xml.spell_defense);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.spell_defense * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理爆击 -->
				//		<physical_crit>12</physical_crit>
				if (data.Xml.physical_crit > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.PHYSICAL_CRIT, data.Xml.physical_crit);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.physical_crit * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术爆击 -->
				//		<spell_crit>21</spell_crit>
				if (data.Xml.spell_cirt > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.SPELL_CRIT, data.Xml.spell_cirt);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.spell_cirt * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 生命回复 -->
				//		<hp_re>12</hp_re>
				if (data.Xml.hp_re > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.HP_RECOVERY, data.Xml.hp_re);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.hp_re * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 能量回复 -->
				//		<energy_re>21</energy_re>
				if (data.Xml.energy_re > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.ENERGY_RECOVERY, data.Xml.energy_re);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.energy_re * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 物理穿透 -->
				//		<physical_penetrate>12</physical_penetrate>
				if (data.Xml.physical_penetrate > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.PHYSICAL_PENETRATION, data.Xml.physical_penetrate);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.physical_penetrate * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 法术穿透 -->
				//		<spell_penetrate>21</spell_penetrate>
				if (data.Xml.spell_penetrate > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.SPELL_PENETRATION, data.Xml.spell_penetrate);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.spell_penetrate * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 吸血等级 -->
				//		<bloodsucking_lv>12</bloodsucking_lv>
				if (data.Xml.bloodsucking_lv > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.BLOODSUCKING_LV, data.Xml.bloodsucking_lv);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.bloodsucking_lv * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 闪避 -->
				//		<dodge>21</dodge>
				if (data.Xml.dodge > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}[-]", UIPanelLang.DODGE, data.Xml.dodge);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.dodge * enchantingVariable).ToString () + "[-]";
					infoStr += Environment.NewLine;
				}
				//		<!-- 治疗效果 -->
				//		<addition_treatment>21</addition_treatment>
				if (data.Xml.addition_treatment > 0) {
					infoStr += string.Format ("{0}[ff0000] + {1}", UIPanelLang.ADDITION_TREATMENT, data.Xml.addition_treatment);
					infoStr += "[33FF00] +" + Mathf.Round (data.Xml.addition_treatment * enchantingVariable).ToString () + "[-]";
					infoStr += " %[-]" + Environment.NewLine;
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
		public void AddPropertie (ref string infoStr, string title, int val, float enchantingVariable)
		{
			infoStr += string.Format ("{0}[ff0000] + {1}[-]", title, val);
			if (enchantingVariable != 0 && Mathf.Round (val * enchantingVariable) != 0) {
				infoStr += "[33FF00] +" + Mathf.Round (val * enchantingVariable).ToString () + "[-]";
			}
			infoStr += Environment.NewLine;
		}
	}
}