using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangLevel
{
	public class HuatuoMiaoshouhuichun : EffectorSpecialBhvr
	{
		// Use this for initialization
		void Awake ()
		{
			tweenPosi = gameObject.GetComponent<TweenPosition> ();
			trailRenderer =	gameObject.GetComponent<TrailRenderer> ();
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

			yield return new WaitForSeconds (0.5f);
			mCast (w.source);
			mCast (skillTarget);
			mCast (nextTarget);
			yield return new WaitForSeconds (0.5f);
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
			HeroBhvr sourceHeroBhvr = w.source.GetComponent<HeroBhvr> ();
			SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
			HeroStatusBhvr targetStatusBhvr = targetObj.GetComponent<HeroStatusBhvr> ();
			// 抛出作用器
			foreach (Effector e in w.effector.subEffectors) {
				EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, w.target);
				sourceSkillBhvr.Cast (cw);
			}

		}

		GameObject skillTarget;
		GameObject nextTarget;
		TweenPosition tweenPosi;
		TrailRenderer trailRenderer;
		public GameObject miaoshouhuichun_sub;

		public override void Play ()
		{
			float trailTime = trailRenderer.time;
			trailRenderer.time = 0;
			transform.position = w.source.transform.position;
			tweenPosi.from = transform.position;
			trailRenderer.time = trailTime;
			skillTarget = HeroSelector.FindSelfWeakest (w.source.GetComponent<HeroBhvr> ().hero);
			nextTarget = HeroSelector.FindclosestFriend (skillTarget.GetComponent<HeroBhvr> ());
			isPlay = true;

			StartCoroutine (MoveTo (transform.position, skillTarget.transform.position));
			StartCoroutine (MoveTo (skillTarget.transform.position, nextTarget.transform.position));
			StartCoroutine (mRelease ());

		}


		IEnumerator MoveTo (Vector3 target, Vector3 next)
		{
			yield return transform.position == target;
//			GameObject obj = NGUITools.AddChild (gameObject, miaoshouhuichun_sub);
//			obj.SetActive (true);
//			obj.transform.parent = null;
//			GameObject.Destroy (obj, 0.5f);
			tweenPosi.enabled = true;
			tweenPosi.from = target;
			tweenPosi.to = next;
			tweenPosi.ResetToBeginning ();
			tweenPosi.Play ();

		}
	}
}