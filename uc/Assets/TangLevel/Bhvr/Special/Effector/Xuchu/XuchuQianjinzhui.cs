using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class XuchuQianjinzhui : EffectorSpecialBhvr
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
				mAnimator.enabled = true;
			} else {
				mAnimator.enabled = false;
			}

		}

		void mRelease (){
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
		void OnGUI(){
			if (GUILayout.Button ("Play")) {
				isPlay = true;
			}
		}

		void mCast ()
		{
			if (w == null || w.source == null)
				return;
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
			// 对方全部活着的英雄
			List<GameObject> gl = sourceHeroBhvr.hero.battleDirection == BattleDirection.RIGHT ? 
				LevelContext.AliveEnemyGobjs : LevelContext.AliveSelfGobjs;
			gl = HeroSelector.FindTargetsWithWidth (gl, transform.position, 3F);
			foreach (GameObject g in gl) {
				// 判断对手状态，没在放大招或者处于眩晕状态
				HeroStatusBhvr targetStatusBhvr = g.GetComponent<HeroStatusBhvr> ();
				if (targetStatusBhvr.Status != HeroStatus.vertigo && !targetStatusBhvr.IsBigMove) {
					// 抛出作用器
					foreach (Effector e in w.effector.subEffectors) {
						EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, g);
						sourceSkillBhvr.Cast (cw);
					}
				}
			}
		}
		public override void Play ()
		{

			Directional mDirectional = w.source.GetComponent<Directional> ();
			transform.position = w.source.transform.position;
			Vector3 osVector = new Vector3 (5.5f,5f,0);
			// 是否需要翻转方向
			if (mDirectional.Direction == BattleDirection.LEFT) {
				transform.localRotation = Quaternion.FromToRotation (Vector3.left, Vector3.right);
				transform.position -= osVector;
			} else {
				transform.localRotation = Quaternion.identity;
				transform.position += osVector;
			}


			isPlay = true;
		}
	}
}