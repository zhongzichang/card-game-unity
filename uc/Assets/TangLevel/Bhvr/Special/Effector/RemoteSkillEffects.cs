using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	/// <summary>
	/// Trajectories.
	/// 运动轨迹
	/// </summary>
	public enum Trajectories
	{
		/// <summary>
		/// 闪现到敌人身上
		/// </summary>
		Flash,
		/// <summary>
		/// 直线
		/// </summary>
		Straight,
		/// <summary>
		/// 抛物线
		/// </summary>
		Parabola
	}

	public class RemoteSkillEffects : EffectorSpecialBhvr
	{

		/// <summary>
		/// 技能的速度
		/// </summary>
		public float moveSpeed = 80f;
		/// <summary>
		/// 选择运动轨迹
		/// </summary>
		public Trajectories mTrajectorie = Trajectories.Straight;

		/// <summary>
		/// The emission objects.
		/// </summary>
		public GameObject emissions;
		/// <summary>
		/// 是否为范围攻击
		/// </summary>
		public bool rangeAttack = false;
		public bool releaseOnSourceDead = false;
		public bool releaseOnTargetDead = false;
		private Animator mAnimator;
		GameObject targetObj;

		int firstNamHash;
		// Use this for initialization
		void Awake ()
		{
			mAnimator = GetComponent<Animator> ();
			if (emissions == null) {
				emissions = gameObject;
			}
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
				switch (mTrajectorie) {
				case Trajectories.Straight:
					if (mMoving)
						transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
					StraightCast ();
					break;
				case Trajectories.Parabola:
					if (mMoving) {
						moveSpeed = w.skill.distance / mAnimator.GetCurrentAnimatorStateInfo (0).length;
						Vector3 moveDis = Vector3.forward * moveSpeed * Time.deltaTime;
						if (!float.IsNaN (moveDis.x) && !float.IsNaN (moveDis.y) && !float.IsNaN (moveDis.z))
							transform.Translate (moveDis);
					}
					break;
				}


			} else {
				mAnimator.enabled = false;
			}

			ReleaseOnBeyondDistance ();
		}

		bool mMoving = false;

		/// <summary>
		/// 开始移动
		/// </summary>
		void StartMove ()
		{
			mMoving = true;
		}

		/// <summary>
		/// 停止移动
		/// </summary>
		void StopMove ()
		{
			mMoving = false;
		}

		/// <summary>
		/// Beyonds the distance release.
		/// 如果超出范围则释放技能
		/// </summary>
		void ReleaseOnBeyondDistance ()
		{
			Vector2 sourcePosiV2 = new Vector2 (w.source.transform.position.x, w.source.transform.position.y);
			Vector2 mPosiV2 = new Vector2 (transform.position.x, transform.position.y);
			float mDistance = Vector2.Distance (sourcePosiV2, mPosiV2);
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

		bool isParabolaCasetStarted = false;

		/// <summary>
		/// Parabolas the caset.
		/// 范围搜索目标的攻击
		/// </summary>
		/// <returns>The caset.</returns>
		void RangeCaset ()
		{
			Vector3 pos = emissions.transform.position;
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
			// 对方全部活着的英雄
			List<GameObject> gl = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
				LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
			// 在范围内的英雄
			gl = HeroSelector.FindTargetsWithWidth (gl, pos, 20F);
			mCast (gl);
		
		}

		void StraightCast ()
		{
			Vector3 pos = emissions.transform.position;
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
			// 对方全部活着的英雄
			List<GameObject> gl = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
				LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
			// 在范围内的英雄
			gl = HeroSelector.FindTargetsWithWidth (gl, pos, 3F);
			mCast (gl);
		}

		bool castStarted = false;

		/// <summary>
		/// 对英雄抛出作用器
		/// </summary>
		/// <param name="gl">Gl.</param>
		void mCast (List<GameObject> gl)
		{
			castStarted = true;
			foreach (GameObject g in gl) {
				SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
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

		Vector3 targetPosi;
		Vector3 sourcePosi;

		public override void Play ()
		{	
			targetPosi = w.target.transform.position;
			sourcePosi = w.source.transform.position;
			transform.localScale = Vector3.one;
			transform.position = sourcePosi;
			if (mTrajectorie == Trajectories.Straight) {
				targetPosi += new Vector3 (0, 2.5f, 0);
				sourcePosi += new Vector3 (0, 2.5f, 0);
				transform.LookAt (targetPosi);
				StartMove ();
			}
			if (mTrajectorie == Trajectories.Parabola) {
				targetPosi.z = w.source.transform.position.z;
				targetPosi.y = w.source.transform.position.y;
				transform.LookAt (targetPosi);
			}
			transform.position = sourcePosi;
			isPlay = true;
		}
	}
}