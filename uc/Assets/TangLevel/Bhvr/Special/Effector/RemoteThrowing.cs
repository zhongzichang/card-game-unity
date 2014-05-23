﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class RemoteThrowing : EffectorSpecialBhvr
	{
		private Animator mAnimator;
		TweenPosition mTweenPosi;
		// Use this for initialization
		void Awake ()
		{
			mAnimator = GetComponent<Animator> ();
			mTweenPosi = GetComponent<TweenPosition> ();
		}
		// Update is called once per frame
		void Update ()
		{
			if (isPlay) {
				mAnimator.enabled = true;
				mCast ();
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

		void OnGUI ()
		{
			if (GUILayout.Button ("Play")) {
				isPlay = true;
			}
		}

		void mCast ()
		{
			Vector3 pos = transform.position;
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
			// 对方全部活着的英雄
			List<GameObject> gl = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
				LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
			// 在范围内的英雄
			gl = HeroSelector.FindTargetsWithWidth (gl, pos, 3F);
			foreach (GameObject g in gl) {
				// 判断对手状态，没在放大招或者处于眩晕状态
				HeroStatusBhvr targetStatusBhvr = g.GetComponent<HeroStatusBhvr> ();
				if (!XuanyunEffector.vertigos.Contains(g) && !targetStatusBhvr.IsBigMove) {
					// 抛出作用器
					foreach (Effector e in w.effector.subEffectors) {
						EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, g);
						sourceSkillBhvr.Cast (cw);
						mRelease ();
						return;
					}
				}
			}
		}

		public override void Play ()
		{
			transform.parent = w.source.transform;
			isPlay = true;
		}
	}
}