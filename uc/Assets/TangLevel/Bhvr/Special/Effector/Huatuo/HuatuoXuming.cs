using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class HuatuoXuming : EffectorSpecialBhvr
	{
		private Animator mAnimator;
		// Use this for initialization
		void Awake ()
		{
			mAnimator = GetComponent<Animator> ();
		}
		// Update is called once per frame
		void Update ()
		{
			if (isPlay) {

			} else {

			}
		}

		IEnumerator mRelease ()
		{
			yield return new WaitForSeconds (3f);
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
			// 判断对手状态，没在放大招或者处于眩晕状态
			HeroStatusBhvr targetStatusBhvr = w.source.GetComponent<HeroStatusBhvr> ();
			// 抛出作用器
			foreach (Effector e in w.effector.subEffectors) {
				EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, w.target);
				sourceSkillBhvr.Cast (cw);
			}
			
		}

		public override void Play ()
		{
			mCast ();
			StartCoroutine (mRelease ());
			transform.localScale = Vector3.one;
			transform.parent = w.source.transform;
			transform.localPosition = Vector3.zero;
			isPlay = true;
		}
	}
}