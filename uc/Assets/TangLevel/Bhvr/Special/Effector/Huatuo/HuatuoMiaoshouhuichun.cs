using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class HuatuoMiaoshouhuichun : EffectorSpecialBhvr
	{
		/// <summary>
		/// The skills mount point.
		/// 技能挂载点
		/// </summary>
		public Vector3 skillsMountPoint = new Vector3 (0, 2.5f, 0);
		// Use this for initialization
		void Awake ()
		{
			tweenPosi = gameObject.GetComponent<TweenPosition> ();
			trailRenderer =	gameObject.GetComponent<TrailRenderer> ();
		}

		Vector3 lastTarget;
		// Update is called once per frame
		void Update ()
		{
			if (isPlay) {
				if (mTarget == transform.position) {
					if (lastTarget != mTarget) {
						lastTarget = mTarget;
						ShowAnimation ();
					}
					if (targetVectors.Count != 0) {
						mCurrent = mTarget;
						mTarget = (Vector3)targetVectors.Dequeue ();
						MoveToBegin ();
					}
				}
			} else {

			}
		}

		IEnumerator mRelease ()
		{

			yield return new WaitForSeconds (0.2f);
			mCast (w.source);
			mCast (skillTarget);
			mCast (nextTarget);
			yield return new WaitForSeconds (0.2f);
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

		void mCast (GameObject targetObj)
		{
			if (w == null || w.source == null)
        return;
      SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
      if (targetObj != null && w.effector.subEffectors != null) {
				// 抛出作用器
				foreach (Effector e in w.effector.subEffectors) {
          EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, targetObj);
					sourceSkillBhvr.Cast (cw);
				}
			}

		}

		GameObject skillTarget;
		GameObject nextTarget;
		TweenPosition tweenPosi;
		TrailRenderer trailRenderer;
		public GameObject miaoshouhuichun_sub;

		public override void Play ()
		{
			mCurrent = w.source.transform.position + skillsMountPoint;
			skillTarget = HeroSelector.FindSelfWeakest (w.source.GetComponent<HeroBhvr> ().hero);
			if (skillTarget != null) {
				nextTarget = HeroSelector.FindclosestFriend (skillTarget.GetComponent<HeroBhvr> ());
			}
			isPlay = true;
			float trailTime = trailRenderer.time;
			trailRenderer.time = 0;
			gameObject.transform.position = mCurrent;
			mTarget = gameObject.transform.position;
			trailRenderer.time = trailTime;
			targetVectors.Enqueue (mCurrent);
			if (skillTarget != null)
				targetVectors.Enqueue (skillTarget.transform.position + skillsMountPoint);
			if (nextTarget != null)
				targetVectors.Enqueue (nextTarget.transform.position + skillsMountPoint);
			StartCoroutine (mRelease ());

		}

		Queue targetVectors = new Queue ();
		Vector3 mTarget;
		Vector3 mCurrent;

		void MoveToBegin ()
		{
			tweenPosi.from = mCurrent;
			tweenPosi.to = mTarget;
			tweenPosi.ResetToBeginning ();
			tweenPosi.Play ();
		}

		void ShowAnimation ()
		{
			GameObject obj = NGUITools.AddChild (gameObject, miaoshouhuichun_sub);
			obj.SetActive (true);
			obj.transform.parent = null;
			obj.transform.position += Vector3.back;
			GameObject.Destroy (obj, 0.3f);
		}
	}
}