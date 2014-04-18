using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame
{
	public class SkillInfoSubPanel : MonoBehaviour
	{

		public GameObject SkillNum;
		public GameObject SkillNumTime;
		public GameObject SkillTable;
		private HeroBase data;


		public void Flush(HeroBase hero){
			this.data = hero;
			this.SetSkillNum (hero.SkillNum);
//			this.SetSkillNumTime (he);FIXME 
			this.SetSkillGrid (hero.skillbases);
		}
		/// <summary>
		/// Sets the skill number.
		/// </summary>
		/// <param name="num">Number.</param>
		void SetSkillNum (int num)
		{
			SkillNum.GetComponent<UILabel> ().text = num.ToString();
		}
		/// <summary>
		/// Sets the skill number time.
		/// </summary>
		/// <param name="time">Time.</param>
		void SetSkillNumTime (string time)
		{
			SkillNumTime.GetComponent<UILabel> ().text = time;
		}
		/// <summary>
		/// Sets the skill grid.
		/// </summary>
		/// <param name="skillbases">Skillbases.</param>
		void SetSkillGrid (Dictionary<int,SkillBase> skillbases)
		{
			foreach (SkillBase skill in skillbases.Values) {
				this.AddHeroItem (skill);
			}
		}

		void AddHeroItem (SkillBase skill)
		{
			SkillItem item = Resources.Load<SkillItem> (UIContext.getWidgetsPath (UIContext.SKILL_ITEM_NAME));
			item = NGUITools.AddChild (SkillTable, item.gameObject).GetComponent<SkillItem> ();
			item.Flush (skill);
		}

	}
}