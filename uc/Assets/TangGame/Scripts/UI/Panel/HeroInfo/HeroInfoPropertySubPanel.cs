﻿using UnityEngine;
using System.Collections;
using TangGame.UI;
using System;
namespace TangGame.UI
{
	public class HeroInfoPropertySubPanel : MonoBehaviour
	{
		public GameObject HeroInfo;
		public GameObject HeroDescription;
		public GameObject Property;
		private HeroBase data;
		// Use this for initialization
		void Start ()
		{
	
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void Flush (HeroBase data)
		{
			this.data = data;
			SetHeroInfo (data.Xml.hero_info);
			SetHeroDescription (data.Xml.hero_tip);
			SetProperty (data);
		}

		/// <summary>
		/// Sets the hero info.
		/// </summary>
		void SetHeroInfo (string info)
		{
			this.HeroInfo.GetComponent<UILabel> ().text = info;
		}

		/// <summary>
		/// Sets the hero description.
		/// </summary>
		void SetHeroDescription (string description)
		{
			this.HeroDescription.GetComponent<UILabel> ().text = description;
		}

		/// <summary>
		/// Sets the property.
		/// </summary>
		/// <param name="str">String.</param>
		void SetProperty (HeroBase herobase)
		{
			this.ClearProperty ();
			AddPropertyItem ("[CC0000]" + UIPanelLang.STRENGTH_GROWTH + "[-]", herobase.Strength_growth);
			AddPropertyItem ("[CC0000]" +UIPanelLang.INTELLECT_GROWTH +"[-]", herobase.Intellect_growth);
			AddPropertyItem ("[CC0000]" +UIPanelLang.AGILE_GROWTH +"[-]", herobase.Agile_growth);

			AddPropertyItem ("[ff8c00]" + UIPanelLang.STRENGTH + "[-]", herobase.Strength);
			AddPropertyItem ("[ff8c00]" + UIPanelLang.INTELLECT + "[-]", herobase.Intellect);
			AddPropertyItem ("[ff8c00]敏捷[-]", herobase.Agile);
		
			AddPropertyItem ("[ff8c00]最大生命值[-]", herobase.HpMax);
			AddPropertyItem ("[ff8c00]物理攻击力[-]", herobase.Attack_damage);

			if (herobase.Ability_power != 0)
				AddPropertyItem ("[ff8c00]魔法强度[-]", herobase.Ability_power);
			if (herobase.Physical_defense != 0)
				AddPropertyItem ("[ff8c00]物理护甲[-]", herobase.Physical_defense);
			if (herobase.Magic_defense != 0)
				AddPropertyItem ("[ff8c00]魔法抗性[-]", herobase.Magic_defense);
			if (herobase.Physical_Crit != 0)
				AddPropertyItem ("[ff8c00]物理爆击[-]", herobase.Physical_Crit);
			if (herobase.Magic_Crit != 0)
				AddPropertyItem ("[ff8c00]法术爆击[-]", herobase.Magic_Crit);
			if (herobase.Hp_recovery != 0)
				AddPropertyItem ("[ff8c00]生命回复[-]", herobase.Hp_recovery);
			if (herobase.Energy_recovery != 0)
				AddPropertyItem ("[ff8c00]能量回复[-]", herobase.Energy_recovery);
			if (herobase.Dodge != 0)
				AddPropertyItem ("[ff8c00]闪避[-]", herobase.Dodge);
			if (herobase.Physical_penetration != 0)
				AddPropertyItem ("[ff8c00]物理穿透[-]", herobase.Physical_penetration);
			if (herobase.Spell_penetration != 0)
				AddPropertyItem ("[ff8c00]法术穿透[-]", herobase.Spell_penetration);
			if (herobase.Bloodsucking_lv != 0)
				AddPropertyItem ("[ff8c00]吸血等级[-]", herobase.Bloodsucking_lv);
			if (herobase.Addition_treatment != 0)
				AddPropertyItem ("[ff8c00]治疗技能加成[-]", herobase.Addition_treatment);
		}

		void AddPropertyItem (string str, float num)
		{
			 AddPropertyItem (str, num, 0);
		}
		/// <summary>
		/// Adds the property item.
		/// 添加一个属性
		/// </summary>
		/// <param name="str">String.</param>
		/// <param name="num">Number.</param>
		/// <param name="addNum">Add number.</param>
		void AddPropertyItem (string str, float num, float addNum)
		{
			Property.SetActive (true);
			UILabel label = Property.GetComponent<UILabel> ();
			string text = str + " : " + num + Environment.NewLine;
			if (addNum > 0) {
				text += " [00CC00]+" + addNum;
			}
			label.text += text;
		}
		void ClearProperty(){
			this.Property.GetComponent<UILabel>().text = "";
		}
	}
}