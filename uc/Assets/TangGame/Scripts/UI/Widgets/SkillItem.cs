using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class SkillItem : MonoBehaviour
	{
		public UIButton Add;
		public UISprite SkillIcon;
		public UILabel Money;
		public UILabel SkillLevel;
		public UILabel SkillName;
		public UILabel SkillDescription;
		public UILabel SkillInfoLabel;
		private SkillBase skill;

		public void Flush (SkillBase skill)
		{
			this.skill = skill;
//		SetSkillName (skill.config.name);
//		SetMoney (skill.config.money);
			SetSkillLv (skill.SkillLv);
//		SetSkillInfoLabel (skill.config.description);
//		SetSkillDescription(skill.config.info);
		}

		void OnTooltip (bool bl)
		{
			this.GetComponent<UIPlayTween> ().Play (true);

		}

		void OnHover ()
		{
//		this.GetComponent<UIPlayTween> ().Play(true);
		}

		public SkillBase Skill {
			get {
				return skill;
			}
			set {
				skill = value;
			}
		}

		void SetSkillInfoLabel (string skillInfoLabel)
		{
			this.SkillInfoLabel.text = skillInfoLabel;
		}

		void SetSkillDescription (string skillDescription)
		{
			this.SkillDescription.text = skillDescription;
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

		void SetSkillLv (int lv)
		{
			this.SkillLevel.text = "lv." + lv;
			//FIXME if skill > herolv addbutton will false;
		}
	}
}