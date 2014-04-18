using UnityEngine;
using System.Collections;

public class SkillItem : MonoBehaviour {

	public UIButton Add;
	public UISprite SkillIcon;
	public UILabel Money;
	public UILabel SkillLevel;
	public UILabel SkillName;
	private SkillBase skill;
	public void Flush(SkillBase skill){
		this.skill = skill;
//		SetSkillName (skill.config.name);
//		SetMoney (skill.config.money);
		SetSkillLv (skill.SkillLv);
	}
	
	public void SetSkillIncon(string skillIconName){
		this.SkillIcon.spriteName = skillIconName;
	}
	public void SetMoney(string money){
		this.Money.text = money;
	}
	public void SetSkillName(string skillName){
		this.SkillName.text = skillName;
	}
	public void SetSkillLv(int lv){
		this.SkillLevel.text = "lv." + lv;
		//FIXME if skill > herolv addbutton will false;
	}
}
