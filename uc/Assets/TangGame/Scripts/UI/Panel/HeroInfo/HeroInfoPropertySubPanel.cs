using UnityEngine;
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

			AddPropertyItem ("[ff8c00]" + UIPanelLang.STRENGTH + "[-]", herobase.Net.Strength);
			AddPropertyItem ("[ff8c00]" + UIPanelLang.INTELLECT + "[-]", herobase.Net.Intellect);
			AddPropertyItem ("[ff8c00]敏捷[-]", herobase.Net.Agile);
		
			AddPropertyItem ("[ff8c00]最大生命值[-]", herobase.Net.HpMax);
			AddPropertyItem ("[ff8c00]物理攻击力[-]", herobase.Net.Attack_damage);

			if (herobase.Net.Ability_power != 0)
				AddPropertyItem ("[ff8c00]魔法强度[-]", herobase.Net.Ability_power);
			if (herobase.Net.Physical_defense != 0)
				AddPropertyItem ("[ff8c00]物理护甲[-]", herobase.Net.Physical_defense);
			if (herobase.Net.Magic_defense != 0)
				AddPropertyItem ("[ff8c00]魔法抗性[-]", herobase.Net.Magic_defense);
			if (herobase.Net.Physical_crit != 0)
				AddPropertyItem ("[ff8c00]物理爆击[-]", herobase.Net.Physical_crit);
			if (herobase.Net.Magic_crit != 0)
				AddPropertyItem ("[ff8c00]法术爆击[-]", herobase.Net.Magic_crit);
			if (herobase.Net.Hp_recovery != 0)
				AddPropertyItem ("[ff8c00]生命回复[-]", herobase.Net.Hp_recovery);
			if (herobase.Net.Energy_recovery != 0)
				AddPropertyItem ("[ff8c00]能量回复[-]", herobase.Net.Energy_recovery);
			if (herobase.Net.Dodge != 0)
				AddPropertyItem ("[ff8c00]闪避[-]", herobase.Net.Dodge);
			if (herobase.Net.Physical_penetration != 0)
				AddPropertyItem ("[ff8c00]物理穿透[-]", herobase.Net.Physical_penetration);
			if (herobase.Net.Spell_penetration != 0)
				AddPropertyItem ("[ff8c00]法术穿透[-]", herobase.Net.Spell_penetration);
			if (herobase.Net.Bloodsucking_lv != 0)
				AddPropertyItem ("[ff8c00]吸血等级[-]", herobase.Net.Bloodsucking_lv);
			if (herobase.Net.Addition_treatment != 0)
				AddPropertyItem ("[ff8c00]治疗技能加成[-]", herobase.Net.Addition_treatment);
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