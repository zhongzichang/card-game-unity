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
		public GameObject SkillInfoBg;
		private SkillBase skill;

		public void Flush (SkillBase skill)
		{
			this.skill = skill;
			SetSkillName (skill.Xml.name);
			if (skill.IsLock) {
				SetSkillLv ("进阶到***后解锁");
			} else {
				SetSkillLv (skill.Level);
			}
//			SetMoney (skill.config.money);
//		SetSkillInfoLabel (skill.config.description);
//		SetSkillDescription(skill.config.info);

			Add.gameObject.SetActive (!skill.IsLock);
			Money.gameObject.SetActive (!skill.IsLock);
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
		/// <summary>
		/// 当技能为解锁时使用
		/// </summary>
		/// <param name="str">String.</param>
		void SetSkillLv(string str){
			this.SkillLevel.text = str;
		}
		void SetSkillLv (int lv)
		{
			this.SkillLevel.text = "lv." + lv;
			//FIXME if skill > herolv addbutton will false;
		}
	}
}