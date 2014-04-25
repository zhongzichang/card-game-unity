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
//			SetSkillDescription(skill.Xml.skill_tag);
//			SetSkillInfoLabel ("这是一个测试数据 \n不要怀疑我的真实性\n不要怀疑我的真实性\n不要怀疑我的真实性\n不要怀疑我的真实性");



			Add.gameObject.SetActive (!skill.IsLock);
			Money.gameObject.SetActive (!skill.IsLock);
		}

		void OnTooltip (bool bl)
		{
			string str = skill.Xml.skill_tag + "\n[FA8000]";
			str += skill.Xml.skill_info;
			UITooltip.ShowText (str);
		}

		void OnHover ()
		{
			UITooltip.ShowText ("");
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