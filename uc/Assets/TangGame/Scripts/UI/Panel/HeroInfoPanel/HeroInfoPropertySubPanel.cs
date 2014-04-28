using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class HeroInfoPropertySubPanel : MonoBehaviour
	{
		public GameObject HeroInfo;
		public GameObject HeroDescription;
		public GameObject Property;
		public GameObject property;
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
			int index = 0;
			AddPropertyItem ("[CC0000]力量成长[-]", herobase.Strength_growth).transform.localPosition += Vector3.down * index++;
			AddPropertyItem ("[CC0000]智力成长[-]", herobase.Intellect_growth).transform.localPosition += Vector3.down * index++;
			AddPropertyItem ("[CC0000]敏捷成长[-]", herobase.Agile_growth).transform.localPosition += Vector3.down * index++;

			AddPropertyItem ("[ff8c00]力量[-]", herobase.Strength).transform.localPosition += Vector3.down * index++;
			AddPropertyItem ("[ff8c00]智力[-]", herobase.Intellect).transform.localPosition += Vector3.down * index++;
			AddPropertyItem ("[ff8c00]敏捷[-]", herobase.Agile).transform.localPosition += Vector3.down * index++;
		
			AddPropertyItem ("[ff8c00]最大生命值[-]", herobase.HpMax).transform.localPosition += Vector3.down * index++;
			AddPropertyItem ("[ff8c00]物理攻击力[-]", herobase.Attack_damage).transform.localPosition += Vector3.down * index++;

			if (herobase.Ability_power != 0)
				AddPropertyItem ("[ff8c00]魔法强度[-]", herobase.Ability_power).transform.localPosition += Vector3.down * index++;
			if (herobase.Physical_defense != 0)
				AddPropertyItem ("[ff8c00]物理护甲[-]", herobase.Physical_defense).transform.localPosition += Vector3.down * index++;
			if (herobase.Magic_defense != 0)
				AddPropertyItem ("[ff8c00]魔法抗性[-]", herobase.Magic_defense).transform.localPosition += Vector3.down * index++;
			if (herobase.Physical_Crit != 0)
				AddPropertyItem ("[ff8c00]物理爆击[-]", herobase.Physical_Crit).transform.localPosition += Vector3.down * index++;
			if (herobase.Magic_Crit != 0)
				AddPropertyItem ("[ff8c00]法术爆击[-]", herobase.Magic_Crit).transform.localPosition += Vector3.down * index++;
			if (herobase.Hp_recovery != 0)
				AddPropertyItem ("[ff8c00]生命回复[-]", herobase.Hp_recovery).transform.localPosition += Vector3.down * index++;
			if (herobase.Energy_recovery != 0)
				AddPropertyItem ("[ff8c00]能量回复[-]", herobase.Energy_recovery).transform.localPosition += Vector3.down * index++;
			if (herobase.Dodge != 0)
				AddPropertyItem ("[ff8c00]闪避[-]", herobase.Dodge).transform.localPosition += Vector3.down * index++;
			if (herobase.Physical_penetration != 0)
				AddPropertyItem ("[ff8c00]物理穿透[-]", herobase.Physical_penetration).transform.localPosition += Vector3.down * index++;
			if (herobase.Spell_penetration != 0)
				AddPropertyItem ("[ff8c00]法术穿透[-]", herobase.Spell_penetration).transform.localPosition += Vector3.down * index++;
			if (herobase.Bloodsucking_lv != 0)
				AddPropertyItem ("[ff8c00]吸血等级[-]", herobase.Bloodsucking_lv).transform.localPosition += Vector3.down * index++;
			if (herobase.Addition_treatment != 0)
				AddPropertyItem ("[ff8c00]治疗技能加成[-]", herobase.Addition_treatment).transform.localPosition += Vector3.down * index++;
		}

		GameObject AddPropertyItem (string str, float num)
		{
			return AddPropertyItem (str, num, 0);
		}

		GameObject AddPropertyItem (string str, float num, float addNum)
		{
			GameObject obj = NGUITools.AddChild (Property.gameObject, property.gameObject);
			UILabel label = obj.GetComponent<UILabel> ();
			label.text = str + " : " + num;
			if (addNum > 0) {
				label.text += " [00CC00]+" + addNum;
			}
			obj.SetActive (true);
			return obj;
		}
	}
}