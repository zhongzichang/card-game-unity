using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class RemoteStraightSkillEffects : EffectorSpecialBhvr
	{
		/// <summary>
		/// 技能的速度
		/// </summary>
		public float moveSpeed = 80f;

		/// <summary>
		/// 是否为范围攻击
		/// </summary>
		public bool rangeAttack = false;

		public bool releaseOnSourceDead = false;
		public bool releaseOnTargetDead = false;
		private Animator mAnimator;
		TweenPosition mTweenPosi;
		GameObject targetObj;
		// Use this for initialization
		void Awake ()
		{
			mAnimator = GetComponent<Animator> ();
			mTweenPosi = GetComponent<TweenPosition> ();
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
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
				mCast ();
			} else {
				mAnimator.enabled = false;
			}

			Vector2 sourcePosiV2 = new Vector2 (w.source.transform.position.x,w.source.transform.position.y);
			Vector2 mPosiV2 = new Vector2 (transform.position.x,transform.position.y);
			float mDistance = Vector2.Distance (sourcePosiV2,mPosiV2);
			if (mDistance >= w.skill.distance) {
				mRelease ();
			}
		}

		/// <summary>
		/// 在施放则死了的情况下释放技能
		/// </summary>
		void ReleaseOnSourceDead ()
		{
			if (w.source.GetComponent<HeroBhvr> ().hero.hp <= 0) {
				mRelease ();
			}
		}

		/// <summary>
		/// 在目标则死了的情况下释放技能
		/// </summary>
		void ReleaseOnTargetDead ()
		{
			if (w.target.GetComponent<HeroBhvr> ().hero.hp <= 0) {
				mRelease ();
			}
		}

		void mRelease ()
		{
			isPlay = false;
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
				// 抛出作用器
				foreach (Effector e in w.effector.subEffectors) {
					EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, g);
					sourceSkillBhvr.Cast (cw);
					if (!rangeAttack) {
						mRelease ();
						return;
					}
				}
			}
		}

		public override void Play ()
		{	
			Vector3 targetPosi = w.target.transform.position + new Vector3 (0, 2.5f, 0);
			Vector3 sourcePosi = w.source.transform.position + new Vector3 (0, 2.5f, 0);
			transform.localScale = Vector3.one;
			transform.position = sourcePosi;
			transform.LookAt (targetPosi);
			isPlay = true;
		}
	}
}