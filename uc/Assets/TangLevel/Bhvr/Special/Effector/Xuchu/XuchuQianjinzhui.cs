using UnityEngine;
using System.Collections;

namespace TangLevel
{
	public class XuchuQianjinzhui : EffectorSpecialBhvr
	{
		public Vector3 offset = Vector3.zero;
		private Transform myTransform;
		private Transform targetTransform;
		private Vector3 backupPos;
		//
		// Use this for initialization
		void Awake ()
		{
			myTransform = transform;
		}
		// Update is called once per frame
		void Update ()
		{

			if (isPlay) {

				if (offset.y >= 0) {
					targetTransform.localPosition = backupPos + offset;
				} else {

					Release ();
				}
			}
		}

		void OnEnable ()
		{
			if (!isPlay) {
				Resume ();
			}
		}

		void mHit ()
		{
			base.Hit ();
		}

		public override void Play ()
		{

			isPlay = true;

			// 找最近的目标
			GameObject target = HeroSelector.FindClosestTarget (w.source.GetComponent<HeroBhvr> ());
			if (target != null) {

			} else {
				Release ();
			}

		}
	}
}