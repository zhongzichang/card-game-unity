using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class HuatuoXuming : EffectorSpecialBhvr
	{
		public bool releaseOnSourceDead = false;
		public bool releaseOnTargetDead = false;
		private Animator mAnimator;
		float duration = 5f;
		// Use this for initialization
		void Awake ()
		{
			mAnimator = GetComponent<Animator> ();
		}
		// Update is called once per frame
		void Update ()
		{
		
			if (releaseOnSourceDead) {
				ReleaseOnSourceDead ();
			}
			if (releaseOnTargetDead) {
				ReleaseOnTargetDead ();
			}

			if (isPlay) {
				mAnimator.enabled = true;
				duration -= Time.deltaTime;
				if (duration <= 0) {
					mRelease ();
				}
			} else {
				mAnimator.enabled = false;
			}
		}
		/// <summary>
		/// 在施放则死了的情况下释放技能
		/// </summary>
		void ReleaseOnSourceDead(){
			if (w.source.GetComponent<HeroBhvr>().hero.hp <= 0) {
				mRelease ();
			}
		}
		/// <summary>
		/// 在目标则死了的情况下释放技能
		/// </summary>
		void ReleaseOnTargetDead(){
			if (skillTarget.GetComponent<HeroBhvr>().hero.hp <= 0) {
				mRelease ();
			}
		}

		void mRelease ()
		{
			isPlay = false;
			transform.parent = null;
			StartRelease ();
		}

		void OnEnable ()
		{
			// 关卡控制
			LevelController.RaisePause += OnPause;
			LevelController.RaiseResume += OnResume;
		}

		void OnDisable ()
		{
			// 关卡控制
			LevelController.RaisePause -= OnPause;
			LevelController.RaiseResume -= OnResume;
		}

		void mCast ()
		{
			if (w == null || w.source == null)
				return;
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
			HeroStatusBhvr targetStatusBhvr = skillTarget.GetComponent<HeroStatusBhvr> ();
			// 抛出作用器
			foreach (Effector e in w.effector.subEffectors) {
				EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, skillTarget);
				sourceSkillBhvr.Cast (cw);
			}
		}

		GameObject skillTarget;

		public override void Play ()
		{
			duration = 5f;
			skillTarget = HeroSelector.FindSelfWeakest (w.source.GetComponent<HeroBhvr> ().hero);
			if (skillTarget == null) {
				skillTarget = w.source;
			}
			mCast ();
			transform.localScale = Vector3.one;
			transform.parent = skillTarget.transform;
			if (skillTarget.GetComponent<Directional> ().Direction == BattleDirection.LEFT)
				transform.localPosition = new Vector3 (0, 0, 1);
			else
				transform.localPosition = new Vector3 (0, 0, -1);
			isPlay = true;
		}
	}
}