using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class XuchuYemansiche : EffectorSpecialBhvr
	{
		public GameObject FlyersObj;
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
				mAnimator.enabled = true;
			} else {
				mAnimator.enabled = false;
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

//		void OnGUI ()
//		{
//			if (GUILayout.Button ("Play")) {
//				isPlay = true;
//			}
//		}
//
		void mCast ()
		{
			if (w == null || w.source == null)
				return;
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();

			// 判断对手状态，没在放大招或者处于眩晕状态
			HeroStatusBhvr targetStatusBhvr = w.target.GetComponent<HeroStatusBhvr> ();
			if (w.effector.subEffectors != null) {
				// 抛出作用器
				foreach (Effector e in w.effector.subEffectors) {
					EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, w.target);
					sourceSkillBhvr.Cast (cw);
				}
			}
		}

		public override void Play ()
		{
			GameObject flyer = NGUITools.AddChild (w.source,FlyersObj);
			flyer.transform.parent = null;
			TweenPosition tweenPosi  = flyer.GetComponent<TweenPosition> ();
			tweenPosi.from = w.source.transform.position;
			tweenPosi.to = w.target.transform.position;
			flyer.transform.LookAt (new Vector3(w.target.transform.position.x,w.target.transform.position.y,w.source.transform.position.z));
			flyer.SetActive (true);
			tweenPosi.Play ();
			Destroy (flyer,tweenPosi.duration);

			transform.localScale = Vector3.one;
			transform.parent = w.target.transform;
			transform.localPosition = Vector3.zero;
			isPlay = true;
		}
	}
}