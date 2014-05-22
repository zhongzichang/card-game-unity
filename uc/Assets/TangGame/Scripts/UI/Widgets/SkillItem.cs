using UnityEngine;
using System.Collections;
using TangGame.UI;
using System.Text.RegularExpressions;

namespace TangGame.UI
{
	public class SkillItem : MonoBehaviour
	{
		public UIButton Add;
		public UISprite SkillIcon;
		public UILabel Money;
		public UILabel SkillLevel;
		public UIPanel SkillTipsPanel;
		public UILabel SkillName;
		private SkillBase skill;
		HeroBase hero;

		public void Flush (HeroBase hero, SkillBase skill)
		{
			this.hero = hero;
			int herolv = hero.Net.level;
			this.skill = skill;
			SetSkillName (skill.Xml.name);
			SetSkillIncon (skill.Xml.skill_icon);
			if (skill.IsLock) {
				if (hero.SkillBases [1] == skill)
					SetSkillLv ("进阶到绿色后解锁");
				if (hero.SkillBases [2] == skill)
					SetSkillLv ("进阶到蓝色后解锁");
				if (hero.SkillBases [3] == skill)
					SetSkillLv ("进阶到紫色后解锁");

			} else {
				SetSkillLv (skill.Level, herolv);
			}
			//TODO
//			SetMoney (skill.config.money);



			Add.gameObject.SetActive (!skill.IsLock);
			Money.gameObject.SetActive (!skill.IsLock);
		}

		void OnPress (bool bl)
		{
			if (bl) {
				string str = skill.Xml.desc + "\n[FA8000]";
				if (skill.Level > 0) {
					string desc = skill.Xml.desc_y;
					Regex re = new Regex (@".+{(\d+)}.+");
					string param = re.Match (desc).Groups [0].Value;
					// TODO 具体需要商议
					str += desc;
				}
				NGUITools.SetActive (SkillTipsPanel.gameObject,true);
				SkillTipsPanel.GetComponentInChildren<UILabel> ().text = str;
			} else {
				NGUITools.SetActive (SkillTipsPanel.gameObject,false);
			}
		}

		public SkillBase Skill {
			get {
				return skill;
			}
			set {
				skill = value;
			}
		}

		void SetSkillIncon (string skillIconName)
		{
			this.SkillIcon.spriteName = skillIconName;
		}

		void SetMoney (string money)
		{
			this.Money.text = money;
		}

		void SetSkillName (string skillName)
		{
			this.SkillName.text = skillName;
		}

		/// <summary>
		/// 当技能为解锁时使用
		/// </summary>
		/// <param name="str">String.</param>
		void SetSkillLv (string str)
		{
			this.SkillLevel.text = str;
		}

		void SetSkillLv (int lv, int herolv)
		{
			this.SkillLevel.text = "lv." + lv;
			if (lv >= herolv) {
				Add.GetComponent<BoxCollider> ().enabled = false;
			} else {
				Add.GetComponent<BoxCollider> ().enabled = true;
			}
		}
	}
}