using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;

namespace TangGame.UI
{
	public class HeroInfoSkillSubPanel : MonoBehaviour
	{
		public GameObject SkillNum;
		public GameObject SkillNumTime;
		public GameObject SkillTable;
		private HeroBase data;
		//		private Time lastTime;
		//之前的一个时间点
		private long startTime;
		//限定时间秒
		private long fixedTime;
		// Use this for initialization
		void Start ()
		{  
			startTime = data.Net.lastUpSkillTime;
			startTime = (System.DateTime.Now.Ticks - System.DateTime.Parse ("1970-01-01").Ticks) / 10000000; //TODO that is test code
			StartCountdown ();
		}

		/// <summary>
		/// 开始倒计时
		/// </summary>
		void StartCountdown ()
		{
			fixedTime = 360;
			CancelInvoke ("CountDown");
			long nowTime = (System.DateTime.Now.Ticks - System.DateTime.Parse ("1970-01-01").Ticks) / 10000000;
			if (nowTime - startTime >= fixedTime) { 
				startTime += fixedTime;
				this.SetSkillNum (++data.Net.skillCount);
				if (data.Net.skillCount < 20) {
					this.StartCountdown ();
				} else {
					//FIXME 倒计时结束提醒玩家升级技能 
				}
			} else {  
				InvokeRepeating ("CountDown", 0, 1);  
			}
		}

		void CountDown ()
		{  
			bool skillTimeBool = true;
			long minute = (fixedTime / 60) % 60;
			long second = fixedTime % 60;
			if (minute == 0 && second < 0) {
				skillTimeBool = false;
				StartCountdown ();
			} else {
				fixedTime -= 1;
			}
			if (SkillNumTime.activeSelf != skillTimeBool)
				this.SkillNumTime.SetActive (skillTimeBool);

			string minuteStr = minute.ToString();
			string secondStr = second.ToString ();
			if (minute < 10) {
				minuteStr = 0 + minuteStr;
			}
			if (second < 10) {
				secondStr = 0 + second.ToString ();
			}


			this.SetSkillNumTime ("(" + minuteStr + ":" + second + ")");
//			SkillNumTime.GetComponent<UILabel>().text = (fixedTime / (60 * 60 * 24)).ToString() + "天"  
//				+ ((fixedTime/60 - fixedTime / (60 * 60 * 24)*24*60)/60).ToString() + "小时"  
//				+ ((fixedTime / 60) % 60).ToString() + "分"  
//				+ (fixedTime % 60).ToString() + "秒";  
		}

		public void Flush (HeroBase hero)
		{
			this.data = hero;
			this.SetSkillNum (hero.Net.skillCount);
			if (hero.Net.skillCount < 20) {
//							this.SetSkillNumTime (he); 
			}
			this.SetSkillGrid (hero.SkillBases);
		}

		/// <summary>
		/// Sets the skill number.
		/// </summary>
		/// <param name="num">Number.</param>
		void SetSkillNum (int num)
		{
			SkillNum.GetComponent<UILabel> ().text = num.ToString ();
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
		void SetSkillGrid (SkillBase[] skillbases)
		{
			foreach (SkillBase skill in skillbases) {
				this.AddHeroItem (skill);
			}
		}

		void AddHeroItem (SkillBase skill)
		{
			SkillItem item = Resources.Load<SkillItem> (UIContext.getWidgetsPath (UIContext.SKILL_ITEM_NAME));
			item = NGUITools.AddChild (SkillTable, item.gameObject).GetComponent<SkillItem> ();
			item.gameObject.name = "skill_" + skill.Xml.id;
			item.Flush (skill,data.Net.level);
		}
	}
}